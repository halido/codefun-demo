using System.Threading.Tasks;

namespace CodeFun.AspNetCore.NLog
{
    public interface INLogInitializer
    {
        Task InitializeCouchBase();
    }
}