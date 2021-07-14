using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortener.Core.Models;
using URLShortener.Core.Services.Interfaces;

namespace URLShortener.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class URLController : ControllerBase
    {
        readonly IURLRepository URLRepository;
        readonly IShortenedURLGenerator URLGenerator;

        public URLController(IURLRepository urlRepo,
            IShortenedURLGenerator urlShortener)
        {
            URLRepository = urlRepo;
            URLGenerator = urlShortener;
        }

        // Сокращене ссылки по полной
        [HttpPost]
        [Route("shorten")]
        public IActionResult Shorten(string urlToShorten)
        {
            if (urlToShorten == null)
            {
                return BadRequest("А что сокращать-то?");
            }

            if (!Uri.TryCreate(urlToShorten, UriKind.Absolute, out var res))
            {
                return BadRequest("Плохую ссылку дал :(");
            }

            var shortenedURL = URLGenerator.Generate();

            var toSave = new URL()
            {
                Original = urlToShorten,
                Shortened = shortenedURL.ToString(),
                ViewCount = 0,
                UserId = HttpContext.Session.GetString(Constants.SessionUserNameKey),
            };

            URLRepository.Save(toSave);

            return Ok(toSave.Shortened);
        }

        // получение оригинала по сокращенной, с увеличением счетчика посещений

        // честно я не совсем понял, почему в запросе на оригинал с бэка мы должны поднимать кол-во просмотров
        // но как было написано в тг так и сделал. настоящий редирект осуществляется по другому эндпоинту в GoController.cs
        [HttpGet]
        [Route("original")]
        public IActionResult Original(string shortenedUrl)
        {
            if (shortenedUrl == null)
            {
                return BadRequest("Не указана сокращенная ссылка");
            }

            var dbURL = URLRepository.FindByShortened(shortenedUrl);

            if (dbURL == null)
            {
                return NotFound("Такая ссылка не существует :(");
            }

            ++dbURL.ViewCount;

            URLRepository.Update(dbURL);

            return Ok(dbURL.Original);
        }

        // получение списка всех сокращенных ссылок с количеством переходов
        [HttpGet]
        [Route("all")]
        public IActionResult All()
        {
            var result = URLRepository
                .GetAll().Select(url =>
            new 
            { 
                Shortened = url.Shortened,
                ViewCount = url.ViewCount,
            });

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        // Опциональные усложнение: показывать ему только его ссылки
        [HttpGet]
        [Route("my")]
        public IActionResult AllForCurrentUser()
        {
            var result = URLRepository
                .GetAllByUserId(HttpContext.Session.GetString(Constants.SessionUserNameKey))
                .Select(url =>
            new
            {
                Shortened = url.Shortened,
                ViewCount = url.ViewCount,
            });

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}
