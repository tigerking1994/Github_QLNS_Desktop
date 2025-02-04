using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11449 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BHangChaDuToan",
                table: "NS_CP_ChungTuChiTiet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TL_BangLuong_Thang_TruyThu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Gia_Tri = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    HuongPC_SN = table.Column<decimal>(type: "numeric(5, 2)", nullable: true),
                    Loai_BL = table.Column<int>(nullable: true),
                    Ma_CachTL = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ma_CB = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CBo = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_DonVi = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_Hieu_CanBo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    Ma_PhuCap = table.Column<string>(maxLength: 50, nullable: true),
                    NAM = table.Column<int>(nullable: true),
                    Ngay_HT = table.Column<DateTime>(nullable: true),
                    parent = table.Column<Guid>(nullable: true),
                    So_TT = table.Column<int>(nullable: true),
                    Ten_CachTL = table.Column<string>(maxLength: 100, nullable: true),
                    Ten_Cbo = table.Column<string>(maxLength: 100, nullable: true),
                    THANG = table.Column<int>(nullable: true),
                    User_Name = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_BangLuong_Thang_TruyThu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_Cach_TinhLuong_TruyThu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    CongThuc = table.Column<string>(type: "ntext", nullable: true),
                    Ma_CachTL = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ma_Cot = table.Column<string>(maxLength: 100, nullable: true),
                    Ma_KMCP = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_KMCP1 = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Nam = table.Column<int>(nullable: true),
                    NoiDung = table.Column<string>(maxLength: 500, nullable: true),
                    Ten_CachTL = table.Column<string>(maxLength: 500, nullable: true),
                    Ten_Cot = table.Column<string>(maxLength: 500, nullable: true),
                    Thang = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_Cach_TinhLuong_TruyThu", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.9_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.9_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.9_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.9_salary_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.9_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.9_social_insurance_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_BangLuong_Thang_TruyThu");

            migrationBuilder.DropTable(
                name: "TL_DM_Cach_TinhLuong_TruyThu");

            migrationBuilder.DropColumn(
                name: "BHangChaDuToan",
                table: "NS_CP_ChungTuChiTiet");
        }
    }
}
