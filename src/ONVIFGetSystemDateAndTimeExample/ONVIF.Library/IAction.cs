using System.IO;
using System.Net.Http;
using System.Xml.Serialization;

namespace ONVIF.Library
{
    public interface IAction<out TRes>
    {
        void SerializeRequest(Stream stream);

        TRes Deserialize(Stream stream);
    }
}