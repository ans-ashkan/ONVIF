using System.IO;

namespace ONVIF.Library.Services.Device.GetSystemDateAndTime
{
    public class GetSystemDateAndTimeAction : IAction<SystemDateAndTime>
    {
        public SystemDateAndTime Deserialize(Stream stream)
        {
            return Envelope<GetSystemDateAndTimeResponseBody>.Deserialize(stream).Body.GetSystemDateAndTimeResponse.SystemDateAndTime;
        }

        public void SerializeRequest(Stream stream)
        {
            var envelope = new Envelope<GetSystemDateAndTimeRequestBody>
            {
                Body = new GetSystemDateAndTimeRequestBody()
            };

            Envelope<GetSystemDateAndTimeRequestBody>.Serialize(envelope, stream);
        }
    }
}