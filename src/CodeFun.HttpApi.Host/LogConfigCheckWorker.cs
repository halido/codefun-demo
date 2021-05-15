using System;
using System.Threading;
using System.Threading.Tasks;
using CodeFun.AspNetCore.NLog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

namespace CodeFun
{
   public class LogConfigCheckWorker : AsyncPeriodicBackgroundWorkerBase
    {
        private readonly INLogConfigLoader _configLoader;
        private DateTime? _lastSyncTime = null;


        public LogConfigCheckWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory,INLogConfigLoader configLoader) : base(timer, serviceScopeFactory)
        {
            _configLoader = configLoader;
            Timer.Period = 5000; //5 sec
        }

        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            Logger.LogWarning("Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext");
            await _configLoader.LoadFromDbIfChangedAsync(_lastSyncTime);
            _lastSyncTime = DateTime.UtcNow;
        }
    }
}