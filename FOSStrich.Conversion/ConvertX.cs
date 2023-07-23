namespace FOSStrich.Conversion;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public interface ITypeConverter
{
    void Convert(ConvertTypeRequest request);
}

public class ConvertX
{
    private readonly List<ITypeConverter> _typeConverters = new List<ITypeConverter>
    {
        new NoOpTypeConverter(),
        new NullTypeConverter(),
        new StringEmptyTypeConverter(),
        new EnumFromUnderlyingTypeConverter(),
        new SystemConvertTypeConverter()
    };

    public TConversionType ConvertType<TConversionType>(object value) =>
        (TConversionType)ConvertType(value, typeof(TConversionType), CultureInfo.CurrentCulture);

    public TConversionType ConvertType<TConversionType>(object value, IFormatProvider provider) =>
        (TConversionType)ConvertType(value, typeof(TConversionType), provider);

    public object ConvertType(object value, Type conversionType) =>
        ConvertType(value, conversionType, CultureInfo.CurrentCulture);

    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allow Converter authors to throw any Exception Type when conversion is not possible.")]
    [SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public object ConvertType(object value, Type conversionType, IFormatProvider provider)
    {
        var request = new ConvertTypeRequest(value, conversionType, provider);

        // Don't create unneccessary garbage; instantiate when the first Exception is caught.
        List<Exception> exceptions = null;

        foreach (var typeConverter in _typeConverters)
        {
            try
            {
                typeConverter.Convert(request);

                if (request.IsConverted)
                    return request.ConvertedValue;
            }
            catch (Exception exception) { (exceptions ?? (exceptions = new List<Exception>())).Add(exception); }
        }

        if (exceptions?.Count > 0)
            throw new AggregateException(exceptions);
        else
            throw new ArgumentException("TODO", nameof(conversionType));
    }
}