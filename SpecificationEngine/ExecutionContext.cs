using System.Collections.Generic;

namespace SpecificationEngine
{
    public class ExecutionContext
    {
        public ExecutionContext()
        {

        }

        private Dictionary<string, object> parameters = new Dictionary<string, object>();

        public object this[string key]
        {
            get => parameters[key];
            set
            {
                if (parameters.ContainsKey(key))
                    parameters[key] = value;
                else
                    parameters.Add(key, value);
            }
        }
    }
}
