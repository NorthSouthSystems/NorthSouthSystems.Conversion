namespace FOSStrich.Conversion;

public class ConvertXTests
{
    private readonly ConvertX _convertX = new();

    [Theory]
    // NoOpTypeConverter
    [InlineData("foobar", typeof(string), "foobar")]
    [InlineData(1, typeof(int), 1)]
    // NullTypeConverter
    [InlineData(null, typeof(string), null)]
    [InlineData(null, typeof(int?), null)]
    // StringEmptyTypeConverter
    [InlineData("", typeof(int?), null)]
    // EnumFromUnderlyingTypeConverter
    [InlineData(1, typeof(DayOfWeek), DayOfWeek.Monday)]
    // SystemConvertTypeConverter
    [InlineData("true", typeof(bool), true)]
    public void IsConvertedTrue(object value, Type conversionType, object expectedConvertedValue)
    {
        _convertX.TryConvertType(value, conversionType, out object convertedValue).Should().BeTrue();
        convertedValue.Should().Be(expectedConvertedValue);

        _convertX.ConvertType(value, conversionType).Should().Be(expectedConvertedValue);
    }

    [Theory]
    [InlineData("foobar", typeof(bool))]
    [InlineData("foobar", typeof(int?))]
    public void IsConvertedFalseSystemConvertTypeConverterFormatException(object value, Type conversionType)
    {
        _convertX.TryConvertType(value, conversionType, out object _).Should().BeFalse();

        Action act = () => _convertX.ConvertType(value, conversionType);
        act.Should().ThrowExactly<Exception>().WithInnerExceptionExactly<FormatException>();
    }

    [Fact]
    public void IsConvertedFalseNotSupported()
    {
        object value = new();
        var conversionType = typeof(ConvertXTests);

        _convertX.TryConvertType(value, conversionType, out object _).Should().BeFalse();

        Action act = () => _convertX.ConvertType(value, conversionType);
        act.Should().ThrowExactly<NotSupportedException>();
    }

    [Fact]
    public void Exceptions()
    {
        Action act;

        act = () => _convertX.ConvertType("", null);
        act.Should().ThrowExactly<ArgumentNullException>();
    }
}