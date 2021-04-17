using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Models
{
    public class UsersModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^[a-zA-Z0-9]{5,20}$", ErrorMessage = "Използвайте букви само от латинската азбука или цифри с дължина от 5 до 20 символа.")]
        [Column(TypeName = "nvarchar(51)")]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^[a-zA-Z0-9а-яА-Я]{8,50}$", ErrorMessage = "Използвайте букви само от латинската азбука и кирилицата или цифри с дължина от 8 до 50 символа.")]
        [Column(TypeName = "nvarchar(21)")]
        [Display(Name = "Парола")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]{3,20}$", ErrorMessage = "Използвайте букви само от латинската азбука и кирилицата с дължина от 3 до 20 букви.")]
        [Column(TypeName = "nvarchar(21)")]
        [Display(Name = "Име")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]{3,20}$", ErrorMessage = "Използвайте букви само от латинската азбука и кирилицата с дължина от 3 до 20 букви.")]
        [Column(TypeName = "nvarchar(21)")]
        [Display(Name = "Презиме")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]{3,20}$", ErrorMessage = "Използвайте букви само от латинската азбука и кирилицата с дължина от 3 до 20 букви.")]
        [Column(TypeName = "nvarchar(21)")]
        [Display(Name = "Фамилия")]
        public string familyName { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Въвели сте невалиден Единен Граждански Номер.")]
        [Column(TypeName = "nvarchar(11)")]
        [Display(Name = "Единен Граждански Номер (ЕГН)")]
        public string IDNumber { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^0[89][7-9][0-9]{7}|(\+359[89][7-9][0-9]{7})$", ErrorMessage = "Въвели сте невалиден телефонен номер.")]
        [Column(TypeName = "nvarchar(13)")]
        [Display(Name = "Телефонен номер")]
        public string phoneNumber { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Въвели сте невалидна електронна поща.")]
        [Column(TypeName = "nvarchar(256)")]
        [Display(Name = "Електронна\nпоща")]
        public string Email { get; set; }
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Дата\nна\nназначаване")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
        [Column(TypeName = "bit")]
        [Display(Name = "Активен")]
        public bool isActive { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Дата\nна\nосвобождаване")]
        public DateTime? releaseDate { get; set; }
    }
}
