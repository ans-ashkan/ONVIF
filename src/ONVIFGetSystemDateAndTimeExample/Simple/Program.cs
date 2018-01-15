using Simple.Onvif;
using System;
using System.Configuration;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Simple
{
    class Program
    {
        static DeviceClient CreateDeviceClient(string uri)
        {
            var deviceEndpointUri = ConfigurationManager.AppSettings["DeviceServiceEndpointUri"];
            if (string.IsNullOrWhiteSpace(deviceEndpointUri))
                throw new ArgumentException("DeviceServiceEndpointUri cannot be null.");

            HttpTransportBindingElement httpBinding = new HttpTransportBindingElement
            {
                AuthenticationScheme = AuthenticationSchemes.Digest
            };

            var messageElement = new TextMessageEncodingBindingElement
            {
                MessageVersion = MessageVersion.Soap12
            };

            return new DeviceClient(new CustomBinding(messageElement, httpBinding), new EndpointAddress(deviceEndpointUri));
        }

        static void Main(string[] args)
        {
            try
            {
                var deviceEndpointUri = ConfigurationManager.AppSettings["DeviceServiceEndpointUri"];
                using (var client = CreateDeviceClient(deviceEndpointUri))
                {
                    var dt = client.GetSystemDateAndTime();
                    var deviceTime = new System.DateTime(dt.UTCDateTime.Date.Year, dt.UTCDateTime.Date.Month, dt.UTCDateTime.Date.Day, dt.UTCDateTime.Time.Hour, dt.UTCDateTime.Time.Minute, dt.UTCDateTime.Time.Second);
                    Console.WriteLine("SystemDateAndTime in UTC: {0}", deviceTime);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
