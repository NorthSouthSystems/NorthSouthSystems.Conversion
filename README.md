# NorthSouthSystems.Conversion

This repository provides the ConvertX class for composing pipelines of ITypeConverters capable of converting .NET objects of one Type to any other Type. The default ConvertX conversion pipeline properly handles empty strings and Nullable of T Types for which System.Convert throws Exceptions.