using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11197 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "iID_BCQuyetToanNienDo",
                table: "VDT_ThongTri",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NH_QT_ChuyenQuyetToan",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "''"),
                    iLoaiThoiGian = table.Column<int>(nullable: true),
                    iThoiGian = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(type: "ntext", nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_ChuyenQuyetToan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_ChuyenQuyetToan_ChiTiet",
                columns: table => new
                {
                    iID_ChuyenQuyetToanChiTietID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    bLaHangCha = table.Column<bool>(nullable: false),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    iID_ChuyenQuyetToanID = table.Column<Guid>(nullable: true),
                    iID_MaMucLucNganSach = table.Column<Guid>(nullable: false),
                    iID_MaMucLucNganSach_Cha = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sK = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sL = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sLNS = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sMoTa = table.Column<string>(nullable: true),
                    sNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_ChuyenQuyetToan_ChiTiet", x => x.iID_ChuyenQuyetToanChiTietID);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_PheDuyetQuyetToanDAHT",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    bIsXoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    dNgayPheDuyet = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "''"),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iNamBaoCaoDen = table.Column<int>(nullable: true),
                    iNamBaoCaoTu = table.Column<int>(nullable: true),
                    SMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoPheDuyet = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_PheDuyetQuyetToanDAHT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_PheDuyetQuyetToanDAHT_ChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    fHopDong_USD = table.Column<double>(nullable: true),
                    fHopDong_VND = table.Column<double>(nullable: true),
                    fKeHoach_TTCP_USD = table.Column<double>(nullable: true),
                    fKinhPhiDuocCap_Tong_USD = table.Column<double>(nullable: true),
                    fKinhPhiDuocCap_Tong_VND = table.Column<double>(nullable: true),
                    fQuyetToanDuocDuyet_Tong_USD = table.Column<double>(nullable: true),
                    fQuyetToanDuocDuyet_Tong_VND = table.Column<double>(nullable: true),
                    fSoSanhKinhPhi_USD = table.Column<double>(nullable: true),
                    fSoSanhKinhPhi_VND = table.Column<double>(nullable: true),
                    fThuaTraNSNN_USD = table.Column<double>(nullable: true),
                    fThuaTraNSNN_VND = table.Column<double>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_PheDuyetQuyetToanDAHT_ID = table.Column<Guid>(nullable: true),
                    iID_ThanhToan_ChiTietID = table.Column<Guid>(nullable: true),
                    iNamBaoCaoDen = table.Column<int>(nullable: true),
                    iNamBaoCaoTu = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_PheDuyetQuyetToanDAHT_ChiTiet", x => x.ID);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.9.7.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NH_QT_ChuyenQuyetToan");

            migrationBuilder.DropTable(
                name: "NH_QT_ChuyenQuyetToan_ChiTiet");

            migrationBuilder.DropTable(
                name: "NH_QT_PheDuyetQuyetToanDAHT");

            migrationBuilder.DropTable(
                name: "NH_QT_PheDuyetQuyetToanDAHT_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_BCQuyetToanNienDo",
                table: "VDT_ThongTri");
        }
    }
}
