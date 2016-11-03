using BoDi;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using TechTalk.SpecFlow;
using Zukini.API.Steps;

namespace Zukini.API.Examples.Features.Steps
{
    [Binding]
    public class JsonPlaceholderApiSteps : ApiSteps
    {
        public JsonPlaceholderApiSteps(IObjectContainer objectContainer)
            : base(objectContainer)
        {
        }

        public Uri BaseApiUrl
        {
            get { return new Uri(TestSettings.JsonPlaceholderApiUrl); }
        }

        #region Post/Patch/Put steps

        [Given(@"I post the following data to the API")]
        public void GivenIPostTheFollowingDataToTheAPI(Table table)
        {
            var result = SimplePost(BaseApiUrl, "/posts", table.Rows[0]);
            PropertyBucket.Remember<Dictionary<string, string>>("PostData", result);
        }

        [Given(@"I ""(Patch|Put)"" a record with id ""(\d+)""")]
        public void GivenIPatchOrPutARecordWithId(string method, int id, Table table)
        {
            Dictionary<string, string> result = null;

            switch (method)
            {
                case "Patch":
                    result = SimplePatch(BaseApiUrl, $"posts/{id}", table.Rows[0]);
                    break;
                case "Put":
                    result = SimplePut(BaseApiUrl, $"posts/{id}", table.Rows[0]);
                    break;
            }

            PropertyBucket.Remember<Dictionary<string, string>>($"{method}Data", result);
        }

        [Then(@"the post data should return ""(.*)"" in the ""(.*)"" field")]
        public void ThenThePostDataShouldReturnInTheField(string expectedValue, string fieldName)
        {
            var postData = PropertyBucket.GetProperty<Dictionary<string, string>>("PostData");
            Assert.AreEqual(expectedValue, postData[fieldName]);
        }


        [Then(@"the ""(Patch|Put)"" data should return ""(.*)"" in the ""(.*)"" field")]
        public void ThenThePutOrPostDataShouldReturnInTheField(string method, string expectedValue, string fieldName)
        {
            var resultData = PropertyBucket.GetProperty<Dictionary<string, string>>($"{method}Data");
            Assert.AreEqual(expectedValue, resultData[fieldName]);
        }

        #endregion

        #region Get Steps

        [Given(@"I perform a GET for post ""(.*)""")]
        public void GivenIPerformAGETForPost(int postId)
        {
            var response = SimpleGet(BaseApiUrl, $"/posts/{postId}");
            PropertyBucket.Remember("PostData", response);
        }

        [Then(@"the Get response should contain the following data")]
        public void ThenTheGetResponseShouldContainTheFollowingData(Table table)
        {
            var postData = PropertyBucket.GetProperty<Dictionary<string, string>>("PostData");
            var row = table.Rows[0];

            foreach (var kvp in row)
            {
                Assert.AreEqual(kvp.Value, postData[kvp.Key]);
            }
        }

        #endregion

        #region Delete Steps

        [Given(@"I perform a DELETE for postId ""(.*)""")]
        public void GivenIPerformADELETEForPostId(int postId)
        {
            var response = Delete(BaseApiUrl, $"posts/{postId}");
            PropertyBucket.Remember<IRestResponse>("DeleteResponse", response);
        }

        [Then(@"I should get a status code of ""(.*)""")]
        public void ThenIShouldGetAStatusCodeOf(string statusCode)
        {
            var deleteResponse = PropertyBucket.GetProperty<IRestResponse>("DeleteResponse");
            HttpStatusCode code;

            if (Enum.TryParse<HttpStatusCode>(statusCode, true, out code))
            {
                Assert.AreEqual(code, deleteResponse.StatusCode);
            }
            else
            {
                throw new Exception($"Invalid HttpStatusCode specified: {statusCode}");
            }            
        }

        #endregion

    }
}
