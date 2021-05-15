using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace CodeFun
{
    [DependsOn(
        typeof(CodeFunDomainModule),
        typeof(CodeFunApplicationContractsModule),
        typeof(AbpDddApplicationModule)
    )]
    public class CodeFunApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
            
        }
    }
}