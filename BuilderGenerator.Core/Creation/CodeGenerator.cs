using System;
using System.Linq;
using System.Text;

namespace BuilderGenerator.Core.Creation
{
    public class CodeGenerator
    {
        private const string _identation = "    ";
        private StringBuilder _stringBuilder;
        private int _indentationCount;

        public CodeGenerator()
        {
            _stringBuilder = new StringBuilder();
            _indentationCount = 1;
        }

        public void OpenBlock()
        {
            AppendLine("{");
            IncreaseIdentation();
        }

        private CodeGenerator IncreaseIdentation()
        {
            _indentationCount++;
            return this;
        }

        public void CloseBlock(string value = null)
        {
            DecreaseIdentation();
            AppendLine("}", value);
        }

        private CodeGenerator DecreaseIdentation()
        {
            if (_indentationCount <= 0)
            {
                throw new NotSupportedException("Identation count cannot be lower than zero!");
            }

            _indentationCount--;
            return this;
        }

        public CodeGenerator Append(params string[] values)
        {
            AppendIdentation();

            if (values == null)
            {
                return this;
            }

            foreach (var value in values)
            {
                _stringBuilder.Append(value);
            }

            return this;
        }

        private void AppendIdentation()
        {
            for (int i = 0; i < _indentationCount; i++)
            {
                _stringBuilder.Append(_identation);
            }
        }

        public CodeGenerator AppendLine(params string[] values)
        {
            if (values == null || !values.Any())
            {
                _stringBuilder.AppendLine();
                return this;
            }

            AppendIdentation();
            for (int i = 0; i < values.Length - 1; i++)
            {
                var value = values[i];
                _stringBuilder.Append(value);
            }

            var lastValue = values[values.Length - 1];
            _stringBuilder.AppendLine(lastValue);
            return this;
        }

        public override string ToString()
        {
            return _stringBuilder.ToString()
                .TrimEnd(Environment.NewLine.ToArray());
        }
    }
}
