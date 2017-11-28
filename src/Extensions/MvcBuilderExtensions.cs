using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Picnic.Options;

namespace Picnic.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder UsePicnic(this IMvcBuilder builder, string prefix = "picnic/")
        {
            builder.Services.Configure<MvcOptions>(opts => opts.Conventions.Insert(0, new PicnicPrefixAppModelConvention(prefix)));
            return builder.AddRazorOptions(x => x.FileProviders.Add(new EmbeddedFileProvider(typeof(MvcBuilderExtensions).GetTypeInfo().Assembly)));
        }
    }
}