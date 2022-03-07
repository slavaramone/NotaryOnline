using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SharedLib.Options;
using System;
using System.Collections.Generic;

namespace SharedLib.Mongo
{
    public abstract class MongoDbStorage : IDisposable
    {
        private readonly string _dbName;

        public string DatabaseName
        {
            get
            {
                return _dbName;
            }
        }

        protected MongoClient Client { get; private set; }

        public MongoDbStorage(IOptions<MongoDbOptions> options)
        {
            _dbName = options.Value.DatabaseName;

            var servers = new List<MongoServerAddress>() { new MongoServerAddress(options.Value.Host, options.Value.Port) };
            var credential = MongoCredential.CreateCredential(options.Value.DatabaseName, options.Value.User, options.Value.Password);
            var mongoClientSettings = new MongoClientSettings()
            {
                DirectConnection = true,
                Credential = credential,
                Servers = servers.ToArray(),
                ApplicationName = "NameOfApp",
            };

            Client = new MongoClient(mongoClientSettings);
        }

        public void Dispose()
        {   
            GC.SuppressFinalize(this);
        }
    }
}
