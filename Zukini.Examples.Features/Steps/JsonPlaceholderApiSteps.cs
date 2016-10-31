using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using BoDi;
using RestSharp;
using TechTalk.SpecFlow;
using Zukini.API.Steps;
using Zukini.API;
using NUnit.Framework;
using System;

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
            var result = Post(new Uri(TestSettings.JsonPlaceholderApiUrl), "/posts", table.Rows[0]);
            PropertyBucket.Remember<dynamic>("ContactData", result);
        }

        [Then(@"I should see that the ""(.*)"" field returned a value of ""(.*)""")]
        public void ThenIShouldSeeThatTheFieldReturnedAValueOf(string fieldName, string expectedValue)
        {
            var contactData = PropertyBucket.GetProperty<dynamic>("ContactData");
            Assert.AreEqual(expectedValue, contactData[fieldName]);
        }
    }
}
