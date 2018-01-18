using System.IO;

namespace ONVIF.Library.Services.Device.GetSystemDateAndTime
{
    public abstract class ActionBase<TRequestBody, TResponseBody, TResult> : IAction<TResult>
        where TRequestBody : EnvelopeBody, new()
        where TResponseBody : EnvelopeBody
    {
        public void SerializeRequest(Stream stream)
        {
            var envelope = new Envelope<TRequestBody>
            {
                Body = new TRequestBody()
            };

            Envelope<TRequestBody>.Serialize(envelope, stream);
        }

        public TResult Deserialize(Stream stream)
        {
            return GetResponse(Envelope<TResponseBody>.Deserialize(stream));
        }

        protected abstract TResult GetResponse(Envelope<TResponseBody> response);
    }
}