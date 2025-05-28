namespace my_web_api.Models
{
    public class Education
    {
        public int educationId { get; set; }
        public string schoolName { get; set; }
        public string degree { get; set; }  
        public string fieldOfStudy { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }  
        public string description { get; set; }
    }
}
