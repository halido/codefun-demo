using CodeFun.AspNetCore.NLog;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace CodeFun
{
    [DependsOn(
        typeof(CodeFunApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(NlogCouchBaseModule)
        
    )]
    public class CodeFunHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}