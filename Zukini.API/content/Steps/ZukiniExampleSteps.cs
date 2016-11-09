using BoDi;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Zukini.API.Steps;


[Binding]
public sealed class ZukiniExampleSteps : ApiSteps
{
	public ZukiniExampleSteps(IObjectContainer objectContainer)
		: base(objectContainer)
	{
	}

	[Given(@"I perform a GET for post ""(.*)""")]
	public void GivenIPerformAGETForPost(int postId)
	{
		var response = SimpleGet(new Uri("http://jsonplaceholder.typicode.com"), String.Format("/posts/{0}", postId));
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
}
