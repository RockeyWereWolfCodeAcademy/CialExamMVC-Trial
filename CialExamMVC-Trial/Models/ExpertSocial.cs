namespace CialExamMVC_Trial.Models
{
    public class ExpertSocial
    {
        public int Id { get; set; }
        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
        public int SocialId { get; set; }
        public Social Social { get; set; }
    }
}
