# Description
Zukini is a simple project that combines Selenium WebDriver, SpecFlow, NUnit and Coypu in a project setup to use a Page Object pattern. 
It provides some basic things typically needed in an automation framework such as taking screenshots on failure, a base StepDef class
that provides a browser session hook for your tests, and some other niceties.

# Getting Started
1. If you do not have Visual Studio, download the community edition of Visual Studio here: [https://www.visualstudio.com/en-us/products/free-developer-offers-vs.aspx]
2. Launch Visual Studio and install the following plugins (Tools->Extentions and Updates->Search):
    * NUnit Test Adapter
    * SpecFlow for Visual Studio 2013
3. Pull down the code
4. Load the Zukini.sln file
5. Write tests

# Test Structure
Zukini uses a simple test structure that is fairly common amongst automation pros:

    Features->StepDefs->PageObjects->Coypu->Selenium (or your choice of driver)

This project is still under construction...