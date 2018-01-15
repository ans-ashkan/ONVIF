using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ONVIF.Library
{
    public class HttpMethod
    {
        public string Method { get; }


        private HttpMethod(string method)
        {
            Method = method;
        }

        public override string ToString()
        {
            return Method;
        }

        public static readonly HttpMethod GET = new HttpMethod("GET");
        public static readonly HttpMethod POST = new HttpMethod("POST");
    }

    public class HttpResponse
    {
        public HttpResponse(MemoryStream ms)
        {
            using (var streamReader = new StreamReader(ms))
            {
                ReadStatusLine(streamReader.ReadLine());

                //ReadHeaders
                Headers = new Dictionary<string, string>();
                string line;
                while ((line = streamReader.ReadLine()) != null && line.Length > 0)
                {
                    var splitPos = line.IndexOf(':');
                    Headers.Add(line.Substring(0, splitPos), line.Substring(splitPos).Trim());
                }

                if (Headers.TryGetValue("Content-Length", out var contentLengthHeader) && int.TryParse(contentLengthHeader, out var contentLength))
                {
                    var contentStream = new MemoryStream();
                    streamReader.BaseStream.CopyTo(contentStream, contentLength);
                }
            }
        }

        private void ReadStatusLine(string line)
        {
            var parts = line.Split(' ');
            HttpVersion = parts[0];
            StatusCode = int.Parse(parts[1]);
            Reason = parts[2];
        }

        public IDictionary<string, string> Headers { get; set; }

        public string HttpVersion { get; set; }

        public int StatusCode { get; set; }

        public string Reason { get; set; }

        public string ContentType { get; set; }

        public long ContentLength { get; set; }

        public Stream Content { get; set; }
    }

    public class HttpRequest
    {
        private string _version = "1.1";

        public HttpMethod Method { get; }
        public Uri Uri { get; }
        public Stream Content { get; }
        public IDictionary<string, string> Headers { get; }

        public HttpRequest(HttpMethod method, string uri)
        {
            Method = method;
            Uri = new Uri(uri);
            Headers = new Dictionary<string, string>();
        }

        public HttpResponse GetResponse()
        {
            using (var client = new TcpClient())
            {
                client.Connect(Uri.Host, Uri.Port);
                using (var stream = client.GetStream())
                {
                    var streamWriter = new StreamWriter(stream);
                    streamWriter.WriteLine($"{Method} {Uri.AbsolutePath} HTTP/{_version}");
                    foreach (var header in Headers)
                    {
                        streamWriter.WriteLine($"{header.Key}: {header.Value}");
                    }
                    streamWriter.WriteLine();
                    streamWriter.Flush();

                    var ms = new MemoryStream();

                    int bytesRead;
                    byte[] buffer = new byte[client.ReceiveBufferSize];
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, bytesRead);
                    }
                    return new HttpResponse(ms);
                }
            }
        }
    }
}
