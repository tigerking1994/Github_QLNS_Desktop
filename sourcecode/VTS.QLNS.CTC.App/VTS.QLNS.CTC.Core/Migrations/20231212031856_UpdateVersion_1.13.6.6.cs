using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11366 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "iNamLamViec");

            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_KHC_KCB",
                newName: "iNamLamViec");

            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_KHC_K",
                newName: "iNamLamViec");

            migrationBuilder.RenameColumn(
                name: "INamLamViec",
                table: "BH_KHC_CheDoBHXH_ChiTiet",
                newName: "iNamLamViec");

            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_KHC_CheDoBHXH",
                newName: "iNamLamViec");

            migrationBuilder.AddColumn<bool>(
                name: "bDisplay",
                table: "TL_DM_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bDaTongHop",
                table: "BH_KHC_KinhPhiQuanLy",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHC_K_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_KHC_K_ChiTiet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_KHC_K_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHC_KCB_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_KHC_KCB_ChiTiet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_KHC_KCB_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bDaTongHop",
                table: "BH_KHC_KCB",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "bDaTongHop",
                table: "BH_KHC_K",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHC_CheDoBHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_KHC_CheDoBHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bDaTongHop",
                table: "BH_KHC_CheDoBHXH",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TL_QuanLyThuNop_BHXH",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dDenNgay = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    dTuNgay = table.Column<DateTime>(nullable: true),
                    iID_MaDonVi = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    iLoai = table.Column<int>(nullable: true),
                    iNam = table.Column<decimal>(type: "numeric(18, 0)", nullable: true),
                    iSo_TT = table.Column<int>(nullable: true),
                    iThang = table.Column<decimal>(type: "numeric(18, 0)", nullable: true),
                    IsTongHop = table.Column<bool>(nullable: false),
                    sMa_CachTL = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    sMa_PBan = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    sMota = table.Column<string>(maxLength: 255, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 255, nullable: true),
                    sTen = table.Column<string>(maxLength: 255, nullable: true),
                    sTongHop = table.Column<string>(nullable: true),
                    status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_QuanLyThuNop_BHXH", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_QuanLyThuNop_BHXH_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgay_HT = table.Column<DateTime>(nullable: true),
                    Gia_Tri = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iId_ParentId = table.Column<Guid>(nullable: true),
                    iLoai = table.Column<int>(nullable: true),
                    iNam = table.Column<int>(nullable: true),
                    iSo_TT = table.Column<int>(nullable: true),
                    iThang = table.Column<int>(nullable: true),
                    sMa_CachTL = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    sMa_CB = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    sMa_CBo = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    sMa_Hieu_CanBo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    sMa_PhuCap = table.Column<string>(maxLength: 50, nullable: true),
                    sTen_CachTL = table.Column<string>(maxLength: 100, nullable: true),
                    sTen_Cbo = table.Column<string>(maxLength: 100, nullable: true),
                    sUserName = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_QuanLyThuNop_BHXH_ChiTiet", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.6_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.6_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.6_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.6_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.6_social_insurance_5.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.6_social_insurance_6.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_QuanLyThuNop_BHXH");

            migrationBuilder.DropTable(
                name: "TL_QuanLyThuNop_BHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "bDisplay",
                table: "TL_DM_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "bDaTongHop",
                table: "BH_KHC_KinhPhiQuanLy");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_KHC_K_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_KHC_K_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_KHC_K_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_KHC_KCB_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_KHC_KCB_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_KHC_KCB_ChiTiet");

            migrationBuilder.DropColumn(
                name: "bDaTongHop",
                table: "BH_KHC_KCB");

            migrationBuilder.DropColumn(
                name: "bDaTongHop",
                table: "BH_KHC_K");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_KHC_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_KHC_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "bDaTongHop",
                table: "BH_KHC_CheDoBHXH");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "iNamChungTu");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_KHC_KCB",
                newName: "iNamChungTu");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_KHC_K",
                newName: "iNamChungTu");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_KHC_CheDoBHXH_ChiTiet",
                newName: "INamLamViec");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_KHC_CheDoBHXH",
                newName: "iNamChungTu");
        }
    }
}
