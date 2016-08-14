:://///////////////////////////////////////////////////////////////////////////
:: This batch file is responsible for running SpecFlow tests and generating
:: a specflow test run report.
::
:: Usage:
::	RunTests.bat [-tags [tag1,tag2]] [-showreport]
::		-tags: 			Optionally specify which tags to run
::		-showreport:	Optionally specify this flag to automatically show
::						the test run report when finished.
:://///////////////////////////////////////////////////////////////////////////
@echo off
@setlocal

call :Defaults
call :ParseArguments %*
call :RunTests
call :GenerateSpecFlowReport
call :ViewReport

:Defaults
rem ===========================================================================
SET NUNIT_EXE=%~dp0packages\NUnit.Runners.2.6.4\tools\nunit-console-x86.exe
SET SPECFLOW_EXE=%~dp0packages\SpecFlow.1.9.0\Tools\specflow.exe
SET TEST_RESULTS=%~dp0TestResults.xml
SET TEST_OUTPUT=%~dp0TestOutput.txt
SET TEST_RESULTS_HTML=%~dp0TestResults.html
SET TEST_FILE=%~dp0Zukini.Examples.Features\bin\Debug\Zukini.Examples.Features.dll
SET PROJECT_FILE=%~dp0Zukini.Examples.Features\Zukini.Examples.Features.csproj
SET REPORT_TEMPLATE=%~dp03rdParty\SpecFlowReportTemplates\v1.0\nunit-dream\ExecutionReport.xslt

goto :eof

:ParseArguments
rem ===========================================================================
if /I .%1 == . goto :eof
if /I .%1 == .-tags goto :ArgumentTags
if /I .%1 == .-showreport goto :ArgumentShowReport

:ArgumentTags
rem ===========================================================================
SET TAGS=%2
shift
shift
goto :ParseArguments

:ArgumentShowReport
rem ===========================================================================
SET SHOWREPORT=TRUE
shift
shift
goto :ParseArguments


:RunTests
rem ===========================================================================
@echo ********************* Running Tests *********************************
IF DEFINED TAGS (
	%NUNIT_EXE% /labels /out:%TEST_OUTPUT% /xml:%TEST_RESULTS% /exclude:"skip" /include:"%TAGS%" /noshadow %TEST_FILE%
) ELSE (
	%NUNIT_EXE% /labels /out:%TEST_OUTPUT% /xml:%TEST_RESULTS% /exclude:"skip" /noshadow %TEST_FILE%
)

if ERRORLEVEL 1 goto :Error
goto :eof

:GenerateSpecFlowReport
rem ===========================================================================
@echo **************** Generating SpecFlow Report *************************
%SPECFLOW_EXE% nunitexecutionreport %PROJECT_FILE% /out:%TEST_RESULTS_HTML% /xmlTestResult:%TEST_RESULTS% /testOutput:%TEST_OUTPUT% /xsltFile:%REPORT_TEMPLATE%


if ERRORLEVEL 1 goto :Error
goto :eof

:ViewReport
rem ===========================================================================
if DEFINED SHOWREPORT (
	start %TEST_RESULTS_HTML%
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
