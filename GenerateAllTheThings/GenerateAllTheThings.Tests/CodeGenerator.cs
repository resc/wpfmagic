using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using GenerateAllTheThings.Data;
using GenerateAllTheThings.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenerateAllTheThings.Tests
{
    [TestClass]
    public class CodeGenerator
    {
        [TestMethod]
        public void Generate()
        {
            var types = typeof(Message).Assembly.GetExportedTypes().Where(x => !x.IsAbstract && typeof(Message).IsAssignableFrom(x));

            var directory = Message.SourceDirectory;
            foreach (var t in types)
            {
                var filepath = Path.Combine(directory, $"{t.Name}.Generated.cs");
                GenerateFile(t, filepath);
            }
        }

        private void GenerateFile(Type t, string filepath)
        {
            var properties = t.GetProperties();
            var types = new[]
            {
                typeof(IXmlSerializable),
                typeof(SerializableAttribute),
                typeof(CultureInfo),
            }
                .Concat(properties.Select(x => x.PropertyType))
                .Concat(typeof(IXmlSerializable).GetMethods().Select(x => x.ReturnType))
                .Concat(typeof(IXmlSerializable).GetMethods().SelectMany(x => x.GetParameters()).Select(x => x.ParameterType))
                .Distinct().ToArray();

            var usingNamespaces = types.Select(x => x.Namespace).Distinct().OrderBy(x => x).ToList();

            using (var w = CodeWriter.CreateFile(filepath))
            {
                // warnings for humans
                using (w.BlockComment())
                {
                    w.WriteLine("THIS FILE IS GENERATED, DO NOT MODIFY BY HAND");
                    w.WriteLine("THIS FILE IS GENERATED, DO NOT MODIFY BY HAND");
                    w.WriteLine("THIS FILE IS GENERATED, DO NOT MODIFY BY HAND");
                }

                // usings
                foreach (var ns in usingNamespaces)
                    w.WriteLine($"using {ns};");

                // namespace
                w.WriteLine($"namespace {t.Namespace}");
                using (w.Block())
                {
                    // class declaration
                    w.WriteLine($"[Serializable]\npublic partial class {t.Name} : {nameof(IXmlSerializable)}");
                    using (w.Block())
                    {
                        // default constructor 
                        w.WriteLine($"public {t.Name}()\n{{\n}}");

                        // constructor with parameters for all properties
                        w.Write($"public {t.Name}(");
                        w.Write(string.Join(", ", properties.Select(p => p.PropertyType.SourceCodeName()+ " " + ParamNameFor(p))));
                        w.WriteLine(")");
                        using (w.Block())
                        {
                            foreach (var p in properties)
                                w.WriteLine($"{p.Name} = {ParamNameFor(p)};");
                        }

                        ImplementIXmlSerializable(w, properties);
                    }
                }
            }
        }

        private static void ImplementIXmlSerializable(CodeWriter w, PropertyInfo[] properties)
        {

            ImplementGetSchema(w);

            ImplementReadXml(w, properties);

            ImplementWriteXml(w, properties);
        }

        private static void ImplementWriteXml(CodeWriter w, PropertyInfo[] properties)
        {
            w.WriteComment($"/ <summary> explicit implementation of {nameof(IXmlSerializable)}.{nameof(IXmlSerializable.WriteXml)} </summary>");
            w.WriteLine($"void {nameof(IXmlSerializable)}.{nameof(IXmlSerializable.WriteXml)}({nameof(XmlWriter)} w)");
            using (w.Block())
            {
                foreach (var p in properties)
                {
                    if (p.PropertyType.IsNullable())
                    {
                        w.WriteLine($"w.WriteAttributeString(\"{p.Name}\", {p.Name} == null ? \"null\" : string.Format(CultureInfo.InvariantCulture, \"{{0}}\", {p.Name}));");
                    }
                    else if (p.PropertyType == typeof(string))
                    {
                        w.WriteLine($"w.WriteAttributeString(\"{p.Name}\", {p.Name});");
                    }
                    else
                    {
                        w.WriteLine($"w.WriteAttributeString(\"{p.Name}\", string.Format(CultureInfo.InvariantCulture,\"{{0}}\", {p.Name}));");
                    }
                }
            }
        }

        private static void ImplementReadXml(CodeWriter w, PropertyInfo[] properties)
        {
            w.WriteComment($"/ <summary> explicit implementation of {nameof(IXmlSerializable)}.{nameof(IXmlSerializable.ReadXml)} </summary>");
            w.WriteLine($"void {nameof(IXmlSerializable)}.{nameof(IXmlSerializable.ReadXml)}({nameof(XmlReader)} r)");
            using (w.Block())
            {
                w.WriteLine("while (r.ReadAttributeValue())");
                using (w.Block())
                {
                    w.WriteLine("switch (r.Name)");
                    using (w.Block())
                    {
                        foreach (var p in properties)
                        {
                            w.WriteLine($"case \"{p.Name}\":");
                            using (w.Block())
                            {
                                if (p.PropertyType.IsNullable())
                                {
                                    var typeName = p.PropertyType.SourceCodeName();
                                    if (p.PropertyType.IsNullable())
                                    {
                                        typeName = p.PropertyType.GetGenericArguments()[0].SourceCodeName();
                                    }

                                    w.WriteLine($"if (r.Value == \"null\")");
                                    using (w.Block())
                                        w.WriteLine($"{p.Name} = null;");
                                    w.WriteLine("else");
                                    using (w.Block())
                                        w.WriteLine($"{p.Name} = {typeName}.Parse(r.Value, CultureInfo.InvariantCulture);");
                                }
                                else if (p.PropertyType == typeof(string))
                                {
                                    w.WriteLine($"{p.Name} = r.Value;");
                                }
                                else
                                {
                                    var typeName = p.PropertyType.SourceCodeName();
                                    if (p.PropertyType.IsNullable())
                                    {
                                        typeName = p.PropertyType.GetGenericArguments()[0].SourceCodeName();
                                    }

                                    w.WriteLine($"{p.Name} = {typeName}.Parse(r.Value, CultureInfo.InvariantCulture);");
                                }
                                w.WriteLine("break;");
                            }
                        }
                    }
                }
            }
        }

        private static void ImplementGetSchema(CodeWriter w)
        {
            w.WriteComment($"/ <summary> explicit implementation of {nameof(IXmlSerializable)}.{nameof(IXmlSerializable.GetSchema)} </summary>");
            w.WriteLine($"{nameof(XmlSchema)} {nameof(IXmlSerializable)}.{nameof(IXmlSerializable.GetSchema)}()");
            using (w.Block())
            {
                w.WriteLine("return null;");
            }
        }

        private string ParamNameFor(PropertyInfo p)
        {
            if (p.Name.Length < 2) return p.Name.ToLower();
            return p.Name.Substring(0, 1).ToLower() + p.Name.Substring(1);
        }
    }
}
