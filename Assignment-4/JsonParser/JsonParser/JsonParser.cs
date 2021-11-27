using System.Collections;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Reflection;

namespace JsonParser;

public interface IJsonParser
{
    bool IsJsonValid(string json);
    string Serialize<T>(T data);
    T Deserialize<T>(string json);
}

public class JsonParser : IJsonParser
{
    public bool IsJsonValid(string json)
    {
        int arrayOffset = 0;
        foreach(char c in json)
        {
            if (c == '[')
                arrayOffset++;
            else if (c == ']')
            {
                if (arrayOffset <= 0)
                    return false;
                else
                    arrayOffset--;
            }   
        }

        if (arrayOffset != 0)
            return false;

        return true;
    }

    public T Deserialize<T>(string json)
    {
        throw new NotImplementedException();
    }

    public string Serialize<T>(T data)
        => data switch
        {
            null => "null",
            string s => HandleString(s),
            IEnumerable array => HandleArray(array),
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
            object obj => HandleComplexType(obj)
        };

    private string HandleComplexType<T>(T obj) where T : notnull
    {
        PropertyInfo[] props = obj.GetType().GetProperties();

        if (!props.Any())
            return "{}";

        StringBuilder builder = new StringBuilder();
        builder.Append('{');

        foreach (PropertyInfo prop in props)
        {
            string value = Serialize(prop.GetValue(obj));
            builder.Append($"\"{prop.Name}\":{value},");
        }

        builder.Remove(builder.Length - 1, 1);
        builder.Append('}');
        return builder.ToString();
    }

    private string HandleArray(IEnumerable list)
    {
        if (!list.GetEnumerator().MoveNext())
            return "[]";

        StringBuilder builder = new StringBuilder();
        builder.Append('[');
       
        foreach(var item in list)
        {
            string result = Serialize(item);
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