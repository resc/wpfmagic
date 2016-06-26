/*
    THIS FILE IS GENERATED, DO NOT MODIFY BY HAND
    THIS FILE IS GENERATED, DO NOT MODIFY BY HAND
    THIS FILE IS GENERATED, DO NOT MODIFY BY HAND
*/
using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
namespace GenerateAllTheThings.Data
{
    [Serializable]
    public partial class SourceCodeGenerated : IXmlSerializable
    {
        public SourceCodeGenerated()
        {
        }
        public SourceCodeGenerated(string generatedFile, string returnAddress, string topic, DateTime timeStamp)
        {
            GeneratedFile = generatedFile;
            ReturnAddress = returnAddress;
            Topic = topic;
            TimeStamp = timeStamp;
        }
        /// <summary> explicit implementation of IXmlSerializable.GetSchema </summary>
        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }
        /// <summary> explicit implementation of IXmlSerializable.ReadXml </summary>
        void IXmlSerializable.ReadXml(XmlReader r)
        {
            while (r.ReadAttributeValue())
            {
                switch (r.Name)
                {
                    case "GeneratedFile":
                    {
                        GeneratedFile = r.Value;
                        break;
                    }
                    case "ReturnAddress":
                    {
                        ReturnAddress = r.Value;
                        break;
                    }
                    case "Topic":
                    {
                        Topic = r.Value;
                        break;
                    }
                    case "TimeStamp":
                    {
                        TimeStamp = DateTime.Parse(r.Value, CultureInfo.InvariantCulture);
                        break;
                    }
                }
            }
        }
        /// <summary> explicit implementation of IXmlSerializable.WriteXml </summary>
        void IXmlSerializable.WriteXml(XmlWriter w)
        {
            w.WriteAttributeString("GeneratedFile", GeneratedFile);
            w.WriteAttributeString("ReturnAddress", ReturnAddress);
            w.WriteAttributeString("Topic", Topic);
            w.WriteAttributeString("TimeStamp",string.Format(CultureInfo.InvariantCulture,"{0}", TimeStamp));
        }
    }
}
