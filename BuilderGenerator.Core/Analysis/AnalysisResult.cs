using System;
using System.Collections.Generic;

namespace BuilderGenerator.Core.Analysis
{
    public class AnalysisResult
    {
        public AnalysisResult(string className, IReadOnlyCollection<ClassMember> properties)
        {
            ClassName = className;
            Properties = properties ?? Array.Empty<ClassMember>();
        }

        public string ClassName { get; }
        public IReadOnlyCollection<ClassMember> Properties { get; }
    }
}
