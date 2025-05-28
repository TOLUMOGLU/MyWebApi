using Microsoft.AspNetCore.Mvc;
using my_web_api.Data;
using my_web_api.Models;

namespace my_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AboutController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAbout()
        {
            var about = _context.AboutMe.ToList();
            //var json = System.Text.Json.JsonSerializer.Serialize(about);
            return Ok(about);
        }

        [HttpPost]
        public IActionResult PostAbout([FromBody] AboutMe newAbout)
        {
            if (newAbout == null)
                return BadRequest("AboutMe verisi boş olamaz.");

            _context.AboutMe.Add(newAbout);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAbout), new { id = newAbout.aboutMeId }, newAbout);
        }

        [HttpPut]
        public IActionResult PutAbout([FromBody] AboutMe updatedAbout, int id)
        {
            var existingAbout = _context.AboutMe.FirstOrDefault(a => a.aboutMeId == id);
            if(existingAbout == null)
            {
                return NotFound($"ID'si {id} olan kayıt bulunamadı.");
            }
            existingAbout.fullName = updatedAbout.fullName;
            existingAbout.title = updatedAbout.title;
            existingAbout.description = updatedAbout.description;
            existingAbout.profileImageUrl = updatedAbout.profileImageUrl;
            existingAbout.location = updatedAbout.location;
            existingAbout.email = updatedAbout.email;
            existingAbout.skills = updatedAbout.skills;

            _context.SaveChanges();
            return Ok(existingAbout);

        }

        [HttpDelete]
        public IActionResult DeleteAbout(int id) 
        {
            var existingAbout = _context.AboutMe.FirstOrDefault(a => a.aboutMeId == id);
            if (existingAbout == null)
            {
                return NotFound($"ID'si {id} olan kayıt bulunamadı.");
            }
            _context.AboutMe.Remove(existingAbout);
            _context.SaveChanges();

            return Ok($"ID'si {id} olan kayıt başarıyla silindi.");

        }
    }
}
