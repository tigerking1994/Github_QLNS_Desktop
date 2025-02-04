using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11472 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "bTinhCN",
                table: "TL_DM_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bTinhNgayLe",
                table: "TL_DM_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bTinhT7",
                table: "TL_DM_CheDoBHXH",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.7.2_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.7.2_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.7.2_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bTinhCN",
                table: "TL_DM_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "bTinhNgayLe",
                table: "TL_DM_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "bTinhT7",
                table: "TL_DM_CheDoBHXH");
        }
    }
}
