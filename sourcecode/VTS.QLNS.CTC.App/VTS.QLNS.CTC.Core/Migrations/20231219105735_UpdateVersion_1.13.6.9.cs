using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11369 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "sTenNgayNghi",
                table: "Tl_DM_NgayNghi",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sMaNgayNghi",
                table: "Tl_DM_NgayNghi",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaLoaiChi",
                table: "BH_KHC_K",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaLoaiChi",
                table: "BH_DTC_DuToanChiTrenGiao",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TL_CanBo_CheDoBHXH_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bTrangThai = table.Column<bool>(nullable: false),
                    dDenNgay = table.Column<DateTime>(type: "datetime", nullable: true),
                    dTuNgay = table.Column<DateTime>(type: "datetime", nullable: true),
                    fSoNgayHuongBHXH = table.Column<double>(nullable: true),
                    iNam = table.Column<int>(nullable: true),
                    iThang = table.Column<int>(nullable: true),
                    sMaCanBo = table.Column<string>(maxLength: 50, nullable: true),
                    sMaCheDo = table.Column<string>(maxLength: 100, nullable: true),
                    sTenCheDo = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_CanBo_CheDoBHXH_ChiTiet", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.9_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.9_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.9_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.9_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_CanBo_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sMaLoaiChi",
                table: "BH_KHC_K");

            migrationBuilder.DropColumn(
                name: "sMaLoaiChi",
                table: "BH_DTC_DuToanChiTrenGiao");

            migrationBuilder.AlterColumn<string>(
                name: "sTenNgayNghi",
                table: "Tl_DM_NgayNghi",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sMaNgayNghi",
                table: "Tl_DM_NgayNghi",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
