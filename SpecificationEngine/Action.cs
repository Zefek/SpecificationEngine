using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpecificationEngine
{
    public class SpecificationAction
    {
        private readonly Delegate action;

        private SpecificationAction(Delegate action)
        {
            this.action=action ?? throw new ArgumentNullException(nameof(action));
        }

        public static SpecificationAction Create<T>(Action<T, ExecutionContext> action) => new SpecificationAction(action);
        public static SpecificationAction Create<T>(Func<T, ExecutionContext, Task> action) => new SpecificationAction(action);
        public static SpecificationAction Create(Action<ExecutionContext> action) => new SpecificationAction(action);
        public static SpecificationAction Create(Func<ExecutionContext, Task> action) => new SpecificationAction(action);

        public async Task Invoke(object input, ExecutionContext context)
        {
            dynamic? result;
            if (action.Method.GetParameters().Count() == 1)
                result = action.DynamicInvoke(context);
            else
                result = action.DynamicInvoke(input, context);
            if (result is Task task)
                await task;
        }
    }
}
