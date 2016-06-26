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
    public partial class OpenPortInFirewall : IXmlSerializable
    {
        public OpenPortInFirewall()
        {
        }
        public OpenPortInFirewall(string routerName, int port, TimeSpan? revertToClosedAfter, string returnAddress, string topic, DateTime timeStamp)
        {
            RouterName = routerName;
            Port = port;
            RevertToClosedAfter = revertToClosedAfter;
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
            r.MoveToAttribute("RouterName");
            RouterName = r.Value;
            r.MoveToAttribute("Port");
            Port = int.Parse(r.Value, CultureInfo.InvariantCulture);
            r.MoveToAttribute("RevertToClosedAfter");
            if (r.Value == "null")
            {
                RevertToClosedAfter = null;
            }
            else
            {
                RevertToClosedAfter = TimeSpan.Parse(r.Value, CultureInfo.InvariantCulture);
            }
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
            w.WriteAttributeString("RouterName", RouterName);
            w.WriteAttributeString("Port", string.Format(CultureInfo.InvariantCulture,"{0}", Port));
            w.WriteAttributeString("RevertToClosedAfter", RevertToClosedAfter == null ? "null" : string.Format(CultureInfo.InvariantCulture, "{0}", RevertToClosedAfter));
            w.WriteAttributeString("ReturnAddress", ReturnAddress);
            w.WriteAttributeString("Topic", Topic);
            w.WriteAttributeString("TimeStamp", TimeStamp.ToString("O"));
        }
        public override string ToString()
        {
            return $"RouterName: {RouterName}, Port: {Port}, RevertToClosedAfter: {RevertToClosedAfter}, ReturnAddress: {ReturnAddress}, Topic: {Topic}, TimeStamp: {TimeStamp}";
        }
    }
}
