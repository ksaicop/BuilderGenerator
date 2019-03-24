using BuilderGenerator.Core.Analysis;
using BuilderGenerator.Core.Creation;

namespace BuilderGenerator
{
    public class BuilderGenerator
    {
        public string Generate(string classAsAString)
        {
            var analyzer = new ClassAnalyzer();
            var analysisResult = analyzer.Analyze(classAsAString);

            var creator = new Creator();
            return creator.Create(analysisResult);
        }
    }
}
