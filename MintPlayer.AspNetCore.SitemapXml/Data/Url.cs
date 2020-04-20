using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using MintPlayer.AspNetCore.SitemapXml.Data;
using MintPlayer.AspNetCore.SitemapXml.Enums;

namespace MintPlayer.AspNetCore.SitemapXml
{
    [XmlRoot("url")]
    public class Url
    {
        /// <summary>URL of the resource</summary>
        [XmlElement("loc")]
        public string Loc { get; set; }

        /// <summary>Last modification of the resource</summary>
        [XmlElement("lastmod")]
        public DateTime LastMod { get; set; }

        /// <summary>Change frequency of the resource</summary>
        [XmlElement("changefreq")]
        public ChangeFreq ChangeFreq { get; set; }

        /// <summary>List of alternate links</summary>
        [XmlElement("link", Namespace = "http://www.w3.org/1999/xhtml")]
        public List<Link> Links { get; set; } = new List<Link>();

        /// <summary>List of URLs</summary>
        [XmlElement("video", Namespace = "http://www.google.com/schemas/sitemap-video/1.1")]
        public List<Video> Videos { get; set; } = new List<Video>();
    }
}
