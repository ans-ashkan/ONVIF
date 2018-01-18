using System;
using System.IO;
using System.Xml.Serialization;

namespace ONVIF.Library
{
    [Serializable]
    [XmlRoot(ElementName = "Envelope")]
    public sealed class Envelope<TBody> where TBody : EnvelopeBody
    {
        [XmlElement]
        public TBody Body
        {
            get;
            set;
        }

        public static void Serialize(Envelope<TBody> envelope, Stream stream)
        {
            var serializer = new XmlSerializer(envelope.GetType(), new[] { envelope.Body.GetType() });
            serializer.Serialize(stream, envelope);
        }

        public static Envelope<TBody> Deserialize(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(Envelope<TBody>), new[] { typeof(TBody) });
            return (Envelope<TBody>)serializer.Deserialize(new NamespaceIgnorantXmlTextReader(new StreamReader(stream)));
        }
    }
}