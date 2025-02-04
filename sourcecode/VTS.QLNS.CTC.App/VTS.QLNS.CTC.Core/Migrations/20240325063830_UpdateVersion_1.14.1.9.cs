using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11419 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.9_salary_before_migration.sql");
            
            migrationBuilder.DropColumn(
                name: "nhom",
                table: "TL_DM_CapBac_Luong_NQ104");

            migrationBuilder.AddColumn<bool>(
                name: "is_theo_cong_chuan",
                table: "TL_DM_PhuCap_NQ104",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ten",
                table: "TL_DM_ChucVu_NQ104",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ma_cha",
                table: "TL_DM_ChucVu_NQ104",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "nam",
                table: "TL_DM_ChucVu_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "xau_noi_ma",
                table: "TL_DM_ChucVu_NQ104",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "loai",
                table: "TL_DM_CapBac_Luong_NQ104",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ma_dm_cha",
                table: "TL_DM_CapBac_Luong_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "nam",
                table: "TL_DM_CapBac_Luong_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_nang_luong",
                table: "TL_DM_CapBac_Luong_NQ104",
                type: "numeric(15, 4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "xau_noi_ma",
                table: "TL_DM_CapBac_Luong_NQ104",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.9_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.9_budget_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.9_salary_master_data.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.9_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.9_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.9_social_insurance_7.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_theo_cong_chuan",
                table: "TL_DM_PhuCap_NQ104");

            migrationBuilder.DropColumn(
                name: "ma_cha",
                table: "TL_DM_ChucVu_NQ104");

            migrationBuilder.DropColumn(
                name: "nam",
                table: "TL_DM_ChucVu_NQ104");

            migrationBuilder.DropColumn(
                name: "xau_noi_ma",
                table: "TL_DM_ChucVu_NQ104");

            migrationBuilder.DropColumn(
                name: "ma_dm_cha",
                table: "TL_DM_CapBac_Luong_NQ104");

            migrationBuilder.DropColumn(
                name: "nam",
                table: "TL_DM_CapBac_Luong_NQ104");

            migrationBuilder.DropColumn(
                name: "tien_nang_luong",
                table: "TL_DM_CapBac_Luong_NQ104");

            migrationBuilder.DropColumn(
                name: "xau_noi_ma",
                table: "TL_DM_CapBac_Luong_NQ104");

            migrationBuilder.AlterColumn<string>(
                name: "ten",
                table: "TL_DM_ChucVu_NQ104",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "loai",
                table: "TL_DM_CapBac_Luong_NQ104",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "nhom",
                table: "TL_DM_CapBac_Luong_NQ104",
                maxLength: 10,
                nullable: true);
        }
    }
}
