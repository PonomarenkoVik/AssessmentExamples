<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
  version="1.0"
  xmlns="http://schemas.microsoft.com/wix/2006/wi"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:wix="http://schemas.microsoft.com/wix/2006/wi"
  xmlns:str="http://xsltsl.org/string"
  exclude-result-prefixes="wix str"
>

  <xsl:output
    encoding="utf-8"
    method="xml"
    version="1.0"
    indent="yes"
  />

  <!-- add shortcut to desktop for Demo.exe -->
  <xsl:template match="wix:Component[ substring( wix:File/@Source, string-length( wix:File/@Source ) - 12) = 'EngineApp.exe' ]">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
      <Shortcut
        Id="ProgExeShortcut"
        Name="EngineApp"
        Icon="EngineApp.exe"
        Directory="DesktopFolder"
        Advertise="yes">
        <xsl:attribute name="WorkingDirectory">
          <xsl:value-of select="@Directory"/>
        </xsl:attribute>
      </Shortcut>
      <RemoveFolder
        Id="ProgExeShortcut_ProgramMenuFolder_ProgVendor"
        Directory="DesktopFolder"
        On="uninstall" />
    </xsl:copy>
  </xsl:template>

  <!-- skip .pdb files -->
  <xsl:template match="wix:Component[ substring( wix:File/@Source, string-length( wix:File/@Source ) - 3 ) = '.pdb' ]">
  </xsl:template>

  <!-- skip .xml files --><!--
  <xsl:template match="wix:Component[ substring( wix:File/@Source, string-length( wix:File/@Source ) - 3 ) = '.xml' ]">
  </xsl:template>

  --><!-- skip .json files --><!--
  <xsl:template match="wix:Component[ substring( wix:File/@Source, string-length( wix:File/@Source ) - 4 ) = '.json' ]">
  </xsl:template>-->

  <!-- copy all other files -->
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match='/'>
    <xsl:comment>*** DO NOT EDIT: Generated by heat.exe; transformed by DesktopShortcut.xsl</xsl:comment>
    <xsl:apply-templates select="@*|node()"/>
  </xsl:template>

</xsl:stylesheet>