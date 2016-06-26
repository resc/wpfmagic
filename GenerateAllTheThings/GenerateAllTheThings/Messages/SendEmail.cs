namespace GenerateAllTheThings.Data
{
    public partial class SendEmail : Message
    {
        public string To { get;private set; }
        public string Cc { get; private set; }
        public string Bcc { get; private set; }
        public string Body { get; private set; }
    }
}