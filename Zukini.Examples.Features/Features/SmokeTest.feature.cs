﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Zukini.Examples.Features.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("SmokeTest")]
    public partial class SmokeTestFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SmokeTest.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "SmokeTest", "\tIn order to provide an example of Zukini\r\n\tAs a user\r\n\tI want to try it out agai" +
                    "nst Google", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Perform a google search for SpecFlow returns specflow.org site")]
        [NUnit.Framework.CategoryAttribute("google_search")]
        public virtual void PerformAGoogleSearchForSpecFlowReturnsSpecflow_OrgSite()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Perform a google search for SpecFlow returns specflow.org site", new string[] {
                        "google_search"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I navigate to Google", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("I enter a search value of \"SpecFlow\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.When("I press Google Search", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.Then("I should see \"www.specflow.org\" in the results", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("I want to show how to use row and cell helpers")]
        [NUnit.Framework.CategoryAttribute("table_example")]
        public virtual void IWantToShowHowToUseRowAndCellHelpers()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("I want to show how to use row and cell helpers", new string[] {
                        "table_example"});
#line 14
this.ScenarioSetup(scenarioInfo);
#line 15
 testRunner.Given("I navigate to W3Schools table reference page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 16
 testRunner.Then("I should see that the table tag is supported in \"Chrome\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 17
 testRunner.And("I should see that the table tag is supported in \"IE\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And("I should see that the table tag is supported in \"FireFox\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("I should see that the table tag is supported in \"Safari\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("I want to demonstrate how to use the property bucket")]
        [NUnit.Framework.CategoryAttribute("property_bucket")]
        public virtual void IWantToDemonstrateHowToUseThePropertyBucket()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("I want to demonstrate how to use the property bucket", new string[] {
                        "property_bucket"});
#line 22
this.ScenarioSetup(scenarioInfo);
#line 23
 testRunner.Given("I navigate to W3Schools table reference page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 24
 testRunner.And("I remember the sub-header text", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.Then("the sub-header text should have been \"THE WORLD\'S LARGEST WEB DEVELOPER SITE\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Performing a search for SpecFlow and expecting random text should fail and give m" +
            "e a screenshot")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        public virtual void PerformingASearchForSpecFlowAndExpectingRandomTextShouldFailAndGiveMeAScreenshot()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Performing a search for SpecFlow and expecting random text should fail and give m" +
                    "e a screenshot", new string[] {
                        "ignore"});
#line 28
this.ScenarioSetup(scenarioInfo);
#line 29
 testRunner.Given("I navigate to Google", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 30
 testRunner.And("I enter a search value of \"SpecFlow\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.When("I press Google Search", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
 testRunner.Then("I should see \"ZZZXXXYYYGGGJJJPPPP\" in the results", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("I want to demonstrate how to use SpecFlow data tables")]
        [NUnit.Framework.CategoryAttribute("table_example")]
        [NUnit.Framework.TestCaseAttribute("Chrome", new string[0])]
        [NUnit.Framework.TestCaseAttribute("IE", new string[0])]
        [NUnit.Framework.TestCaseAttribute("FireFox", new string[0])]
        [NUnit.Framework.TestCaseAttribute("Safari", new string[0])]
        public virtual void IWantToDemonstrateHowToUseSpecFlowDataTables(string browser, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "table_example"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("I want to demonstrate how to use SpecFlow data tables", @__tags);
#line 35
this.ScenarioSetup(scenarioInfo);
#line 36
 testRunner.Given("I navigate to W3Schools table reference page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Browser"});
            table1.AddRow(new string[] {
                        string.Format("{0}", browser)});
#line 37
 testRunner.Then("I should see that the table tag is supported for the following", ((string)(null)), table1, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Make a post to an API and check the response returned")]
        [NUnit.Framework.CategoryAttribute("api_example")]
        public virtual void MakeAPostToAnAPIAndCheckTheResponseReturned()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Make a post to an API and check the response returned", new string[] {
                        "api_example"});
#line 48
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "email",
                        "name",
                        "address",
                        "city",
                        "state",
                        "zip"});
            table2.AddRow(new string[] {
                        "test@test.com",
                        "Joe Tester",
                        "123 Main St",
                        "Somehwere",
                        "CA",
                        "90210"});
#line 49
 testRunner.Given("I make a fake API call with the data", ((string)(null)), table2, "Given ");
#line 52
 testRunner.Then("I should see that the \"email\" field returned a value of \"test@test.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 53
 testRunner.And("I should see that the \"name\" field returned a value of \"Joe Tester\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.And("I should see that the \"address\" field returned a value of \"123 Main St\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("I should see that the \"city\" field returned a value of \"Somewhere\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("I should see that the \"state\" field returned a value of \"CA\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And("I should see that the \"zip\" field returned a value of \"90210\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
