using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MintPlayer.AspNetCore.SitemapXml.DependencyInjection.Interfaces;

namespace MintPlayer.AspNetCore.SitemapXml.Test.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class SitemapController : Controller
    {
        private readonly ISitemapXml sitemapXml;
        private List<Dtos.Person> people = new List<Dtos.Person>();
        public SitemapController(ISitemapXml sitemapXml)
        {
            this.sitemapXml = sitemapXml;

            var firstNames = new[] { "James", "John", "Robert", "Michael", "William", "David", "Richard", "Charles", "Joseph", "Thomas", "Christopher", "Daniel", "Paul", "Mark", "Donald", "George", "Kenneth", "Steven", "Edward", "Brian", "Ronald", "Anthony", "Kevin", "Jason", "Jeff" };
            var lastNames = new[] { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young" };
            var random = new Random();

            var randomDate = new Func<DateTime>(() =>
            {
                var start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;
                return start.AddDays(random.Next(range));
            });

            for (int i = 0; i < 500; i++)
            {
                people.Add(new Dtos.Person
                {
                    Id = i,
                    FirstName = firstNames[random.Next(firstNames.Length)],
                    LastName = lastNames[random.Next(lastNames.Length)],
                    DateUpdate = randomDate()
                });
            }
        }

        [Produces("text/xml")]
        [HttpGet(Name = "web-sitemap-index")]
        public SitemapIndex Index()
        {
            const int per_page = 100;

            var person_urls = sitemapXml.GetSitemapIndex(people, per_page, (perPage, page) => Url.RouteUrl("web-sitemap-sitemap", new { subject = "person", count = perPage, page }, Request.Scheme));

            return new SitemapIndex(person_urls);
        }

        [Produces("text/xml")]
        [HttpGet("{subject}/{count}/{page}", Name = "web-sitemap-sitemap")]
        public IActionResult Sitemap(string subject, int count, int page)
        {
            var people_page = people.Skip((page - 1) * count).Take(count);

            return Ok(new UrlSet(people.Select(p => {
                var url = new Url
                {
                    Loc = $"{Request.Scheme}://{Request.Host}/{subject}/{p.Id}",
                    ChangeFreq = SitemapXml.Enums.ChangeFreq.Monthly,
                    LastMod = p.DateUpdate,
                };
                url.Links.Add(new Link
                {
                    Rel = "alternate",
                    HrefLang = "nl",
                    Href = $"{Request.Scheme}://{Request.Host}/{subject}/{p.Id}?lang=nl"
                });
                url.Links.Add(new Link
                {
                    Rel = "alternate",
                    HrefLang = "fr",
                    Href = $"{Request.Scheme}://{Request.Host}/{subject}/{p.Id}?lang=fr"
                });
                return url;
            })));
        }
    }
}