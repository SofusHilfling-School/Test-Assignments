using JsonParserLibrary;
using System.Collections;
using Xunit;

namespace JsonParserUnitTests;
public class JsonParserTests
{
    [Theory]
    [InlineData("[[]")]
    [InlineData("[]][]]][[")]
    public void IsJsonValid_UnEqualArrayBeginAndEnd_ReturnFalse(string input)
        => BaseAssertInvalidJson(input);

    [Theory]
    [InlineData("][")]
    [InlineData("][[[][")]
    [InlineData("[][][")]
    public void IsJsonValid_ArrayBeginAndEndInvalidOrder_ReturnFalse(string input)
        => BaseAssertInvalidJson(input);

    [Theory]
    [InlineData("[]")]
    [InlineData("[[[]]]")]
    public void IsJsonValid_ValidArray_ReturnTrue(string input)
        => BaseAssertValidJson(input);

    [Theory]
    [InlineData("\"")]
    [InlineData("\"\"\"")]
    public void IsJsonValid_UnEquaalStringBeginAndEnd_ReturnFalse(string input)
        => BaseAssertInvalidJson(input);

    [Theory]
    [InlineData("\"\\\"\"")]
    [InlineData("\"hello\\\" world!\"")]
    public void IsJsonValid_QuoteWithBackslashInfrontIsIgnored_ValidString(string input)
        => BaseAssertValidJson(input);

    [Theory]
    [InlineData("\"string ]\"")]
    [InlineData("\"string [\"")]
    public void IsJsonValid_IgnoreSymbolsIfInsideString_ValidJson(string input)
        => BaseAssertValidJson(input);

    [Theory]
    [InlineData("lookhere")]
    [InlineData("this is stupid")]
    [InlineData("whatstringwouldthisbe")]
    public void IsJsonValid_CharsOutsideString_InvalidJson(string input)
        => BaseAssertInvalidJson(input);

    [Theory]
    [InlineData("trueblash")]
    [InlineData("falselallaal")]
    [InlineData("nulljustsomerandomtext")]
    [InlineData("tru")]
    [InlineData("fa")]
    [InlineData("nul")]
    public void IsJsonValid_StaticValuesBeforeWithOtherChars_InvalidJson(string input)
        => BaseAssertInvalidJson(input);

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("null")]
    public void IsJsonValid_ParseStaticValues_ValidJson(string input)
        => BaseAssertValidJson(input);

    [Theory]
    [InlineData("[\"test\",\"test\"]")]
    [InlineData("[[],[]]")]
    [InlineData("[\"string\",false,true,null]")]
    public void IsJsonValid_CommaAfterEndOfValue_ValidJson(string input)
        => BaseAssertValidJson(input);

    [Theory]
    [InlineData("\"test\",\"test\"")]
    [InlineData("[],[]")]
    [InlineData("true,false,null")]
    [InlineData("null,")]
    public void IsJsonValid_CommaAfterEndOfValue_InvalidJson(string input)
        => BaseAssertInvalidJson(input);

    [Fact]
    public void IsJsonValid_IgnoreCommaInsideString_ValidJson()
        => BaseAssertValidJson(json: "\"blash, llallal, lalshd\"");


    [Theory]
    [InlineData("390127463782164")]
    [InlineData("12387714")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("5")]
    [InlineData("6")]
    [InlineData("7")]
    [InlineData("8")]
    [InlineData("9")]
    public void IsJsonValid_NatrualPositiveNumbers_ValidJson(string input)
        => BaseAssertValidJson(input);

    [Theory]
    [InlineData("-213894756")]
    [InlineData("-8997234656634")]
    [InlineData("-0")]
    [InlineData("-1")]
    [InlineData("-2")]
    [InlineData("-3")]
    [InlineData("-4")]
    [InlineData("-5")]
    [InlineData("-6")]
    [InlineData("-7")]
    [InlineData("-8")]
    [InlineData("-9")]
    public void IsJsonValid_NatrualNegativeNumbers_ValidJson(string input)
        => BaseAssertValidJson(input);

    [Theory]
    [InlineData("-blash")]
    [InlineData("-true")]
    [InlineData("-")]
    [InlineData("-null")]
    [InlineData("-false")]
    [InlineData("-t352")]
    [InlineData("---")]
    public void IsJsonValid_MinusBeforeNonNumbers_InvalidJson(string input)
        => BaseAssertInvalidJson(input);

    [Theory]
    [InlineData("+213894756")]
    [InlineData("+8997234656634")]
    [InlineData("+0")]
    [InlineData("+1")]
    [InlineData("+2")]
    [InlineData("+3")]
    [InlineData("+4")]
    [InlineData("+5")]
    [InlineData("+6")]
    [InlineData("+7")]
    [InlineData("+8")]
    [InlineData("+9")]
    public void IsJsonValid_PlusBeforeNumbers_ValidJson(string input)
        => BaseAssertValidJson(input);

    [Theory]
    [InlineData("+blash")]
    [InlineData("+true")]
    [InlineData("+")]
    [InlineData("+null")]
    [InlineData("+false")]
    [InlineData("+t352")]
    [InlineData("+++")]
    public void IsJsonValid_PlusBeforeNonNumbers_InvalidJson(string input)
        => BaseAssertInvalidJson(input);

    [Theory]
    [InlineData("Eblash")]
    [InlineData("Etrue")]
    [InlineData("E")]
    [InlineData("Enull")]
    [InlineData("Efalse")]
    [InlineData("Et352")]
    [InlineData("eblash")]
    [InlineData("etrue")]
    [InlineData("e")]
    [InlineData("enull")]
    [InlineData("efalse")]
    [InlineData("et352")]
    [InlineData("eee")]
    [InlineData("EEE")]
    [InlineData("eE")]
    [InlineData("Ee")]
    public void IsJsonValid_ExponentBeforeNonNumbers_InvalidJson(string input)
        => BaseAssertInvalidJson(input);

    [Theory]
    [InlineData("500e3")]
    [InlineData("32457E343")]
    [InlineData("0e+55")]
    [InlineData("500e-3")]
    [InlineData("753E+5")]
    [InlineData("97E-773")]
    [InlineData("-5e5")]
    [InlineData("+75e3")]
    [InlineData("+32E42")]
    [InlineData("-234E88")]
    public void IsJsonValid_NumberWithExponent_ValidJson(string input)
        => BaseAssertValidJson(input);

    [Fact]
    public void IsJsonValid_ExponentBeforeNumbers_ValidJson()
        => BaseAssertInvalidJson("e373");

    [Theory]
    [InlineData(".blash")]
    [InlineData(".true")]
    [InlineData(".")]
    [InlineData(".null")]
    [InlineData(".false")]
    [InlineData(".t352")]
    [InlineData("...")]
    public void IsJsonValid_DecimalBeforeNonNumbers_InvalidJson(string input)
        => BaseAssertInvalidJson(input);

    [Theory]
    [InlineData("500.3")]
    [InlineData("0.55")]
    [InlineData("-753.5")]
    [InlineData("+124.878")]
    public void IsJsonValid_NumberWithDecimal_ValidJson(string input)
        => BaseAssertValidJson(input);

    [Fact]
    public void IsJsonValid_DecimalBeforeNumbers_ValidJson()
        => BaseAssertInvalidJson(".373");

    private void BaseAssertInvalidJson(string json)
    {
        IJsonParser parser = new JsonParser();

        bool isValid = parser.IsJsonValid(json);

        Assert.False(isValid);
    }

    private void BaseAssertValidJson(string json)
    {
        IJsonParser parser = new JsonParser();

        bool isValid = parser.IsJsonValid(json);

        Assert.True(isValid);
    }

    // Tests for Serialize method
    #region Serialize
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

    [Fact]
    public void Serialize_IEnumerableInputType_RetunValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        IEnumerable array = Enumerable.Range(1, 5);
        string expectedResult = "[1,2,3,4,5]";

        string jsonResult = jsonParser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_EmptyObject_ReturnValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        object obj = new object();
        string expectedResult = "{}";

        string jsonResult = jsonParser.Serialize(obj);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_ObjectWithStringProperty_ReturnValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        var obj = new { Text = "Hello World!" };
        string expectedResult = "{\"Text\":\"Hello World!\"}";

        string jsonResult = jsonParser.Serialize(obj);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_ObjectWithIntProperty_ReturnValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        var obj = new { Count = 42 };
        string expectedResult = "{\"Count\":42}";

        string jsonResult = jsonParser.Serialize(obj);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_ObjectWithArray_ReturnValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        var obj = new { Texts = new string[] { "Hello", "World" } };
        string expectedResult = "{\"Texts\":[\"Hello\",\"World\"]}";

        string jsonResult = jsonParser.Serialize(obj);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_ObjectWithDouble_ReturnValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        var obj = new { Price = 3.14159 };
        string expectedResult = "{\"Price\":3.14159}";

        string jsonResult = jsonParser.Serialize(obj);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_ObjectWithBoolean_ReturnValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        var obj = new { Texts = new string[] { "Hello", "World" } };
        string expectedResult = "{\"Texts\":[\"Hello\",\"World\"]}";

        string jsonResult = jsonParser.Serialize(obj);

        Assert.Equal(expectedResult, jsonResult);
    }
    [Fact]
    public void Serialize_ObjectWithComplexType_ReturnValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        var obj = new { Person = new { Name = "John", Age = 69 } };
        string expectedResult = "{\"Person\":{\"Name\":\"John\",\"Age\":69}}";

        string jsonResult = jsonParser.Serialize(obj);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_ObjectWithMultipleProperties_ReturnValidJson()
    {
        IJsonParser jsonParser = new JsonParser();
        var obj = new 
        { 
            Texts = new string[] { "Hello", "World" },
            Author = "John Doe",
            Year = 2007,
            AverageRating = 4.4,
            IsAvailable = true
        };
        string expectedResult = "{\"Texts\":[\"Hello\",\"World\"],\"Author\":\"John Doe\",\"Year\":2007,\"AverageRating\":4.4,\"IsAvailable\":true}";

        string jsonResult = jsonParser.Serialize(obj);

        Assert.Equal(expectedResult, jsonResult);
    }

    [Fact]
    public void Serialize_ArrayWithComplexType_ReturnValidJson()
    {
        IJsonParser parser = new JsonParser();
        string expectedResult = "[{\"Name\":\"John\",\"Age\":24},{\"Name\":\"Jane\",\"Age\":42}]";
        var array = new[] { new { Name = "John", Age = 24 }, new { Name = "Jane", Age = 42 } };
        
        string jsonResult = parser.Serialize(array);

        Assert.Equal(expectedResult, jsonResult);
    }
    #endregion
}
