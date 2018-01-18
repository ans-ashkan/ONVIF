using System;
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

        public async Task<DateTime> GetSystemDateAndTimeAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var systemDateAndTime =  await _client.ExecuteAction(new GetSystemDateAndTime.GetSystemDateAndTimeAction(), cancellationToken);

            var date = systemDateAndTime.UTCDateTime.Date;
            var time = systemDateAndTime.UTCDateTime.Time;
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, DateTimeKind.Utc);
        }
    }
}