using System;
using System.Collections.Generic;
using System.Text;

namespace URLShortener.Core.Config
{
    public class GUIDShortenerSettings
    {
        /// <summary>
        /// Base path when generating a shortened URL
        /// In form of:
        ///     https://localhost:5000/go/
        /// </summary>
        public string BasePath { get; set; }
    }
}
