using System;
using System.Collections.Generic;
using System.Text;
using URLShortener.Core.Models;

namespace URLShortener.Core.Services.Interfaces
{
    public interface IURLRepository
    {
        URL Save(URL toSave);
        URL Update(URL toUpdate);
        IEnumerable<URL> GetAll();
        URL FindByShortened(string shortened);
    }
}
