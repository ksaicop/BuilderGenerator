using System.Collections.Generic;

namespace BuilderGenerator.Core.Creation
{
    public class BuilderCodeGenerator
    {
        public string Generate(BuilderModel model)
        {
            var cb = new CodeGenerator();
            BuildClassDeclaration(cb, model.BuilderClassName);
            cb.OpenBlock();

            BuildFields(cb, model.Properties);
            cb.AppendLine();
            BuildConstructor(cb, model);
            cb.AppendLine();
            BuildWiths(cb, model);
            cb.AppendLine();
            BuildBuildMethod(cb, model);

            cb.CloseBlock();

            return cb.ToString();
        }

        private static CodeGenerator BuildClassDeclaration(CodeGenerator cb, string builderClassName)
        {
            return cb.AppendLine("public class ", builderClassName);
        }

        private static CodeGenerator BuildFields(CodeGenerator cb, IEnumerable<BuilderProperty> properties)
        {
            foreach (var property in properties)
            {
                cb.AppendLine("private ", property.Type, " ", property.FieldName, ";");
            }

            return cb;
        }

        private static CodeGenerator BuildConstructor(CodeGenerator cb, BuilderModel model)
        {
            cb.AppendLine("public ", model.BuilderClassName, "()");
            cb.OpenBlock();
            foreach (var property in model.Properties)
            {
                BuildFieldAssignment(cb, property);
            }

            cb.CloseBlock();
            return cb;
        }

        private static void BuildFieldAssignment(CodeGenerator cb, BuilderProperty property)
        {
            cb.AppendLine(property.FieldName, " = ", property.FieldValue, ";");
        }

        private static CodeGenerator BuildWiths(CodeGenerator cb, BuilderModel model)
        {
            bool first = true;
            foreach (var property in model.Properties)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    cb.AppendLine();
                }

                BuildWith(cb, property, model.BuilderClassName);
            }

            return cb;
        }

        private static CodeGenerator BuildWith(CodeGenerator cb, BuilderProperty property, string builderClassName)
        {
            cb.AppendLine("public ", builderClassName, " With", property.PropertyName, "(" + property.Type, " ", property.ParameterName, ")");
            cb.OpenBlock();

            cb.AppendLine(property.FieldName, " = ", property.ParameterName, ";");
            cb.AppendLine("return this;");

            cb.CloseBlock();
            return cb;
        }

        private static CodeGenerator BuildBuildMethod(CodeGenerator cb, BuilderModel model)
        {
            cb.AppendLine("public ", model.OriginalClassName, " Build()");
            cb.OpenBlock();
            cb.Append("return new ", model.OriginalClassName, "()");
            if (model.Properties.Count == 0)
            {
                cb.AppendLine(";");
            }
            else
            {
                cb.AppendLine();
                cb.OpenBlock();
                bool first = true;
                foreach (var property in model.Properties)
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

        private static CodeGenerator BuildPropertyInit(CodeGenerator cb, BuilderProperty property)
        {
            cb.AppendLine(property.PropertyName, " = ", property.FieldName);
            return cb;
        }
    }
}
