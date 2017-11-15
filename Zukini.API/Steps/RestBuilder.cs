using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace Zukini.API.Steps
{
    /// <summary>
    /// Convenient Rest helper. Helps to build rest requests without pain. 
    /// <author>Oleh Ilnytskyi</author> 
    /// </summary>
    public class RestBuilder
    {
        private Func<IRestRequest, IRestRequest> _confFn;
        private readonly Uri _baseDomain;

        public RestBuilder(Uri url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            _baseDomain = url;
            _confFn = (nothing) => (nothing);
        }

        /// <summary>
        /// Specifies method to be used.
        /// </summary>
        /// <param name="method">method to be executed</param>
        /// <returns>RestBuilder</returns>
        public virtual RestBuilder Method(Method method)
        {
            _confFn = FunUtils.Compose(_confFn, (request) =>
            {
                request.Method = method;
                return request;
            });
            return this;
        }

        public virtual RestBuilder Get() => Method(RestSharp.Method.GET);
        public virtual RestBuilder Post() => Method(RestSharp.Method.POST);
        public virtual RestBuilder Patch() => Method(RestSharp.Method.PATCH);
        public virtual RestBuilder Put() => Method(RestSharp.Method.PUT);
        public virtual RestBuilder Delete() => Method(RestSharp.Method.DELETE);

        /// <summary>
        /// Here you can add json data.
        /// </summary>
        /// <param name="data">data to post</param>
        /// <returns>RestBuilder</returns>
        public virtual RestBuilder Data(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            _confFn = FunUtils.Compose(_confFn, (request) =>
            {
                request.AddJsonBody(data);
                return request;
            });
            return this;
        }
        
        /// <summary>
        /// Here you can specify request headers.
        /// </summary>
        /// <param name="headers">key-value pairs of headers</param>
        /// <returns></returns>
        public virtual RestBuilder AddHeaders(IDictionary<string, string> headers)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            _confFn = FunUtils.Compose(_confFn, (request) =>
            {
                foreach (KeyValuePair<string, string> pair in headers)
                {
                    request.AddHeader(pair.Key, pair.Value);
                }
                return request;
            });
            return this;
        }

        /// <summary>
        /// Here you can add specific header.
        /// </summary>
        /// <param name="key">header name</param>
        /// <param name="value">header value</param>
        /// <returns>RestBuilder</returns>
        public virtual RestBuilder AddHeader(string key, string value)
        {
            _confFn = FunUtils.Compose(_confFn, (request) =>
            {
                request.AddHeader(key, value);
                return request;
            });
            return this;
        }

        /// <summary>
        /// Specifies end point or url suffix. Like: http://baseurl/endpoint?param=value
        /// </summary>
        /// <param name="resource">endpoint or resource to be executed against</param>
        /// <returns>RestBuilder</returns>
        public virtual RestBuilder ToEndPoint(string resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            _confFn = FunUtils.Compose(_confFn, (request) =>
            {
                request.Resource = resource;
                return request;
            });
            return this;
        }

        /// <summary>
        /// Specifies file to be uploaded as a result request.
        /// That may be a good idea to use Stream for this scenario, but it seems the is some internal issue 
        /// RestSharp atm. See discussion here: https://github.com/restsharp/RestSharp/issues/742
        /// This approach with abs file path seems to be working.
        /// </summary>
        /// <param name="absFilePath">Specifies file absolute path</param>
        /// <param name="fileString"></param>
        /// <param name="fileNameString"></param>
        /// <param name="isMultiPart"></param>
        /// <returns>RestBuilder</returns>
        public virtual RestBuilder AddFile(string absFilePath,
                                           string fileString = "file",
                                           string fileNameString = "filename",
                                           bool isMultiPart = true)
        {
            if (absFilePath == null)
            {
                throw new ArgumentNullException(nameof(absFilePath));
            }

            _confFn = FunUtils.Compose(_confFn, (request) =>
            {
                string filename = Path.GetFileName(absFilePath);
                request.AlwaysMultipartFormData = isMultiPart;
                request.AddParameter(fileNameString, filename, ParameterType.GetOrPost);
                request.AddFile(fileString, File.ReadAllBytes(absFilePath), filename);
                return request;
            });
            return this;
        }

        /// <summary>
        /// Adds query url params. Like: http://baseurl/endpoint?param=value
        /// </summary>
        /// <param name="param">param name</param>
        /// <param name="value">param value</param>
        /// <returns></returns>
        public virtual RestBuilder AddUrlQueryParam(string param, string value)
        {
            _confFn = FunUtils.Compose(_confFn, (request) =>
            {
                request.AddQueryParameter(param, value);
                return request;
            });
            return this;
        }

        /// <summary>
        /// Performs configuration for the default RestRequest object.
        /// </summary>
        /// <returns>IRestRequest</returns>
        public virtual IRestRequest Build()
        {

            return _confFn(new RestRequest());
        }

        /// <summary>
        /// Performs configuration for the custom RestRequest object.
        /// </summary>
        /// <returns>IRestRequest</returns>
        public virtual IRestRequest Build(IRestRequest request)
        {

            return _confFn(request);
        }

        /// <summary>
        /// Creates configuration and actualy sends a request with given configuration.
        /// </summary>
        /// <typeparam name="T">Response object type</typeparam>
        /// <returns>IRestResponse</returns>
        public virtual IRestResponse<T> Exec<T>() where T : new()
        {
            var constructedRequest = Build();
            return new RestClient(_baseDomain).Execute<T>(constructedRequest);
        }

        /// <summary>
        /// Provides convenient way to quickly get the T response if response.data is expected be serializable.
        /// </summary>
        /// <typeparam name="T">Reponse type</typeparam>
        /// <param name="expHttpStatusCode">OK by default</param>
        /// <returns>T</returns>
        public virtual T ExecCheck<T>(HttpStatusCode expHttpStatusCode = HttpStatusCode.OK) where T : new()
        {
            var response = Exec<T>();
            CheckHttpStatusCode(response, expHttpStatusCode);
            if (response.Data == null)
            {
                throw new AssertionException(
                $"Response from {response.ResponseUri} unexpected. " +
                $"Cannot serialise response, data is null. Response expHttpStatusCode: {response.StatusCode}. Message: {response.ErrorMessage}");
            }
            return response.Data;
        }

        /// <summary>
        /// Provides convenient way to quickly get the T response if response.content is expected be serializable.
        /// Sometimes response.data is null but response.content is JSON serializable
        /// </summary>
        /// <typeparam name="T">Reponse type</typeparam>
        /// <param name="expHttpStatusCode">OK by default</param>
        /// <returns>T</returns>
        public T ExecContentCheck<T>(HttpStatusCode expHttpStatusCode = HttpStatusCode.OK) where T : new()
        {
            var response = Exec<T>();
            CheckHttpStatusCode(response, expHttpStatusCode);
            if (string.IsNullOrEmpty(response.Content))
            {
                throw new AssertionException(
                $"Response from {response.ResponseUri} unexpected. " +
                $"Cannot serialise response, content is null. Response expHttpStatusCode: {response.StatusCode}. Message: {response.ErrorMessage}");
            }
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        /// <summary>
        /// Useful when no data to be serialised in response.
        /// </summary>
        /// <param name="expHttpStatusCode"></param>
        public virtual IRestResponse ExecCheck(HttpStatusCode expHttpStatusCode = HttpStatusCode.OK)
        {
            var response = Exec();
            CheckHttpStatusCode(response, expHttpStatusCode);
            return response;
        }

        /// <summary>
        /// Execute request without serialized type.
        /// </summary>
        /// <returns>IRestResponse</returns>
        public virtual IRestResponse Exec() => new RestClient(_baseDomain).Execute(Build());

        /// <summary>
        /// Creates configuration and sends a request with custom rest client.
        /// </summary>
        /// <typeparam name="T">Response object type</typeparam>
        /// <param name="client">custom client</param>
        /// <returns>IRestResponse</returns>
        public virtual IRestResponse<T> Exec<T>(IRestClient client) where T : new()
        {
            return client.Execute<T>(Build());
        }

        /// <summary>
        /// Executes request as async task.
        /// </summary>
        /// <returns>Task</returns>
        public virtual Task<IRestResponse> ExecAsync()
        {
            return new RestClient(_baseDomain).ExecuteTaskAsync(Build());
        }

        /// <summary>
        /// Performs request with custom client and custom predifined request.
        /// </summary>
        /// <typeparam name="T">Response object type</typeparam>
        /// <param name="client">custom client</param>
        /// <param name="request">predifined request</param>
        /// <returns>IRestResponse</returns>
        public virtual IRestResponse<T> Exec<T>(IRestClient client, IRestRequest request) where T : new()
        {
            return client.Execute<T>(Build(request));
        }
        
        /// <summary>
        /// Checks the HTTP status code and throws an AsssertionException if it's not expected
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="expHttpStatusCode">The exp HTTP status code.</param>
        /// <exception cref="AssertionException"></exception>
        private void CheckHttpStatusCode(IRestResponse response, HttpStatusCode expHttpStatusCode)
        {
            if (response.StatusCode != expHttpStatusCode)
            {
                throw new AssertionException(
                $"Response from {response.ResponseUri} unexpected.\n" +
                $"Response Code: {response.StatusCode}. Expected: {expHttpStatusCode}.\n" +
                $"Message: {(string.IsNullOrEmpty(response.ErrorMessage) ? response.Content : response.ErrorMessage)}");
            }
        }
    }
}
