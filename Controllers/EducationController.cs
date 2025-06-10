using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using my_web_api.Data;
using my_web_api.Models;

namespace my_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EducationController(ApplicationDbContext context) 
        {
            _context  = context;
        }

        [HttpGet]
        public IActionResult GetEducation()
        {
            var education = _context.Education.ToList();
            return Ok(education);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostEducation([FromBody] Education newEducation)
        {
            if (newEducation == null)
                return BadRequest("AboutMe verisi boş olamaz.");

            _context.Education.Add(newEducation);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEducation), new { id = newEducation.educationId }, newEducation);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutEducation(int id, [FromBody] Education updatedEducation)
        {
            var existingEducation = _context.Education.FirstOrDefault(a => a.educationId == id);
            if (existingEducation == null)
            {
                return NotFound($"ID'si {id} olan kayıt bulunamadı.");
            }
            existingEducation.endDate = updatedEducation.endDate;
            existingEducation.startDate = updatedEducation.startDate;
            existingEducation.description = updatedEducation.description;
            existingEducation.fieldOfStudy = updatedEducation.fieldOfStudy;
            existingEducation.schoolName = updatedEducation.schoolName;
            existingEducation.degree = updatedEducation.degree;

            _context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteEducation(int id)
        {
            var existingEducation = _context.Education.FirstOrDefault(a => a.educationId == id);

            if (existingEducation == null)
                return NotFound($"ID'si {id} olan kayıt bulunamadı.");

            _context.Education.Remove(existingEducation);
            _context.SaveChanges();

            return Ok($"ID'si {id} olan kayıt başarıyla silindi.");
        }

    }
}
