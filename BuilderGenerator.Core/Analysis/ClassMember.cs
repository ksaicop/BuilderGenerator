namespace BuilderGenerator.Core.Analysis
{
    public class ClassMember
    {
        public ClassMember(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get; }
        public string Name { get; }
    }
}
