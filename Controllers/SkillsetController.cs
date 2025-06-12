using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_web_api.Data;
using my_web_api.Models;
using Microsoft.AspNetCore.Authorization;

namespace my_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SkillsetController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skillset>>> GetSkillset()
        {
            var skillsets = await _context.Skillsets.ToListAsync();
            if (skillsets == null || !skillsets.Any())
            {
                return NotFound();
            }
            return Ok(skillsets);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Skillset>> CreateSkillset(Skillset newSkillset)
        {
            _context.Skillsets.Add(newSkillset);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkillset), new { id = newSkillset.Id }, newSkillset);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkillset(int id, Skillset updatedSkillset)
        {
            var existing = await _context.Skillsets.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Title = updatedSkillset.Title;
            existing.Subtitle = updatedSkillset.Subtitle;
            existing.Description = updatedSkillset.Description;
            existing.SkillsetUrl = updatedSkillset.SkillsetUrl;
            existing.Category = updatedSkillset.Category;

            _context.Skillsets.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkillset(int id)
        {
            var skillset = await _context.Skillsets.FindAsync(id);
            if (skillset == null)
            {
                return NotFound();
            }

            _context.Skillsets.Remove(skillset);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
