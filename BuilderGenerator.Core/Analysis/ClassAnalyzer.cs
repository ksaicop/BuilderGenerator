using System;
using System.Collections.Generic;
using System.Linq;

namespace BuilderGenerator.Core.Analysis
{
    public class ClassAnalyzer
    {
        public AnalysisResult Analyze(string classAsAString)
        {
            var lines = classAsAString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string className = GetClassName(lines);
            List<Property> properties = GetProperties(lines);
            return new AnalysisResult(className, properties);
        }

        private static string GetClassName(string[] lines)
        {
            var classLine = lines.ElementAt(2);
            var classLineSplitted = Split(classLine);
            return classLineSplitted[2];
        }

        private static List<Property> GetProperties(string[] lines)
        {
            var properties = new List<Property>();
            for (int i = 4; i < lines.Length - 2; i++)
            {
                var propertyLine = lines[i];
                var propertyLineSplitted = Split(propertyLine);
                var type = propertyLineSplitted[1];
                var name = propertyLineSplitted[2];
                var property = new Property(type, name);
                properties.Add(property);
            }

            return properties;
        }

        private static string[] Split(string line)
        {
            return line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
