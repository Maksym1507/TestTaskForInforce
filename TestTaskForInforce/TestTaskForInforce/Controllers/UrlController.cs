using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskForInforce.Models.Requests;
using TestTaskForInforce.Models.Responses;
using TestTaskForInforce.Services.Abstractions;

namespace TestTaskForInforce.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public async Task<IActionResult> ShortUrlsTable()
        {
            return View(await _urlService.GetUrlsAsync());
        }

        public async Task<IActionResult> ShortUrlInfo(int id)
        {
            return View(await _urlService.GetShortUrlByIdAsync(id));
        }

        public async Task<ActionResult> Urls()
        {
            return Json(await _urlService.GetUrlsAsync());
        }

        [HttpGet("/{path:required}")]
        public async Task<ActionResult> RedirectTo(string path)
        {
            try
            {
                var shortUrl = await _urlService.GetByShortenUrlAsync(path);
                return Redirect(shortUrl!.BaseUrl);
            }
            catch (Exception)
            {
                return NotFound();
            }            
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddShortenedUrl([FromBody] UrlRequest request)
        {
            try
            {
                var result = await _urlService.CreateShortenedUrlAsync(request.Url, HttpContext.User.Identity!.Name!);
                return Ok(new AddItemResponse<int?>() { Id = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _urlService.DeleteAsync(id);
            return Ok(new DeleteItemResponse<bool>() { IsDeleted = result });
        }
    }
}
