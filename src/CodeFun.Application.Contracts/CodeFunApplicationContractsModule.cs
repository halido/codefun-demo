using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace CodeFun
{
    [DependsOn(
        typeof(CodeFunDomainSharedModule),
        typeof(AbpObjectExtendingModule)
    )]
    public class CodeFunApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            CodeFunDtoExtensions.Configure();
        }
    }
}