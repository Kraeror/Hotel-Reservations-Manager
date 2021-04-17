using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Models
{
    public class ClientsModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]{3,20}$", ErrorMessage = "Използвайте букви само от латинската азбука и кирилицата с дължина от 3 до 20 букви.")]
        [Column(TypeName = "nvarchar(21)")]
        [Display(Name = "Име")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]{3,20}$", ErrorMessage = "Използвайте букви само от латинската азбука и кирилицата с дължина от 3 до 20 букви.")]
        [Column(TypeName = "nvarchar(21)")]
        [Display(Name = "Фамилия")]
        public string familyName { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [Column(TypeName = "nvarchar(13)")]
        [RegularExpression(@"^0[89][7-9][0-9]{7}|(\+359[89][7-9][0-9]{7})$", ErrorMessage = "Въвели сте невалиден телефонен номер.")]
        [Display(Name = "Телефонен номер")]
        public string phoneNumber { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Въвели сте невалидна електронна поща.")]
        [Column(TypeName = "nvarchar(256)")]
        [Display(Name = "Електронна поща")]
        public string Email { get; set; }
        [Column(TypeName = "bit")]
        [Display(Name = "Пълнолетен")]
        public bool isAdoult { get; set; }
        [NotMapped]
        public int RefID { get; set; }
        [Column(TypeName = "bit")]
        public bool isBusy { get; set; }
    }
}
