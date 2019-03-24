using BuilderGenerator.Core.Analysis;
using BuilderGenerator.Core.Creation;
using Xunit;

namespace BuilderGenerator.Core.UnitTests.Creation
{
    public class CreatorTests
    {
        [Fact]
        public void NoConstructors_OneGetSetProperty()
        {
            Property property = new Property("string", "Name");
            var properties = new[]
            {
                property
            };
            var analysisResult = new AnalysisResult("ExampleClass", properties);

            var result = Execute(analysisResult);

            var expected = @"    public class ExampleClassBuilder
    {
        private string _name;

        public ExampleClassBuilder()
        {
            _name = ""name"";
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

        public string Execute(AnalysisResult analysisResult)
        {
            var creator = new Creator();
            return creator.Create(analysisResult);
        }
    }
}
