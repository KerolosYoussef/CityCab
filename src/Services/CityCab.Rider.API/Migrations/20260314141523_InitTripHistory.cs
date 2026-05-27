using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityCab.Rider.API.Migrations
{
    /// <inheritdoc />
    public partial class InitTripHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TripId = table.Column<Guid>(type: "uuid", nullable: false),
                    RiderId = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PickupLocation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DropoffLocation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripHistories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripHistories_DriverId",
                table: "TripHistories",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TripHistories_IsActive",
                table: "TripHistories",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_TripHistories_RiderId",
                table: "TripHistories",
                column: "RiderId");

            migrationBuilder.CreateIndex(
                name: "IX_TripHistories_Status",
                table: "TripHistories",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_TripHistories_TripId",
                table: "TripHistories",
                column: "TripId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripHistories");
        }
    }
}
