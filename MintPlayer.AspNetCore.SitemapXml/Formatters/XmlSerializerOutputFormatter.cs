using System;
using System.IO;
using System.Xml;

namespace MintPlayer.AspNetCore.SitemapXml.Formatters
{
    /// <summary>This formatter adds an XML stylesheet reference to each application/xml response</summary>
    public class XmlSerializerOutputFormatter : Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter
    {
        private string stylesheetUrl;
        public XmlSerializerOutputFormatter(string stylesheetUrl)
        {
            this.stylesheetUrl = stylesheetUrl;
            WriterSettings.OmitXmlDeclaration = false;
            SupportedMediaTypes.Clear(); SupportedMediaTypes.Add("text/xml");
        }

        public override XmlWriter CreateXmlWriter(TextWriter writer, XmlWriterSettings xmlWriterSettings)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (xmlWriterSettings == null)
            {
                throw new ArgumentNullException(nameof(xmlWriterSettings));
            }

            // We always close the TextWriter, so the XmlWriter shouldn't.
            xmlWriterSettings.CloseOutput = false;

            var xmlWriter = XmlWriter.Create(writer, xmlWriterSettings);
            if (stylesheetUrl != string.Empty)
                xmlWriter.WriteProcessingInstruction("xml-stylesheet", $@"type=""text/xsl"" href=""{stylesheetUrl}""");
            return xmlWriter;
        }
    }
}
