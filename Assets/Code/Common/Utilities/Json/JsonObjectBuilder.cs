using System;
using System.Collections;
using System.Text;

namespace ChessMath.Shared.Common.Json
{
    public class JsonObjectBuilder:IEnumerable
    {
        private readonly StringBuilder builder = new StringBuilder();

        private int childrenCount;
        private string resultingString;

        public JsonObjectBuilder() =>
            WriteObjectStart();

        private void WriteObjectStart() =>
            builder.Append('{');

        private void WriteObjectEnd() =>
            builder.Append('}');

        public void Add(string key, int value)
        {
            ThrowIfBuilderClosed();
            AppendPropertyName(key);
            builder.Append(value);
        }

        public void Add(string key, long value)
        {
            ThrowIfBuilderClosed();
            AppendPropertyName(key);
            builder.Append(value);
        }

        public void Add(string key, string value)
        {
            ThrowIfBuilderClosed();
            AppendPropertyName(key);
            AppendString(value);
        }

        private void AppendPropertyName(string key)
        {
            if (childrenCount > 0)
                WriteChildSeparator();

            childrenCount++;

            AppendString(key);
            builder.Append(": ");
        }

        private void WriteChildSeparator() =>
            builder.Append(", ");

        IEnumerator IEnumerable.GetEnumerator() =>
            throw new NotImplementedException();

        private void AppendString(string text)
        {
            builder.Append("\"");
            builder.Append(text);
            builder.Append("\"");
        }

        public override string ToString()
        {
            if (resultingString != null)
                return resultingString;

            WriteObjectEnd();
            resultingString = builder.ToString();

            return resultingString;
        }

        private void ThrowIfBuilderClosed()
        {
            if (!IsOpen)
                throw new InvalidOperationException("Builder cannot be used anymore because calling ToString() closes it for future writing");
        }

        private bool IsOpen =>
            resultingString == null;
    }
}

