namespace BuilderGenerator.Core.Analysis
{
    public class Property
    {
        public Property(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get; }
        public string Name { get; }
    }
}
