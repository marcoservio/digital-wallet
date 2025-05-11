using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigitalWallet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    UserIdentifier = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    WalletKey = table.Column<Guid>(type: "uuid", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromWalletId = table.Column<long>(type: "bigint", nullable: false),
                    ToWalletId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_FromWalletId",
                        column: x => x.FromWalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_ToWalletId",
                        column: x => x.ToWalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "Password", "UpdatedAt", "UserIdentifier" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 5, 10, 22, 38, 25, 468, DateTimeKind.Utc).AddTicks(2535), "admin@gmail.com", true, "Admin", "$2a$11$pOpUBsD/FJOGeqxrki2wLuNYr5TGLGJyBxgQdbMzu9X7KkQSSoOAG", new DateTime(2025, 5, 10, 22, 38, 25, 468, DateTimeKind.Utc).AddTicks(2538), new Guid("b2db69f1-ab7b-4244-8696-8a6df20f5f42") },
                    { 2L, new DateTime(2025, 5, 10, 22, 38, 25, 722, DateTimeKind.Utc).AddTicks(9352), "bob@gmail.com", true, "Bob", "$2a$11$DUN.qJWoQu8SaMQe.wfCrecL59n7d1j0Cuyr0hz0m5WMezXt0lM.m", new DateTime(2025, 5, 10, 22, 38, 25, 722, DateTimeKind.Utc).AddTicks(9357), new Guid("cc7231e3-a67c-4611-b913-f535fbc6563a") },
                    { 3L, new DateTime(2025, 5, 10, 22, 38, 25, 878, DateTimeKind.Utc).AddTicks(8180), "charlie@gmail.com", true, "Charlie", "$2a$11$1lMJZfzyU8m.GdfvpFn/4ubR3fyTvB558kgH7YGikH2Di/Yd4J6I.", new DateTime(2025, 5, 10, 22, 38, 25, 878, DateTimeKind.Utc).AddTicks(8186), new Guid("9faec17d-1a7d-40e8-a326-5add0880f048") },
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "CreatedAt", "IsActive", "UpdatedAt", "UserId", "WalletKey" },
                values: new object[,]
                {
                    { 1L, 1000m, new DateTime(2025, 5, 10, 22, 38, 26, 70, DateTimeKind.Utc).AddTicks(2053), true, new DateTime(2025, 5, 10, 22, 38, 26, 70, DateTimeKind.Utc).AddTicks(2055), 1L, new Guid("330889c2-de95-49ce-bc74-c336ef21846b") },
                    { 2L, 500m, new DateTime(2025, 5, 10, 22, 38, 26, 70, DateTimeKind.Utc).AddTicks(2606), true, new DateTime(2025, 5, 10, 22, 38, 26, 70, DateTimeKind.Utc).AddTicks(2607), 2L, new Guid("e4b9803b-2252-437b-a557-a64de1158097") },
                    { 3L, 300m, new DateTime(2025, 5, 10, 22, 38, 26, 70, DateTimeKind.Utc).AddTicks(2609), true, new DateTime(2025, 5, 10, 22, 38, 26, 70, DateTimeKind.Utc).AddTicks(2610), 3L, new Guid("e02e6454-2429-4cdd-b4ab-47f4d1ff8806") },
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "CreatedAt", "Description", "FromWalletId", "IsActive", "Status", "ToWalletId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 100m, new DateTime(2025, 5, 10, 22, 38, 25, 459, DateTimeKind.Utc).AddTicks(1032), "Admin to Bob", 1L, true, 0, 2L, new DateTime(2025, 5, 10, 22, 38, 25, 458, DateTimeKind.Utc).AddTicks(9582) },
                    { 2L, 50m, new DateTime(2025, 5, 10, 22, 38, 25, 459, DateTimeKind.Utc).AddTicks(1256), "Bob to Charlie", 2L, true, 0, 3L, new DateTime(2025, 5, 10, 22, 38, 25, 459, DateTimeKind.Utc).AddTicks(1253) },
                    { 3L, 30m, new DateTime(2025, 5, 10, 22, 38, 25, 459, DateTimeKind.Utc).AddTicks(1259), "Charlie to Admin", 3L, true, 0, 1L, new DateTime(2025, 5, 10, 22, 38, 25, 459, DateTimeKind.Utc).AddTicks(1257) },
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromWalletId",
                table: "Transactions",
                column: "FromWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToWalletId",
                table: "Transactions",
                column: "ToWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserIdentifier",
                table: "Users",
                column: "UserIdentifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_WalletKey",
                table: "Wallets",
                column: "WalletKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
