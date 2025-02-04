using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11302 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BhKhcKinhphiQuanlyChiTiets",
                table: "BhKhcKinhphiQuanlyChiTiets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BhKhcKinhphiQuanlys",
                table: "BhKhcKinhphiQuanlys");

            migrationBuilder.DropColumn(
                name: "fThuBHYTNLDDong",
                table: "BH_KHTM_BHYT");

            migrationBuilder.DropColumn(
                name: "fThuBHYTNSDDong",
                table: "BH_KHTM_BHYT");

            migrationBuilder.RenameTable(
                name: "BhKhcKinhphiQuanlyChiTiets",
                newName: "BH_KHC_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.RenameTable(
                name: "BhKhcKinhphiQuanlys",
                newName: "BH_KHC_KinhPhiQuanLy");

            migrationBuilder.RenameColumn(
                name: "fTongBHYT",
                table: "BH_KHTM_BHYT",
                newName: "fTongThanhTien");

            migrationBuilder.RenameColumn(
                name: "fTong",
                table: "BH_KHTM_BHYT",
                newName: "fTongDinhMuc");

            migrationBuilder.RenameColumn(
                name: "FTongThuBHYT",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fTongThuBHYT");

            migrationBuilder.RenameColumn(
                name: "FTongThuBHXH",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fTongThuBHXH");

            migrationBuilder.RenameColumn(
                name: "FTongThuBHTN",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fTongThuBHTN");

            migrationBuilder.RenameColumn(
                name: "fTongQuyTienLuongNam",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fTongQTLN");

            migrationBuilder.RenameColumn(
                name: "fThuBHYTNguoiSuDungLaoDongDong",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThu_BHYT_NSD");

            migrationBuilder.RenameColumn(
                name: "fThuBHYTNguoiLaoDongDong",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThu_BHYT_NLD");

            migrationBuilder.RenameColumn(
                name: "fThuBHXHNguoiSuDungLaoDongDong",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThu_BHXH_NSD");

            migrationBuilder.RenameColumn(
                name: "fThuBHXHNguoiLaoDongDong",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThu_BHXH_NLD");

            migrationBuilder.RenameColumn(
                name: "fThuBHTNNguoiSuDungLaoDongDong",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThu_BHTN_NSD");

            migrationBuilder.RenameColumn(
                name: "fThuBHTNNguoiLaoDongDong",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThu_BHTN_NLD");

            migrationBuilder.RenameColumn(
                name: "fThuBHYTNSDDong",
                table: "BH_KHT_BHXH",
                newName: "fBHYT_NSD");

            migrationBuilder.RenameColumn(
                name: "fThuBHYTNLDDong",
                table: "BH_KHT_BHXH",
                newName: "fBHYT_NLD");

            migrationBuilder.RenameColumn(
                name: "fThuBHXHNSDDong",
                table: "BH_KHT_BHXH",
                newName: "fBHXH_NSD");

            migrationBuilder.RenameColumn(
                name: "fThuBHXHNLDDong",
                table: "BH_KHT_BHXH",
                newName: "fBHXH_NLD");

            migrationBuilder.RenameColumn(
                name: "fThuBHXH",
                table: "BH_KHT_BHXH",
                newName: "fTongBHXH");

            migrationBuilder.RenameColumn(
                name: "fThuBHTNNSDDong",
                table: "BH_KHT_BHXH",
                newName: "fBHTN_NSD");

            migrationBuilder.RenameColumn(
                name: "fThuBHTNNLDDong",
                table: "BH_KHT_BHXH",
                newName: "fBHTN_NLD");

            migrationBuilder.RenameColumn(
                name: "fThuBHTN",
                table: "BH_KHT_BHXH",
                newName: "fTongBHTN");

            migrationBuilder.RenameColumn(
                name: "SSoQuyetDinh",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "sSoQuyetDinh");

            migrationBuilder.RenameColumn(
                name: "SSoChungTu",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "sSoChungTu");

            migrationBuilder.RenameColumn(
                name: "SMoTa",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "sMoTa");

            migrationBuilder.RenameColumn(
                name: "INamChungTu",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "iNamChungTu");

            migrationBuilder.RenameColumn(
                name: "IIdDonViId",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "iID_DonVi");

            migrationBuilder.RenameColumn(
                name: "IID_MaDonVi",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "iID_MaDonVi");

            migrationBuilder.RenameColumn(
                name: "DNgayQuyetDinh",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "dNgayQuyetDinh");

            migrationBuilder.RenameColumn(
                name: "DNgayChungTu",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "dNgayChungTu");

            migrationBuilder.AddColumn<Guid>(
                name: "iID_ChiPhiId",
                table: "VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iLoai",
                table: "VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dNgayBanHanhKBNN",
                table: "NH_DM_TiGia",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamTiGia",
                table: "NH_DM_TiGia",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iThangTiGia",
                table: "NH_DM_TiGia",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sSoThongBaoKBNN",
                table: "NH_DM_TiGia",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "fThanhTien",
                table: "BH_KHTM_BHYT_ChiTiet",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "fDinhMuc",
                table: "BH_KHTM_BHYT_ChiTiet",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iTongSoNguoi",
                table: "BH_KHTM_BHYT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iTongSoThang",
                table: "BH_KHTM_BHYT",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_BH_KHC_KinhPhiQuanLy_ChiTiet",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_BH_KHC_KinhPhiQuanLy",
                table: "BH_KHC_KinhPhiQuanLy",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_KHC_KinhPhiQuanLy_ChiTiet",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                column: "iID_BH_KHC_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_KHC_KinhPhiQuanLy",
                table: "BH_KHC_KinhPhiQuanLy",
                column: "iID_BH_KHC_KinhPhiQuanLy");

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_KHC_KinhPhiQuanLy_ChiTiet",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_KHC_KinhPhiQuanLy",
                table: "BH_KHC_KinhPhiQuanLy");

            migrationBuilder.DropColumn(
                name: "iID_ChiPhiId",
                table: "VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan");

            migrationBuilder.DropColumn(
                name: "iLoai",
                table: "VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan");

            migrationBuilder.DropColumn(
                name: "dNgayBanHanhKBNN",
                table: "NH_DM_TiGia");

            migrationBuilder.DropColumn(
                name: "iNamTiGia",
                table: "NH_DM_TiGia");

            migrationBuilder.DropColumn(
                name: "iThangTiGia",
                table: "NH_DM_TiGia");

            migrationBuilder.DropColumn(
                name: "sSoThongBaoKBNN",
                table: "NH_DM_TiGia");

            migrationBuilder.DropColumn(
                name: "iTongSoNguoi",
                table: "BH_KHTM_BHYT");

            migrationBuilder.DropColumn(
                name: "iTongSoThang",
                table: "BH_KHTM_BHYT");

            migrationBuilder.RenameTable(
                name: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                newName: "BhKhcKinhphiQuanlyChiTiets");

            migrationBuilder.RenameTable(
                name: "BH_KHC_KinhPhiQuanLy",
                newName: "BhKhcKinhphiQuanlys");

            migrationBuilder.RenameColumn(
                name: "fTongThanhTien",
                table: "BH_KHTM_BHYT",
                newName: "fTongBHYT");

            migrationBuilder.RenameColumn(
                name: "fTongDinhMuc",
                table: "BH_KHTM_BHYT",
                newName: "fTong");

            migrationBuilder.RenameColumn(
                name: "fTongThuBHYT",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "FTongThuBHYT");

            migrationBuilder.RenameColumn(
                name: "fTongThuBHXH",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "FTongThuBHXH");

            migrationBuilder.RenameColumn(
                name: "fTongThuBHTN",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "FTongThuBHTN");

            migrationBuilder.RenameColumn(
                name: "fTongQTLN",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fTongQuyTienLuongNam");

            migrationBuilder.RenameColumn(
                name: "fThu_BHYT_NSD",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThuBHYTNguoiSuDungLaoDongDong");

            migrationBuilder.RenameColumn(
                name: "fThu_BHYT_NLD",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThuBHYTNguoiLaoDongDong");

            migrationBuilder.RenameColumn(
                name: "fThu_BHXH_NSD",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThuBHXHNguoiSuDungLaoDongDong");

            migrationBuilder.RenameColumn(
                name: "fThu_BHXH_NLD",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThuBHXHNguoiLaoDongDong");

            migrationBuilder.RenameColumn(
                name: "fThu_BHTN_NSD",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThuBHTNNguoiSuDungLaoDongDong");

            migrationBuilder.RenameColumn(
                name: "fThu_BHTN_NLD",
                table: "BH_KHT_BHXH_ChiTiet",
                newName: "fThuBHTNNguoiLaoDongDong");

            migrationBuilder.RenameColumn(
                name: "fBHYT_NSD",
                table: "BH_KHT_BHXH",
                newName: "fThuBHYTNSDDong");

            migrationBuilder.RenameColumn(
                name: "fBHYT_NLD",
                table: "BH_KHT_BHXH",
                newName: "fThuBHYTNLDDong");

            migrationBuilder.RenameColumn(
                name: "fBHXH_NSD",
                table: "BH_KHT_BHXH",
                newName: "fThuBHXHNSDDong");

            migrationBuilder.RenameColumn(
                name: "fBHXH_NLD",
                table: "BH_KHT_BHXH",
                newName: "fThuBHXHNLDDong");

            migrationBuilder.RenameColumn(
                name: "fTongBHXH",
                table: "BH_KHT_BHXH",
                newName: "fThuBHXH");

            migrationBuilder.RenameColumn(
                name: "fBHTN_NSD",
                table: "BH_KHT_BHXH",
                newName: "fThuBHTNNSDDong");

            migrationBuilder.RenameColumn(
                name: "fBHTN_NLD",
                table: "BH_KHT_BHXH",
                newName: "fThuBHTNNLDDong");

            migrationBuilder.RenameColumn(
                name: "fTongBHTN",
                table: "BH_KHT_BHXH",
                newName: "fThuBHTN");

            migrationBuilder.RenameColumn(
                name: "sSoQuyetDinh",
                table: "BhKhcKinhphiQuanlys",
                newName: "SSoQuyetDinh");

            migrationBuilder.RenameColumn(
                name: "sSoChungTu",
                table: "BhKhcKinhphiQuanlys",
                newName: "SSoChungTu");

            migrationBuilder.RenameColumn(
                name: "sMoTa",
                table: "BhKhcKinhphiQuanlys",
                newName: "SMoTa");

            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BhKhcKinhphiQuanlys",
                newName: "INamChungTu");

            migrationBuilder.RenameColumn(
                name: "iID_DonVi",
                table: "BhKhcKinhphiQuanlys",
                newName: "IIdDonViId");

            migrationBuilder.RenameColumn(
                name: "iID_MaDonVi",
                table: "BhKhcKinhphiQuanlys",
                newName: "IID_MaDonVi");

            migrationBuilder.RenameColumn(
                name: "dNgayQuyetDinh",
                table: "BhKhcKinhphiQuanlys",
                newName: "DNgayQuyetDinh");

            migrationBuilder.RenameColumn(
                name: "dNgayChungTu",
                table: "BhKhcKinhphiQuanlys",
                newName: "DNgayChungTu");

            migrationBuilder.AlterColumn<float>(
                name: "fThanhTien",
                table: "BH_KHTM_BHYT_ChiTiet",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "fDinhMuc",
                table: "BH_KHTM_BHYT_ChiTiet",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThuBHYTNLDDong",
                table: "BH_KHTM_BHYT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThuBHYTNSDDong",
                table: "BH_KHTM_BHYT",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_BH_KHC_KinhPhiQuanLy_ChiTiet",
                table: "BhKhcKinhphiQuanlyChiTiets",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_BH_KHC_KinhPhiQuanLy",
                table: "BhKhcKinhphiQuanlys",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BhKhcKinhphiQuanlyChiTiets",
                table: "BhKhcKinhphiQuanlyChiTiets",
                column: "iID_BH_KHC_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BhKhcKinhphiQuanlys",
                table: "BhKhcKinhphiQuanlys",
                column: "iID_BH_KHC_KinhPhiQuanLy");
        }
    }
}
