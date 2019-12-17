namespace Kangarooper.Conversion
{
    using FluentAssertions;
    using System;
    using Xunit;

    public class ConvertXTests
    {
        private readonly ConvertX _convertX = new ConvertX();

        [Theory]
        [InlineData("true", typeof(bool), true)]
        public void IsConvertedTrue(object value, Type conversionType, object expectedConvertedValue) =>
            _convertX.ConvertType(value, conversionType).Should().BeEquivalentTo(expectedConvertedValue);
    }
}