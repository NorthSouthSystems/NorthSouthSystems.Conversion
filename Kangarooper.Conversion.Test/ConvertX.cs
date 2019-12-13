namespace Kangarooper.Conversion
{
    using Xunit;

    public class ConvertXTests
    {
        private readonly ConvertX _convertX = new ConvertX();

        [Fact]
        public void Simple()
        {
            Assert.True(_convertX.ChangeType<bool>("true"));
        }
    }
}