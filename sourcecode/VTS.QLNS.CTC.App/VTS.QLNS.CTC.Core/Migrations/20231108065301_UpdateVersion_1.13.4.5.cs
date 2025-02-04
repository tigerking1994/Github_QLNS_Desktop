using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_Name",
                table: "TL_BangLuong_ThangBHXH",
                newName: "sUserName");

            migrationBuilder.RenameColumn(
                name: "THANG",
                table: "TL_BangLuong_ThangBHXH",
                newName: "iThang");

            migrationBuilder.RenameColumn(
                name: "Ten_Cbo",
                table: "TL_BangLuong_ThangBHXH",
                newName: "sTenCbo");

            migrationBuilder.RenameColumn(
                name: "Ten_CachTL",
                table: "TL_BangLuong_ThangBHXH",
                newName: "sTenCachTL");

            migrationBuilder.RenameColumn(
                name: "So_TT",
                table: "TL_BangLuong_ThangBHXH",
                newName: "iSoTT");

            migrationBuilder.RenameColumn(
                name: "parent",
                table: "TL_BangLuong_ThangBHXH",
                newName: "iID_Parent");

            migrationBuilder.RenameColumn(
                name: "Ngay_HT",
                table: "TL_BangLuong_ThangBHXH",
                newName: "dNgayHT");

            migrationBuilder.RenameColumn(
                name: "NAM",
                table: "TL_BangLuong_ThangBHXH",
                newName: "iNam");

            migrationBuilder.RenameColumn(
                name: "Ma_Hieu_CanBo",
                table: "TL_BangLuong_ThangBHXH",
                newName: "sMaHieuCanBo");

            migrationBuilder.RenameColumn(
                name: "Ma_DonVi",
                table: "TL_BangLuong_ThangBHXH",
                newName: "sMaDonVi");

            migrationBuilder.RenameColumn(
                name: "Ma_CheDo",
                table: "TL_BangLuong_ThangBHXH",
                newName: "sMaCheDo");

            migrationBuilder.RenameColumn(
                name: "Ma_CBo",
                table: "TL_BangLuong_ThangBHXH",
                newName: "sMaCBo");

            migrationBuilder.RenameColumn(
                name: "Ma_CB",
                table: "TL_BangLuong_ThangBHXH",
                newName: "sMaCB");

            migrationBuilder.RenameColumn(
                name: "Ma_CachTL",
                table: "TL_BangLuong_ThangBHXH",
                newName: "sMaCachTL");

            migrationBuilder.RenameColumn(
                name: "Loai_BL",
                table: "TL_BangLuong_ThangBHXH",
                newName: "iLoaiBL");

            migrationBuilder.RenameColumn(
                name: "HuongPC_SN",
                table: "TL_BangLuong_ThangBHXH",
                newName: "nHuongPCSN");

            migrationBuilder.RenameColumn(
                name: "Gia_Tri",
                table: "TL_BangLuong_ThangBHXH",
                newName: "nGiaTri");

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.5_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.5_social_insurance_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sUserName",
                table: "TL_BangLuong_ThangBHXH",
                newName: "User_Name");

            migrationBuilder.RenameColumn(
                name: "iThang",
                table: "TL_BangLuong_ThangBHXH",
                newName: "THANG");

            migrationBuilder.RenameColumn(
                name: "sTenCbo",
                table: "TL_BangLuong_ThangBHXH",
                newName: "Ten_Cbo");

            migrationBuilder.RenameColumn(
                name: "sTenCachTL",
                table: "TL_BangLuong_ThangBHXH",
                newName: "Ten_CachTL");

            migrationBuilder.RenameColumn(
                name: "iSoTT",
                table: "TL_BangLuong_ThangBHXH",
                newName: "So_TT");

            migrationBuilder.RenameColumn(
                name: "iID_Parent",
                table: "TL_BangLuong_ThangBHXH",
                newName: "parent");

            migrationBuilder.RenameColumn(
                name: "dNgayHT",
                table: "TL_BangLuong_ThangBHXH",
                newName: "Ngay_HT");

            migrationBuilder.RenameColumn(
                name: "iNam",
                table: "TL_BangLuong_ThangBHXH",
                newName: "NAM");

            migrationBuilder.RenameColumn(
                name: "sMaHieuCanBo",
                table: "TL_BangLuong_ThangBHXH",
                newName: "Ma_Hieu_CanBo");

            migrationBuilder.RenameColumn(
                name: "sMaDonVi",
                table: "TL_BangLuong_ThangBHXH",
                newName: "Ma_DonVi");

            migrationBuilder.RenameColumn(
                name: "sMaCheDo",
                table: "TL_BangLuong_ThangBHXH",
                newName: "Ma_CheDo");

            migrationBuilder.RenameColumn(
                name: "sMaCBo",
                table: "TL_BangLuong_ThangBHXH",
                newName: "Ma_CBo");

            migrationBuilder.RenameColumn(
                name: "sMaCB",
                table: "TL_BangLuong_ThangBHXH",
                newName: "Ma_CB");

            migrationBuilder.RenameColumn(
                name: "sMaCachTL",
                table: "TL_BangLuong_ThangBHXH",
                newName: "Ma_CachTL");

            migrationBuilder.RenameColumn(
                name: "iLoaiBL",
                table: "TL_BangLuong_ThangBHXH",
                newName: "Loai_BL");

            migrationBuilder.RenameColumn(
                name: "nHuongPCSN",
                table: "TL_BangLuong_ThangBHXH",
                newName: "HuongPC_SN");

            migrationBuilder.RenameColumn(
                name: "nGiaTri",
                table: "TL_BangLuong_ThangBHXH",
                newName: "Gia_Tri");
        }
    }
}
