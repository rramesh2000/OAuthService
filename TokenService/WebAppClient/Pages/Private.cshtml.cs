using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebAppClient.Models;

namespace WebAppClient.Pages
{
    public class PrivateModel : PageModel
    {


        [BindProperty]
        public Authorization authorization { get; set; }
        public AccessTokenRequest accessToken { get; set; }
        public AccessTokenResponse accessTokenResponse { get; set; }
        public PrivateModel()
        {

        }
        public void OnGet(string code, string state, string access_token, string refresh_token)
        {
            authorization = new Authorization
            {
                label = @"Get access token using the authorization code",
                Code = HttpUtility.UrlDecode(code),
                State = state,
                access_token = access_token,
                refresh_token = HttpUtility.UrlDecode(refresh_token)
            };
        }
        public async Task<IActionResult> OnPostCodeAsync()
        {

            string Url = "https://localhost:44306/api/token";
            string Client_Id = "9EF5B182-D476-41CD-8B5B-05BB2E9D3C83";
            string Grant_Type = "authorization_code";
            string Code = HttpUtility.UrlEncode(authorization.Code);
            string responseBody = await GetToken(Url, Client_Id, Grant_Type, Code, null);
            AccessTokenResponse accessTokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(responseBody);
            authorization.access_token = accessTokenResponse.access_token;
            authorization.refresh_token = accessTokenResponse.refresh_token;
            return RedirectToPage("Private", authorization);
        }
        public async Task<IActionResult> OnPostRefreshAsync()
        {
            string Url = "https://localhost:44306/api/token";
            string Client_Id = "9EF5B182-D476-41CD-8B5B-05BB2E9D3C83";
            string Grant_Type = "refresh_token";
            string refresh_token = HttpUtility.UrlEncode(authorization.refresh_token);
            string responseBody = await GetToken(Url, Client_Id, Grant_Type, null, refresh_token);
            AccessTokenResponse accessTokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(responseBody);
            authorization.access_token = accessTokenResponse.access_token;
            authorization.refresh_token = accessTokenResponse.refresh_token;
            return RedirectToPage("Private", authorization);
        }

        private async Task<string> GetToken(string Url, string Client_Id, string Grant_Type, string Code, string refresh_token)
        {
            string responseBody = string.Empty;
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url);
                accessToken = new AccessTokenRequest { Code = Code, Client_Id = Client_Id, Grant_Type = Grant_Type, refresh_token = refresh_token };
                var json = JsonConvert.SerializeObject(accessToken);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Url, request.Content);
                responseBody = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                responseBody = ex.Message + "" + ex.StackTrace;
            }
            return responseBody;
        }
    }
}