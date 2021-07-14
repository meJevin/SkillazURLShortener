using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using URLShortener.Core.Config;
using URLShortener.Core.Services.Interfaces;

namespace URLShortener.Core.Services
{
    public class GUIDShortenedURLGenerator : IShortenedURLGenerator
    {
        readonly GUIDShortenerSettings _settings;

        public GUIDShortenedURLGenerator(IOptions<GUIDShortenerSettings> options)
        {
            _settings = options.Value;
        }

        public Uri Generate()
        {
            return new Uri(_settings.BasePath + Guid.NewGuid().ToString().Split('-')[0]);
        }
    }
}
