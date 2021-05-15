using CodeFun.Localization;
using Volo.Abp.Application.Services;

namespace CodeFun
{
    /* Inherit your application services from this class.
     */
    public abstract class CodeFunAppService : ApplicationService
    {
        protected CodeFunAppService()
        {
            LocalizationResource = typeof(CodeFunResource);
        }
    }
}