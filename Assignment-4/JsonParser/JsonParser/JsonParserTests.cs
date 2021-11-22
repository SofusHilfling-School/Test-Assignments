using Xunit;

namespace JsonParser;
public class JsonParserTests
{
    [Fact]
    public void Serialize_NullObject_Throw()
    {
        IJsonParser parser = new JsonParser();
        object? data = null;

        Assert.Throws<ArgumentNullException>(() => parser.Serialize(data));
    }
}
