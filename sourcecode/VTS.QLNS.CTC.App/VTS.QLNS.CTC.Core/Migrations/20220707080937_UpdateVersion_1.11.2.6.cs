using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11126 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stng",
                table: "TL_PhuCap_MLNS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stng1",
                table: "TL_PhuCap_MLNS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stng2",
                table: "TL_PhuCap_MLNS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stng3",
                table: "TL_PhuCap_MLNS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stng",
                table: "TL_PhuCap_MLNS");

            migrationBuilder.DropColumn(
                name: "Stng1",
                table: "TL_PhuCap_MLNS");

            migrationBuilder.DropColumn(
                name: "Stng2",
                table: "TL_PhuCap_MLNS");

            migrationBuilder.DropColumn(
                name: "Stng3",
                table: "TL_PhuCap_MLNS");
        }
    }
}
