namespace NorthSouthSystems.Conversion;

public class EnumFromUnderlyingTypeConverterTests : TypeConverterTests<EnumFromUnderlyingTypeConverter>
{
    // An Enum can be cast from any value that is in range of its UnderlyingType, hence the 8th day of the week test.
    public static IEnumerable<object[]> IsConvertedTrueData =>
        Enumerable.Range(0, 8)
            .SelectMany(i => new[]
            {
                new object[] { (byte)i, (DayOfWeek)i },
                new object[] { (sbyte)i, (DayOfWeek)i },
                new object[] { (ushort)i, (DayOfWeek)i },
                new object[] { (short)i, (DayOfWeek)i },
                new object[] { (uint)i, (DayOfWeek)i },
                new object[] { (int)i, (DayOfWeek)i },
                new object[] { (ulong)i, (DayOfWeek)i },
                new object[] { (long)i, (DayOfWeek)i }
            });

    [Theory]
    [MemberData(nameof(IsConvertedTrueData))]
    public void IsConvertedTrue(object value, object expectedValue)
    {
        Assert(Convert(value, typeof(DayOfWeek)));
        Assert(Convert(value, typeof(DayOfWeek?)));

        void Assert(ConvertTypeRequest request)
        {
            request.IsConverted.Should().BeTrue();
            request.ConvertedValue.Should().Be(expectedValue);
            request.ConvertedValue.GetType().Should().Be(typeof(DayOfWeek));
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("Monday")]
    [InlineData(StringComparison.OrdinalIgnoreCase)]
    public void IsConvertedFalse(object value)
    {
        Assert(Convert(value, typeof(DayOfWeek)));
        Assert(Convert(value, typeof(DayOfWeek?)));

        static void Assert(ConvertTypeRequest request)
        {
            request.IsConverted.Should().BeFalse();
            request.ConvertedValue.Should().BeNull();
        }
    }
}