using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CodeFun
{
    [Dependency(ReplaceServices = true)]
    public class CodeFunBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "CodeFun";
    }
}