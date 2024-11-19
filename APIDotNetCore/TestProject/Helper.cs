using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace TestProject
{
    public class Helper
    {
        public static string Token { get; set; } = string.Empty;

        public void GetToken()
        {
            string urlApiGeraToken = "https://localhost:7006/api/CreateTokenIdentity";

            using (var httpClient = new HttpClient())
            {
                var data = new
                {
                    email = "pedro.test@testmail.com",
                    password = "Pedro@1234",
                    document = "123456789"
                };

                string JsonObject = JsonConvert.SerializeObject(data);
                var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

                var result = httpClient.PostAsync(urlApiGeraToken, content);
                result.Wait();
                if (result.Result.IsSuccessStatusCode)
                {
                    var tokenJson = result.Result.Content.ReadAsStringAsync();
                    Token = JsonConvert.DeserializeObject(tokenJson.Result).ToString();
                }
            }
        }

        public async Task<string> execApiGet(
            bool hasAuth, 
            string url,
            string method)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                if (hasAuth)
                {
                    GetToken();

                    if (string.IsNullOrWhiteSpace(Token))
                        return null;

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                var response = await httpClient.GetStringAsync(url + method);
                return response;
            }
        }

        public async Task<HttpStatusCode> execApiPost(
            bool hasAuth, 
            string url,
            string method,
            object data = null)
        {
            string JsonObjeto = data != null ? JsonConvert.SerializeObject(data) : "";
            var content = new StringContent(JsonObjeto, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                if (hasAuth)
                {
                    GetToken();

                    if (string.IsNullOrWhiteSpace(Token))
                        return HttpStatusCode.BadRequest;

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                var response = httpClient.PostAsync(url + method, content);
                response.Wait();

                return response.Result.StatusCode;
            }
        }
    }
}
