﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcLaundry.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JenisPakaian",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NamaPakaian = table.Column<string>(nullable: true),
                    HargaPerKg = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JenisPakaian", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaksi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NamaUser = table.Column<string>(nullable: true),
                    AlamatUser = table.Column<string>(nullable: true),
                    NoHPUser = table.Column<string>(nullable: true),
                    TglTransaksi = table.Column<DateTime>(nullable: false),
                    TotalTransaksi = table.Column<int>(nullable: false),
                    JenisPakaianId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaksi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaksi_JenisPakaian_JenisPakaianId",
                        column: x => x.JenisPakaianId,
                        principalTable: "JenisPakaian",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaksi_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaksi_JenisPakaianId",
                table: "Transaksi",
                column: "JenisPakaianId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaksi_UserId",
                table: "Transaksi",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaksi");

            migrationBuilder.DropTable(
                name: "JenisPakaian");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
