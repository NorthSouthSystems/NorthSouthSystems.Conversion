namespace FOSStrich.Conversion;

using System;

public class NoOpTypeConverter : ITypeConverter
{
    public void Convert(ConvertTypeRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        // All Nullable<T> instances box as their UnderlyingType.
        if (request.Value?.GetType() == request.ConversionType.FlattenGenericNullable())
            request.Converted(request.Value);
    }
}