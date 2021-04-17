using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Models
{
    public class ReservationsModel
    {
        [NotMapped]
        public List<int> ClientsIDs { get; set; }
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "int")]
        [Display(Name = "Номер на стая")]
        public int foreignID { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [Column(TypeName = "int")]
        [Display(Name = "Номер на стая")]
        public int RoomId { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        [Display(Name = "Направил резервацията")]
        public string ReservatedBy { get; set; }
        [Column(TypeName = "varchar(64)")]
        [Display(Name = "Клиентите в стаята")]
        public string ClientsInTheRoom { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Дата на настаняване")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime AccommodationDate { get; set; }
        [Required(ErrorMessage = "Полето е задължително!")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата на освобождаване")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; }
        [Column(TypeName = "bit")]
        [Display(Name = "Включена закуска")]
        public bool isBreakfastIncluded { get; set; }
        [Column(TypeName = "bit")]
        [Display(Name = "All inclusive")]
        public bool isAllInclusive { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
