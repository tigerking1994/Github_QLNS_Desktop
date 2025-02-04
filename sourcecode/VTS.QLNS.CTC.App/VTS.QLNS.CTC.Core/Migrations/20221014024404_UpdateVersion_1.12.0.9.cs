using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11209 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BActive",
                table: "NH_DM_NhaThau",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IThuTu",
                table: "NH_DM_NhaThau",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.0.9.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BActive",
                table: "NH_DM_NhaThau");

            migrationBuilder.DropColumn(
                name: "IThuTu",
                table: "NH_DM_NhaThau");
        }
    }
}
