#Zukini
Zukini is a simple project that sets out to provide an out of the box test automation framework for both UI and API testing. At it's core,
Zukini provides a well established design pattern for authoring BDD style tests as well as some helper methods that we have found useful
throughout our careers in software engineering.

There are three main parts to Zukini:

###Zukini
This is the core assembly to Zukini and contains logic that applies to any type of testing whether it is API or UI based. Examples include common Hooks, PropertyBucket helpers, Unique test id generator, etc...

### Zukini.UI
Zukini.UI provides functionality specific to performing Selenium based UI tests. Zukini.UI combines Selenium WebDriver, SpecFlow, NUnit and Coypu in a project setup to use a Page Object pattern. 

It also provides some basic things typically needed in a UI automation framework such as taking screenshots on failure, a base StepDef class
that provides a browser session hook for your tests, and some other niceties.

### Zukini.API
Zukini.API utilizes RestSharp to provide some simple helper methods used to test API's. It attempts to simplify the task of testing different API verbs (e.g. POST, PUT, GET, etc..). 

##Getting Started
1. If you don't have Visual Studio, download the community edition of Visual Studio here: [https://www.visualstudio.com/en-us/products/free-developer-offers-vs.aspx]
2. Launch Visual Studio and install the following plugins (Tools->Extentions and Updates->Search):
    * NUnit3 Test Adapter (Version 2 will no longer work) 
    * SpecFlow for Visual Studio 2015
3. Pull down the code
4. Load the Zukini.sln file
5. Write tests

##Test Structure
Zukini.UI uses a simple test structure that is fairly common amongst automation pros:

    Features->StepDefs->PageObjects->Coypu->Selenium (or your choice of driver)

For Zukini.API, the structure is simply:

    Features->StepDefs

You could probably add an Endpoint (instead of PageObject) layer in there if you want but up to you.

###Features
Features are normal SpecFlow features. If you installed the SpecFlow for Visual Studio plugin, when creating a new item in visual studio, should be given the option to create a new SpecFlow feature file. See here for instructions on creating SpecFlow Feature Files: http://www.specflow.org/getting-started/

I like to create a seperate folder for my Feature files called, well, "Features", but you can name yours whatever you want.
    

###Step Definitions
When creating features, you are likely going to create Step Defintitions (otherwise your tests won't do anything). When you generate step definitions, you are going to get a generated class that looks something like this:

    [Binding]
    public class SmokeTestSteps
    {
        [Given(@"I navigate to Google")]
        public void GivenINavigateToGoogle()
        {
        }
    }
    
#####Zukini.UI
To use Zukini.UI, we need to do a couple of things:

1. Derive our class from Zukini.UI.Steps.BaseSteps
2. Create a constructor to inject in the IObjectContainer

For step 1, simply change your class declaration to:

    [Binding]
    public class SmokeTestSteps : Zukini.UI.Steps.BaseSteps
    {
        ...
    }

Doing this gives us access to a class level Browser object that is used for navigating to things in the browser, manipulating contols, but mostly so we can pass the browser to our pages (more on this later).

Step 2, add a constructor with the following signature:

    public SmokeTestSteps(IObjectContainer objectContainer)
        : base(objectContainer)
    {
    }

We need to pass in the ObjectContainer so we can get access to the Browser. In Zukini.UI, there is a BeforeScenario hook that initializes a BrowserSession with the goodness needed to drive the browser.

#####Zukini.API
For Zukini.API, the process is similar:

1. Derive our class from Zukini.API.Steps.ApiSteps
2. Create a constructor to inject in the IObjectContainer

Step 1, simply change your class declaration to:

    [Binding]
    public class JsonPlaceholderApiSteps : Zukini.API.Steps.ApiSteps
    {
        ...
    }

Step 2, add a constructor with the following signature:

    public JsonPlaceholderApiSteps(IObjectContainer objectContainer)
            : base(objectContainer)
        {
        }

#####Step definition Structure

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
            
####PropertyBucket
Another utility included in the Zukini.BaseSteps class is a PropertyBucket that can be used for storing and retrieving properties between steps. The PropertyBucket gets initialized at the beginning of each test, so properties can be stored between steps, but not between tests. This is very much on purpose as you do not want to store global properties that can cause inter-test dependencies.

#####Example
The PropertyBucket is available within any of your step defs.

    // To remember a property
    PropertyBucket.Remember("MyPropertyName", MyPropertyToSave);
    OR
    PropertyBucket.Remember<string>("MyPropertyName", "MyStringValue");

The first argument is simply a key to identify how to retrieve the property later. The second is any object you want to save. This can be a string, an object of some sort, an integer, whatever. The second example is a generic overload that allows you to explicitly tell the method what type the item is that your saving. I prefer the second just to be explicit.

If the property you are trying to save is already there, a PropertyAlreadyExistsException will be thrown. You can change this behavior by passing in a third parameter like so:

    // Overwrite my property
    PropertyBucket.Remember("MyPropertyName", MyPropertyToSave, true);
    

To retrieve a property:

    PropertyBucket.GetProperty("MyPropertyName")
    OR
    PropertyBucket.GetProperty<String>("MyPropertyName")
    
Same thing here, your passing in the property name you want to retrieve. If the property has not been remembered yet, a PropertyNotFoundException will be thrown.

#####TestId
I find it handy to have a UniqueID that I can use to reference my tests and that stays consistent throughout the test. I use it to do things like name screenshots. As such, there is a TestId automatically available on the property bucket. Simply reference with PropertyBucket.TestId. I also exposed this as a protected Step Definition property, so within a step definition, you can simply call this.TestId. 

Screenshots are also named using the TestId to make matching them up a bit easier.


###Pages
Okay, getting down to the nitty gritty here. Page classes are a common pattern for keeping tests from becoming brittle, annoying, and downright unmaintainable. There are numerous blogs and articles out there about the subject so I will not go into why this is a good pattern here but I will give you some resources to check out, starting with CheezyWorld. When I started with Cucumber, I used CheezyWorlds blog as a map on how to implement a good page object pattern and it turned out pretty well. He has a series of blogs on the subject starting here: http://www.cheezyworld.com/2010/11/09/ui-tests-not-brittle/. I would reccommend checking it out, when you are done reading the first article, click the 'Next Article' link. Repeat until the articles are no longer talking about Cucumber and UI Tests.

I usually seperate my Page assembly from my Features. Not required but IMO it keeps the focus of the assemblies cleaner. You are also likely to have utility methods and other things that will go into the Page assembly and I just assume not pollute the Feature assembly. 

Pages are just classes, so to create a new Page class, simply create a new class in Visual Studio. Okay, just like the Step Definitions, there are a couple of things we need to do to leverage the utility methods in Zukini:

1. Derive your page class from Zukini.Pages.BasePage
2. Define a constructure that takes in a BrowserSession object

Deriving your page from Zukini.Pages.BasePage gives you access to a couple of things:

####Browser property
A protected Browser property is included that makes it easy to access the BrowserSession (for manipulating controls, navigating, etc...)

####AssertCurrentPage helper method
The AssertCurrentPage method is a simple pattern I have used in the past to provide an easy way to check to make sure we are on the proper page before continuing on with our testing. Typically in testing, when we do not land in the right spot, we get some other error that says we couldn't find something we were looking for. This might lead to confusion when troubleshooting the test. I like to verify we are on the proper page and if we are not, display an informative message that says "Hey, we are not where we should be!" This just makes troubleshooting a bit easier.

#####Usage:
    AssertCurrentPage(string pageName, bool condition)
    
    pageName = The name of the page we are verifying
    condition = Usually an Exists method on something that is always on the target page (like a header or something)
    
#####Example:
    
    // See the included W3Schools example page in the code for a full example.
    AssertCurrentPage("W3Schools Table", Browser.Title == "HTML table tag");

A failure in the condition will cause a CurrentPageException to be thrown.

###Hooks
Zukini includes some hooks that take care of a couple of typical things you need to do when setting up your test project.

###BeforeScenario
The BeforeScenario hook in Zukini will handle firing up a browser for the beginning of the test. It also will take into consideration any SessionConfiguration values that are set. The way you set your own configuration settings is to provide your own BeforeScenario hook and set settings up. 

#####Example:

    private readonly SessionConfiguration _sessionConfiguration;

    public Hooks(SessionConfiguration config)
    {
        _sessionConfiguration = config;
    }
    
    [BeforeScenario]
    public void BeforeScenario()
    {
        _sessionConfiguration.Browser = Coypu.Drivers.Browser.Firefox;
        _sessionConfiguration.Timeout = TimeSpan.FromSeconds(3);
        _sessionConfiguration.RetryInterval = TimeSpan.FromSeconds(0.1);
    }
        
This example will setup the browser to use FireFox, set a default timeout of 3 seconds, and set the retry interval to 0.1 seconds. I would reccommend setting this stuff up in a config file so you can change these settings easily. Take a look at the Hooks.cs file that is provided in the Zukini.Examples.Features project for an example of this.

Additionally, the BeforeScenario hook in Zukini will register the BrowserSession with the injected IObjectContainer so it is available to our Steps and Pages. (See here for more details on IObjectContainer in SpecFlow: https://github.com/techtalk/SpecFlow/wiki/Context-Injection).

###ZukiniUIConfiguration
There is also a ZukiniUIConfiguration that can be set. To utilize, simply change your Hooks constructor to take in a ZukiniUIConfiguration as well as a SessionConfiguration like so:

    private readonly SessionConfiguration _sessionConfiguration;
    private readonly ZukiniConfiguration _zukiniConfiguration;
    private readonly IObjectContainer _objectContainer;

    public Hooks(IObjectContainer container, 
                 SessionConfiguration sessionConfig, 
                 ZukiniUIConfiguration zukiniConfig)
    {
        _objectContainer = container;
        _sessionConfiguration = sessionConfig;
        _zukiniConfiguration = zukiniConfig;
    }

A couple of notes about the other arguments in this constructor:

1. We take in the IObjectContainer so we have access to the DI container. In the example project, this allows us to register custom drivers that are setup with custom profiles (see Providing a custom driver below).
2. The SessionConfiguration is a Coypu object that we use to setup set our Session for the test.
 

The ZukiniUIConfiguration allows you to specify a couple of extra items such as whether to Maximize the browser on startup or not. It also allows you to set where your screenshots are saved when executing. Example:

    [BeforeScenario]
    public void BeforeScenario()
    {
        _sessionConfiguration.Browser = Coypu.Drivers.Browser.Firefox;
        _sessionConfiguration.Timeout = TimeSpan.FromSeconds(3);
        _sessionConfiguration.RetryInterval = TimeSpan.FromSeconds(0.1);

        // Set Zukini Specific configurations
        _zukiniConfiguration.MaximizeBrowser = true;
        _zukiniConfiguration.ScreenshotDirectory = "..\..\Screenshots";
    }

The previous settings will maximize the browser on startup, and change the directory where screenshots are saved to the relative directory specified. I find this useful when integrating with CI tools like Jenkins as it lets you put the screenshots in an accessible place for viewing.

###Providing a custom Driver
In most cases, you will want to customize something about the driver you are using. For example, you may want to provide your own FireFox profile or set specific settings when using Chrome. As of version 1.1.5, you can now do this. To provide your own Driver, you must instantiate a custom driver and pass it into a BrowserSession object, then register that type with the Dependency Injection container. This way, the rest of the code base will know to use your driver. 

####Example

    private void RegisterCustomChromeBrowser()
    {
        // create our chrome options and set a value
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("no-sandbox");

        // Pass options to a new chrome browser and pass into the BrowserSession
        var customChromeDriver = new CustomChromeSeleniumDriver(chromeOptions);
            var browserSession = new BrowserSession(customChromeDriver);

        // Finally, register with the DI container.
        _objectContainer.RegisterInstanceAs<BrowserSession>(browserSession);
    }
    
You would then call this method from within the BeginScenario method. For a full example, see the BeforeScenario() hook in https://github.com/MikeMugu/Zukini/blob/master/Zukini.Examples.Features/Hooks.cs 

###AfterScenario
The AfterScenario hook in Zukini will properly close and dispose of the Browser object. It will also take a screenshot if a test error ocurred. The screenshot currently shows up in the TestResults folder of the current directory and is named with a unique name for each test.


##Extension Methods
Coypu is a great library, it does the work of wrapping the browser controls into an easy to use API that makes manipulating the browser pretty darn easy. The one thing it does not do great is manipulate tables. Typically you need to do things like, hey, is there a row in this table that contains the text "blah". Or, give me the 3rd row in the table, and from that row, give me the 2nd cell. For this, Zukini provides a few extension methods to help dealing with tables.

All of these extension methods extend the ElementScope element or BrowserSession, and can be used by simply adding a "using Zukini;" to your using statements.

###TableRows
    
    IEnumerable<SnapshotElementScope> FindAllRows()
    
Finds all rows that are descendants of the current element scope. This method is usually called when the current element scope is a table.

    IEnumerable<SnapshotElementScope> FindRows(string searchValue)
    
Returns any child table rows that contain the specified search value. If no elements are found, an empty colleciton is returned.

    SnapshotElementScope FindRow(string searchValue)
    
Like FindRows but returns the fist row that contains the specified searchValue. If no row is found, this method returns null.

    SnapshotElementScope FindRow(int columnIndex, string searchValue)
    
Finds a row where the column at columnIndex (Zero based) contains the specified searchValue. If no row is found, this method returns null.

###TableCells

    IEnumerable<SnapshotElementScope> FindAllCells()
    
Finds all descendant cells in a table or row. Usually this is called once you are scoped to a row element.

    IEnumerable<SnapshotElementScope> FindCells(string searchValue)
    
Finds all cells that match the specified searchValue. If no match is found, this method returns an empty collection.

    SnapshotElementScope FindCell(string searchValue)
    
Finds the first cell that matches the provided searchValue. If no match is found, this method returns null.

###Example Usage
I have indcluded an example test that goes to the W3Schools Table tag page and verifies that the tag is supported in the various browsers by going through the table and verifying that we see the word "Yes" in the correct cell. It looks something like this:

    public bool IsBrowserSupported(string browserName)
    {
        BrowserName browser;
        if (!Enum.TryParse<BrowserName>(browserName, out browser))
        {
            throw new Exception(String.Format("Invalid browser name {0} supplied.", browserName));
        }

        // Get second row
        var row = BrowserReferenceTable.FindAllRows().ElementAt(1);

        // Get the cell we want using the cell index (add 1 to account for row header on the page)
        ElementScope cell = row.FindAllCells().ElementAt(((int)browser + 1));

        // If cell text == yes, the browser is supported
        return cell.Text.Equals("Yes", StringComparison.CurrentCultureIgnoreCase);
    }

###Rectangle

	Rectangle Rectangle()

Convenient accessor to get the location and size properties from the native WebElement.

###Example Usage

	public Size GetElementSize(ElementScope element)
	{
		return element.Rectangle().Size;
	}

	public Point GetElementLocation(ElementScope element)
	{
		return element.Rectangle().Location;
	}

###ScrollIntoView

	 void ScrollIntoView(ElementScope element)

Using the BrowserSession, scrolls an element into view.

###Example Usage

	var customRemoteChromeDriver = new CustomRemoteChromeSeleniumDriver(new DesiredCapabilities());
	var browserSession = new BrowserSession(_sessionConfiguration, customRemoteChromeDriver);
	browserSession.ScrollIntoView(pageObject.SomeElement);

###TryUntil

	void TryUntil(Action action, [Options actionOptions,] Func<bool> until [,Options tryUntilOptions])

Convenience method that provides a wrapper around Coypu's built-in TryUntil methods that allows the caller to use lambda expressions directly rather than BrowserActions and PredicateQueries

###Example Usage

	
	Browser.TryUntil(()=> pageObject.AddElementsButton.Click(), () => pageObject.ElementsThatIncreaseInNumber.Count() > 0, "Elements were not added to the page");

###WaitUntil

	void WaitUntil(Func<bool> until [Options options,] [string descriptionForError])

Convenience method for "no op" usages of TryUntil where the Action parameter is a "do nothing" lambda expression like () => {}. This is helpful when you do not want to continually execute an Action parameter while waiting for the until condition becomes true such as clicking a button and then waiting for something to update. It is recommended to use the optional parameter 'descriptionForError' to aid with debugging test failures and timeouts.

	var oldCount = page.ElementsThatIncreaseInNumber.Count();
	pageObject.AddElementsButton.Click();
	Browser.WaitUntil(() => pageObject.ElementsThatIncreaseInNumber.Count() > oldCount, "Elements were not added to the page");

##API Helper Methods
If you derive from the Zukini.API.Steps.ApiSteps base class, you will get some helper methods for free.

* There are several HTTP VERB methods used to perform actions such as GET, POST, etc... These methods return full response objects that can be interrogated as part of your tests.

* For each VERB method, there are also SimpleVERB methods. These methods perform the same VERB actions, but instead of returning the entire Response object, it only returns the data (for those times where you just want to validate the data coming back).

##RunTests.bat
In addition to the base classes and extension methods, there is a RunTest.bat file that makes it easy to run your tests. This batch file does a few things:
1. First, it runs the tests using the NUnit test runner. 
2. After running the tests, it generates a report using SpecFlow.exe (default report) 
3. Provides a way to specify tags

**Before executing, copy the specflow.exe.config file located in this repository into the packages\SpecFlow.1.9.0\Tools folder. This is to force SpecFlow.exe to use the .NET 4.0 runtime.

####Usage:

    RunTests.bat [-tags [tag1,tag2] [-showreport]
    
    -tags:          Specify which tags to run (comma seperated list). Omitting this flag will run all tests.
    -showreport:    Specify this option to have the generated test report launch in your default browser after the test run is complete.
    
By default, any tests with the @skip tag will be skipped during execution. To use this for a different project, you just have to modify the batch file to specify your Test dll and .csproj file.

##Other Recommendations
One other recommendation I would make is to factor out your test settings into your App.config. Usually XML transforms are for web projects, however I use the wonder Visual Studio plugin "Configuration Transform" to generate an App.config file for each Visual Studio Configuration I have (e.g. Debug, Release, etc...). This allows me to override configuration values depending on what environment I am testing in.

For example, you might have an App.confg that has the settings for testing locally, but then an App.Internal.Config for the internal environment, an App.Staging.config for your Pre-prod environment, and an App.Prod.config file for your production environment. I have included an example of this in the Zukini.Examples.Features project.

Checkout: https://msdn.microsoft.com/en-us/library/dd465326%28v=vs.110%29.aspx for more information on XML transforms in Visual Studio.
