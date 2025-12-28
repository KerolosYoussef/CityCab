using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityCab.Rider.API.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintOnPhoneNumberToBeUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Riders_PhoneNumber",
                table: "Riders",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Riders_PhoneNumber",
                table: "Riders");
        }
    }
}
