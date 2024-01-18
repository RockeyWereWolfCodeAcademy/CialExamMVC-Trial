using System.ComponentModel.DataAnnotations;

namespace CialExamMVC_Trial.ViewModels.ExpertVMs
{
    public class ExpertListVM
    {
        public string ImgUrl { get; set; }
        [Required, MaxLength(16)]
        public string Content { get; set; }
    }
}
