using System.ComponentModel.DataAnnotations;

namespace CialExamMVC_Trial.Models
{
    public class Expert
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        [Required, MaxLength(16)]
        public string Content { get; set; }
        public IEnumerable<ExpertSocial> ExpertSocials { get; set; }
    }
}
