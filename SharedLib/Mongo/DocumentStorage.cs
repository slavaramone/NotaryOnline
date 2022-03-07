using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using SharedLib.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharedLib.Mongo
{
	public class DocumentStorage : MongoDbStorage
    {
        protected string _collectionName;
        protected GridFSBucket _bucket;

        public DocumentStorage(IOptions<MongoDbOptions> mongoDbOptions) : base(mongoDbOptions)
        {
            _collectionName = mongoDbOptions.Value.CollectionName;
        }

        public ObjectId UploadFile(string fileName, Stream source, long clientId = 0)
        {
            var options = GetClientOptions(clientId);
            return Bucket.UploadFromStream(fileName, source, options);
        }

        public ObjectId UploadFile(string path, long clientId = 0)
        {
            var options = GetClientOptions(clientId);
			using var source = File.Open(path, FileMode.Open);
			return Bucket.UploadFromStream(path, source, options);
		}

        public void DownloadFile(string id, Stream stream)
        {
            DownloadFile(new ObjectId(id), stream);
        }

        public void DownloadFile(ObjectId id, Stream stream)
        {
            Bucket.DownloadToStream(id, stream);
        }

        public async Task DownloadFileAsync(ObjectId id, Stream stream)
        {
            await Bucket.DownloadToStreamAsync(id, stream);
        }

        public async Task<ObjectId> UploadFileAsync(string path, long clientId = 0)
        {
            var options = GetClientOptions(clientId);
			using var source = File.Open(path, FileMode.Open);
			return await Bucket.UploadFromStreamAsync(path, source, options);
		}

        public async Task<ObjectId> UploadFileAsync(string filename, Stream stream, long clientId = 0)
        {
            var options = GetClientOptions(clientId);
            return await Bucket.UploadFromStreamAsync(filename, stream, options);
        }

        protected static GridFSUploadOptions GetClientOptions(long clientId)
        {
            return new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                {
                    { "date", DateTime.UtcNow.ToFileTime() },
                    { "client_id", clientId.ToString() }
                }
            };
        }

        protected GridFSBucket Bucket
        {
            get
            {
                if (_bucket == null)
                {
                    var docs = Client.GetDatabase(DatabaseName);
                    _bucket = new GridFSBucket(docs, new GridFSBucketOptions
                    {
                        BucketName = _collectionName,
                        ChunkSizeBytes = 1048576, // 1MB
                        WriteConcern = WriteConcern.WMajority,
                        ReadPreference = ReadPreference.Secondary
                    });
                }

                return _bucket;
            }
        }
    }
}
