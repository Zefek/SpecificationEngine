using Demo.Model;
using SpecificationEngine;

namespace Demo.Specifications
{
    internal class Rules
    {
        public static Rule HasName => new Rule("HasName", Specifications.HasName, ErrorMessage.Create("Model has not Name", MessageType.Warning));

        public static Rule NameIsTest => new Rule("NameIsTest", Specifications.NameIsTest, ErrorMessage.Create("Name is not Test", MessageType.Error));

        public static Rule ValueBetweenZeroAndTen => new Rule("ValueBetweenZeroAndTen", Specifications.ValueIsGreatThanZero & Specifications.ValueIsLessThanTen, ErrorMessage.Create("Value is out of range (0, 10)", MessageType.Info));

        public static Rule Discount30Percent => new Rule("Discount30Percent", Specifications.AgeLessThan30, successAction: SpecificationAction.Create<DemoModel>((model, context) => model.Discount = 30));

        public static Rule Discount60Percent => new Rule("Discount60Percent", Specifications.AgeGreatThan30 & Specifications.AgeLessThan60, successAction: SpecificationAction.Create<DemoModel>((model, context) => model.Discount = 60));

        public static Rule Discount90Percent => new Rule("Discount90Percent", Specifications.AgeGreatThan60, successAction: SpecificationAction.Create<DemoModel>((model, context) => model.Discount = 90));
    }
}
