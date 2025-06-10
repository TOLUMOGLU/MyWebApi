using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using my_web_api.Data;
using my_web_api.Models;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace my_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AboutController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAbout()
        {
            var aboutList = _context.AboutMe.ToList();
            return Ok(aboutList);
        }

        [HttpGet("{id}")]
        public IActionResult GetAboutById(int id)
        {
            var about = _context.AboutMe.FirstOrDefault(a => a.aboutMeId == id);
            if (about == null)
                return NotFound($"ID'si {id} olan kayıt bulunamadı.");

            return Ok(about);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostAbout([FromBody] AboutMe newAbout)
        {
            if (newAbout == null)
                return BadRequest("AboutMe verisi boş olamaz.");

            _context.AboutMe.Add(newAbout);
            _context.SaveChanges();

            // CreatedAtAction ile yeni kaydın URL'si dönülür
            return CreatedAtAction(nameof(GetAboutById), new { id = newAbout.aboutMeId }, newAbout);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutAbout(int id, [FromBody] AboutMe updatedAbout)
        {
            var existingAbout = _context.AboutMe.FirstOrDefault(a => a.aboutMeId == id);
            if (existingAbout == null)
                return NotFound($"ID'si {id} olan kayıt bulunamadı.");

            existingAbout.name = updatedAbout.name;
            existingAbout.surname = updatedAbout.surname;
            existingAbout.title = updatedAbout.title;
            existingAbout.description = updatedAbout.description;
            existingAbout.profileImageUrl = updatedAbout.profileImageUrl;
            existingAbout.skills = updatedAbout.skills;

            _context.SaveChanges();
            return Ok(existingAbout);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteAbout(int id)
        {
            var existingAbout = _context.AboutMe.FirstOrDefault(a => a.aboutMeId == id);
            if (existingAbout == null)
                return NotFound($"ID'si {id} olan kayıt bulunamadı.");

            _context.AboutMe.Remove(existingAbout);
            _context.SaveChanges();

            return Ok($"ID'si {id} olan kayıt başarıyla silindi.");
        }
    }
}
