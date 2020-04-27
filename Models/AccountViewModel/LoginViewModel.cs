using System.ComponentModel.DataAnnotations;

namespace loginIdentity.Models.AccountViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        [Display(Name = "Lembrar Login")]
        public bool Rememberme { get; set; }

    }
}