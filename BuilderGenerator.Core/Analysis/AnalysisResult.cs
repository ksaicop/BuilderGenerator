using System.Collections.Generic;

namespace BuilderGenerator.Core.Analysis
{
    public class AnalysisResult
    {
        public AnalysisResult(string className, IEnumerable<Property> properties)
        {
            ClassName = className;
            if (properties != null)
            {
                Properties = new List<Property>(properties);
            }
            else
            {
                Properties = new List<Property>();
            }
        }

        public string ClassName { get; }
        public List<Property> Properties { get; }
    }
}
