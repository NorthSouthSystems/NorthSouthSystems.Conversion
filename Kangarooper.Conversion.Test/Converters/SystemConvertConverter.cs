namespace Kangarooper.Conversion
{
    using System.Globalization;
    using Xunit;

    public class SystemConvertConverterTests
    {
        private readonly SystemConvertConverter _converter = new SystemConvertConverter();

        [Fact]
        public void Simple()
        {
            Assert.Equal(true, _converter.ChangeType("true", typeof(bool), CultureInfo.CurrentCulture).ConvertedValue);
        }
    }
}