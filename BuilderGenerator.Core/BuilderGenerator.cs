using BuilderGenerator.Core.Analysis;
using BuilderGenerator.Core.Creation;

namespace BuilderGenerator.Core
{
    public class BuilderGenerator
    {
        public string Generate(string classAsAString)
        {
            var analyzer = new ClassAnalyzer();
            var analysisResult = analyzer.Analyze(classAsAString);

            var modelCreator = new BuilderModelCreator();
            var model = modelCreator.Create(analysisResult);

            var codeGenerator = new BuilderCodeGenerator();
            return codeGenerator.Generate(model);
        }
    }
}
