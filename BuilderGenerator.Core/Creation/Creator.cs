using BuilderGenerator.Core.Analysis;
using System.Collections.Generic;
using System.Linq;

namespace BuilderGenerator.Core.Creation
{
    public class Creator
    {
        public string Create(AnalysisResult analysisResult)
        {
            var cb = new CodeBuilder();
            var builderClassName = CreateBuilderClassName(analysisResult);
            BuildClassDeclaration(cb, builderClassName);
            cb.OpenBlock();

            BuildFields(cb, analysisResult.Properties);
            cb.AppendLine();
            BuildConstructor(cb, analysisResult);
            cb.AppendLine();
            BuildWiths(cb, analysisResult.Properties, builderClassName);
            cb.AppendLine();
            BuildBuildMethod(cb, analysisResult);

            cb.CloseBlock();

            return cb.ToString();
        }

        private string CreateBuilderClassName(AnalysisResult analysisResult)
        {
            return analysisResult.ClassName + "Builder";
        }

        private static CodeBuilder BuildClassDeclaration(CodeBuilder cb, string builderClassName)
        {
            return cb.AppendLine("public class ", builderClassName);
        }

        private static CodeBuilder BuildFields(CodeBuilder cb, IEnumerable<Property> properties)
        {
            foreach (var property in properties)
            {
                cb.AppendLine("private ", property.Type, " _", property.Name.ToLower(), ";");
            }

            return cb;
        }

        private static CodeBuilder BuildConstructor(CodeBuilder cb, AnalysisResult analysisResult)
        {
            cb.AppendLine("public ", analysisResult.ClassName, "Builder()");
            cb.OpenBlock();
            foreach (var property in analysisResult.Properties)
            {
                BuildFieldAssignment(cb, property);
            }

            cb.CloseBlock();
            return cb;
        }

        private static void BuildFieldAssignment(CodeBuilder cb, Property property)
        {
            var fieldName = ToFieldName(property.Name);
            var fieldValue = ToFirstLetterLower(property.Name);
            cb.AppendLine(fieldName, " = \"", fieldValue, "\";");
        }

        private static string ToFieldName(string name)
        {
            const string prefix = "_";
            var withFirstLetterLower = ToFirstLetterLower(name);
            return prefix + withFirstLetterLower;
        }

        private static string ToFirstLetterLower(string name)
        {
            return name[0].ToString().ToLower() + name.Substring(1);
        }

        private static CodeBuilder BuildWiths(CodeBuilder cb, IEnumerable<Property> properties, string builderClassName)
        {
            bool first = true;
            foreach (var property in properties)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    cb.AppendLine();
                }

                BuildWith(cb, property, builderClassName);
            }

            return cb;
        }

        private static CodeBuilder BuildWith(CodeBuilder cb, Property property, string builderClassName)
        {
            var paramName = ToFirstLetterLower(property.Name);
            cb.AppendLine("public ", builderClassName, " With", property.Name, "(" + property.Type, " ", paramName, ")");
            cb.OpenBlock();

            var fieldName = ToFieldName(property.Name);
            cb.AppendLine(fieldName, " = ", paramName, ";");
            cb.AppendLine("return this;");

            cb.CloseBlock();
            return cb;
        }

        private static CodeBuilder BuildBuildMethod(CodeBuilder cb, AnalysisResult analysisResult)
        {
            cb.AppendLine("public ", analysisResult.ClassName, " Build()");
            cb.OpenBlock();
            cb.Append("return new ", analysisResult.ClassName, "()");
            if (analysisResult.Properties.Count == 0)
            {
                cb.AppendLine(";");
            }
            else
            {
                cb.AppendLine();
                cb.OpenBlock();
                bool first = true;
                foreach (var property in analysisResult.Properties)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        cb.AppendLine(",");
                    }

                    BuildPropertyInit(cb, property);
                }

                cb.CloseBlock(";");
            }

            cb.CloseBlock();
            return cb;
        }

        private static CodeBuilder BuildPropertyInit(CodeBuilder cb, Property property)
        {
            var fieldName = ToFieldName(property.Name);
            cb.AppendLine(property.Name, " = ", fieldName);
            return cb;
        }
    }
}
