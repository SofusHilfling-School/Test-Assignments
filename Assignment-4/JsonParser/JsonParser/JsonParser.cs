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
            Array array => HandleArray(array),
            _ => throw new NotImplementedException()
        };
    
    private string HandleArray(Array array)
    {
        if (array.Length == 0)
            return "[]";

        StringBuilder builder = new StringBuilder();
        builder.Append('[');
        for (int i = 0; i < array.Length; i++)
        {
            object? obj = array.GetValue(i);
            builder.Append($"{obj}".ToLower());
           
            builder.Append(',');
        }
        builder.Remove(builder.Length - 1, 1);
        builder.Append(']');
            
        return builder.ToString();
    }
}