namespace CialExamMVC_Trial.Models
{
    public class Social
    {
        public int Id { get; set; }
        public string IconUrl { get; set; }
        public string SocialUrl { get; set; }
        public IEnumerable<ExpertSocial> ExpertSocials { get; set; }
    }
}
