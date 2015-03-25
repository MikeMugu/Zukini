Specflow Report Templates
=========================

[Specflow](https://github.com/techtalk/SpecFlow/) is a fantastic BDD tool, 
but the default template that comes with it's report generator is not only ugly but also missing
some useful information from the test result (such as categories and namespaces).

The aim of this project is to collect templates that can be passed to specflow report generator to get better output.

## Contributing

Have you made your own XSLT for this purpose? Why not share it with others here?
Also if you found an issue or want to improve something. I welcome pull-requests :)

## Usage

You should use the report generator command-line [as always](https://github.com/techtalk/SpecFlow/wiki/Reporting) but with an additional `xsltFile` parameter, passing to it the XSLT file path.

```bash
  specflow.exe nunitexecutionreport "xyz.csproj" /xsltFile:"<path to the XSLT file of the template>"
```

## Example

Here the [default template](https://github.com/mvalipour/specflow-report-templates/tree/master/nunit-default) 
and [a new template called dream](https://github.com/mvalipour/specflow-report-templates/tree/master/nunit-dream)
are compared:

<hr>

<img width="50%" src='https://raw.github.com/mvalipour/specflow-report-templates/master/nunit-default/sample/sample.png' />
<img width="50%" src='https://raw.github.com/mvalipour/specflow-report-templates/master/nunit-dream/sample/sample.png' />
