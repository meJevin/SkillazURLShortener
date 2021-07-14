using System;
using System.Collections.Generic;
using System.Text;

namespace URLShortener.Core.Config
{
    public class MongoDBSettings
    {
        public string URLsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
