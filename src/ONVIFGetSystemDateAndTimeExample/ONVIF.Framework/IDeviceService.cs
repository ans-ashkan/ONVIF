using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONVIF.Framework
{
    public interface IDeviceService
    {
        Task<DateTime> GetSystemDateAndTimeAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}