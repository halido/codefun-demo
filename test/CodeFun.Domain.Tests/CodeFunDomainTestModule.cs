using Volo.Abp;
using Volo.Abp.Modularity;

namespace CodeFun
{
    [DependsOn(
        typeof(AbpTestBaseModule)
    )]
    public class CodeFunDomainTestModule : AbpModule
    {
    }
}