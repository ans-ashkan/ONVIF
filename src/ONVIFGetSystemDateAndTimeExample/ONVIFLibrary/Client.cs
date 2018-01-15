using System;
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
    }
}