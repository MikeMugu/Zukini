:://///////////////////////////////////////////////////////////////////////////
:: This batch file is responsible for running SpecFlow tests and generating
:: a specflow test run report.
:://///////////////////////////////////////////////////////////////////////////
@echo off
@setlocal

call :Defaults
call :RunTests
call :GenerateSpecFlowReport
call :ViewReport

:Defaults
rem ===========================================================================
SET NUNIT_EXE=%~dp03rdParty\NUnit\v2.6.4\bin\nunit-console-x86.exe
SET SPECFLOW_EXE=%~dp0packages\SpecFlow.1.9.0\Tools\specflow.exe
SET TEST_RESULTS=%~dp0TestResults.xml
SET TEST_OUTPUT=%~dp0TestOutput.txt
SET TEST_RESULTS_HTML=%~dp0TestResults.html
SET TEST_FILE=%~dp0IContactPro.Test.Functional\bin\Debug\IContactPro.Test.Functional.dll
SET PROJECT_FILE=%~dp0IContactPro.Test.Functional\IContactPro.Test.Functional.csproj
SET REPORT_TEMPLATE=%~dp03rdParty\SpecFlowReportTemplates\v1.0\nunit-dream\ExecutionReport.xslt

goto :eof

:RunTests
rem ===========================================================================
@echo ********************* Running Tests *********************************
%NUNIT_EXE% /labels /out:%TEST_OUTPUT% /xml:%TEST_RESULTS% /noshadow %TEST_FILE%

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
%TEST_RESULTS_HTML%

if ERRORLEVEL 1 goto :Error
goto :eof


:Exit
rem ===========================================================================
exit /b 0

:Error
rem ===========================================================================
exit /b 1

:End
