using BoDi;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Zukini.API.Steps
{
    public abstract class ApiSteps<T> : TechTalk.SpecFlow.Steps
    {

        /// <summary>
        /// A context that can contain various data for the step definition class
        /// </summary>
        protected T Context;

        /// <summary>
        /// Represents the Injected ObjectContainer.
        /// </summary>
        private readonly IObjectContainer _objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiSteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public ApiSteps(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        /// <summary>
        /// Request builder - provides convinient way to create requests of different types. 
        /// </summary>
        /// <param name="baseUrl">base url to run against</param>
        /// <returns></returns>
        public virtual RestBuilder Request(Uri baseUrl)
        {
            return new RestBuilder(baseUrl);
        }

        /// <summary>
        /// Performs a GET request to the specified API call and returns the data returned.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource in the API to get.</param>
        /// <returns>Dictionary of key/value pairs containing the result data.</returns>
        protected IRestResponse<Dictionary<string, string>> Get(Uri baseUrl, string resource)
        {
            return Request(baseUrl).Get().ToEndPoint(resource).Exec<Dictionary<string, string>>();
        }

        /// <summary>
        /// Helper method for posting to a Rest client and receiving a full response.
        /// Return data is stored as a string dictionary for easy access.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource in the API (e.g. /posts).</param>
        /// <param name="obj">A JSON Serializable object to use as Post data.</param>
        /// <returns>RestResponse object (from RestSharp)</returns>
        protected IRestResponse<Dictionary<string, string>> Post(Uri baseUrl, string resource, object postData)
        {
            return Request(baseUrl).Post().ToEndPoint(resource).Data(postData).Exec<Dictionary<string, string>>();
        }

        /// <summary>
        /// Helper method for making a put call to a Rest client and receiving a full response.
        /// Return data is stored as a string dictionary for easy access.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource in the API (e.g. /posts).</param>
        /// <param name="putData">A JSON Serializable object to use as Put data.</param>
        /// <returns>RestResponse object (from RestSharp)</returns>
        protected IRestResponse<Dictionary<string, string>> Put(Uri baseUrl, string resource, object putData)
        {
            return Request(baseUrl).Put().ToEndPoint(resource).Data(putData).Exec<Dictionary<string, string>>();
        }

        /// <summary>
        /// Helper method for making a put call to a Rest client and receiving a full response.
        /// Return data is stored as a string dictionary for easy access.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource in the API (e.g. /posts).</param>
        /// <param name="updateData">A JSON Serializable object to use as Put data.</param>
        /// <returns>RestResponse object (from RestSharp)</returns>
        protected IRestResponse<Dictionary<string, string>> Update(Uri baseUrl, string resource, object updateData)
        {
            return Put(baseUrl, resource, updateData);
        }

        /// <summary>
        /// Helper method for making a patch call to a Rest client and receiving a full response.
        /// Return data is stored as a string dictionary for easy access.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource in the API (e.g. /posts).</param>
        /// <param name="patchData">A JSON Serializable object to use as Put data.</param>
        /// <returns>RestResponse object (from RestSharp)</returns>
        protected IRestResponse<Dictionary<string, string>> Patch(Uri baseUrl, string resource, object patchData)
        {
            return Request(baseUrl).Patch().ToEndPoint(resource).Data(patchData).Exec<Dictionary<string, string>>();
        }

        /// <summary>
        /// Performs a delete operation for the specified ressource.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource in the API to delete.</param>
        /// <returns>RestSharp response object resulting from the call.</returns>
        protected IRestResponse<Dictionary<string, string>> Delete(Uri baseUrl, string resource)
        {
            return Request(baseUrl).Delete().ToEndPoint(resource).Exec<Dictionary<string, string>>();
        }

        /// <summary>
        /// Helper method for posting to a Rest client and receiving a simple response
        /// in the form of a string dictionary. This allows simple access to members.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource in the API (e.g. /posts).</param>
        /// <param name="postData">A JSON Serializable object to use as Post data.</param>
        /// <returns>Dictionary of key/value pairs containing the result data.</returns>
        protected Dictionary<string, string> SimplePost(Uri baseUrl, string resource, object postData)
        {
            return Post(baseUrl, resource, postData).Data;
        }

        /// <summary>
        /// Performs a GET request to the specified API call and returns the data returned.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource in the API to get.</param>
        /// <returns>Dictionary of key/value pairs containing the result data.</returns>
        protected Dictionary<string, string> SimpleGet(Uri baseUrl, string resource)
        {
            return Get(baseUrl, resource).Data;
        }

        /// <summary>
        /// Helper method for making a simple patch call to a Rest client and receiving just the data returned.
        /// Return data is stored as a string dictionary for easy access.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource in the API (e.g. /posts).</param>
        /// <param name="patchData">A JSON Serializable object to use as Patch data.</param>
        /// <returns>RestResponse object (from RestSharp)</returns>
        protected Dictionary<string, string> SimplePatch(Uri baseUrl, string resource, object patchData)
        {
            return Patch(baseUrl, resource, patchData).Data;
        }

        /// <summary>
        /// Helper method for making a simple put call to a Rest client and receiving just the data returned.
        /// Return data is stored as a string dictionary for easy access.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource in the API (e.g. /posts).</param>
        /// <param name="putData">A JSON Serializable object to use as Put data.</param>
        /// <returns>RestResponse object (from RestSharp)</returns>
        protected Dictionary<string, string> SimplePut(Uri baseUrl, string resource, object putData)
        {
            return Patch(baseUrl, resource, putData).Data;
        }

        /// <summary>
        /// Performs a delete of the specified resource and returns the only the HttpResponseCode.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource to delete.</param>
        /// <returns>HttpResponseCode as a result of the operation.</returns>
        protected HttpStatusCode SimpleDelete(Uri baseUrl, string resource)
        {
            return Delete(baseUrl, resource).StatusCode;
        }

    }
}