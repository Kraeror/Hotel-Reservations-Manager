using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Models
{
    public class Models
    {
        public IEnumerable<ReservationsModel> Reservations { get; set; }
        public ReservationsModel Reservations2 { get; set; }
        public IEnumerable<ClientsModel> Clients { get; set; }
        public ClientsModel Clients2 { get; set; }
        public RoomsModel Rooms2 { get; set; }
        public IEnumerable<RoomsModel> Rooms { get; set; }
    }
}