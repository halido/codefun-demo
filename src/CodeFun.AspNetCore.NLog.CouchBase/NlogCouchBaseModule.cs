using Volo.Abp.Modularity;

namespace CodeFun.AspNetCore.NLog
{
    [DependsOn(typeof(CodeFunApplicationModule))]
    public class NlogCouchBaseModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
          
        }
    }
}