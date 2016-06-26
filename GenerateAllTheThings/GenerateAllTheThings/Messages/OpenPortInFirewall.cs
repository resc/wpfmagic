using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GenerateAllTheThings.Utils;

namespace GenerateAllTheThings.Data
{
    public partial class OpenPortInFirewall : Message
    {
        public string RouterName { get; private set; }
        public int Port { get; private set; }
        public TimeSpan? RevertToClosedAfter{ get; private set; }
    }
}
