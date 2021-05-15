using Volo.Abp.Modularity;

namespace CodeFun
{
    [DependsOn(
        typeof(CodeFunApplicationModule),
        typeof(CodeFunDomainTestModule)
    )]
    public class CodeFunApplicationTestModule : AbpModule
    {
    }
}