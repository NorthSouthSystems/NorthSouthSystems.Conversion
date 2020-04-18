namespace FreeAndWithBeer.Conversion
{
    using System;

    public class EnumFromUnderlyingTypeConverter : ITypeConverter
    {
        public void Convert(ConvertTypeRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Type conversionType = request.ConversionType.FlattenGenericNullable();

            if (request.Value != null && request.Value.GetType().IsEnumUnderlyingType() && conversionType.IsEnum)
                request.Converted(Enum.ToObject(conversionType, request.Value));
        }
    }
}