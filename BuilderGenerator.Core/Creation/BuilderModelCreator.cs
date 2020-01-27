using BuilderGenerator.Core.Analysis;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BuilderGenerator.Core.Creation
{
    public class BuilderModelCreator
    {
        public BuilderModel Create(AnalysisResult analysisResult)
        {
            if (analysisResult == null)
            {
                throw new ArgumentNullException(nameof(analysisResult));
            }

            var builderClassName = CreateBuilderClassName(analysisResult);
            var properties = CreateBuilderProperties(analysisResult);
            return new BuilderModel(analysisResult.ClassName, builderClassName, properties);
        }

        private static string CreateBuilderClassName(AnalysisResult analysisResult)
        {
            return analysisResult.ClassName + "Builder";
        }

        private static IReadOnlyCollection<BuilderProperty> CreateBuilderProperties(AnalysisResult analysisResult)
        {
            var builderProperties = new List<BuilderProperty>(analysisResult.Properties.Count);
            foreach (var property in analysisResult.Properties)
            {
                var fieldName = ToFieldName(property);
                var fieldValue = ToFieldValue(property);
                var parameterName = ToParameterName(property);
                var builderProperty = new BuilderProperty(property.Type, property.Name, fieldName, fieldValue, parameterName);
                builderProperties.Add(builderProperty);
            }

            return builderProperties;
        }

        private static string ToFieldName(ClassMember property)
        {
            const string prefix = "_";
            var withFirstLetterLower = ToFirstLetterLower(property.Name);
            return prefix + withFirstLetterLower;
        }

        private static string ToFieldValue(ClassMember property)
        {
            return $"\"{property.Name}\"";
        }

        private static string ToParameterName(ClassMember property)
        {
            return ToFirstLetterLower(property.Name);
        }

        private static string ToFirstLetterLower(string name)
        {
            return name[0].ToString(CultureInfo.InvariantCulture).ToLowerInvariant() + name.Substring(1);
        }
    }
}
