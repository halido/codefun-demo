using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CodeFun.LogConfig;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog.Extensions.Logging;
using NLog.Web;
using Volo.Abp.DependencyInjection;

namespace CodeFun.AspNetCore.NLog
{
    public class NLogConfigLoader:INLogConfigLoader
    {
        private readonly IBucketProvider _bucketProvider;
        private readonly CouchBaseLogging _couchBaseOptions;

        public NLogConfigLoader(IBucketProvider bucketProvider,IOptions<CouchBaseLogging> couchBaseOptions)
        {
            _bucketProvider = bucketProvider;
            _couchBaseOptions = couchBaseOptions.Value;
        }

        public async Task LoadFromDbIfChangedAsync(DateTime? lastSyncTime)
        {


            var bucket = await _bucketProvider.GetBucketAsync(_couchBaseOptions.Bucket);
            
            var settingResult = await bucket.DefaultCollection().GetAsync(_couchBaseOptions.ConfigId);

            var contentAs = settingResult.ContentAs<NLogItemEntity>();

            if (lastSyncTime.HasValue && !(lastSyncTime.Value < contentAs.LastModifiedDate))
            {
                
            }
            else
            {
                var obj = new JObject();
                obj.Add("NLog", JToken.Parse(JsonConvert.SerializeObject(contentAs)));
                var configuration = new ConfigurationBuilder()
                    .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(obj.ToString())))
                    .Build();
                var configurationSection = configuration.GetSection("NLog");
                NLogBuilder.ConfigureNLog(new NLogLoggingConfiguration(configurationSection));
            }
        }
        
        
        
    }
}