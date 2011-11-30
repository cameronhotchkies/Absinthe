<?xml version="1.0" encoding="ISO-8859-1" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">

		<xsl:for-each select="Absinthedata">
		<absinthedata version="1.4">
			<xsl:text>&#10;</xsl:text>
			<xsl:for-each select="target">
			<target address="{@address}" method="{@method}" 
			ssl="{concat(translate(substring(@ssl,1,1), 'TF', 'tf'), substring(@ssl, 2, string-length(@ssl)))}" 
			terminatequery="{concat(translate(substring(@TerminateQuery,1,1), 'TF', 'tf'), substring(@TerminateQuery, 2, string-length(@TerminateQuery)))}" 
			throttle="{@Throttle}" delimiter="{@Delimiter}" 
			tolerance="{@tolerance}" 
			blind="{concat(translate(substring(@blind,1,1), 'TF', 'tf'), substring(@blind, 2, string-length(@blind)))}" 
			plugin="{@PluginName}"><xsl:text>&#10;</xsl:text>
				<xsl:for-each select="authentication">
					<authentication authtype="{@authtype}" username="{@username}" password="{@password}" domain="{@domain}" />
				</xsl:for-each>
				<xsl:for-each select="parameter">
					<parameter name="{@name}" value="{@value}" 
					injectable="{concat(translate(substring(@injectable,1,1), 'TF', 'tf'), substring(@injectable, 2, string-length(@injectable)))}" 
					/>
					<xsl:text>&#10;</xsl:text>
				</xsl:for-each>
				<xsl:for-each select="cookie">
					<cookie name="{@name}" value="{@value}" /><xsl:text>&#10;</xsl:text>
				</xsl:for-each>
				
				<xsl:for-each select="../attackvector">
					<attackvector name="{@name}" buffer="{@buffer}" type="{@type}" 
					injectasstring="{concat(translate(substring(@InjectAsString,1,1), 'TF', 'tf'), substring(@InjectAsString, 2, string-length(@InjectAsString)))}"
					postbuffer="{@PostBuffer}"><xsl:text>&#10;</xsl:text>
						<truepage><xsl:text>&#10;</xsl:text>
						<xsl:for-each select="truepage/signature-item">
							<signature-item>
								<xsl:value-of select="."/>
							</signature-item><xsl:text>&#10;</xsl:text>
						</xsl:for-each>
						</truepage><xsl:text>&#10;</xsl:text>
						<falsepage><xsl:text>&#10;</xsl:text>
						<xsl:for-each select="falsepage/signature-item">
							<signature-item>
								<xsl:value-of select="."/>
							</signature-item><xsl:text>&#10;</xsl:text>
						</xsl:for-each>
						</falsepage><xsl:text>&#10;</xsl:text>
						<truefilter><xsl:text>&#10;</xsl:text>
							<xsl:for-each select="truefilter/filter-item">
								<filter-item>
									<xsl:value-of select="."/>
								</filter-item><xsl:text>&#10;</xsl:text>
							</xsl:for-each>
						</truefilter><xsl:text>&#10;</xsl:text>
						<falsefilter><xsl:text>&#10;</xsl:text>
							<xsl:for-each select="falsefilter/filter-item">
								<filter-item>
									<xsl:value-of select="."/>
								</filter-item><xsl:text>&#10;</xsl:text>
							</xsl:for-each>
						</falsefilter><xsl:text>&#10;</xsl:text>

					</attackvector><xsl:text>&#10;</xsl:text>
				</xsl:for-each>				
			</target><xsl:text>&#10;</xsl:text>
			</xsl:for-each>

			<xsl:for-each select="DatabaseSchema">
				<databaseschema username="{@username}">
					<xsl:for-each select="table">
						<table id="{@id}" name="{@name}" recordcount="{@recordcount}">
							<xsl:for-each select="field">
								<field id="{@id}" name="{@name}" datatype="{@datatype}"
									primary="{concat(translate(substring(@primary,1,1), 'TF', 'tf'), substring(@primary, 2, string-length(@primary)))}" />
							</xsl:for-each>
						</table>
					</xsl:for-each>
				</databaseschema>
			</xsl:for-each>
		</absinthedata>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>

