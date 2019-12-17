namespace Kangarooper.Conversion
{
    using System;

    public class SystemConvertTypeConverter : ITypeConverter
    {
        public void Convert(ConvertTypeRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // System.Convert.ChangeType requires that value implements IConvertible.
            // https://docs.microsoft.com/en-us/dotnet/api/system.convert.changetype?view=netstandard-2.0
            //
            // TODO : Check that ConversionType is supported by IConvertible.
            if ((request.Value == null && !request.ConversionType.IsValueType) || request.Value is IConvertible)
                request.Converted(System.Convert.ChangeType(request.Value, request.ConversionType.FlattenGenericNullable(), request.Provider));
        }
    }
}