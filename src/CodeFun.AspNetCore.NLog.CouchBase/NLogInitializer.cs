using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using CodeFun.LogConfig;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog.Web;
using Volo.Abp.DependencyInjection;

namespace CodeFun.AspNetCore.NLog
{
    public class NLogInitializer : INLogInitializer, ISingletonDependency
    {
        private readonly IBucketProvider _bucketProvider;
        private readonly IConfiguration _configuration;
        private readonly IClusterProvider _clusterProvider;
        private readonly ILogConfigAppService _logConfigAppService;
        private readonly CouchBaseLogging _setupAction;

        public NLogInitializer(IClusterProvider clusterProvider, IBucketProvider bucketProvider,
            IOptions<CouchBaseLogging> setupAction, IConfiguration configuration,
            ILogConfigAppService logConfigAppService)
        {
            _clusterProvider = clusterProvider;
            _bucketProvider = bucketProvider;
            _configuration = configuration;
            _logConfigAppService = logConfigAppService;
            _setupAction = setupAction.Value;
        }

        public async Task InitializeCouchBase()
        {
            var cluster = await _clusterProvider.GetClusterAsync();

            var bucketExist =
                (await cluster.Buckets.GetAllBucketsAsync()).ContainsKey(_setupAction.Bucket);
            IBucket bucket = null;
            if (bucketExist)
            {
                bucket = await _bucketProvider.GetBucketAsync(_setupAction.Bucket);
            }
            else
            {
                bucket = await cluster.BucketAsync(_setupAction.Bucket);
            }


            var settingExist = await bucket.DefaultCollection().ExistsAsync(_setupAction.ConfigId);
            //Seed To DB from Appsettings
            if(settingExist.Exists)
                return;

            var nLogJson = BuildJson(_configuration.GetSection("NLog"));

            await _logConfigAppService.UpsertAsync(nLogJson.ToObject<object>());
        }
        private static JToken BuildJson(IConfiguration configuration)
        {
            if (configuration is IConfigurationSection configurationSection)
            {
                if (configurationSection.Value != null)
                {
                    return JValue.CreateString(configurationSection.Value);
                }
            }

            var children = configuration.GetChildren().ToList();
            if (!children.Any())
            {
                return JValue.CreateNull();
            }

            if (children[0].Key == "0")
            {
                var result = new JArray();
                foreach (var child in children)
                {
                    result.Add(BuildJson(child));
                }

                return result;
            }
            else
            {
                var result = new JObject();
                foreach (var child in children)
                {
                    result.Add(new JProperty(child.Key, BuildJson(child)));
                }

                return result;
            }
        }
    }
}