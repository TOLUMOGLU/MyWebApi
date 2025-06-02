using System.ComponentModel.DataAnnotations;

namespace my_web_api.Models
{
    public class Skillset
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }
        public string SkillsetUrl { get; set; }
        public string Category { get; set; }  


    }
}
