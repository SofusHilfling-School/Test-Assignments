using System.Collections;
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
                int i => i.ToString(),
                bool b => b.ToString().ToLower(),
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