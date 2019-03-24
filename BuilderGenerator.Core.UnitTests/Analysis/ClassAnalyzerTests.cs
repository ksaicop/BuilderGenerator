using BuilderGenerator.Core.Analysis;
using Xunit;

namespace BuilderGenerator.Core.UnitTests.Analysis
{
    public class ClassAnalyzerTests
    {
        public class NoConstructors_OneGetSetProperty
        {
            private string classAsAString = Properties.Resources.ExampleClass;

            private AnalysisResult Execute()
            {
                var analyzer = new ClassAnalyzer();
                return analyzer.Analyze(classAsAString);
            }

            [Fact]
            public void ClassName_Is_Correct()
            {
                var analysisResult = Execute();

                Assert.Equal("ExampleClass", analysisResult.ClassName);
            }

            [Fact]
            public void OneProperty()
            {
                var analysisResult = Execute();

                Assert.Single(analysisResult.Properties);
            }

            [Fact]
            public void Property_Name_Is_Correct()
            {
                var analysisResult = Execute();

                var property = Assert.Single(analysisResult.Properties);
                Assert.Equal("Name", property.Name);
            }

            [Fact]
            public void Property_Type_Is_Correct()
            {
                var analysisResult = Execute();

                var property = Assert.Single(analysisResult.Properties);
                Assert.Equal("string", property.Type);
            }
        }
    }
}
