using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_Reservations_Manager.Migrations
{
    public partial class HotelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    familyName = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    isAdoult = table.Column<bool>(type: "bit", nullable: false),
                    isBusy = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    foreignID = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ReservatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    ClientsInTheRoom = table.Column<string>(type: "varchar(64)", nullable: true),
                    AccommodationDate = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    isBreakfastIncluded = table.Column<bool>(type: "bit", nullable: false),
                    isAllInclusive = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(64)", nullable: true),
                    isFree = table.Column<bool>(type: "bit", nullable: false),
                    AdoultPrice = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    KidsPrice = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(51)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    familyName = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(11)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    releaseDate = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
