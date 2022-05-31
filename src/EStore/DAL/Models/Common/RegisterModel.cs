using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Common
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public long RoleId { get; set; }
    }
}