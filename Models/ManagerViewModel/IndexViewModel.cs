using System.ComponentModel.DataAnnotations;

namespace loginIdentity.Models.ManagerViewModel
{
    public class IndexViewModel
    {
        public string UserName { get; set; }
        public bool IsEmailConfirmed { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Número de Telefone")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}