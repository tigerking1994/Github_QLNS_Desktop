using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11158 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fDuToan",
                table: "NS_DC_ChungTuChiTiet",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.5.8.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fDuToan",
                table: "NS_DC_ChungTuChiTiet");
        }
    }
}
