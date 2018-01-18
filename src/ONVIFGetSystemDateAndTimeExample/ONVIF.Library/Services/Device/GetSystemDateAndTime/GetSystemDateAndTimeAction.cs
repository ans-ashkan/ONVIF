namespace ONVIF.Library.Services.Device.GetSystemDateAndTime
{
    public class GetSystemDateAndTimeAction : ActionBase<GetSystemDateAndTimeRequestBody, GetSystemDateAndTimeResponseBody, SystemDateAndTime>
    {
        protected override SystemDateAndTime GetResponse(Envelope<GetSystemDateAndTimeResponseBody> response)
        {
            return response.Body.GetSystemDateAndTimeResponse.SystemDateAndTime;
        }
    }
}