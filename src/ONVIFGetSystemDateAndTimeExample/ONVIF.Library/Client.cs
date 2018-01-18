using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ONVIF.Framework;
using ONVIF.Library.Services.Device;

namespace ONVIF.Library
{
    internal class Client : IOnvifClient
    {
        public string BaseUri
        {
            get;
        }

        public Client(string baseUri)
        {
            BaseUri = baseUri;
        }

        public IDeviceService DeviceService => new DeviceService(this);

        public async Task<TRes> ExecuteAction<TRes>(IAction<TRes> action, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var ms = new MemoryStream())
            {
                action.SerializeRequest(ms);
                ms.Position = 0;
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(BaseUri),
                    Content = new SoapHttpContent(ms)
                };

                using (var httpClient = new HttpClient())
                {
                    var resp = await httpClient.SendAsync(request, cancellationToken);
                    resp.EnsureSuccessStatusCode();
                    using (var contentStream = await resp.Content.ReadAsStreamAsync())
                        return action.Deserialize(contentStream);
                }
            }
        }
    }
}