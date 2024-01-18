using System.ComponentModel.DataAnnotations;

namespace CialExamMVC_Trial.Areas.Admin.ViewModels.AdminExpertVMs
{
    public class AdminExpertCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required, MaxLength(16)]
        public string Content { get; set; }
    }
}
