# Description
Zukini is a simple project that combines Selenium WebDriver, SpecFlow, NUnit and Coypu in a project setup to use a Page Object pattern. 
It provides some basic things typically needed in an automation framework such as taking screenshots on failure, a base StepDef class
that provides a browser session hook for your tests, and some other niceties.

# Getting Started
1. If you don't have Visual Studio, download the community edition of Visual Studio here: [https://www.visualstudio.com/en-us/products/free-developer-offers-vs.aspx]
2. Launch Visual Studio and install the following plugins (Tools->Extentions and Updates->Search):
    * NUnit Test Adapter
    * SpecFlow for Visual Studio 2013
3. Pull down the code
4. Load the Zukini.sln file
5. Write tests

# Test Structure
Zukini uses a simple test structure that is fairly common amongst automation pros:

    Features->StepDefs->PageObjects->Coypu->Selenium (or your choice of driver)

# Features
Features are normal SpecFlow features. If you installed the SpecFlow for Visual Studio plugin, when creating a new item in visual studio, should be given the option to create a new SpecFlow feature file. See here for instructions on creating SpecFlow Feature Files: http://www.specflow.org/getting-started/

I like to create a seperate folder for my Feature files called, well, "Features", but you can name yours whatever you want.
    

#Step Definitions
When creating features, you are likely going to create Step Defintitions (otherwise your tests won't do anything). When you generate step definitions, you are going to get a generated class that looks something like this:

    [Binding]
    public class SmokeTestSteps
    {
        [Given(@"I navigate to Google")]
        public void GivenINavigateToGoogle()
        {
        }
    }
    
To use Zukini, we need to do a couple of things:
1. Derive our class from Zukini.Steps.BaseSteps
2. Create a constructor to inject in the IObjectContainer

For step 1, simply change your class declaration to:

    [Binding]
    public class SmokeTestSteps : Zukini.Steps.BaseSteps
    {
        ...
    }

Doing this gives us access to a class level Browser object that is used for navigating to things in the browser, manipulating contols, but mostly so we can pass the browser to our pages (more on this later).

For step 2, add a constructure with the following signature:

    public SmokeTestSteps(IObjectContainer objectContainer)
        : base(objectContainer)
    {
    }

We need to pass in the ObjectContainer so we can get access to the Browser. In Zukini, there is a BeforeScenario hook that initializes a BrowserSession with the goodness needed to drive the browser.

Again, I like to place my step definition files in a folder called, you guessed it, "Steps". The structure of my test assembly usually looks something like this:

Company.Product.Tests (Or Features)
    Features
        AreaOfTest1
            Feature1.feature
            Feature2.feature
        AreaOfTest2
            Feature1.feature
            Feature2.feature
    Steps
        AreaOfTest1
            Feature1Steps.cs
            Feature2Steps.cs
        AreaOfTest2
            Feature1Steps.cs
            Feature2Steps.cs
            
#Pages
Okay, getting down to the nitty gritty here. Page classes are a common pattern for keeping tests from becoming brittle, annoying, and downright unmaintainable. There are numerous blogs and articles out there about the subject so I will not go into why this is a good pattern here but I will give you some resources to check out, starting with CheezyWorld. When I started with Cucumber, I used CheezyWorlds blog as a map on how to implement a good page object pattern and it turned out pretty well. He has a series of blogs on the subject starting here: http://www.cheezyworld.com/2010/11/09/ui-tests-not-brittle/. I would reccommend checking it out, when you are done reading the first article, click the 'Next Article' link. Repeat until the articles are no longer talking about Cucumber and UI Tests.

I usually seperate my Page assembly from my Features. Not required but IMO it keeps the focus of the assemblies cleaner. You are also likely to have utility methods and other things that will go into the Page assembly and I just assume not pollute the Feature assembly. 

Pages are just classes, so to create a new Page class, simply create a new class in Visual Studio. Okay, just like the Step Definitions, there are a couple of things we need to do to leverage the utility methods in Zukini:

1. Derive your page class from Zukini.Pages.BasePage
2. Define a constructure that takes in a BrowserSession object

Deriving your page from Zukini.Pages.BasePage gives you access to a couple of things:

###Browser property
A protected Browser property is included that makes it easy to access the BrowserSession (for manipulating controls, navigating, etc...)

###AssertCurrentPage helper method
The AssertCurrentPage method is a simple pattern I have used in the past to provide an easy way to check to make sure we are on the proper page before continuing on with our testing. Typically in testing, when we do not land in the right spot, we get some other error that says we couldn't find something we were looking for. This might lead to confusion when troubleshooting the test. I like to verify we are on the proper page and if we are not, display an informative message that says "Hey, we are not where we should be!" This just makes troubleshooting a bit easier.

####Usage:
    AssertCurrentPage(string pageName, bool condition)
    
    pageName = The name of the page we are verifying
    condition = Usually an Exists method on something that is always on the target page (like a header or something)
    
####Example:
    
    // See the included W3Schools example page in the code for a full example.
    AssertCurrentPage("W3Schools Table", Browser.Title == "HTML table tag");

A failure in the condition will cause a CurrentPageException to be thrown.

#Extension Methods
Coypu is a great library, it does the work of wrapping the browser controls into an easy to use API that makes manipulating the browser pretty darn easy. The one thing it does not do great is manipulate tables. Typically you need to do things like, hey, is there a row in this table that contains the text "blah". Or, give me the 3rd row in the table, and from that row, give me the 2nd cell. For this, Zukini provides a few extension methods to help dealing with tables.

All of these extension methods extend the ElementScope element, and can be used by simply adding a "using Zukini;" to your using statements.

##TableRows
    FindAllRows()








