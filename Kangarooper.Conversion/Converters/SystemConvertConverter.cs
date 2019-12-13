namespace Kangarooper.Conversion
{
    using System;

    public class SystemConvertConverter : IConverter
    {
        public (bool IsConverted, object ConvertedValue) ChangeType(object value, Type conversionType, IFormatProvider provider)
        {
            if (conversionType == null) throw new ArgumentNullException(nameof(conversionType));

            // System.Convert.ChangeType requires that value implements IConvertible.
            // https://docs.microsoft.com/en-us/dotnet/api/system.convert.changetype?view=netstandard-2.0
            return ((value == null && !conversionType.IsValueType) || value is IConvertible)
                ? (true, Convert.ChangeType(value, conversionType, provider))
                : (false, null);
        }
    }
}