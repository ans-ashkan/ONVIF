using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using ONVIF.Framework;

namespace ONVIF.Library.Services.Device
{
    internal class DeviceService : IDeviceService
    {
        private readonly Client _client;

        public DeviceService(Client client)
        {
            _client = client;
        }

        private OnvifWebService.DeviceClient GetDeviceClient()
        {
            var httpBinding = new HttpTransportBindingElement
            {
                AuthenticationScheme = AuthenticationSchemes.Digest
            };

            var messageElement = new TextMessageEncodingBindingElement
            {
                MessageVersion = MessageVersion.Soap12
            };

            return new OnvifWebService.DeviceClient(new CustomBinding(messageElement, httpBinding), new EndpointAddress(_client.BaseUri));
        }

        public async Task<DateTime> GetSystemDateAndTimeAsync(CancellationToken cancellationToken = default (CancellationToken))
        {
            using (var client = GetDeviceClient())
            {
                var dt = await client.GetSystemDateAndTimeAsync();
                var deviceTime = new DateTime(dt.UTCDateTime.Date.Year, dt.UTCDateTime.Date.Month, dt.UTCDateTime.Date.Day, dt.UTCDateTime.Time.Hour, dt.UTCDateTime.Time.Minute, dt.UTCDateTime.Time.Second);
                return deviceTime;
            }
        }
    }
}