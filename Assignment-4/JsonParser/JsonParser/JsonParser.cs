using System.Collections;
using System.Globalization;
using System.Text;

namespace JsonParser;

public interface IJsonParser
{
    string Serialize<T>(T data);
    T Deserialize<T>(string json);
}

public class JsonParser : IJsonParser
{
    public T Deserialize<T>(string json)
    {
        throw new NotImplementedException();
    }

    public string Serialize<T>(T data)
        => data switch
        {
            null => throw new ArgumentNullException(nameof(data)),
            IList array => HandleArray(array),
            _ => throw new NotImplementedException()
        };
    
    private string HandleArray(IList array)
    {
        if (array.Count == 0)
            return "[]";

        StringBuilder builder = new StringBuilder();
        builder.Append('[');
       
        for (int index = 0; index < array.Count; index++)
        {
            string result = array[index] switch
            {
                string s => HandleString(s),
                bool b => b.ToString().ToLower(),
                int i => i.ToString(),
                sbyte sb => sb.ToString(),
                byte b => b.ToString(),
                short s => s.ToString(),
                ushort s => s.ToString(),
                uint s => s.ToString(),
                long l => l.ToString(),
                ulong ul => ul.ToString(),
                float f => f.ToString(CultureInfo.InvariantCulture),
                double d => d.ToString(CultureInfo.InvariantCulture),
                decimal d => d.ToString(CultureInfo.InvariantCulture),
                nint ni => ni.ToString(),
                nuint nui => nui.ToString(),
                null => "null",
                _ => throw new NotImplementedException()
            };
            builder.Append($"{result},");
        }
        builder.Remove(builder.Length - 1, 1);
        builder.Append(']');
            
        return builder.ToString();
    }

    private string HandleString(string str)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append('"');
        foreach (char c in str)
        {
            if (c == '"')
                builder.Append("\\\"");
            else if (c == '\\')
                builder.Append("\\\\");
            else
                builder.Append(c);
        }
        builder.Append('"');
        return builder.ToString();
    }
}