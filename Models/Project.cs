namespace my_web_api.Models
{
    public class Project
    {
        public int projectId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string projectUrl { get; set; } 
        public string category { get; set; }  
        public DateTime createdAt { get; set; }
    }
}
