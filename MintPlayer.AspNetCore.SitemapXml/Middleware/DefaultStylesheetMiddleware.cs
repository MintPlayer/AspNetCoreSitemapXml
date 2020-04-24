using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MintPlayer.AspNetCore.SitemapXml.Options;

namespace MintPlayer.AspNetCore.SitemapXml.Middleware
{
    internal class DefaultStylesheetMiddleware : IMiddleware
    {
        private readonly SitemapXmlOptions sitemapOptions;

        public DefaultStylesheetMiddleware(IOptions<SitemapXmlOptions> sitemapOptions)
        {
            this.sitemapOptions = sitemapOptions.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path == sitemapOptions.StylesheetUrl)
            {
                try
                {
                    context.Response.ContentType = "text/xsl; charset=UTF-8";

                    using (var stream = typeof(Sitemap).Assembly.GetManifestResourceStream("MintPlayer.AspNetCore.SitemapXml.Assets.sitemap.xsl"))
                    using (var streamreader = new System.IO.StreamReader(stream))
                    {
                        var content = await streamreader.ReadToEndAsync();
                        await context.Response.WriteAsync(content);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}
