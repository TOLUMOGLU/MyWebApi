namespace my_web_api.Models
{
    public class AboutMe
    {
        public int aboutMeId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string title { get; set; }  
        public string description { get; set; }  
        public string profileImageUrl { get; set; }  
        public List<string> skills { get; set; }  
    }

}
