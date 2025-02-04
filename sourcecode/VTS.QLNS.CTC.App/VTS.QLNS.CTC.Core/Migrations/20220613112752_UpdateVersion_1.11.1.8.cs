using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11118 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dDateCreate",
                table: "VDT_KHV_PhanBoVon_ChiPhi",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dDateDelete",
                table: "VDT_KHV_PhanBoVon_ChiPhi",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dDateUpdate",
                table: "VDT_KHV_PhanBoVon_ChiPhi",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sUserCreate",
                table: "VDT_KHV_PhanBoVon_ChiPhi",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sUserDelete",
                table: "VDT_KHV_PhanBoVon_ChiPhi",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sUserUpdate",
                table: "VDT_KHV_PhanBoVon_ChiPhi",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.1.8.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dDateCreate",
                table: "VDT_KHV_PhanBoVon_ChiPhi");

            migrationBuilder.DropColumn(
                name: "dDateDelete",
                table: "VDT_KHV_PhanBoVon_ChiPhi");

            migrationBuilder.DropColumn(
                name: "dDateUpdate",
                table: "VDT_KHV_PhanBoVon_ChiPhi");

            migrationBuilder.DropColumn(
                name: "sUserCreate",
                table: "VDT_KHV_PhanBoVon_ChiPhi");

            migrationBuilder.DropColumn(
                name: "sUserDelete",
                table: "VDT_KHV_PhanBoVon_ChiPhi");

            migrationBuilder.DropColumn(
                name: "sUserUpdate",
                table: "VDT_KHV_PhanBoVon_ChiPhi");
        }
    }
}
