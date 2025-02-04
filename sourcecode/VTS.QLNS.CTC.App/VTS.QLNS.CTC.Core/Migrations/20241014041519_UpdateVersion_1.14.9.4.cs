using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11494 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "iID_ChungTuDieuChinh",
                table: "NS_DT_ChungTu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sSoChungTuDieuChinh",
                table: "NS_DT_ChungTu",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.9.4_budget_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iID_ChungTuDieuChinh",
                table: "NS_DT_ChungTu");

            migrationBuilder.DropColumn(
                name: "sSoChungTuDieuChinh",
                table: "NS_DT_ChungTu");
        }
    }
}
