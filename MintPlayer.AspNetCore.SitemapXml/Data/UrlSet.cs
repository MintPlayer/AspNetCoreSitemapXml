using System.Collections.Generic;
using System.Xml.Serialization;

namespace MintPlayer.AspNetCore.SitemapXml
{
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class UrlSet
    {
        public UrlSet()
        {
            xmlns.Add("xhtml", "http://www.w3.org/1999/xhtml");
            xmlns.Add("video", "http://www.google.com/schemas/sitemap-video/1.1");
        }

        public UrlSet(IEnumerable<Url> urls) : this()
        {
            Urls.AddRange(urls);
        }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();

        /// <summary>List of URLs</summary>
        [XmlElement("url", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
        public List<Url> Urls { get; set; } = new List<Url>();
    }
}
