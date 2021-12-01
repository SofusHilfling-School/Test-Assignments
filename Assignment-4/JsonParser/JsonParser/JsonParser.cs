using System.Collections;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Reflection;

namespace JsonParserLibrary;

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
        int stringCounter = 0;
        bool isInsideString() => stringCounter % 2 == 1;
        bool isEndOfValue = false;
        for (int i = 0; i < json.Length; i++)
        {
            char currentChar = json[i];
            if (isEndOfValue && i != json.Length - 1)
            {
                if(currentChar == ',' && arrayOffset > 0)
                    continue;
            }
            if (currentChar == '"')
            {
                if (i == 0 || json[i - 1] != '\\')
                    stringCounter++;

                isEndOfValue = !isInsideString();
            }
            else if (isInsideString())
                continue;
            else if (currentChar == '[')
                arrayOffset++;
            else if (currentChar == ']')
            {
                if (arrayOffset <= 0)
                    return false;
                
                arrayOffset--;
                isEndOfValue = true;
            }
            else if (currentChar == 'n' || currentChar == 't' || currentChar == 'f')
            {
                int remainingLength = (json.Length) - i;
                Range range = new Range(i, i + 4);

                if (remainingLength < 4)
                    return false;
                else if (remainingLength >= 5 && json[new Range(i, i + 5)].SequenceEqual("false"))
                    i += 4;
                else if (json[range].SequenceEqual("true") || json[range].SequenceEqual("null"))
                    i += 3;
                else
                    return false;

                isEndOfValue = true;
            }
            else if (IsNumber(currentChar) || IsBasicMathChar(currentChar) || IsAdvancedMathChar(currentChar))
            {
                bool wasNumberBefore = i > 0 && (IsNumber(json[i - 1]));
                if (IsAdvancedMathChar(currentChar) && !wasNumberBefore)
                    return false;
                if (IsAdvancedMathChar(currentChar))
                    i++;
                if (IsBasicMathChar(json[i]))
                    i++;

                bool numberAfterMathSign = false; 
                while (i < json.Length && IsNumber(json[i]))
                {
                    i++;
                    numberAfterMathSign = true;
                }
                i--;

                if (!numberAfterMathSign)
                    return false;

                isEndOfValue = true;
            }
            else
                return false;
        }

        if (arrayOffset != 0)
            return false;
        else if (stringCounter % 2 == 1)
            return false;

        return true;
    }

    private bool IsNumber(char currentChar)
    {
        switch(currentChar)
        {
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                return true;
            default:
                return false;
        }
    }

    private bool IsBasicMathChar(char currentChar)
        => currentChar == '-' || currentChar == '+';
    private bool IsAdvancedMathChar(char currentChar)
    {
        switch (currentChar) 
        {
            case 'e':
            case 'E':
            case '.':
                return true;
            default:
                return false;
        }

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