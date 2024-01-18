using System.ComponentModel.DataAnnotations;

namespace CialExamMVC_Trial.Areas.Admin.ViewModels.AdminExpertVMs
{
    public class AdminExpertListVM
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        [Required, MaxLength(16)]
        public string Content { get; set; }
    }
}
