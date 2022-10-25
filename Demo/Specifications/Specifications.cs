using Demo.Model;
using SpecificationEngine;

namespace Demo.Specifications
{
    internal class Specifications
    {
        public static Specification HasName => Specification.Create<DemoModel>((model, context) => !string.IsNullOrEmpty(model.Name));

        public static Specification NameIsTest => Specification.Create<DemoModel>((model, context) => model.Name == "Test");

        public static Specification ValueIsGreatThanZero => Specification.Create<DemoModel>((model, context) => model.Value>0);

        public static Specification ValueIsLessThanTen => Specification.Create<DemoModel>((model, context) => model.Value<10);

        public static Specification AgeLessThan30 => Specification.Create<DemoModel>((model, context) => model.Age<30);
        public static Specification AgeGreatThan30 => Specification.Create<DemoModel>((model, context) => model.Age>30);
        public static Specification AgeLessThan60 => Specification.Create<DemoModel>((model, context) => model.Age<60);
        public static Specification AgeGreatThan60 => Specification.Create<DemoModel>((model, context) => model.Age>60);
    }
}
