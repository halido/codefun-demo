using CodeFun.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CodeFun.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class CodeFunController : AbpController
    {
        protected CodeFunController()
        {
            LocalizationResource = typeof(CodeFunResource);
        }
    }
}