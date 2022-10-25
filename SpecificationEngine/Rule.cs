using System.Threading.Tasks;

namespace SpecificationEngine
{
    public class Rule
    {
        private readonly Specification specification;

        public Rule(string name, Specification specification)
        {
            Name=name;
            this.specification=specification;
        }

        public Rule(string name, Specification specification, ErrorMessage? errorMessage = null, SpecificationAction? successAction = null, SpecificationAction? failureAction = null)
        {
            Name=name;
            this.specification=specification;
            ErrorMessage=errorMessage;
            SuccessAction=successAction;
            FailureAction=failureAction;
        }

        public string Name { get; }
        internal ErrorMessage? ErrorMessage { get; }
        internal SpecificationAction? SuccessAction { get; }
        internal SpecificationAction? FailureAction { get; }

        public async Task<bool> Evaluate(object input, ExecutionContext context)
        {
            return await specification.IsSpecifiedBy(input, context);
        }
    }
}
