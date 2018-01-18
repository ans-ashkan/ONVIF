using System.Xml;

namespace ONVIF.Library
{
    public class NamespaceIgnorantXmlTextReader : XmlTextReader
    {
        public NamespaceIgnorantXmlTextReader(System.IO.TextReader reader) : base(reader) { }
        public override string NamespaceURI => "";
    }
}