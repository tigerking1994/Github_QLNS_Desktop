using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11458 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "tien_bao_luu_cb",
                table: "TL_DM_CanBo_KeHoach_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_bao_luu_cvd",
                table: "TL_DM_CanBo_KeHoach_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_luong_cb",
                table: "TL_DM_CanBo_KeHoach_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_luong_cvd",
                table: "TL_DM_CanBo_KeHoach_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_nang_luong_cb",
                table: "TL_DM_CanBo_KeHoach_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_nang_luong_cvd",
                table: "TL_DM_CanBo_KeHoach_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "TL_CanBo_PhuCap_KeHoach_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "TL_BangLuong_KeHoach_NQ104",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TL_BangLuong_KeHoach_Bridge_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    gia_tri = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    ma_cb = table.Column<string>(maxLength: 50, nullable: true),
                    ma_can_bo = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ma_don_vi = table.Column<string>(maxLength: 50, nullable: true),
                    ma_hieu_can_bo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    ma_phu_cap = table.Column<string>(maxLength: 50, nullable: true),
                    nam = table.Column<int>(nullable: false),
                    parent = table.Column<Guid>(nullable: true),
                    thang = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_BangLuong_KeHoach_Bridge_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_CanBo_PhuCap_KeHoach_Bridge_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    gia_tri = table.Column<decimal>(type: "numeric(17, 4)", nullable: true),
                    ma_can_bo = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    ma_phu_cap = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    ngay_huong_phu_cap = table.Column<decimal>(type: "numeric(5, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_CanBo_PhuCap_KeHoach_Bridge_NQ104", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.5.8_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.5.8_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.5.8_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.5.8_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.5.8_social_insurance_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_BangLuong_KeHoach_Bridge_NQ104");

            migrationBuilder.DropTable(
                name: "TL_CanBo_PhuCap_KeHoach_Bridge_NQ104");

            migrationBuilder.DropColumn(
                name: "tien_bao_luu_cb",
                table: "TL_DM_CanBo_KeHoach_NQ104");

            migrationBuilder.DropColumn(
                name: "tien_bao_luu_cvd",
                table: "TL_DM_CanBo_KeHoach_NQ104");

            migrationBuilder.DropColumn(
                name: "tien_luong_cb",
                table: "TL_DM_CanBo_KeHoach_NQ104");

            migrationBuilder.DropColumn(
                name: "tien_luong_cvd",
                table: "TL_DM_CanBo_KeHoach_NQ104");

            migrationBuilder.DropColumn(
                name: "tien_nang_luong_cb",
                table: "TL_DM_CanBo_KeHoach_NQ104");

            migrationBuilder.DropColumn(
                name: "tien_nang_luong_cvd",
                table: "TL_DM_CanBo_KeHoach_NQ104");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "TL_CanBo_PhuCap_KeHoach_NQ104");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "TL_BangLuong_KeHoach_NQ104");
        }
    }
}
