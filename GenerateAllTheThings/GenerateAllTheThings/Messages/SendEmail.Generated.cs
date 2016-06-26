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
            r.MoveToAttribute("To");
            To = r.Value;
            r.MoveToAttribute("Cc");
            Cc = r.Value;
            r.MoveToAttribute("Bcc");
            Bcc = r.Value;
            r.MoveToAttribute("Body");
            Body = r.Value;
            r.MoveToAttribute("ReturnAddress");
            ReturnAddress = r.Value;
            r.MoveToAttribute("Topic");
            Topic = r.Value;
            r.MoveToAttribute("TimeStamp");
            TimeStamp = DateTime.Parse(r.Value, CultureInfo.InvariantCulture);
            if (r.MoveToContent() == XmlNodeType.Element) r.Read();
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
            w.WriteAttributeString("TimeStamp", TimeStamp.ToString("O"));
        }
        public override string ToString()
        {
            return $"To: {To}, Cc: {Cc}, Bcc: {Bcc}, Body: {Body}, ReturnAddress: {ReturnAddress}, Topic: {Topic}, TimeStamp: {TimeStamp}";
        }
    }
}
