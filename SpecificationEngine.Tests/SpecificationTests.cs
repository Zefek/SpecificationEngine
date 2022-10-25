using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace SpecificationEngine.Tests
{
    internal class TestModel
    {
        internal string Name { get; set; }
    }

    [TestClass()]
    public class SpecificationTests
    {
        [TestMethod()]
        public async Task IsSpecifiedByTest()
        {
            var model = new TestModel { Name = "Test" };
            var spec = Specification.Create<TestModel>((model, context) => !string.IsNullOrEmpty(model.Name));
            Assert.IsTrue(await spec.IsSpecifiedBy(model, new ExecutionContext()));
        }

        [TestMethod()]
        public async Task IsSpecifiedByTest1()
        {
            (string name, int order) b = ("Test", 0);
            var spec = Specification.Create<(string name, int order)>((a, context) => !string.IsNullOrEmpty(a.name) && a.order == 0);
            Assert.IsTrue(await spec.IsSpecifiedBy(b, new ExecutionContext()));
        }

        [TestMethod()]
        public async Task IsSpecifiedByAsyncTest1()
        {
            (string name, int order) b = ("Test", 0);
            var spec = Specification.Create<(string name, int order)>(async (a, context) => await Task.FromResult(!string.IsNullOrEmpty(a.name) && a.order == 0));
            Assert.IsTrue(await spec.IsSpecifiedBy(b, new ExecutionContext()));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public async Task IsSpecifiedByAsyncTest2()
        {
            var model = new TestModel { Name = "Test" };
            var spec = Specification.Create<(string name, int order)>(async (a, context) => await Task.FromResult(!string.IsNullOrEmpty(a.name) && a.order == 0));
            Assert.IsTrue(await spec.IsSpecifiedBy(model, new ExecutionContext()));
        }
    }
}