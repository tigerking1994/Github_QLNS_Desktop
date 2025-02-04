using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11211 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iQuyKeHoach",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.1.1.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iQuyKeHoach",
                table: "NH_TT_ThanhToan");
        }
    }
}
