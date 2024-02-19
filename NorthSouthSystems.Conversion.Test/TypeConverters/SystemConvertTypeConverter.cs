namespace NorthSouthSystems.Conversion;

public class SystemConvertConverterTests : TypeConverterTests<SystemConvertTypeConverter>
{
    [Theory]
    [InlineData("true", typeof(bool), true)]
    [InlineData("1", typeof(int), 1)]
    [InlineData("1", typeof(double), 1.0)]
    [InlineData("Monday", typeof(DayOfWeek), DayOfWeek.Monday)]
    public void IsConvertedTrue(object value, Type conversionType, object expectedConvertedValue)
    {
        var request = Convert(value, conversionType);

        request.IsConverted.Should().BeTrue();
        request.ConvertedValue.Should().Be(expectedConvertedValue);
    }
}