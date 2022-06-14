using System;
using System.ComponentModel.DataAnnotations;

namespace SanTsgProject.Domain.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Karakter Sayısını Aştınız!")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string CreationDate { get; set; }

        public bool isActive { get; set; } = false;

    }
}
