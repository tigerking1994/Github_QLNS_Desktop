using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11246 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ID_DuAn_HangMuc",
                table: "VDT_TT_DeNghiThanhToan",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "iID_LoaiCongTrinh",
                table: "VDT_TongHop_NguonNSDauTu",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_LoaiCongTrinh",
                table: "VDT_QT_BCQuyetToanNienDo_PhanTich",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_LoaiCongTrinh",
                table: "VDT_QT_BCQuyetToanNienDo_ChiTiet_01",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_LoaiCongTrinh",
                table: "VDT_KT_KhoiTao_DuLieu_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_DuAn_HangMucID",
                table: "VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_DuAn_HangMucID",
                table: "VDT_KHV_PhanBoVon_DonVi_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_DuAn_HangMucID",
                table: "VDT_KHV_PhanBoVon_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ID_DuAn_HangMuc",
                table: "VDT_KHV_KeHoachVonUng_DX_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ID_DuAn_HangMuc",
                table: "VDT_KHV_KeHoachVonUng_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sStt",
                table: "VDT_KHV_KeHoach5Nam_ChiTiet",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTiGiaNhap",
                table: "NH_MSTN_KeHoachDatHang",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTiGiaNhap",
                table: "NH_HDNK_CacQuyetDinh",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTiGiaNhap",
                table: "NH_DA_KHLCNhaThau",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sThanhToanBang",
                table: "NH_DA_HopDong_HangMuc",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isCheck",
                table: "NH_DA_HopDong_GoiThau_NhaThau",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "FTiGiaNhap",
                table: "NH_DA_HopDong",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iIDGoiThauCheck",
                table: "NH_DA_GoiThau_HangMuc",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isCheck",
                table: "NH_DA_GoiThau_HangMuc",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_ChiPhiID",
                table: "NH_DA_GoiThau_ChiPhi",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTiGiaNhap",
                table: "NH_DA_GoiThau",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTiGiaNhap",
                table: "NH_DA_DuToan",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.4.6_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.4.6_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.4.6_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.4.6_forex.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_DuAn_HangMuc",
                table: "VDT_TT_DeNghiThanhToan");

            migrationBuilder.DropColumn(
                name: "iID_LoaiCongTrinh",
                table: "VDT_TongHop_NguonNSDauTu");

            migrationBuilder.DropColumn(
                name: "iID_LoaiCongTrinh",
                table: "VDT_QT_BCQuyetToanNienDo_PhanTich");

            migrationBuilder.DropColumn(
                name: "iID_LoaiCongTrinh",
                table: "VDT_QT_BCQuyetToanNienDo_ChiTiet_01");

            migrationBuilder.DropColumn(
                name: "iID_LoaiCongTrinh",
                table: "VDT_KT_KhoiTao_DuLieu_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_DuAn_HangMucID",
                table: "VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet");

            migrationBuilder.DropColumn(
                name: "iID_DuAn_HangMucID",
                table: "VDT_KHV_PhanBoVon_DonVi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_DuAn_HangMucID",
                table: "VDT_KHV_PhanBoVon_ChiTiet");

            migrationBuilder.DropColumn(
                name: "ID_DuAn_HangMuc",
                table: "VDT_KHV_KeHoachVonUng_DX_ChiTiet");

            migrationBuilder.DropColumn(
                name: "ID_DuAn_HangMuc",
                table: "VDT_KHV_KeHoachVonUng_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sStt",
                table: "VDT_KHV_KeHoach5Nam_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fTiGiaNhap",
                table: "NH_MSTN_KeHoachDatHang");

            migrationBuilder.DropColumn(
                name: "fTiGiaNhap",
                table: "NH_HDNK_CacQuyetDinh");

            migrationBuilder.DropColumn(
                name: "fTiGiaNhap",
                table: "NH_DA_KHLCNhaThau");

            migrationBuilder.DropColumn(
                name: "sThanhToanBang",
                table: "NH_DA_HopDong_HangMuc");

            migrationBuilder.DropColumn(
                name: "isCheck",
                table: "NH_DA_HopDong_GoiThau_NhaThau");

            migrationBuilder.DropColumn(
                name: "FTiGiaNhap",
                table: "NH_DA_HopDong");

            migrationBuilder.DropColumn(
                name: "iIDGoiThauCheck",
                table: "NH_DA_GoiThau_HangMuc");

            migrationBuilder.DropColumn(
                name: "isCheck",
                table: "NH_DA_GoiThau_HangMuc");

            migrationBuilder.DropColumn(
                name: "iID_ChiPhiID",
                table: "NH_DA_GoiThau_ChiPhi");

            migrationBuilder.DropColumn(
                name: "fTiGiaNhap",
                table: "NH_DA_GoiThau");

            migrationBuilder.DropColumn(
                name: "fTiGiaNhap",
                table: "NH_DA_DuToan");
        }
    }
}
