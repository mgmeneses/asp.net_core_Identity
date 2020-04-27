using System;
using System.ComponentModel.DataAnnotations;

namespace loginIdentity.Models.ManagerViewModel
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no minímo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        public string NewPassword { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova Senha")]
        [Compare("NewPassword", ErrorMessage = "As senha devem ser iguais.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}