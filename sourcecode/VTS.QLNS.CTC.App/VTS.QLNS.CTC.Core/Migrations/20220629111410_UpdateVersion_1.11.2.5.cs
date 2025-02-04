using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11125 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sTenChuongTrinh",
                table: "NH_DA_DuToan",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.2.5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sTenChuongTrinh",
                table: "NH_DA_DuToan");
        }
    }
}
