using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace SpecificationEngine.Tests
{
    internal class MyOwnMessage : Message
    {
        public MyOwnMessage(string value, MessageType messageType, int messageId) : base(value, messageType)
        {
            MessageId=messageId;
        }

        public int MessageId { get; }
    }


    [TestClass]
    public class OwnMessageTests
    {
        [TestMethod]
        public async Task OwnMessageGetByRuleTest()
        {
            var ruleEvaluator = new RuleEvaluator();
            var rule = new Rule("TestOwnMessage", Specification.Create<int>((value, context) => value == 4), ErrorMessage.Create<int>((value) => new MyOwnMessage("MyOwnTestMessage", MessageType.Error, 1001)));
            ruleEvaluator.AddRule(rule);
            var result = await ruleEvaluator.Evaluate(8);
            Assert.IsTrue(result.Result.First().Value is MyOwnMessage ownmessage && ownmessage.MessageId == 1001);
        }
    }
}
