namespace my_web_api.Models
{
    public class ContactMessage
    {
        public int contactMessageId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public DateTime createdAt { get; set; }
    }

}
