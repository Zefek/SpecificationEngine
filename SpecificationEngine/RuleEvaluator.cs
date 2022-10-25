using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpecificationEngine
{
    public class RuleEvaluator
    {
        private List<Rule> rules = new List<Rule>();

        private readonly ExecutionContext context;

        public RuleEvaluator()
        {
            context = new ExecutionContext();
        }

        public RuleEvaluator(ExecutionContext context)
        {
            this.context = context;
        }

        public void AddRule(Rule rule) => rules.Add(rule);

        public async Task<EvaluatorResult> Evaluate(object input)
        {
            var evaluatorResult = new EvaluatorResult(context);
            foreach (var rule in rules)
            {
                var result = await rule.Evaluate(input, context);
                if (result && rule.SuccessAction!=null)
                {
                    await rule.SuccessAction.Invoke(input, context);
                }
                if (!result && rule.ErrorMessage!=null)
                {
                    evaluatorResult.AddMessage(rule.Name, await rule.ErrorMessage.GetMessage(input));
                }
                if (!result && rule.FailureAction!=null)
                {
                    await rule.FailureAction.Invoke(input, context);
                }
            }
            return evaluatorResult;
        }
    }
}
