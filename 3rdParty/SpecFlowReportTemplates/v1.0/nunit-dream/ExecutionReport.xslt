<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:sfr="urn:TechTalk:SpecFlow.Report"
                xmlns:nunit="urn:NUnit"
                exclude-result-prefixes="msxsl">
  <xsl:output method="html" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN"/>

  <xsl:include href="../Common/Common.xslt"/>

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:key name="step-usage-variation" match="sfr:Instance" use="normalize-space(string(sfr:ScenarioStep))"/>

  <xsl:template match="/">
    <xsl:variable name="title">
      <xsl:value-of select="sfr:NUnitExecutionReport/@projectName"/> Test Execution Report
    </xsl:variable>
    <xsl:text disable-output-escaping="yes">&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <head>
        <xsl:call-template name="html-common-header">
          <xsl:with-param name="title" select="$title" />
        </xsl:call-template>
        <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/bootstrap/2.3.2/css/bootstrap.css" type="text/css" />
        <link rel="stylesheet" href="http://prismjs.com/themes/prism-okaidia.css" data-noprefix="" />
        <style>
          <![CDATA[
          *:target {background:#FFFFCC; outline:solid 10px #FFFFCC;}
          
          .nowrap {white-space:nowrap;}
          .traceMessage {display:none;}
          
          .table .progress {margin-bottom:0;}
          
          .table th.rotated-text-container{
vertical-align:bottom;
height:70px;
width:20px;
}
.table th.rotated-text-container .rotated-text{
display:block;
width:20px;
white-space:nowrap;
/*transform*/
-webkit-transform:rotate(270deg);
   -moz-transform:rotate(270deg);
    -ms-transform:rotate(270deg);
     -o-transform:rotate(270deg);
        transform:rotate(270deg);
}
table.table-bordered tr td.namespace-tree-container{
padding:0;
width:1px;
}
.table-bordered tr td table.namespace-tree-table{
table-layout:fixed;
color:#888;
}
.table-bordered tr td table.namespace-tree-table td{
padding:0;
}
.table-bordered tr td table.namespace-tree-table td.node{
width:120px;
height:150px;
outline:solid #ddd 1px;
vertical-align:middle;
padding:0 5px;
}
.table-bordered tr td table.namespace-tree-table td.node .rotated-text{
/*transform*/
-webkit-transform:rotate(270deg);
   -moz-transform:rotate(270deg);
    -ms-transform:rotate(270deg);
     -o-transform:rotate(270deg);
        transform:rotate(270deg);
/*transform-origin*/
-webkit-transform-origin:50%;
   -moz-transform-origin:50%;
    -ms-transform-origin:50%;
     -o-transform-origin:50%;
        transform-origin:50%;
display:block;
position:absolute;
top:0;
left:0;
white-space:nowrap;
width:150px;
text-align:center;
margin-left:-55px;
margin-top:-10px;
}
.table-bordered tr td table.namespace-tree-table td.node .text-container{
position:relative;
}   
          .bar
          {
            height: 1.5em;
          }
          .bar div
          {
            height: 1.5em;
            float: left;
          }
          div.failurePanel
          {
            background-color: #DDDDDD;
            width: 800px;
            overflow:auto;
          }
          span.traceMessage
          {
            font-style:italic;
            margin-left: 2em;
            color: #888888;
          }
          a {text-decoration:none; }
          .path-partition { padding:2px 5px; overflow: hidden; text-overflow: ellipsis; vertical-align: middle; }
          .path {color:#ccc; }
          ]]>
        </style>
      </head>
      <body>
        <div class="container">
          <xsl:call-template name="html-body-header">
            <xsl:with-param name="title" select="$title" />
          </xsl:call-template>
		  <div>Template by <a href='https://github.com/mvalipour'>M Valipour</a> via specflow-report-templates project (see on <a href='https://github.com/mvalipour/specflow-report-templates'>GitHub</a>)</div>
          <h2>Summary</h2>
          <xsl:variable name="summary">
            <xsl:call-template name="get-summary" />
          </xsl:variable>

          <table class="table table-bordered table-hover" cellpadding="0" cellspacing="0">
            <tr>
              <th class="top left">Features</th>
              <th class="top" colspan="2">Success rate</th>
              <th class="top rotated-text-container">
                <span class="rotated-text">Scenarios</span>
              </th>
              <th class="top rotated-text-container">
                <span class="rotated-text">Success</span>
              </th>
              <th class="top rotated-text-container">
                <span class="rotated-text">Failed</span>
              </th>
              <th class="top rotated-text-container">
                <span class="rotated-text">Pending</span>
              </th>
              <th class="top rotated-text-container">
                <span class="rotated-text">Ignored</span>
              </th>
            </tr>
            <tr>
              <td class="left">
                <xsl:value-of select="count(//nunit:test-suite[@type='TestFixture'])"/> features
              </td>
              <xsl:call-template name="summary-row">
                <xsl:with-param name="summary" select="$summary" />
              </xsl:call-template>
            </tr>
          </table>
          <hr />
          <h2>Feature Summary</h2>
          <table class="table table-bordered" cellpadding="0" cellspacing="0">
            <tr>
              <th class="top left" colspan="2">Feature</th>
              <th class="top" colspan="2">Success rate</th>
              <th class="top rotated-text-container">
                <span class="rotated-text">Scenarios</span>
              </th>
              <th class="top rotated-text-container">
                <span class="rotated-text">Success</span>
              </th>
              <th class="top rotated-text-container">
                <span class="rotated-text">Failed</span>
              </th>
              <th class="top rotated-text-container">
                <span class="rotated-text">Pending</span>
              </th>
              <th class="top rotated-text-container">
                <span class="rotated-text">Ignored</span>
              </th>
            </tr>
            <xsl:apply-templates select="//nunit:test-suite[@type='TestFixture']" mode="summary">
              <!--<xsl:sort select="@description" />
            <xsl:sort select="@name"/>-->
            </xsl:apply-templates>
          </table>
          <h2>Feature Execution Details</h2>
          <xsl:apply-templates select="//nunit:test-suite[@type='TestFixture']">
            <!--<xsl:sort select="@description" />
          <xsl:sort select="@name"/>-->
          </xsl:apply-templates>
        </div>

        <xsl:text disable-output-escaping="yes">
        <![CDATA[
        <script src="http://netdna.bootstrapcdn.com/bootstrap/2.3.2/js/bootstrap.js"></script>
        <script src="http://prismjs.com/prism.js"></script>
        <script src="http://prismjs.com/components/prism-gherkin.js"></script>
        <script src="http://prismjs.com/components/prism-bash.js"></script>
        ]]>
        </xsl:text>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="nunit:test-suite" mode="summary">
    <xsl:variable name="featureSummary">
      <xsl:call-template name="get-summary" />
    </xsl:variable>
    <xsl:variable name="feature-id" select="generate-id()" />
    <tr>
      <xsl:if test="position() = 1">
        <xsl:variable name="totalNumberOfTests" select="count(//nunit:test-case)" />
        <td rowspan="{$totalNumberOfTests}" class="namespace-tree-container">
          <xsl:call-template name="namespaceTree" />
        </td>
      </xsl:if>
      <td class="left">
        <div class="pull-right">
          <xsl:call-template name="get-categories"/>
        </div>
        <a href="#{$feature-id}">
          <xsl:call-template name="get-name"/>
        </a>
      </td>
      <xsl:call-template name="summary-row">
        <xsl:with-param name="summary" select="$featureSummary" />
      </xsl:call-template>
    </tr>
  </xsl:template>

  <xsl:template name="summary-row">
    <xsl:param name="summary" />
    <td>
      <xsl:call-template name="get-success-rate">
        <xsl:with-param name="summary" select="$summary" />
      </xsl:call-template>
    </td>
    <td style="width:21em">
      <xsl:call-template name="draw-bar">
        <xsl:with-param name="summary" select="$summary" />
      </xsl:call-template>
    </td>
    <td>
      <xsl:value-of select="msxsl:node-set($summary)/*/all"/>
    </td>
    <td>
      <xsl:value-of select="msxsl:node-set($summary)/*/success"/>
    </td>
    <td>
      <xsl:value-of select="msxsl:node-set($summary)/*/failure"/>
    </td>
    <td>
      <xsl:value-of select="msxsl:node-set($summary)/*/pending"/>
    </td>
    <td>
      <xsl:value-of select="msxsl:node-set($summary)/*/ignored"/>
    </td>
  </xsl:template>

  <xsl:template match="nunit:test-suite">
    <xsl:variable name="feature-id" select="generate-id()" />
    <div id="{$feature-id}" name="{$feature-id}">
      <h3>
        <xsl:call-template name="get-keyword">
          <xsl:with-param name="keyword" select="'Feature'" />
        </xsl:call-template>: <xsl:call-template name="get-name-and-path"/>
      </h3>
      <table class="table table-hover" cellpadding="0" cellspacing="0">
        <tr>
          <th class="top left">
            <xsl:call-template name="get-keyword">
              <xsl:with-param name="keyword" select="'Scenario'" />
            </xsl:call-template>
          </th>
          <th class="top" style="width: 5em">Status</th>
          <th class="top" style="width: 5em">Time(s)</th>
        </tr>
        <xsl:apply-templates select=".//nunit:test-case" />
      </table>
    </div>
  </xsl:template>

  <xsl:template match="nunit:test-case">
    <xsl:variable name="scenarioSummary">
      <xsl:call-template name="get-summary">
        <xsl:with-param name="nodes" select="." />
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="status" select="local-name(msxsl:node-set($scenarioSummary)/*/*[local-name()!='all' and text() = '1'])" />
    <xsl:variable name="scenario-id" select="generate-id()" />
    <xsl:variable name="testName" select="@name" />

    <xsl:variable name="className">
      <xsl:choose>
        <xsl:when test="$status = 'failure'">
          <xsl:text>error</xsl:text>
        </xsl:when>
        <xsl:when test="$status = 'ignored'">
          <xsl:text>warning</xsl:text>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="$status"/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>

    <tr class="{$className}">
      <td class="left">
        <xsl:call-template name="get-name"/>
        <xsl:if test="/sfr:NUnitExecutionReport/sfr:ScenarioOutput[@name = $testName]">
          <xsl:text> </xsl:text>
          <button onclick="toggle('out{$scenario-id}', event); return false;" class="btn btn-mini">
            <xsl:call-template name="get-common-tool-text">
              <xsl:with-param name="text-key" select="'Show'" />
            </xsl:call-template>
          </button>
        </xsl:if>
      </td>
      <td class="nowrap">
        <xsl:value-of select="$status"/>
        <xsl:if test="$status = 'failure'">
          <xsl:text> </xsl:text>
          <button onclick="toggle('err{$scenario-id}', event); return false;" class="btn btn-mini">
            <xsl:call-template name="get-common-tool-text">
              <xsl:with-param name="text-key" select="'Show'" />
            </xsl:call-template>
          </button>
        </xsl:if>
      </td>
      <td>
        <xsl:choose>
          <xsl:when test="@time">
            <xsl:value-of select="@time"/>
          </xsl:when>
          <xsl:otherwise>N/A</xsl:otherwise>
        </xsl:choose>
      </td>
    </tr>
    <xsl:if test="/sfr:NUnitExecutionReport/sfr:ScenarioOutput[@name = $testName]">
      <tr id="out{$scenario-id}" class="hide">
        <td class="left subRow" colspan="3">
          <div class="failurePanel">
            <pre>
              <code class="language-gherkin">
                <xsl:call-template name="scenario-output">
                  <xsl:with-param name="word" select="/sfr:NUnitExecutionReport/sfr:ScenarioOutput[@name = $testName]/sfr:Text"/>
                </xsl:call-template>
              </code>
            </pre>
          </div>
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="$status = 'failure'">
      <tr id="err{$scenario-id}" class="hide">
        <td class="left subRow" colspan="3">
          <xsl:apply-templates select="nunit:failure" />
        </td>
      </tr>
    </xsl:if>
  </xsl:template>

  <xsl:template match="nunit:failure">
    <div class="failurePanel">
      <xsl:choose>
        <xsl:when test="not(nunit:message)">N/A</xsl:when>
        <xsl:otherwise>
          <b>
            <xsl:call-template name="br-replace">
              <xsl:with-param name="word" select="nunit:message"/>
            </xsl:call-template>
          </b>
        </xsl:otherwise>
      </xsl:choose>
      <!-- display the stacktrace -->
      <pre>
        <code class="language-bash">
          <xsl:value-of select="nunit:stack-trace" disable-output-escaping="yes"/>
          <!--<xsl:call-template name="br-replace">
            <xsl:with-param name="word" select="nunit:stack-trace"/>
          </xsl:call-template>-->
        </code>
      </pre>
    </div>
  </xsl:template>

  <xsl:template name="br-replace">
    <xsl:param name="word"/>
    <xsl:choose>
      <xsl:when test="contains($word,'&#xA;')">
        <xsl:value-of select="substring-before($word,'&#xA;')"/>
        <br/>
        <xsl:call-template name="br-replace">
          <xsl:with-param name="word" select="substring-after($word,'&#xA;')"/>
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$word"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="scenario-output">
    <xsl:param name="word"/>
    <xsl:param name="traceMode" select="Normal" />
    <xsl:choose>
      <xsl:when test="contains($word,'&#xA;')">
        <xsl:variable name="line" select="substring-before($word,'&#xA;')" />
        <xsl:variable name="newTraceMode">
          <xsl:call-template name="get-scenario-trace-mode">
            <xsl:with-param name="text" select="$line" />
            <xsl:with-param name="traceMode" select="$traceMode" />
          </xsl:call-template>
        </xsl:variable>
        <xsl:call-template name="scenario-line-output">
          <xsl:with-param name="text" select="$line" />
          <xsl:with-param name="traceMode" select="$newTraceMode" />
        </xsl:call-template>
        <xsl:call-template name="scenario-output">
          <xsl:with-param name="word" select="substring-after($word,'&#xA;')"/>
          <xsl:with-param name="traceMode" select="$newTraceMode" />
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:variable name="newTraceMode">
          <xsl:call-template name="get-scenario-trace-mode">
            <xsl:with-param name="text" select="$word" />
            <xsl:with-param name="traceMode" select="$traceMode" />
          </xsl:call-template>
        </xsl:variable>
        <xsl:call-template name="scenario-line-output">
          <xsl:with-param name="text" select="$word" />
          <xsl:with-param name="traceMode" select="$newTraceMode" />
        </xsl:call-template>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="get-scenario-trace-mode">
    <xsl:param name="text" />
    <xsl:param name="traceMode" />

    <xsl:choose>
      <xsl:when test="starts-with($text, '->')">Trace</xsl:when>
      <xsl:when test="$traceMode = 'Trace' and not(starts-with($text, ' '))">Normal</xsl:when>
      <xsl:when test="$traceMode = 'Trace'">Trace</xsl:when>
      <xsl:otherwise>Normal</xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="scenario-line-output">
    <xsl:param name="text" />
    <xsl:param name="traceMode" />
    <xsl:choose>
      <xsl:when test="$traceMode = 'Trace'">
        <!--<span class="traceMessage">
          <xsl:value-of select="$text"/>
        </span>-->
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$text"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="get-summary">
    <xsl:param name="nodes" select=".//nunit:test-case" />
    <testCaseSummary>
      <all>
        <xsl:value-of select="count($nodes)"/>
      </all>
      <success>
        <xsl:value-of select="count($nodes[@executed = 'True' and @success='True'])"/>
      </success>
      <failure>
        <xsl:value-of select="count($nodes[@executed = 'True' and @success='False' and nunit:failure])"/>
      </failure>
      <pending>
        <xsl:value-of select="count($nodes[@executed = 'True' and @success='False' and not(nunit:failure)])"/>
      </pending>
      <ignored>
        <xsl:value-of select="count($nodes[@executed = 'False'])"/>
      </ignored>
    </testCaseSummary>
  </xsl:template>

  <xsl:template name="get-success-rate">
    <xsl:param name="summary" />

    <xsl:value-of select="round(msxsl:node-set($summary)/*/success * 100 div msxsl:node-set($summary)/*/all)"/>
    <xsl:text>%</xsl:text>
  </xsl:template>

  <xsl:template name="draw-bar">
    <xsl:param name="summary" />

    <xsl:variable name="testCount" select="msxsl:node-set($summary)/*/all" />
    <xsl:variable name="successWidth" select="(msxsl:node-set($summary)/*/success * 100 div $testCount)" />
    <xsl:variable name="failureWidth" select="(msxsl:node-set($summary)/*/failure * 100 div $testCount)" />
    <xsl:variable name="pendingWidth" select="(msxsl:node-set($summary)/*/pending * 100 div $testCount)" />
    <xsl:variable name="ignoredWidth" select="(msxsl:node-set($summary)/*/ignored * 100 div $testCount)" />

    <div class="progress">
      <xsl:if test="$successWidth != 0">
        <div class="bar bar-success" style="width:{$successWidth}%" title="{msxsl:node-set($summary)/*/success} succeeded">
          <xsl:text disable-output-escaping="yes"><![CDATA[&nbsp;]]></xsl:text>
        </div>
      </xsl:if>
      <xsl:if test="$failureWidth != 0">
        <div class="bar bar-danger" style="width:{$failureWidth}%" title="{msxsl:node-set($summary)/*/failure} failed">
          <xsl:text disable-output-escaping="yes"><![CDATA[&nbsp;]]></xsl:text>
        </div>
      </xsl:if>
      <xsl:if test="$pendingWidth != 0">
        <div class="bar bar-warning" style="width:{$pendingWidth}%" title="{msxsl:node-set($summary)/*/pending} pending/not bound">
          <xsl:text disable-output-escaping="yes"><![CDATA[&nbsp;]]></xsl:text>
        </div>
      </xsl:if>
      <xsl:if test="$ignoredWidth != 0">
        <div class="bar bar-info" style="width:{$ignoredWidth}%" title="{msxsl:node-set($summary)/*/ignored} ignored">
          <xsl:text disable-output-escaping="yes"><![CDATA[&nbsp;]]></xsl:text>
        </div>
      </xsl:if>
    </div>
  </xsl:template>

  <xsl:template name="get-categories">
    <xsl:for-each select="./nunit:categories/nunit:category">
      <xsl:sort select="./@name" />
      
      <span class="label">
        <xsl:value-of select="./@name"/>
      </span>&#160;
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="get-name">
    <xsl:choose>
      <xsl:when test="./@description">
        <span title="{@name}">
          <xsl:value-of select="./@description"/>
        </span>
      </xsl:when>
      <xsl:when test="../../@type='ParameterizedTest' and ../../@description">
        <span title="{@name}">
          <xsl:value-of select="../../@description"/>
          <xsl:text>(</xsl:text>
          <xsl:value-of select="substring-after(./@name,'(')"/>
        </span>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="./@name"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="get-name-and-path">
    <xsl:choose>
      <xsl:when test="./@description">
        <span class="path">
          <xsl:call-template name="printPath" />
        </span>
        <span title="{@name}">
          <xsl:value-of select="./@description"/>
        </span>
      </xsl:when>
      <xsl:when test="../../@type='ParameterizedTest' and ../../@description">
        <span title="{@name}">
          <xsl:value-of select="../../@description"/>
          <xsl:text>(</xsl:text>
          <xsl:value-of select="substring-after(./@name,'(')"/>
        </span>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="./@name"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="printPath">
    <xsl:call-template name="printParentNamespace">
      <xsl:with-param name="parentElement" select="./ancestor::nunit:test-suite[1]" />
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="printParentNamespace">
    <xsl:param name="parentElement" />

    <xsl:variable name="VALUE">
      <xsl:value-of select="count($parentElement/*/nunit:test-suite[@type='Namespace'])"/>
    </xsl:variable>

    <xsl:if test="$VALUE != 1">
      <xsl:call-template name="printParentNamespace">
        <xsl:with-param name="parentElement" select="$parentElement/ancestor::nunit:test-suite[1]" />
      </xsl:call-template>

      <xsl:variable name="partName">
        <xsl:value-of select="$parentElement/@name"/>
      </xsl:variable>

      <span class="path-partition" title="{$partName}">
        <xsl:value-of select="$partName"/>
      </span>
      »
    </xsl:if>

  </xsl:template>

  <xsl:template name="namespaceTree">
    <xsl:variable name="rootNamespace" select="//descendant::nunit:test-suite[@type='Namespace']" />
    <xsl:call-template name="namespaceTreeInner">
      <xsl:with-param name="element" select="$rootNamespace[1]"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="namespaceTreeInner">
    <xsl:param name="element" />
    <xsl:variable name="numberOfTests" select="count($element/descendant::nunit:test-suite[@type='TestFixture'])" />
    <xsl:variable name="childElements" select="$element/*/nunit:test-suite[@type='Namespace']" />

    <xsl:choose>
      <xsl:when test="count($childElements) = 1">
        <xsl:call-template name="namespaceTreeInner">
          <xsl:with-param name="element" select="$childElements[1]"/>
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <table class="namespace-tree-table" cellpadding="0" cellspacing="0">
          <tr>
            <xsl:variable name="nodeHeight" select="$numberOfTests * 37" />
            <td class="node" style="height:{$nodeHeight}px;">
              <xsl:value-of select="$element/@name"/>
            </td>

            <xsl:if test="count($childElements) > 0">
              <td>
                <xsl:for-each select="$childElements">
                  <xsl:call-template name="namespaceTreeInner">
                    <xsl:with-param name="element" select="."/>
                  </xsl:call-template>
                </xsl:for-each>
              </td>
            </xsl:if>
          </tr>
        </table>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>
