using System;
using System.Linq;

namespace GenerateAllTheThings.Tests
{
    static class TypeExtensions
    {
        public static bool IsNullable(this Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static string SourceCodeName(this Type type)
        {
            if (type.IsArray)
            {
                return SourceCodeName(type.GetElementType()) + "[]";
            }

            if (type == typeof(string)) return "string";
            if (type == typeof(bool)) return "bool";
            if (type == typeof(int)) return "int";
            if (type == typeof(uint)) return "uint";
            if (type == typeof(short)) return "short";
            if (type == typeof(ushort)) return "ushort";
            if (type == typeof(long)) return "long";
            if (type == typeof(ulong)) return "ulong";
            if (type == typeof(double)) return "double";
            if (type == typeof(float)) return "float";
            if (type == typeof(decimal)) return "decimal";
            if (type == typeof(byte)) return "byte";
            if (type == typeof(sbyte)) return "sbyte";
            if (type.IsNullable())
            {
                return SourceCodeName(type.GetGenericArguments()[0]) + "?";
            }
            if (type.IsGenericType)
            {
                var name = type.Name.Substring(0, type.Name.IndexOf("`", StringComparison.Ordinal));
                return $"{name}<{string.Join(", ", type.GetGenericArguments().Select(SourceCodeName))}>";
            }
            return type.Name;
        }
    }
}