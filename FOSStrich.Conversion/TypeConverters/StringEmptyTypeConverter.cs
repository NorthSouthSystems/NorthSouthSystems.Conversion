namespace FOSStrich.Conversion;

public class StringEmptyTypeConverter : ITypeConverter
{
    public void Convert(ConvertTypeRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        if ((request.Value as string)?.Length == 0)
        {
            if (request.ConversionType == typeof(string))
                request.Converted(request.Value);
            else if (request.ConversionTypeAllowsNull)
                request.Converted(null);
        }
    }
}