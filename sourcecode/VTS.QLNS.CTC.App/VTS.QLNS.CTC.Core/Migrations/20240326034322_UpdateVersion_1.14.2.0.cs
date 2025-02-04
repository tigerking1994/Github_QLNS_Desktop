using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11420 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "nam",
                table: "TL_DM_PhuCap_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "nam",
                table: "TL_DM_CapBac_NQ104",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "loai",
                table: "TL_DM_CapBac_Luong_NQ104",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<double>(
                name: "LCS",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.0_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.0_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.0_salary_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.0_salary_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.0_salary_master_data.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.0_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.0_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nam",
                table: "TL_DM_PhuCap_NQ104");

            migrationBuilder.DropColumn(
                name: "nam",
                table: "TL_DM_CapBac_NQ104");

            migrationBuilder.DropColumn(
                name: "LCS",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet");

            migrationBuilder.AlterColumn<int>(
                name: "loai",
                table: "TL_DM_CapBac_Luong_NQ104",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
