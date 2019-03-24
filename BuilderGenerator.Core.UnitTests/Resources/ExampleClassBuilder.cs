namespace BuilderGenerator.Core.UnitTests.Resources
{
    public class ExampleClassBuilder
    {
        private string _name;

        public ExampleClassBuilder()
        {
            _name = "name";
        }

        public ExampleClassBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ExampleClass Build()
        {
            return new ExampleClass()
            {
                Name = _name
            };
        }
    }
}