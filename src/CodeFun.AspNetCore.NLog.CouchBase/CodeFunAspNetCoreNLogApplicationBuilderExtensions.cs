using CodeFun.LogConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nito.AsyncEx;

namespace CodeFun.AspNetCore.NLog
{
    public static class CodeFunAspNetCoreNLogApplicationBuilderExtensions
    {


        public static IApplicationBuilder UseCouchBaseLoggingConfiguration(
            this IApplicationBuilder app,
            IOptions<CouchBaseLogging> setupAction = null)
        {
            var nlogInitializer = app.ApplicationServices.GetRequiredService<INLogInitializer>();
            AsyncContext.Run(() => nlogInitializer.InitializeCouchBase());
            return app;
        }

        
    }
}