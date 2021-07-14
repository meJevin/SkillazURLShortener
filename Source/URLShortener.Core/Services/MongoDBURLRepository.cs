using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using URLShortener.Core.Config;
using URLShortener.Core.Models;
using URLShortener.Core.Services.Interfaces;

namespace URLShortener.Core.Services
{
    public class MongoDBURLRepository : IURLRepository
    {
        readonly IMongoCollection<URL> _urls;

        public MongoDBURLRepository(IOptions<MongoDBSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _urls = database.GetCollection<URL>(settings.URLsCollectionName);
        }

        public IEnumerable<URL> GetAll()
        {
            return _urls.Find(url => true).ToList();
        }

        public URL FindByShortened(string shortened)
        {
            return _urls.Find(url => url.Shortened == shortened).FirstOrDefault();
        }

        public URL Save(URL toSave)
        {
            _urls.InsertOne(toSave);

            return toSave;
        }

        public URL Update(URL toUpdate)
        {
            _urls.ReplaceOne(b => b.Id == toUpdate.Id, toUpdate);

            return toUpdate;
        }

        public IEnumerable<URL> GetAllByUserId(string userId)
        {
            return _urls.Find(url => url.UserId == userId).ToList();
        }
    }
}
