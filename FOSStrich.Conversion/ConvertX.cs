namespace FOSStrich.Conversion;

using System.Globalization;

public interface ITypeConverter
{
    void Convert(ConvertTypeRequest request);
}

public class ConvertX
{
    private readonly List<ITypeConverter> _typeConverters = new()
    {
        new NoOpTypeConverter(),
        new NullTypeConverter(),
        new StringEmptyTypeConverter(),
        new EnumFromUnderlyingTypeConverter(),
        new SystemConvertTypeConverter()
    };

    // ConvertType Generic and Object

    public TConversionType ConvertType<TConversionType>(object value,
        IFormatProvider provider = null, bool throwIntermediateExceptions = false) =>
            (TConversionType)ConvertType(value, typeof(TConversionType), provider, throwIntermediateExceptions);

    public object ConvertType(object value, Type conversionType,
        IFormatProvider provider = null, bool throwIntermediateExceptions = false)
    {
        var request = ConvertTypeImpl(value, conversionType, provider, throwIntermediateExceptions, false);

        return request.IsConverted ? request.ConvertedValue : throw request.ExceptionToThrow();
    }

    // TryConvertType Generic

    public bool TryConvertType<TConversionType>(object value,
        out TConversionType convertedValue) =>
            TryConvertType(value, null, false, out convertedValue);

    public bool TryConvertType<TConversionType>(object value,
        IFormatProvider provider, out TConversionType convertedValue) =>
            TryConvertType(value, provider, false, out convertedValue);

    public bool TryConvertType<TConversionType>(object value,
        bool abortIntermediateExceptions, out TConversionType convertedValue) =>
            TryConvertType(value, null, abortIntermediateExceptions, out convertedValue);

    public bool TryConvertType<TConversionType>(object value,
        IFormatProvider provider, bool abortIntermediateExceptions, out TConversionType convertedValue)
    {
        var request = ConvertTypeImpl(value, typeof(TConversionType), provider, false, abortIntermediateExceptions);

        convertedValue = request.IsConverted ? (TConversionType)request.ConvertedValue : default;

        return request.IsConverted;
    }

    // TryConvertType Object

    public bool TryConvertType(object value, Type conversionType,
        out object convertedValue) =>
            TryConvertType(value, conversionType, null, false, out convertedValue);

    public bool TryConvertType(object value, Type conversionType,
        IFormatProvider provider, out object convertedValue) =>
            TryConvertType(value, conversionType, provider, false, out convertedValue);

    public bool TryConvertType(object value, Type conversionType,
        bool abortIntermediateExceptions, out object convertedValue) =>
            TryConvertType(value, conversionType, null, abortIntermediateExceptions, out convertedValue);

    public bool TryConvertType(object value, Type conversionType,
        IFormatProvider provider, bool abortIntermediateExceptions, out object convertedValue)
    {
        var request = ConvertTypeImpl(value, conversionType, provider, false, abortIntermediateExceptions);

        convertedValue = request.ConvertedValue;

        return request.IsConverted;
    }

    // Implementation

    private ConvertTypeRequest ConvertTypeImpl(object value, Type conversionType,
        IFormatProvider provider, bool throwIntermediateExceptions, bool abortIntermediateExceptions)
    {
        var request = new ConvertTypeRequest(value, conversionType, provider ?? CultureInfo.CurrentCulture);

        foreach (var typeConverter in _typeConverters)
        {
            try
            {
                typeConverter.Convert(request);

                if (request.IsConverted)
                    return request;
            }
            catch (Exception exception)
            {
                if (throwIntermediateExceptions)
                    throw;
                else if (abortIntermediateExceptions)
                    return request;

                request.Exception(exception);
            }
        }

        return request;
    }
}