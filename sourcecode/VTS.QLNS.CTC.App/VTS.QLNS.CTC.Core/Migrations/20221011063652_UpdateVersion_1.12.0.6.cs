using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11206 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "fGiaTriLonNhat",
                table: "TL_DM_PhuCap",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "fGiaTriNhoNhat",
                table: "TL_DM_PhuCap",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "fGiaTriPhuCap_KemTheo",
                table: "TL_DM_PhuCap",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iId_Ma_PhuCap_KemTheo",
                table: "TL_DM_PhuCap",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iId_PhuCap_KemTheo",
                table: "TL_DM_PhuCap",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.0.6.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fGiaTriLonNhat",
                table: "TL_DM_PhuCap");

            migrationBuilder.DropColumn(
                name: "fGiaTriNhoNhat",
                table: "TL_DM_PhuCap");

            migrationBuilder.DropColumn(
                name: "fGiaTriPhuCap_KemTheo",
                table: "TL_DM_PhuCap");

            migrationBuilder.DropColumn(
                name: "iId_Ma_PhuCap_KemTheo",
                table: "TL_DM_PhuCap");

            migrationBuilder.DropColumn(
                name: "iId_PhuCap_KemTheo",
                table: "TL_DM_PhuCap");
        }
    }
}
