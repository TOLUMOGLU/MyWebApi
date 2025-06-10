using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_web_api.Models.Dto;

namespace my_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [Authorize]
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage()
        {
            var contentType = Request.ContentType;

            if (contentType != null && contentType.Contains("application/json"))
            {
                var dto = await JsonSerializer.DeserializeAsync<Base64ImageDto>(Request.Body);

                if (dto == null || string.IsNullOrEmpty(dto.Base64Image))
                    return BadRequest("Base64 görsel verisi yok.");

                var match = Regex.Match(dto.Base64Image, @"data:image/(?<type>.+?);base64,(?<data>.+)");
                if (!match.Success)
                    return BadRequest("Base64 formatı geçersiz.");

                var base64Data = match.Groups["data"].Value;
                byte[] imageBytes;
                try
                {
                    imageBytes = Convert.FromBase64String(base64Data);
                }
                catch
                {
                    return BadRequest("Base64 string dönüştürülemedi.");
                }

                var ext = match.Groups["type"].Value;
                var fileName = $"upload_{DateTime.Now.Ticks}.{ext}";
                var filePath = Path.Combine("wwwroot", "uploads", fileName);

                var folder = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                var imageUrl = $"/uploads/{fileName}";
                return Ok(new { imageUrl });
            }
            else if (contentType != null && contentType.Contains("multipart/form-data"))
            {
                var files = Request.Form.Files;
                if (files.Count == 0)
                    return BadRequest("Dosya gelmedi.");

                var file = files[0];

                var ext = Path.GetExtension(file.FileName).TrimStart('.');
                var fileName = $"upload_{DateTime.Now.Ticks}.{ext}";
                var filePath = Path.Combine("wwwroot", "uploads", fileName);

                var folder = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var imageUrl = $"/uploads/{fileName}";
                return Ok(new { imageUrl });
            }

            return BadRequest("Desteklenmeyen içerik türü.");
        }

    }
}
