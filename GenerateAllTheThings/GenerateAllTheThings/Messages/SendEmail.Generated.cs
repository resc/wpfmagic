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
    public partial class SendEmail : IXmlSerializable
    {
        public SendEmail()
        {
        }
        public SendEmail(string to, string cc, string bcc, string body, string returnAddress, string topic, DateTime timeStamp)
        {
            To = to;
            Cc = cc;
            Bcc = bcc;
            Body = body;
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
                    case "To":
                    {
                        To = r.Value;
                        break;
                    }
                    case "Cc":
                    {
                        Cc = r.Value;
                        break;
                    }
                    case "Bcc":
                    {
                        Bcc = r.Value;
                        break;
                    }
                    case "Body":
                    {
                        Body = r.Value;
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
            w.WriteAttributeString("To", To);
            w.WriteAttributeString("Cc", Cc);
            w.WriteAttributeString("Bcc", Bcc);
            w.WriteAttributeString("Body", Body);
            w.WriteAttributeString("ReturnAddress", ReturnAddress);
            w.WriteAttributeString("Topic", Topic);
            w.WriteAttributeString("TimeStamp",string.Format(CultureInfo.InvariantCulture,"{0}", TimeStamp));
        }
    }
}
