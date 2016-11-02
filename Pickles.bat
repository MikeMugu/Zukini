:://///////////////////////////////////////////////////////////////////////////
:: This batch file will execute the Pickles app and will produce documentation
:: for feature files in your project.
::
:: Information regarding Pickles can be found at: http://docs.picklesdoc.com/en/latest/
::
:: Links in this file are relative to the Zukini project setup and may need to be edited
:: to your project structure.
:://///////////////////////////////////////////////////////////////////////////
.\packages\Pickles.CommandLine.2.10.0\tools\pickles.exe --feature-directory=.\Zukini.Examples.Features\Features --output-directory=.\Pickles --link-results-file=.\TestResults.xml 