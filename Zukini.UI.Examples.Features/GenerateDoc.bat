:://///////////////////////////////////////////////////////////////////////////
:: This batch file will execute the Pickles app and will produce documentation
:: for feature files in your project.
::
:: Information regarding Pickles can be found at: http://docs.picklesdoc.com/en/latest/
::
:: Usage:
::	GenerateDocs.bat [-features] <featureDir> [-outputDir] <outputDir> [-testResults] <testResults> [-showDoc]
::		-features: 		Optionally specify which directory contains your feature files
::		-outputDir:		Optionally specify where to place the documentation files.
::		-testResults:	Optionally specify a path to your test results file (NUnit format)
::		-showDoc:		Optionally set to show the documentation after generating
::						
:://///////////////////////////////////////////////////////////////////////////
@echo off
@setlocal

call :Defaults
call :ParseArguments %*
call :GenerateDocs
call :ShowDoc

:Defaults
rem ===========================================================================
SET PICKLES_EXE=%~dp0..\packages\Pickles.CommandLine.2.10.0\tools\pickles.exe
SET FEATURE_DIR=%~dp0Features
SET OUTPUT_DIR=%~dp0Documenation
SET TEST_RESULTS=%~dp0TestResults.xml
SET FORMAT=dhtml

goto :eof

:ParseArguments
rem ===========================================================================
if /I .%1 == . goto :eof
if /I .%1 == .-features goto :ArgumentFeatures
if /I .%1 == .-outputDir goto :ArgumentOutputDir
if /I .%1 == .-testResults goto :ArgumentTestResults
if /I .%1 == .-showDoc goto :ArgumentShowDoc

:ArgumentFeatures
rem ===========================================================================
SET FEATURE_DIR=%2
shift
shift
goto :ParseArguments

:ArgumentOutputDir
rem ===========================================================================
SET OUTPUT_DIR=%2
shift
shift
goto :ParseArguments

:ArgumentTestResults
rem ===========================================================================
SET TEST_RESULTS=%2
shift
shift
goto :ParseArguments

:ArgumentShowDoc
rem ===========================================================================
SET SHOWDOC=TRUE
shift
shift
goto :ParseArguments

:GenerateDocs
rem ===========================================================================
@echo ********************* Generating Documentation *******************************
%PICKLES_EXE% --feature-directory=%FEATURE_DIR% --output-directory=%OUTPUT_DIR% --link-results-file=%TEST_RESULTS% --documentation-format=%FORMAT%

if ERRORLEVEL 1 goto :Error
goto :eof

:ShowDoc
rem ===========================================================================
if DEFINED SHOWDOC (
	start %OUTPUT_DIR%\index.html
)

if ERRORLEVEL 1 goto :Error
goto :eof


:Exit
rem ===========================================================================
exit /b 0

:Error
rem ===========================================================================
exit /b 1

:End