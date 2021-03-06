﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MintPlayer.AspNetCore.SitemapXml.Enums
{
    public enum ChangeFreq
    {
        [XmlEnum("hourly")]
        Hourly,
        [XmlEnum("daily")]
        Daily,
        [XmlEnum("monthly")]
        Monthly,
        [XmlEnum("yearly")]
        Yearly
    }
}
