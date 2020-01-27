using System;
using System.Collections.Generic;

namespace BuilderGenerator.Core.Creation
{
    public class BuilderModel
    {
        public BuilderModel(string originalClassName, string builderClassName, IReadOnlyCollection<BuilderProperty> properties)
        {
            OriginalClassName = originalClassName;
            BuilderClassName = builderClassName;
            Properties = properties ?? Array.Empty<BuilderProperty>();
        }

        public string OriginalClassName { get; }
        public string BuilderClassName { get; }
        public IReadOnlyCollection<BuilderProperty> Properties { get; }
    }
}
