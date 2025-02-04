using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "iID_GoiThauId",
                table: "VDT_QT_QuyetToan_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_DeNghiQuyetToanID",
                table: "VDT_QT_QuyetToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThanhToanDeXuat",
                table: "VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThanhToanDeXuat",
                table: "VDT_KHV_PhanBoVon_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bActive",
                table: "VDT_KHV_KeHoachVonUng_DX",
                nullable: true,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<bool>(
                name: "bIsGoc",
                table: "VDT_KHV_KeHoachVonUng_DX",
                nullable: true,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<Guid>(
                name: "iID_ParentId",
                table: "VDT_KHV_KeHoachVonUng_DX",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bActive",
                table: "VDT_KHV_KeHoachVonUng",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bIsGoc",
                table: "VDT_KHV_KeHoachVonUng",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_ParentId",
                table: "VDT_KHV_KeHoachVonUng",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sSoTaiKhoan2",
                table: "VDT_DM_NhaThau",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sSoTaiKhoan3",
                table: "VDT_DM_NhaThau",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dBatDauBaoLanhHopDong",
                table: "VDT_DA_TT_HopDong",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dKetThucBaoLanhHopDong",
                table: "VDT_DA_TT_HopDong",
                type: "datetime",
                nullable: true);

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "iID_TiGia",
            //    table: "NH_TH_TongHop",
            //    nullable: true,
            //    oldClrType: typeof(double),
            //    oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iCoQuanThanhToan",
                table: "NH_TH_TongHop",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayDeNghi",
                table: "NH_TH_TongHop",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<int>(
                name: "iLoaiNoiDungChi",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iGiaiDoanDen_BQP",
                table: "NH_KHTongThe",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iGiaiDoanDen_TTCP",
                table: "NH_KHTongThe",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iGiaiDoanTu_BQP",
                table: "NH_KHTongThe",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iGiaiDoanTu_TTCP",
                table: "NH_KHTongThe",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dNgayTao",
                table: "NH_DM_NhaThau",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sNguoiTao",
                table: "NH_DM_NhaThau",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fGiaTriHopDong_EUR",
                table: "NH_DA_HopDong_GoiThau_NhaThau",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fGiaTriHopDong_NgoaiTeKhac",
                table: "NH_DA_HopDong_GoiThau_NhaThau",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fGiaTriHopDong_USD",
                table: "NH_DA_HopDong_GoiThau_NhaThau",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fGiaTriHopDong_VND",
                table: "NH_DA_HopDong_GoiThau_NhaThau",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DNgayKeHoach",
                table: "NH_DA_GoiThau",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_DuToanID",
                table: "NH_DA_GoiThau",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iCheckLuong",
                table: "NH_DA_GoiThau",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sSoKeHoachDatHang",
                table: "NH_DA_GoiThau",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iLoaiDuToan",
                table: "NH_DA_DuToan",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.3.4.0.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.3.4.1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iID_GoiThauId",
                table: "VDT_QT_QuyetToan_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_DeNghiQuyetToanID",
                table: "VDT_QT_QuyetToan");

            migrationBuilder.DropColumn(
                name: "fThanhToanDeXuat",
                table: "VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet");

            migrationBuilder.DropColumn(
                name: "fThanhToanDeXuat",
                table: "VDT_KHV_PhanBoVon_ChiTiet");

            migrationBuilder.DropColumn(
                name: "bActive",
                table: "VDT_KHV_KeHoachVonUng_DX");

            migrationBuilder.DropColumn(
                name: "bIsGoc",
                table: "VDT_KHV_KeHoachVonUng_DX");

            migrationBuilder.DropColumn(
                name: "iID_ParentId",
                table: "VDT_KHV_KeHoachVonUng_DX");

            migrationBuilder.DropColumn(
                name: "bActive",
                table: "VDT_KHV_KeHoachVonUng");

            migrationBuilder.DropColumn(
                name: "bIsGoc",
                table: "VDT_KHV_KeHoachVonUng");

            migrationBuilder.DropColumn(
                name: "iID_ParentId",
                table: "VDT_KHV_KeHoachVonUng");

            migrationBuilder.DropColumn(
                name: "sSoTaiKhoan2",
                table: "VDT_DM_NhaThau");

            migrationBuilder.DropColumn(
                name: "sSoTaiKhoan3",
                table: "VDT_DM_NhaThau");

            migrationBuilder.DropColumn(
                name: "dBatDauBaoLanhHopDong",
                table: "VDT_DA_TT_HopDong");

            migrationBuilder.DropColumn(
                name: "dKetThucBaoLanhHopDong",
                table: "VDT_DA_TT_HopDong");

            migrationBuilder.DropColumn(
                name: "iLoaiNoiDungChi",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iGiaiDoanDen_BQP",
                table: "NH_KHTongThe");

            migrationBuilder.DropColumn(
                name: "iGiaiDoanDen_TTCP",
                table: "NH_KHTongThe");

            migrationBuilder.DropColumn(
                name: "iGiaiDoanTu_BQP",
                table: "NH_KHTongThe");

            migrationBuilder.DropColumn(
                name: "iGiaiDoanTu_TTCP",
                table: "NH_KHTongThe");

            migrationBuilder.DropColumn(
                name: "dNgayTao",
                table: "NH_DM_NhaThau");

            migrationBuilder.DropColumn(
                name: "sNguoiTao",
                table: "NH_DM_NhaThau");

            migrationBuilder.DropColumn(
                name: "fGiaTriHopDong_EUR",
                table: "NH_DA_HopDong_GoiThau_NhaThau");

            migrationBuilder.DropColumn(
                name: "fGiaTriHopDong_NgoaiTeKhac",
                table: "NH_DA_HopDong_GoiThau_NhaThau");

            migrationBuilder.DropColumn(
                name: "fGiaTriHopDong_USD",
                table: "NH_DA_HopDong_GoiThau_NhaThau");

            migrationBuilder.DropColumn(
                name: "fGiaTriHopDong_VND",
                table: "NH_DA_HopDong_GoiThau_NhaThau");

            migrationBuilder.DropColumn(
                name: "DNgayKeHoach",
                table: "NH_DA_GoiThau");

            migrationBuilder.DropColumn(
                name: "iID_DuToanID",
                table: "NH_DA_GoiThau");

            migrationBuilder.DropColumn(
                name: "iCheckLuong",
                table: "NH_DA_GoiThau");

            migrationBuilder.DropColumn(
                name: "sSoKeHoachDatHang",
                table: "NH_DA_GoiThau");

            migrationBuilder.DropColumn(
                name: "iLoaiDuToan",
                table: "NH_DA_DuToan");

            migrationBuilder.AlterColumn<double>(
                name: "iID_TiGia",
                table: "NH_TH_TongHop",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iCoQuanThanhToan",
                table: "NH_TH_TongHop",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayDeNghi",
                table: "NH_TH_TongHop",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }
    }
}
