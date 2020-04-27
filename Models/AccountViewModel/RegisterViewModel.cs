using System;
using System.ComponentModel.DataAnnotations;

namespace loginIdentity.Models.AccountViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no minímo {1} e no máximo {2} caracteres.", MinimumLength = 8)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "As senha devem ser iguais.")]
        public string ConfirmPassword { get; set; }
    }
}