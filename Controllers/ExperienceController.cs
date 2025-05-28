using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_web_api.Data;
using my_web_api.Models;

namespace my_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public ExperienceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetExperience()
        {
            var experience = _context.Experience.ToList();
            return Ok(experience);
        }

        [HttpPost]
        public IActionResult PostExperience([FromBody] Experience newExperience)
        {
            if (newExperience == null)
                return BadRequest("AboutMe verisi boş olamaz.");

            _context.Experience.Add(newExperience);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetExperience), new { id = newExperience.experienceId }, newExperience);
        }

        [HttpPut]
        public IActionResult PutExperience(int id, [FromBody] Experience updatedExperience)
        {
            var existingExperience = _context.Experience.FirstOrDefault(a => a.experienceId == id);
            if (existingExperience == null)
            {
                return NotFound($"ID'si {id} olan kayıt bulunamadı.");
            }
            existingExperience.startDate = updatedExperience.startDate;
            existingExperience.endDate = updatedExperience.endDate;
            existingExperience.description = updatedExperience.description;
            existingExperience.location = updatedExperience.location;
            existingExperience.jobTitle = updatedExperience.jobTitle;
            existingExperience.companyName = updatedExperience.companyName;

            _context.Experience.Add(existingExperience);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteExperience(int id)
        {
            var existingExperience = _context.Experience.FirstOrDefault(a => a.experienceId.Equals(id));

            if (existingExperience == null)
                return NotFound($"ID'si {id} olan kayıt bulunamadı.");

            _context.Experience.Remove(existingExperience);
            _context.SaveChanges();

            return Ok($"ID'si {id} olan kayıt başarıyla silindi.");
        }
    }
}
