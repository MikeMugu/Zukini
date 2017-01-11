::///////////////////////////////////////////////////////////////////////////////////////
:: This batch file will execute the Pickles app and will produce documentation
:: for feature files in your project.
::
:: Information regarding Pickles can be found at: http://docs.picklesdoc.com/en/latest/
:: Usage:
::	Pickles [-out <output-dir>] [-format <pickles-doc-format>] [results <test-results-file>]
::		- out 		==> Output directory for Pickles documenation
::		- format 	==> One of the valid pickles document formats (defaults to Dhtml)
::		- results	==> File containing the test results as produced by your test execution engine
::///////////////////////////////////////////////////////////////////////////////////////
@echo off
@setlocal

call :Defaults
call :ParseArguments %*
call :GenerateDocumentation

:Defaults
rem ===========================================================================
SET PICKLES_EXE=%~dp0packages\Pickles.CommandLine.2.11.1\tools\pickles.exe
SET OUTPUT_DIR=%~dp0\Pickles
SET TEST_RESULTS=%~dp0TestResults.xml
SET DOC_FORMAT=Dhtml

goto :eof

:ParseArguments
rem ===========================================================================
if /I .%1 == . goto :eof
if /I .%1 == .-out goto :ArgumentOutput
if /I .%1 == .-format goto :ArgumentFormat
if /I .%1 == .-results goto :ArgumentResults

:ArgumentOutputDir
rem ===========================================================================
SET OUTPUT_DIR=%2
shift
shift
goto :ParseArguments

:ArgumentFormat
rem ===========================================================================
SET DOC_FORMAT=%2
shift
shift
goto :ParseArguments

:ArgumentResults
rem ===========================================================================
SET TEST_RESULTS=%2
shift
shift
goto :ParseArguments

:GenerateDocumentation
rem ===========================================================================
@echo ********************* Generating Pickles Docs *********************************
%PICKLES_EXE% --feature-directory=. --output-directory=%OUTPUT_DIR% --link-results-file=%TEST_RESULTS% --documentation-format=%DOC_FORMAT%

if ERRORLEVEL 1 goto :Error
goto :eof

:Exit
rem ===========================================================================
exit /b 0

:Error
rem ===========================================================================
exit /b 1

:End