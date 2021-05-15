using System;
using System.Threading.Tasks;
using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace CodeFun.LogConfig
{
    [RemoteService(false, IsMetadataEnabled = false)]
    public class LogConfigAppService : CodeFunAppService, ILogConfigAppService
    {
        private readonly CouchBaseLogging _options;
        private readonly IBucketProvider _provider;

        public LogConfigAppService(IBucketProvider provider, IOptions<CouchBaseLogging> options)
        {
            _provider = provider;
            _options = options.Value;
        }

        public async Task<NLogItemEntity> UpsertAsync(object item)
        {
            var bucket = await _provider.GetBucketAsync(_options.Bucket);
            NLogItemEntity nLogItemEntity;
            try
            {
                var configContent = await CollectionExtensions.GetAsync(bucket.DefaultCollection(), _options.ConfigId);
                nLogItemEntity = configContent.ContentAs<NLogItemEntity>();
            }
            catch (DocumentNotFoundException e)
            {
                nLogItemEntity = new NLogItemEntity();
                nLogItemEntity.CreationDate = DateTime.UtcNow;
            }

            nLogItemEntity.LastModifiedDate = DateTime.Now;
            nLogItemEntity.Config = item;

            await bucket.DefaultCollection().UpsertAsync(_options.ConfigId, nLogItemEntity);
            return nLogItemEntity;
        }
    }

    public interface ILogConfigAppService : IApplicationService
    {
        Task<NLogItemEntity> UpsertAsync(object item);
    }
}