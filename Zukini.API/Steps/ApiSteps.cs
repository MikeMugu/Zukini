using BoDi;
using RestSharp;
using System;
using System.Collections.Generic;
using Zukini.Steps;

namespace Zukini.API.Steps
{
    public abstract class ApiSteps : BaseSteps
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiSteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public ApiSteps(IObjectContainer objectContainer) :
            base(objectContainer)
        {
        }

        /// <summary>
        /// Helper method for posting to a Rest client and receiving a simple response
        /// in the form of a string dictionary. This allows simple access to members.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API endpoint.</param>
        /// <param name="resource">The resource in the API (e.g. /posts).</param>
        /// <param name="obj">A JSON Serializable object to use as Post data.</param>
        /// <returns></returns>
        protected Dictionary<string, string> Post(Uri baseUrl, string resource, object postData)
        {
            if (postData == null)
            {
                throw new ArgumentNullException(nameof(postData));
            }

            // Setup rest client
            var restClient = new RestClient(baseUrl);

            // Setup rest request
            var request = new RestRequest(resource, Method.POST);
            request.AddJsonBody(postData);

            // Get response
            var response = restClient.Execute<Dictionary<string, string>>(request);

            return response.Data;
        }
    }
}
