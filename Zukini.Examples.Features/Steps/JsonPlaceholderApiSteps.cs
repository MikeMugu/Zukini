using BoDi;
using RestSharp;
using TechTalk.SpecFlow;
using Zukini.API.Steps;
using Zukini.API;
using NUnit.Framework;

namespace Zukini.Examples.Features.Steps
{
    [Binding]
    public class JsonPlaceholderApiSteps : ApiSteps
    {
        public JsonPlaceholderApiSteps(IObjectContainer objectContainer)
            : base(objectContainer)
        {
        }

        [Given(@"I make a fake API call with the data")]
        public void GivenIMakeAFakeAPICallWithTheData(Table table)
        {
            // Setup rest client
            var restClient = new RestClient(TestSettings.JsonPlaceholderApiUrl);
            restClient.AddHandler("application/json", new DynamicJsonDeserializer());

            // Setup rest request
            var request = new RestRequest("/posts", Method.POST);
            request.AddJsonBody(table);

            // Get response
            var response = restClient.Execute<dynamic>(request);
            
            // Have to understand the Table structure for this
            // TODO: Factor out to helper method in Zukini.API
            PropertyBucket.Remember<dynamic>("ContactData", response.Data.Rows[0]);
        }

        [Then(@"I should see that the ""(.*)"" field returned a value of ""(.*)""")]
        public void ThenIShouldSeeThatTheFieldReturnedAValueOf(string fieldName, string expectedValue)
        {
            var contactData = PropertyBucket.GetProperty<dynamic>("ContactData");
            Assert.AreEqual(expectedValue, contactData.GetValue(fieldName).ToString());
        }
    }
}
