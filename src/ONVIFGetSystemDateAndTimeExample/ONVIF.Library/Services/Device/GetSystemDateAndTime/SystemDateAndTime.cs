
namespace ONVIF.Library.Services.Device.GetSystemDateAndTime
{
    public class SystemDateAndTime
    {
        public string DateTimeType { get; set; }

        public bool DaylightSavings { get; set; }

        public TimeZone TimeZone { get; set; }

        public DateTime UTCDateTime { get; set; }
    }
}