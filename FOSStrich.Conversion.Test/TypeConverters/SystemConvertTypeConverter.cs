namespace FOSStrich.Conversion;

using FluentAssertions;
using System;
using Xunit;

public class SystemConvertConverterTests : TypeConverterTests<SystemConvertTypeConverter>
{
    [Theory]
    [InlineData("true", typeof(bool), true)]
    public void IsConvertedTrue(object value, Type conversionType, object expectedConvertedValue)
    {
        var request = Convert(value, conversionType);

        request.IsConverted.Should().BeTrue();
        request.ConvertedValue.Should().BeEquivalentTo(expectedConvertedValue);
    }
}