using BoDi;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Zukini.Examples.Pages;
using Zukini.Steps;
using TechTalk.SpecFlow.Assist;

namespace Zukini.Examples.Features.Steps
{
    [Binding]
    public class APISteps : BaseSteps
    {
        public APISteps(IObjectContainer objectContainer)
            : base(objectContainer)
        {
        }

        private Fields _fakeModel = new Fields();

        [Given(@"I make a fake API call wwith the following Data")]
        public void GivenIMakeAFakeAPICallWwithTheFollowingData(Table table)
        {
            var client = new RestClient("http://jsonplaceholder.typicode.com");
            var request = new RestRequest("/posts", Method.POST);
            request.RequestFormat = DataFormat.Json;
            table.FillInstance(_fakeModel);
            request.AddBody(_fakeModel);
            var response = client.Execute(request);
            RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            var JSONObj = deserial.Deserialize<Dictionary<string, string>>(response);
            string r = response.Content;
            Console.Write(r);
            PropertyBucket.Remember("SavedJsonResponse", r);
        }

        [StepDefinition(@"I should see that the ""(.*)"" field returned the (.*) value")]
        public void ThenIShouldSeeThatTheFieldReturnedTheValue(string FieldName, string ValueFromTable)
        {
            string SavedJsonResponse = PropertyBucket.GetProperty<string>("SavedJsonResponse");
            Dictionary<string, string> htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(SavedJsonResponse);
            string JSONData = (htmlAttributes[FieldName]);
            Assert.AreEqual(JSONData, ValueFromTable);
        }
       
    }
}
