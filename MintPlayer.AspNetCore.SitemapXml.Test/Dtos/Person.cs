using MintPlayer.Timestamps;
using System;

namespace MintPlayer.AspNetCore.SitemapXml.Test.Dtos
{
    public class Person : IUpdateTimestamp
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
