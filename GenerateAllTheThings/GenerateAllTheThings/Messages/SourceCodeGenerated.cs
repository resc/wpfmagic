using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GenerateAllTheThings.Utils;

namespace GenerateAllTheThings.Data
{
    public partial class SourceCodeGenerated : Message
    {

        public string GeneratedFile { get; private set; }

    }
}
