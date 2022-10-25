using Demo.Model;
using Demo.Specifications;
using SpecificationEngine;
using System;
using System.Threading.Tasks;

namespace Demo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var model = new DemoModel
            {
                Name = "Abcd",
                Value = 5,
                Age = 29
            };

            var evaluator = new RuleEvaluator();
            evaluator.AddRule(Rules.HasName);
            evaluator.AddRule(Rules.NameIsTest);
            evaluator.AddRule(Rules.ValueBetweenZeroAndTen);
            evaluator.AddRule(Rules.Discount30Percent);
            evaluator.AddRule(Rules.Discount60Percent);
            evaluator.AddRule(Rules.Discount90Percent);

            var result = await evaluator.Evaluate(model);

            foreach (var keyValue in result.Result)
            {
                Console.WriteLine("{0} : {1}", keyValue.Key, keyValue.Value);
            }
            Console.WriteLine(model.Discount);
        }
    }
}
