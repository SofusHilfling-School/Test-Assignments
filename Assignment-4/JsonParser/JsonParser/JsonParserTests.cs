using System.Collections;
using Xunit;

namespace JsonParser;
public class JsonParserTests
{
    [Fact]
    public void Serialize_NullObject_ReturnValidJson()
    {
        IJsonParser parser = new JsonParser();
        object? data = null;
        string expectedResult = "null";

        string jsonResult = parser.Serialize(data);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_Int_ReturnValidJson()
    {
        IJsonParser parser = new JsonParser();
        int data = 1;
        string expectedResult = "1";

        string jsonResult = parser.Serialize(data);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_String_ReturnValidJson()
    {
        IJsonParser parser = new JsonParser();
        string data = "This is a random text!";
        string expectedResult = "\"This is a random text!\"";

        string jsonResult = parser.Serialize(data);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_Double_ReturnValidJson()
    {
        IJsonParser parser = new JsonParser();
        double data = 1.57912e58;
        string expectedResult = "1.57912E+58";

        string jsonResult = parser.Serialize(data);

        Assert.Equal(expectedResult, jsonResult);
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
    public void Serialize_ArrayWithDouble_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        double[] array = new double[] { 1.44, 2.2, 3.362123, 1.63201978555e30 };
        string expectedResult = "[1.44,2.2,3.362123,1.63201978555E+30]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_ArrayWithDecimal_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        decimal[] array = new decimal[] { 1.12983479012364m, 2.312498732190847123m, 3.219834712309560m };
        string expectedResult = "[1.12983479012364,2.312498732190847123,3.219834712309560]";

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

    [Fact]
    public void Serialize_ArrayWithNull_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        string?[] array = new string?[] { null, "hello World!" };
        string expectedResult = "[null,\"hello World!\"]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }
}
