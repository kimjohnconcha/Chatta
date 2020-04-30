using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace APIService
{
    public class Api
    {
        private readonly ApiConfig _apiConfig;

        private readonly string _token;
        private WebApiService _webApiService;

        public Api(ApiConfig apiConfig)
        {
            // this.Token = token;
            _apiConfig = apiConfig;
            _token = GetAuthenticationToken(apiConfig.Email, apiConfig.Password);
            _webApiService = new WebApiService(_token);
        }

        public string GetAuthenticationToken(string username, string password)
        {
            var token = $"{username}:{password}";
            token = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            return token;
        }

        public async Task<APIQueryResult<TRecord>> LoginQuery<TRecord>(string whereClause = null,
            Func<JToken, TRecord> customDeserialization = null, string strToken = null) where TRecord : IAPIRecord
        {
            var url = GetUrl2(typeof(TRecord), "login");
            if (!string.IsNullOrEmpty(whereClause)) url += $"?sTextSearch={whereClause}";

            _webApiService = new WebApiService(strToken);

            if (customDeserialization == null)
            {
                var res = await _webApiService.GetWebDataAsync<APIQueryResult<TRecord>>(url);
                return res;
            }

            var jObject = await _webApiService.GetWebDataAsync<JObject>(url);
            var total = jObject["Total"].Value<int>();
            var data = jObject["Data"] as JArray;
            var dataItems = data?.Select(customDeserialization).ToList() ?? new List<TRecord>();
            return new APIQueryResult<TRecord> { Total = total, Data = dataItems };
        }



        public string GetUrl2(Type entityType, string strType = null)
        {
            string url;
            if (string.IsNullOrEmpty(strType))
            {
                if (!(entityType.GetTypeInfo().GetCustomAttribute(typeof(APITableAttribute)) is APITableAttribute
                    tableAttr))
                    throw new InvalidOperationException("Unknown table.");
                url = "";
                    //$"{Configuration.UrlAPI}/api/accounts/{_tdbConfig.AccountId}/tables/{_tdbConfig.TableId}/records";
            }
            else
            {
                if (strType == "login")
                {
                    url = $"{Configuration.UrlAPI}/api/logininfo";
                }
                else
                {
                    if (!(entityType.GetTypeInfo().GetCustomAttribute(typeof(APITableAttribute)) is APITableAttribute
                        tableAttr))
                        throw new InvalidOperationException("Unknown table.");
                    url = "";
                        //$"{Configuration.UrlAPI}/api/accounts/{_tdbConfig.AccountId}/tables/{_tdbConfig.TableId}/records";
                }
            }

            return url;
        }
    }
}
