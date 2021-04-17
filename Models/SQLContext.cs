using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Reservations_Manager.Models;

namespace Hotel_Reservations_Manager.Models
{
    public class SQLContext : DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> options) : base(options)
        {
        }
        public DbSet<ClientsModel> Clients { get; set; }
        public DbSet<RoomsModel> Rooms { get; set; }
        public DbSet<ReservationsModel> Reservations { get; set; }
        public DbSet<UsersModel> Users { get; set; }
    }
}
