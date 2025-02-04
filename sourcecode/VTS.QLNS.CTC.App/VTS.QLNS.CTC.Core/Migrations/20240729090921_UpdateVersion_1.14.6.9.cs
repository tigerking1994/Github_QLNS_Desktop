using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11469 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "iIdMaDonVi",
                table: "TL_BangLuong_ThangBHXH",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.9_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.9_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.9_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.9_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iIdMaDonVi",
                table: "TL_BangLuong_ThangBHXH");
        }
    }
}
