using System;
using System.Threading.Tasks;

namespace SpecificationEngine
{
    public class Specification
    {
        private readonly Delegate functionDelegate;

        private Specification(Delegate functionDelegate)
        {
            this.functionDelegate = functionDelegate ?? throw new ArgumentNullException(nameof(functionDelegate));
        }

        public static Specification Create<T>(Func<T, ExecutionContext, bool> function)
        {
            return new Specification(function);
        }

        public static Specification Create<T>(Func<T, ExecutionContext, Task<bool>> function)
        {
            return new Specification(function);
        }

        public async Task<bool> IsSpecifiedBy(object input, ExecutionContext context)
        {
            var result = functionDelegate.DynamicInvoke(input, context);
            if (result is Task<bool> task)
            {
                return await task;
            }
            if (result is bool functionResult)
            {
                return functionResult;
            }
            throw new NotSupportedException("Result type "+(result==null ? "null" : result.GetType().FullName)+" is not supported on function. Supported result type is Task<bool> or bool.");
        }

        public static Specification operator &(Specification left, Specification right)
        {
            return new Specification(async (object x, ExecutionContext context) => await left.IsSpecifiedBy(x, context) && await right.IsSpecifiedBy(x, context));
        }

        public static Specification operator |(Specification left, Specification right)
        {
            return new Specification(async (object x, ExecutionContext context) => await left.IsSpecifiedBy(x, context) || await right.IsSpecifiedBy(x, context));
        }

        public static Specification operator !(Specification specification)
        {
            return new Specification(async (object x, ExecutionContext context) => !await specification.IsSpecifiedBy(x, context));
        }
    }
}