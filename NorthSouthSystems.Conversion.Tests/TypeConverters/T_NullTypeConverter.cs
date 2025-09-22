﻿namespace NorthSouthSystems.Conversion;

public class NullTypeConverterTests : TypeConverterTests<NullTypeConverter>
{
    [Theory]
    [InlineData(typeof(string))]
    [InlineData(typeof(int?))]
    [InlineData(typeof(ITypeConverter))]
    public void IsConvertedTrue(Type conversionType)
    {
        var request = Convert(null, conversionType);

        request.IsConverted.Should().BeTrue();
        request.ConvertedValue.Should().BeNull();
    }

    [Theory]
    [InlineData(typeof(int))]
    [InlineData(typeof(DayOfWeek))]
    public void IsConvertedFalse(Type conversionType)
    {
        var request = Convert(null, conversionType);

        request.IsConverted.Should().BeFalse();
        request.ConvertedValue.Should().BeNull();
    }
}