using System.Text;

namespace BuilderGenerator.Core.Creation
{
    public static class StringBuilderExtensions
    {
        private const string tabValue = "    ";

        public static StringBuilder Append(this StringBuilder sb, int indentationCount, string value)
        {
            sb.AppendIntendation(indentationCount);
            sb.Append(value);
            return sb;
        }

        public static StringBuilder AppendLine(this StringBuilder sb, int indentationCount, string value)
        {
            sb.AppendIntendation(indentationCount);
            sb.AppendLine(value);
            return sb;
        }

        private static StringBuilder AppendIntendation(this StringBuilder sb, int indentationCount)
        {
            for (int i = 0; i < indentationCount; i++)
            {
                sb.Append(tabValue);
            }

            return sb;
        }
    }
}
