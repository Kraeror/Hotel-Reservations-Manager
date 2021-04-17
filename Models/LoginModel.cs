using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Models
{
    public class LoginModel
    {
        [Display(Name = "Потребителско име или\nелектронна поща")]
        [Required(ErrorMessage = "Полето е задължително!")]
        public string Username { get; set; }
        [Display(Name = "Парола")]
        [Required(ErrorMessage = "Полето е задължително!")]
        public string Password { get; set; }
    }
}
