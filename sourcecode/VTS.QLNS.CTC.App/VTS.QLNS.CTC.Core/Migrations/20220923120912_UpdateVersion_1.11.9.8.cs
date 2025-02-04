using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11198 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fLuyKeEUR",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeNgoaiTeKhac",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeUSD",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeVND",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongDeNghi_EUR",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongDeNghi_NgoaiTeKhac",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongDeNghi_USD",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongDeNghi_VND",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongPheDuyet_EUR",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongPheDuyet_NgoaiTeKhac",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongPheDuyet_USD",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongPheDuyet_VND",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_DuAnID",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iThanhToanTheo",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fNguyenGia",
                table: "NH_QT_TaiSan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fSoLuong",
                table: "NH_QT_TaiSan",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_ChungTuTaiSanID",
                table: "NH_QT_TaiSan",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_HopDongID",
                table: "NH_QT_TaiSan",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "iID_MaDonViID",
                table: "NH_QT_TaiSan",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "iLoaiTaiSan",
                table: "NH_QT_TaiSan",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iTinhTrangSuDung",
                table: "NH_QT_TaiSan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SDonViTinh",
                table: "NH_QT_TaiSan",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NH_QT_ChungTuTaiSan",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgayChungTu = table.Column<DateTime>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sTenChungTu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_ChungTuTaiSan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_ThongTriQuyetToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    dNgayLap = table.Column<DateTime>(nullable: true),
                    fThongTri_USD = table.Column<double>(nullable: true),
                    fThongTri_VND = table.Column<double>(nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iLoaiNoiDungChi = table.Column<int>(nullable: true),
                    iLoaiThongTri = table.Column<int>(nullable: true),
                    iNamThongTri = table.Column<int>(nullable: true),
                    sSoThongTri = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_ThongTriQuyetToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_ThongTriQuyetToan_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    fDeNghiQuyetToanNam_USD = table.Column<float>(nullable: true),
                    fDeNghiQuyetToanNam_VND = table.Column<float>(nullable: true),
                    fThuaNopTraNSNN_USD = table.Column<float>(nullable: true),
                    fThuaNopTraNSNN_VND = table.Column<float>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_ThanhToan_ChiTietID = table.Column<Guid>(nullable: true),
                    iID_ThongTriQuyetToanID = table.Column<Guid>(nullable: true),
                    sMaThuTu = table.Column<string>(maxLength: 50, nullable: true),
                    sTenNoiDungChi = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_ThongTriQuyetToan_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_FtpFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    iID_FtpRoot = table.Column<Guid>(nullable: false),
                    sFileName = table.Column<string>(maxLength: 150, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 200, nullable: true),
                    sRootPath = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_FtpFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_FtpRoot",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    sFolderRoot = table.Column<string>(maxLength: 200, nullable: true),
                    sIpAddress = table.Column<string>(maxLength: 50, nullable: true),
                    sMaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_FtpRoot", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.9.8.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NH_QT_ChungTuTaiSan");

            migrationBuilder.DropTable(
                name: "NH_QT_ThongTriQuyetToan");

            migrationBuilder.DropTable(
                name: "NH_QT_ThongTriQuyetToan_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_FtpFile");

            migrationBuilder.DropTable(
                name: "VDT_FtpRoot");

            migrationBuilder.DropColumn(
                name: "fLuyKeEUR",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fLuyKeNgoaiTeKhac",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fLuyKeUSD",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fLuyKeVND",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTongDeNghi_EUR",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTongDeNghi_NgoaiTeKhac",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTongDeNghi_USD",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTongDeNghi_VND",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTongPheDuyet_EUR",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTongPheDuyet_NgoaiTeKhac",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTongPheDuyet_USD",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTongPheDuyet_VND",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "iID_DuAnID",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "iThanhToanTheo",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fNguyenGia",
                table: "NH_QT_TaiSan");

            migrationBuilder.DropColumn(
                name: "fSoLuong",
                table: "NH_QT_TaiSan");

            migrationBuilder.DropColumn(
                name: "iID_ChungTuTaiSanID",
                table: "NH_QT_TaiSan");

            migrationBuilder.DropColumn(
                name: "iID_HopDongID",
                table: "NH_QT_TaiSan");

            migrationBuilder.DropColumn(
                name: "iID_MaDonViID",
                table: "NH_QT_TaiSan");

            migrationBuilder.DropColumn(
                name: "iLoaiTaiSan",
                table: "NH_QT_TaiSan");

            migrationBuilder.DropColumn(
                name: "iTinhTrangSuDung",
                table: "NH_QT_TaiSan");

            migrationBuilder.DropColumn(
                name: "SDonViTinh",
                table: "NH_QT_TaiSan");
        }
    }
}
