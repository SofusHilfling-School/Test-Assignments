using System.Collections;
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

    [Fact]
    public void Serialize_StringArrayWithLowercase_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        string[] array = new string[] { "hello", "world" };
        string expectedResult = "[\"hello\",\"world\"]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_StringArrayWithUppercase_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        string[] array = new string[] { "HELLO", "WORLD" };
        string expectedResult = "[\"HELLO\",\"WORLD\"]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_StringArrayWithMixedCase_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        string[] array = new string[] { "Hello", "World" };
        string expectedResult = "[\"Hello\",\"World\"]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_StringArrayWithUnicode_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        string[] array = new string[] { "Hello \uB208", "World ❤" };
        string expectedResult = "[\"Hello \uB208\",\"World ❤\"]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_StringArrayWithEscapeChars_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        string[] array = new string[] { "\"", "\\" };
        string expectedResult = "[\"\\\"\",\"\\\\\"]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_ArrayWithMixedTypes_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        ArrayList array = new ArrayList { true, "hello World!", 5000 };
        string expectedResult = "[true,\"hello World!\",5000]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }
}
