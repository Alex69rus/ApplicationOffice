using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationOffice.Web.UI.Tools
{
    public static class HttpResponseHelper
    {
        public static async Task<ErrorResponse?> Validate(this HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
                return null;

            var str = await responseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ErrorResponse>(str);
        }
    }
}
