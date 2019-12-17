namespace Kangarooper.Conversion
{
    using System;
    using System.Collections.Generic;

    internal static class TypeExtensions
    {
        internal static bool IsEnumUnderlyingType(this Type type) => _enumUnderlyingTypes.Contains(type);

        private static readonly HashSet<Type> _enumUnderlyingTypes = new HashSet<Type>(
            new[] { typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long) });

        internal static bool IsGenericNullable(this Type type) => Nullable.GetUnderlyingType(type) != null;
        internal static Type FlattenGenericNullable(this Type type) => Nullable.GetUnderlyingType(type) ?? type;
    }
}