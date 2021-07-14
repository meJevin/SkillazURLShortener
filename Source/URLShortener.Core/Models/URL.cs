using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace URLShortener.Core.Models
{
    /// <summary>
    /// Модель описывающая ссылку в БД
    /// </summary>
    public class URL
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Shortened { get; set; }
        public string Original { get; set; }
        public int ViewCount { get; set; }
        public string UserId { get; set; }
    }
}
