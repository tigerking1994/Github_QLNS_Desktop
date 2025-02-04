using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11338 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTT_MucLucGiaiThich");

            migrationBuilder.AddColumn<bool>(
                name: "bTinhBHXH",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Nam",
                table: "TL_DM_Cach_TinhLuong_BaoHiem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Thang",
                table: "TL_DM_Cach_TinhLuong_BaoHiem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TL_CanBo_CheDoBHXH",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDenNgay = table.Column<DateTime>(type: "datetime", nullable: true),
                    dTuNgay = table.Column<DateTime>(type: "datetime", nullable: true),
                    fSoNgayHuongBHXH = table.Column<double>(nullable: true),
                    iSoNgayNghi = table.Column<int>(nullable: true),
                    sMaCanBo = table.Column<string>(nullable: true),
                    sMaCheDo = table.Column<string>(nullable: true),
                    sTenCheDo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_CanBo_CheDoBHXH", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tl_DM_NgayNghi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDenNgay = table.Column<DateTime>(type: "datetime", nullable: true),
                    dTuNgay = table.Column<DateTime>(type: "datetime", nullable: true),
                    sMaNgayNghi = table.Column<string>(nullable: true),
                    sTenNgayNghi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tl_DM_NgayNghi", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.8_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.8_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.8_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.8_social_insurance_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_CanBo_CheDoBHXH");

            migrationBuilder.DropTable(
                name: "Tl_DM_NgayNghi");

            migrationBuilder.DropColumn(
                name: "bTinhBHXH",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "Nam",
                table: "TL_DM_Cach_TinhLuong_BaoHiem");

            migrationBuilder.DropColumn(
                name: "Thang",
                table: "TL_DM_Cach_TinhLuong_BaoHiem");

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTT_MucLucGiaiThich",
                nullable: false,
                defaultValue: 0);
        }
    }
}
