using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CialExamMVC_Trial.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
