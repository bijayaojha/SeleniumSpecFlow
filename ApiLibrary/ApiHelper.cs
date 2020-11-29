using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApiLibrary
{
    public class ApiHelper
    {
        public RestClient SetUri(string endpoint)
        {
            string baseUri = "https://reqres.in/";
            string uri = Path.Combine(baseUri, endpoint);
            return new RestClient(uri);
        }

        public RestRequest CreateRequest(dynamic payload, string method)
        {
            RestRequest request = new RestRequest();
            var dict = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
            };
            request.AddHeaders(dict);
            switch (method)
            {
                case Methods.GET:
                    request.Method = Method.GET;
                    return request;
                case Methods.POST:
                    request.Method = Method.POST;
                    break;
                case Methods.PUT:
                    request.Method = Method.PUT;
                    break;
                case Methods.DELETE:
                    request.Method = Method.DELETE;
                    break;
            }
          //  request.AddParameter("application/json", payload, ParameterType.RequestBody);
            return request;
        }

        public RestRequest CreateRequest(string method)
        {
            return CreateRequest(null, method);
        }

        public IRestResponse GetResponse(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }

      /*  public T GetContent<T>(dynamic response) where T : ICommonModel
        {
           // return JsonConvert.DeserializeObject<T>(response.Content);
        }*/

        public string Serialize<T>(T request) where T : ICommonModel
        {
            return JsonConvert.SerializeObject(request);
        }
    }

    public static class Methods
    {
        public const string GET = "GET";
        public const string POST = "POST";
        public const string PUT = "PUT";
        public const string DELETE = "DELETE";
    }
}
