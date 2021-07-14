using System;
using System.Collections.Generic;
using System.Text;

namespace URLShortener.Core.Services.Interfaces
{
    public interface IShortenedURLGenerator
    {
        Uri Generate();
    }
}
