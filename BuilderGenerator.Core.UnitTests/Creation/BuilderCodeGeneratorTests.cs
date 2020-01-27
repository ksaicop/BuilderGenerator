using BuilderGenerator.Core.Creation;
using Xunit;

namespace BuilderGenerator.Core.UnitTests.Creation
{
    public class BuilderCodeGeneratorTests
    {
        [Fact]
        public void NoConstructors_OneGetSetProperty()
        {
            // Arrange
            var property = new BuilderProperty("string", "Name", "_name", "\"Name\"", "name");
            var properties = new[]
            {
                property
            };
            var builderModel = new BuilderModel("ExampleClass", "ExampleClassBuilder", properties);

            // Act
            var result = Execute(builderModel);

            // Assert
            var expected = @"    public class ExampleClassBuilder
    {
        private string _name;

        public ExampleClassBuilder()
        {
            _name = ""Name"";
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
    }";
            Assert.Equal(expected, result);
        }

        public string Execute(BuilderModel builderModel)
        {
            var creator = new BuilderCodeGenerator();
            return creator.Generate(builderModel);
        }
    }
}
