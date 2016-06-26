using System;
using System.IO;
using System.Xml.Serialization;
using GenerateAllTheThings.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenerateAllTheThings.Tests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void CanSerializeSendEmail()
        {
            var ser = new XmlSerializer(typeof(SendEmail));
            var sendEmail = new SendEmail(
                timeStamp: DateTime.Now,
                to: "recepient@example.com",
                cc: "",
                bcc: "",
                body: "Hi there!",
                returnAddress: "non@example.com",
                topic: "emailsender");
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, sendEmail);
                Console.WriteLine(sw.ToString());
                using (var sr = new StringReader(sw.ToString()))
                {
                    var deserializedEmail = (SendEmail) ser.Deserialize(sr);

                    Assert.AreEqual(sendEmail.TimeStamp,deserializedEmail.TimeStamp);
                }
            }



        }
    
        [TestMethod]
        public void CanSerializeOpenPortinFireWall()
        {
            var ser = new XmlSerializer(typeof(OpenPortInFirewall));
            var openPort = new OpenPortInFirewall(
                timeStamp: DateTime.Now,
                returnAddress: "non@example.com",
                topic: "emailsender",
                routerName:"my.router",
                port:1234,
                revertToClosedAfter:null);

            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, openPort);
                Console.WriteLine(sw.ToString());
                using (var sr = new StringReader(sw.ToString()))
                {
                    var deserializedOpenPort = (OpenPortInFirewall) ser.Deserialize(sr);

                    Assert.AreEqual(openPort.TimeStamp,deserializedOpenPort.TimeStamp);
                    Assert.AreEqual(openPort.RevertToClosedAfter,deserializedOpenPort.RevertToClosedAfter);
                }
            }



        }
        [TestMethod]
        public void CanSerializeOpenPortinFireWallWithRevertTime()
        {
            var ser = new XmlSerializer(typeof(OpenPortInFirewall));
            var openPort = new OpenPortInFirewall(
                timeStamp: DateTime.Now,
                returnAddress: "non@example.com",
                topic: "emailsender",
                routerName:"my.router",
                port:1234,
                revertToClosedAfter:TimeSpan.FromDays(1)-DateTime.Now.TimeOfDay);

            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, openPort);
                Console.WriteLine(sw.ToString());
                using (var sr = new StringReader(sw.ToString()))
                {
                    var deserializedOpenPort = (OpenPortInFirewall) ser.Deserialize(sr);

                    Assert.AreEqual(openPort.TimeStamp,deserializedOpenPort.TimeStamp);
                    Assert.AreEqual(openPort.RevertToClosedAfter,deserializedOpenPort.RevertToClosedAfter);
                }
            }



        }
    }
}
