using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Models
{
    public class RoomsModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^[1-9][0-9]?$", ErrorMessage = "Моля, използвайте числа в диапазон от 1 до 99.")]
        [Column(TypeName = "int")]
        [Display(Name = "Капацитет")]
        public int Capacity { get; set; }
        [Column(TypeName = "nvarchar(64)")]
        [Display(Name = "Тип")]
        public string Type { get; set; }
        [Column(TypeName = "bit")]
        [Display(Name = "Свободна")]
        public bool isFree { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^([1-9]?[0-9]?[0-9])|[1-9]\.?[0-9]?[0-9]$", ErrorMessage = "Моля, използвайте числа в диапазон от 0.00 до 999.99, закръглени с точност до втория знак.")]
        [Display(Name = "Цена - възрастни")]
        public double AdoultPrice { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^([1-9]?[0-9]?[0-9])|[1-9]\.?[0-9]?[0-9]$", ErrorMessage = "Моля, използвайте числа в диапазон от 0.00 до 999.99, закръглени с точност до втория знак.")]
        [Display(Name = "Цена - деца")]
        public double KidsPrice { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [RegularExpression(@"^[0-9]{1,5}$", ErrorMessage = "Моля, използвайте цифри от 0 до 9 с максимална дължина от 5 символа.")]
        [Column(TypeName = "int")]
        [Display(Name = "Номер на стая")]
        public int RoomID { get; set; }
    }
}
