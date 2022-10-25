using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace SpecificationEngine.Tests
{
    [TestClass]
    public class ContextTests
    {

        [TestMethod]
        public async Task ContextValueTest()
        {
            int a = 100;
            var evaluator = new RuleEvaluator();
            var rule = new Rule("TestContext", Specification.Create<int>((a, context) =>
            {
                context["SuperValue"]= a*0.01;
                return a == 5;
            }));
            evaluator.AddRule(rule);
            var result = await evaluator.Evaluate(a);
            Assert.AreEqual(a*0.01, result["SuperValue"]);
        }
    }
}
