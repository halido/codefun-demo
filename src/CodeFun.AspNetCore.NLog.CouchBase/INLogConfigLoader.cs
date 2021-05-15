using System;
using System.Threading.Tasks;
using Couchbase.Query.Couchbase.N1QL;
using Volo.Abp.DependencyInjection;

namespace CodeFun.AspNetCore.NLog
{
    public interface INLogConfigLoader:ITransientDependency
    {
        Task LoadFromDbIfChangedAsync(DateTime? lastSyncTime);
    }
}