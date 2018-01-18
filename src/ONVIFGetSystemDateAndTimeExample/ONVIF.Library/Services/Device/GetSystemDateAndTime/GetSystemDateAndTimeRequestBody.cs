namespace ONVIF.Library.Services.Device.GetSystemDateAndTime
{
    public class GetSystemDateAndTimeRequestBody : EnvelopeBody
    {
        public GetSystemDateAndTime GetSystemDateAndTime { get; set; }

        public GetSystemDateAndTimeRequestBody()
        {
            GetSystemDateAndTime = new GetSystemDateAndTime();
        }
    }
}