using Microsoft.AspNetCore.Mvc;
using MusicSchool.Models;
using MusicSchool.Services;

namespace MusicSchool.Controllers
{
    [ApiController]
    [Route("api/gallery")]
    public class GalleryController : ControllerBase
    {
        private readonly BlobService _blobService;
        private readonly TableService _tableService;

        public GalleryController(BlobService blobService, TableService tableService)
        {
            _blobService = blobService;
            _tableService = tableService;
        }

        // 🔹 Upload Image
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            IFormFile image,
            string title,
            string description,
            int sortOrder)
        {
            var imageUrl = await _blobService.UploadAsync(image);

            var entity = new GalleryEntity
            {
                RowKey = Guid.NewGuid().ToString(),
                ImageUrl = imageUrl,
                Title = title,
                Description = description,
                SortOrder = sortOrder,
                IsActive = true,
                CreatedOn = DateTime.UtcNow
            };

            _tableService.Add(entity);
            return Ok(entity);
        }

        // 🔹 Get Gallery
        [HttpGet]
        public IActionResult GetGallery()
        {
            return Ok(_tableService.GetAll().OrderBy(x => x.SortOrder));
        }

        // 🔹 Delete Image
        [HttpDelete("{rowKey}")]
        public async Task<IActionResult> Delete(string rowKey, string imageUrl)
        {
            await _blobService.DeleteAsync(imageUrl);
            _tableService.Delete(rowKey);
            return Ok();
        }
    }
}
