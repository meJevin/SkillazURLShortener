using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortener.Core.Config;
using URLShortener.Core.Services;
using URLShortener.Core.Services.Interfaces;

namespace URLShortener.API.Controllers
{
    /// <summary>
    /// This controller doesn't do anything, just performs an actual redirect
    /// 
    /// Views increase in the <see cref="URLController"/>
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GoController : ControllerBase
    {
        readonly IURLRepository _repo;
        readonly GUIDShortenerSettings _settings;

        public GoController(IURLRepository repository,
            IOptions<GUIDShortenerSettings> options)
        {
            _repo = repository;
            _settings = options.Value;
        }

        [HttpGet("go/{shortURL}")]
        public IActionResult Get(string shortURL)
        {
            if (shortURL == null)
            {
                return LocalRedirect("/swagger/index.html");
            }

            var result = _repo.FindByShortened(_settings.BasePath + shortURL);

            if (result == null)
            {
                return LocalRedirect("/swagger/index.html");
            }

            return Redirect(result.Original);
        }
    }
}
