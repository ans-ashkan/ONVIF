using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ONVIF.Library
{
    public class SoapHttpContent : HttpContent
    {
        private readonly Stream _stream;

        public SoapHttpContent(Stream stream)
        {
            Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/soap+xml");
            Headers.ContentType.CharSet = "utf-8";
            _stream = stream;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            _stream.CopyTo(stream);
            return Task.CompletedTask;
        }

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return false;
        }
    }
}
