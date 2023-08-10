namespace FOSStrich.Conversion;

public class ConvertXTests
{
    private readonly ConvertX _convertX = new();

    [Theory]
    [InlineData("true", typeof(bool), true)]
    public void IsConvertedTrue(object value, Type conversionType, object expectedConvertedValue) =>
        _convertX.ConvertType(value, conversionType).Should().Be(expectedConvertedValue);
}