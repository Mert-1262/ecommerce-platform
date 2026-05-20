using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCargoFactory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CargoTracks_OrderId",
                table: "CargoTracks");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTracks_OrderId",
                table: "CargoTracks",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CargoTracks_OrderId",
                table: "CargoTracks");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTracks_OrderId",
                table: "CargoTracks",
                column: "OrderId");
        }
    }
}
