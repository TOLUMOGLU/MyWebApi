namespace my_web_api.Models
{
    public class Experience
    {  
        public int experienceId { get; set; }
        public string companyName { get; set; }
        public string jobTitle { get; set; }
        public string location { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string description { get; set; }
        public List<string>? technologiesUsed { get; set; }  
    }

}
