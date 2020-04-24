using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MintPlayer.AspNetCore.SitemapXml.Options;

namespace MintPlayer.AspNetCore.SitemapXml
{
    public static class SitemapXmlExtensions
    {
        public static IServiceCollection AddSitemapXml(this IServiceCollection services)
        {
            return services
                .AddScoped<DependencyInjection.Interfaces.ISitemapXml, DependencyInjection.SitemapXml>()
                .AddTransient<Middleware.DefaultStylesheetMiddleware>();
        }

        /// <summary>
        /// Adds an OutputFormatter for Content-Type application/xml.
        /// Either place a XML stylesheet in the webroot, or call the UseDefaultSitemapXmlStylesheet() middleware.
        /// </summary>
        public static IMvcBuilder AddSitemapXmlFormatters(this IMvcBuilder mvc, Action<SitemapXmlOptions> options)
        {
            var opt = new SitemapXmlOptions();
            options(opt);

            return mvc.AddMvcOptions(mvc_options =>
            {
                if (!string.IsNullOrEmpty(opt.StylesheetUrl))
                    mvc_options.OutputFormatters.Insert(0, new Formatters.XmlSerializerOutputFormatter(opt.StylesheetUrl));
            });
        }

        /// <summary>Hosts a template XML stylesheet on the specified URL</summary>
        public static IApplicationBuilder UseDefaultSitemapXmlStylesheet(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Middleware.DefaultStylesheetMiddleware>();
        }
    }
}
