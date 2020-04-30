using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APIService
{
    public class WebApiService
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();
        private readonly string _token;

        public WebApiService(string token)
        {
            _token = token;
        }

        private HttpClient GetHttpClient()
        {
            ////MAKING SURE DEVICE INFO IS SENT
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _token);
            return httpClient;
        }

        public async Task<T> PostWebDataAsync<T>(string url, object obj)
        {
            T result;

            using (var client = GetHttpClient())
            {
                var requestObject = JsonConvert.SerializeObject(obj);
                var dataContent = new StringContent(requestObject, Encoding.UTF8, "application/json");

                System.Diagnostics.Debug.WriteLine("POST: " + url + " -- " + requestObject);

                var response = await client.PostAsync(url, dataContent, CancellationToken.None);
#if DEBUG
                var resultTest = await response.Content.ReadAsStringAsync();
#endif
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    result = _serializer.Deserialize<T>(json);
                }
            }

            return result;
        }

        public async Task<T> GetWebDataAsync<T>(string url)
        {
            T result;

            using (var client = GetHttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                System.Diagnostics.Debug.WriteLine("GET: " + url);


                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    result = _serializer.Deserialize<T>(json);
                }
            }

            return result;
        }

        public async Task<HttpResponseMessage> DeleteWebDataAsync(string url)
        {
            using (var client = GetHttpClient())
            {
                var response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                return response;
            }
        }

        public async Task<HttpResponseMessage> PutWebDataAsync(string url, object obj)
        {
            using (var client = GetHttpClient())
            {
                var requestObject = JsonConvert.SerializeObject(obj);
                var dataContent = new StringContent(requestObject, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, dataContent);
                System.Diagnostics.Debug.WriteLine("PUT: " + url + " -- " + requestObject);
                response.EnsureSuccessStatusCode();
                return response;
            }
        }

        public async Task<T> PostWebBinaryDataAsync<T>(string url, byte[] payload)
        {
            T result;

            using (var client = GetHttpClient())
            {
                var content = new ByteArrayContent(payload);
                var response = await client.PostAsync(url, content, CancellationToken.None);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    result = _serializer.Deserialize<T>(json);
                }
            }

            return result;
        }

        public async Task<T> PostWebBinaryDataAsync<T>(string url, Stream payload, string fileName)
        {
            T result;

            using (var client = GetHttpClient())
            {
                var requestContent = new MultipartFormDataContent { { new StreamContent(payload), "bilddatei", fileName } };
                var response = await client.PostAsync(url, requestContent, CancellationToken.None);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    result = _serializer.Deserialize<T>(json);
                }
            }

            return result;
        }
    }
}
