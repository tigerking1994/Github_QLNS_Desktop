using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11429 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "thoi_han_tang_cvd",
                table: "TL_DM_CanBo_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_luong_ke_khoach",
                table: "TL_DM_CanBo_NQ104",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.9_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.9_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.9_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.9_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.9_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.9_social_insurance_6.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "thoi_han_tang_cvd",
                table: "TL_DM_CanBo_NQ104");

            migrationBuilder.DropColumn(
                name: "tien_luong_ke_khoach",
                table: "TL_DM_CanBo_NQ104");
        }
    }
}
