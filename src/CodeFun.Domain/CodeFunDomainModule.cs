using Volo.Abp.Modularity;

namespace CodeFun
{
    [DependsOn(
        typeof(CodeFunDomainSharedModule)
    )]
    public class CodeFunDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}