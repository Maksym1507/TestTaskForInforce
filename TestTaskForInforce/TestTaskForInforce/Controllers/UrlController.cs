﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> AddShortenedUrl(UrlRequest request)
        {
            try
            {
                var result = await _urlService.CreateShortenedUrlAsync(request.Url);
                return Ok(new AddItemResponse<int?>() { Id = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
