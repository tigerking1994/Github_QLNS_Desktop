using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11297 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sTongHop",
                table: "BH_KHC_CheDoBHXH",
                newName: "STongHop");

            migrationBuilder.RenameColumn(
                name: "sNguoiTao",
                table: "BH_KHC_CheDoBHXH",
                newName: "SNguoiTao");

            migrationBuilder.RenameColumn(
                name: "sNguoiSua",
                table: "BH_KHC_CheDoBHXH",
                newName: "SNguoiSua");

            migrationBuilder.RenameColumn(
                name: "iTongSoUocThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "iTongSoSQ",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoSQ");

            migrationBuilder.RenameColumn(
                name: "iTongSoQNCN",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoQNCN");

            migrationBuilder.RenameColumn(
                name: "iTongSoLDHD",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoLDHD");

            migrationBuilder.RenameColumn(
                name: "iTongSoKeHoachThucHienNamNay",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "iTongSoHSQBS",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoHSQBS");

            migrationBuilder.RenameColumn(
                name: "iTongSoDaThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "iTongSoCNVQP",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoCNVQP");

            migrationBuilder.RenameColumn(
                name: "iLoaiTongHop",
                table: "BH_KHC_CheDoBHXH",
                newName: "ILoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "iID_TongHopID",
                table: "BH_KHC_CheDoBHXH",
                newName: "IID_TongHopID");

            migrationBuilder.RenameColumn(
                name: "fTongTienUocThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "fTongTienSQ",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienSQ");

            migrationBuilder.RenameColumn(
                name: "fTongTienQNCN",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienQNCN");

            migrationBuilder.RenameColumn(
                name: "fTongTienLDHD",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienLDHD");

            migrationBuilder.RenameColumn(
                name: "fTongTienKeHoachThucHienNamNay",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "fTongTienHSQBS",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienHSQBS");

            migrationBuilder.RenameColumn(
                name: "fTongTienDaThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "fTongTienCNVQP",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienCNVQP");

            migrationBuilder.RenameColumn(
                name: "dNgayTao",
                table: "BH_KHC_CheDoBHXH",
                newName: "DNgayTao");

            migrationBuilder.RenameColumn(
                name: "dNgaySua",
                table: "BH_KHC_CheDoBHXH",
                newName: "DNgaySua");

            migrationBuilder.RenameColumn(
                name: "bIsKhoa",
                table: "BH_KHC_CheDoBHXH",
                newName: "BIsKhoa");

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHT_BHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sTenDonVi",
                table: "BH_KHT_BHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bDaTongHop",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.Sql(
           @"
            IF EXISTS (
                SELECT *
                FROM sys.indexes 
                WHERE name = 'idx_bangluong' AND object_id = OBJECT_ID('TL_BangLuong_Thang')
            )
                DROP INDEX TL_BangLuong_Thang.idx_bangluong
            ");

            migrationBuilder.CreateIndex(
                name: "idx_bangluong",
                table: "TL_BangLuong_Thang",
                columns: new[] { "Thang", "Nam", "Ma_DonVi" });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.9.7.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.9.7_salary.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_KHT_BHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sTenDonVi",
                table: "BH_KHT_BHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "bDaTongHop",
                table: "BH_KHT_BHXH");

            migrationBuilder.RenameColumn(
                name: "STongHop",
                table: "BH_KHC_CheDoBHXH",
                newName: "sTongHop");

            migrationBuilder.RenameColumn(
                name: "SNguoiTao",
                table: "BH_KHC_CheDoBHXH",
                newName: "sNguoiTao");

            migrationBuilder.RenameColumn(
                name: "SNguoiSua",
                table: "BH_KHC_CheDoBHXH",
                newName: "sNguoiSua");

            migrationBuilder.RenameColumn(
                name: "ITongSoUocThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "ITongSoSQ",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoSQ");

            migrationBuilder.RenameColumn(
                name: "ITongSoQNCN",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoQNCN");

            migrationBuilder.RenameColumn(
                name: "ITongSoLDHD",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoLDHD");

            migrationBuilder.RenameColumn(
                name: "ITongSoKeHoachThucHienNamNay",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "ITongSoHSQBS",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoHSQBS");

            migrationBuilder.RenameColumn(
                name: "ITongSoDaThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "ITongSoCNVQP",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoCNVQP");

            migrationBuilder.RenameColumn(
                name: "ILoaiTongHop",
                table: "BH_KHC_CheDoBHXH",
                newName: "iLoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "IID_TongHopID",
                table: "BH_KHC_CheDoBHXH",
                newName: "iID_TongHopID");

            migrationBuilder.RenameColumn(
                name: "FTongTienUocThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "FTongTienSQ",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienSQ");

            migrationBuilder.RenameColumn(
                name: "FTongTienQNCN",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienQNCN");

            migrationBuilder.RenameColumn(
                name: "FTongTienLDHD",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienLDHD");

            migrationBuilder.RenameColumn(
                name: "FTongTienKeHoachThucHienNamNay",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "FTongTienHSQBS",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienHSQBS");

            migrationBuilder.RenameColumn(
                name: "FTongTienDaThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "FTongTienCNVQP",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienCNVQP");

            migrationBuilder.RenameColumn(
                name: "DNgayTao",
                table: "BH_KHC_CheDoBHXH",
                newName: "dNgayTao");

            migrationBuilder.RenameColumn(
                name: "DNgaySua",
                table: "BH_KHC_CheDoBHXH",
                newName: "dNgaySua");

            migrationBuilder.RenameColumn(
                name: "BIsKhoa",
                table: "BH_KHC_CheDoBHXH",
                newName: "bIsKhoa");
        }
    }
}
