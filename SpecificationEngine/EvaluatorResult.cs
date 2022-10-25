using System.Collections.Generic;

namespace SpecificationEngine
{
    public class EvaluatorResult : Result
    {
        private readonly Dictionary<string, Result> result = new Dictionary<string, Result>();
        private readonly ExecutionContext context;

        internal EvaluatorResult(ExecutionContext context)
        {
            this.context=context;
        }

        internal void AddMessage(string ruleName, Message message)
        {
            result.Add(ruleName, message);
        }

        internal void AddResult(string ruleName, EvaluatorResult evaluatorResult)
        {
            result.Add(ruleName, evaluatorResult);
        }

        public object this[string key]
        {
            get => context[key];
            set => context[key]=value;
        }

        public IEnumerable<KeyValuePair<string, Result>> Result
        {
            get
            {
                return result;
            }
        }
    }
}
