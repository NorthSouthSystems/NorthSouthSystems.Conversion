namespace Kangarooper.Conversion
{
    using System;

    public interface IConverter
    {
        (bool IsConverted, object ConvertedValue) ChangeType(object value, Type conversionType, IFormatProvider provider);
    }
}