namespace Kangarooper.Conversion
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    public class ConvertX
    {
        private readonly List<IConverter> _converters = new List<IConverter>
        {
            new SystemConvertConverter()
        };

        public TConversionType ChangeType<TConversionType>(object value) =>
            (TConversionType)ChangeType(value, typeof(TConversionType), CultureInfo.CurrentCulture);

        public TConversionType ChangeType<TConversionType>(object value, IFormatProvider provider) =>
            (TConversionType)ChangeType(value, typeof(TConversionType), provider);

        public object ChangeType(object value, Type conversionType) =>
            ChangeType(value, conversionType, CultureInfo.CurrentCulture);

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allow Converter authors to throw any Exception Type when conversion is not possible.")]
        public object ChangeType(object value, Type conversionType, IFormatProvider provider)
        {
            // Don't create unneccessary garbage; instantiate when the first Exception is caught.
            List<Exception> exceptions = null;

            foreach (var converter in _converters)
            {
                try
                {
                    (var isConverted, var convertedValue) = converter.ChangeType(value, conversionType, provider);

                    if (isConverted)
                        return convertedValue;
                }
                catch (Exception exception) { (exceptions ?? (exceptions = new List<Exception>())).Add(exception); }
            }

            if (exceptions?.Count > 0)
                throw new AggregateException(exceptions);

            return null;
        }
    }
}