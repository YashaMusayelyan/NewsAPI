using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace News.DAL
{
    public class RedisService
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static RedisService()
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { "redis-19285.c299.asia-northeast1-1.gce.cloud.redislabs.com:19285" },
                KeepAlive = 10,
                AbortOnConnectFail = false,
                ConfigurationChannel = "",
                TieBreaker = "",
                ConfigCheckSeconds = 0,
                CommandMap = CommandMap.Create(new HashSet<string>
                {
                    "SUBSCRIBE", "UNSUBSCRIBE", "CLUSTER"
                }, available: false),
                Password = "lmp2M2KMtlx8eKIQheubioiJVH4VSoUH",
                AllowAdmin = true
            };

            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase GetDB => Connection.GetDatabase();
    }
}
