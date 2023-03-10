using System;
using System.Text;
using System.Text.Json;

namespace Sales.WEB.repositories
{
	public class Repository : IRepository
	{
        private readonly HttpClient _httpClient;

        private JsonSerializerOptions _jsonSerializerOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

		public Repository(HttpClient httpClient)
		{
            _httpClient = httpClient;
		}


        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var responseHttp = await _httpClient.GetAsync(url);

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<T>(responseHttp,_jsonSerializerOptions);
                return new HttpResponseWrapper<T>(response,false,responseHttp);
            }

            return new HttpResponseWrapper<T>(default,true,responseHttp);
        }


        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T model)
        {
            var messageJSON = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJSON, Encoding.UTF8 , "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messageContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }


        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T model)
        {
            var messageJSON = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messageContent);

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<TResponse>(responseHttp, _jsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }

            return new HttpResponseWrapper<TResponse>(default,responseHttp.IsSuccessStatusCode, responseHttp);

        }

        private async Task<T> UnserializeAnswer<T>(HttpResponseMessage httpResponseMessage ,JsonSerializerOptions jsonSerializerOptions)
        {
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString,jsonSerializerOptions)!;
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHTTP = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null,!responseHTTP.IsSuccessStatusCode,responseHTTP);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T model)
        {
            var messageJSON = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
            var responseHTTP = await _httpClient.PutAsync(url, messageContent);
            return new HttpResponseWrapper<object>(null,!responseHTTP.IsSuccessStatusCode,responseHTTP);
        }

        public async Task<HttpResponseWrapper<TResponse>> Put<T, TResponse>(string url, T model)
        {
            var messageJSON = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PutAsync(url, messageContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<TResponse>(responseHttp, _jsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }

            return new HttpResponseWrapper<TResponse>(default, responseHttp.IsSuccessStatusCode, responseHttp);

        }
    }
}

