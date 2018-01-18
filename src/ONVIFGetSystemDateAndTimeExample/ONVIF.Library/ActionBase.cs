using System.IO;

namespace ONVIF.Library
{
    public abstract class ActionBase<TRequestBody, TResponseBody, TResult> : IAction<TResult>
        where TRequestBody : EnvelopeBody, new()
        where TResponseBody : EnvelopeBody
    {

        /// <summary>
        /// Serialize request to soap envelope
        /// </summary>
        /// <param name="stream"></param>
        public void SerializeRequest(Stream stream)
        {
            var envelope = new Envelope<TRequestBody>
            {
                Body = CreateRequestBody()
            };

            Envelope<TRequestBody>.Serialize(envelope, stream);
        }


        /// <summary>
        /// Deserialize soap content into actual result
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public TResult Deserialize(Stream stream)
        {
            return GetResponse(Envelope<TResponseBody>.Deserialize(stream));
        }


        /// <summary>
        /// Enable subclasses to provide parametrized bodies
        /// </summary>
        /// <returns></returns>
        protected virtual TRequestBody CreateRequestBody()
        {
            return new TRequestBody();
        }

        /// <summary>
        /// Extract actual result from envelope body
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected abstract TResult GetResponse(Envelope<TResponseBody> response);
    }
}