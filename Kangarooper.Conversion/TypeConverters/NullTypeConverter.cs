namespace Kangarooper.Conversion
{
    using System;

    public class NullTypeConverter : ITypeConverter
    {
        public void Convert(ConvertTypeRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (request.Value == null && request.ConversionTypeAllowsNull)
                request.Converted(null);
        }
    }
}