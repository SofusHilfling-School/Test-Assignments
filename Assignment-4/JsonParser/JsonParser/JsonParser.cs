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
    {
        if(data is null) 
            throw new ArgumentNullException(nameof(data));

        throw new NotImplementedException();
    }
}

