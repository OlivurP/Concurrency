using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concurrency.Migrations
{
    /// <inheritdoc />
    public partial class WithSeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seat",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "SeatId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taken = table.Column<bool>(type: "bit", nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seat_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_SeatId",
                table: "Order",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_MovieId",
                table: "Seat",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Seat_SeatId",
                table: "Order",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Seat_SeatId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Order_SeatId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "Seat",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
