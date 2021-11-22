using Xunit;

namespace JsonParser;
public class JsonParserTests
{
    [Fact]
    public void Serialize_NullObject_ThrowArgumentNullException()
    {
        IJsonParser parser = new JsonParser();
        object? data = null;

        Assert.Throws<ArgumentNullException>(() => parser.Serialize(data));
    }

    [Fact]
    public void Serialize_EmptyArray_ReutrnValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        int[] array = new int[0];
        string expectedResult = "[]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_ArrayWithInt_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        int[] array = new int[] { 1, 2, 3 };
        string expectedResult = "[1,2,3]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_ArrayWithBool_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        bool[] array = new bool[] { true, false, false };
        string expectedResult = "[true,false,false]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }
}
