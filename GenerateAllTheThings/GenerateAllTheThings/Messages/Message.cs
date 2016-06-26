using System;

namespace GenerateAllTheThings.Data
{
    public abstract class Message
    {
        internal static string SourceDirectory
        {
            get { return Utils.Source.Directory(); }
        }

        public string ReturnAddress { get; protected set; }
        public string Topic { get; protected set; }
        public DateTime TimeStamp { get; protected set; }
    }
}