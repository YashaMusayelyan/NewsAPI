using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News.DAL.Entities;
using NewsAPI.Entities;
using NewsAPI.Entities.Models;
using NewsAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;

        public readonly NewsService _newsService;
        private readonly AccountService _accountService;

        public NewsController(ILogger<NewsController> logger, NewsService newssService, AccountService accountService)
        {
            _logger = logger;
            _newsService = newssService;
            _accountService = accountService;
        }

        [HttpGet()]
        public async Task<ActionResult<List<NewsEntity>>> GetAll()
        {
            return await _newsService.GetAll();
        }

        [HttpGet("{newsId}")]
        public async Task<ActionResult<NewsEntity>> Get(long newsId)
        {
            return await _newsService.Get(newsId);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AddInputNewsModel news)
        {
            var userId = _accountService.GetUserId(HttpContext);

            await _newsService.Add(userId, news);

            return NoContent();
        }

        [HttpPut("{newsId}")]
        public async Task<ActionResult> Put(long newsId, [FromBody] UpdateInputNewsModel news)
        {
            var userId = _accountService.GetUserId(HttpContext);
            await _newsService.Update(userId, newsId, news);

            return NoContent();
        }

        [HttpDelete("{newsId}")]
        public async Task<ActionResult> Delete(long newsId)
        {
            var userId = _accountService.GetUserId(HttpContext);

            await _newsService.Delete(userId, newsId);

            return NoContent();
        }
    }
}
