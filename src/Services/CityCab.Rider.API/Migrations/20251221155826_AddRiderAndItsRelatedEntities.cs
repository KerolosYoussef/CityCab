using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityCab.Rider.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRiderAndItsRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Riders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Rating = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false, defaultValue: 5.0m),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiderAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiderAddresses", x => new { x.Id, x.RiderId });
                    table.ForeignKey(
                        name: "FK_RiderAddresses_Riders_RiderId",
                        column: x => x.RiderId,
                        principalTable: "Riders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiderPaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardHolderName = table.Column<string>(type: "text", nullable: false),
                    CardHolderType = table.Column<string>(type: "text", nullable: false),
                    ProviderToken = table.Column<string>(type: "text", nullable: false),
                    Last4Digits = table.Column<string>(type: "text", nullable: false),
                    ExpiryMonth = table.Column<string>(type: "text", nullable: false),
                    ExpiryYear = table.Column<string>(type: "text", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiderPaymentMethods", x => new { x.Id, x.RiderId });
                    table.ForeignKey(
                        name: "FK_RiderPaymentMethods_Riders_RiderId",
                        column: x => x.RiderId,
                        principalTable: "Riders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiderAddresses_RiderId",
                table: "RiderAddresses",
                column: "RiderId");

            migrationBuilder.CreateIndex(
                name: "IX_RiderPaymentMethods_RiderId",
                table: "RiderPaymentMethods",
                column: "RiderId");

            migrationBuilder.CreateIndex(
                name: "IX_Riders_Email",
                table: "Riders",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiderAddresses");

            migrationBuilder.DropTable(
                name: "RiderPaymentMethods");

            migrationBuilder.DropTable(
                name: "Riders");
        }
    }
}
