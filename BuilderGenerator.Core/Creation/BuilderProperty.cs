namespace BuilderGenerator.Core.Creation
{
    public class BuilderProperty
    {
        public BuilderProperty(string type, string propertyName, string fieldName, string fieldValue, string parameterName)
        {
            Type = type;
            PropertyName = propertyName;
            FieldName = fieldName;
            FieldValue = fieldValue;
            ParameterName = parameterName;
        }

        public string Type { get; }
        public string PropertyName { get; }
        public string FieldName { get; }
        public string FieldValue { get; }
        public string ParameterName { get; }
    }
}
