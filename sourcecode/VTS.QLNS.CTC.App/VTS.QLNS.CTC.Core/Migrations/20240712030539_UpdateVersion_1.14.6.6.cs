using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11466 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "bCapNhatTruyThu",
                table: "TL_DM_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bTamGiamTamGiu",
                table: "TL_DM_CanBo",
                nullable: true,
                defaultValueSql: "((0))");

            migrationBuilder.AddColumn<double>(
                name: "fSoNgayNghiPhep",
                table: "TL_CanBo_CheDoBHXH",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.6_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.6_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.6_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.6_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.6_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.6_social_insurance_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bCapNhatTruyThu",
                table: "TL_DM_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "bTamGiamTamGiu",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "fSoNgayNghiPhep",
                table: "TL_CanBo_CheDoBHXH");
        }
    }
}
