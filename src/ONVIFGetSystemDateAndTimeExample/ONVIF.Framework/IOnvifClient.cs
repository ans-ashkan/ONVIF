using System;

namespace ONVIF.Framework
{
    public interface IOnvifClient
    {
        IDeviceService DeviceService
        {
            get;
        }
    }
}