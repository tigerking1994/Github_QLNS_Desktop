using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VDT_KHV_PhanBoVon_ChiPhi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bActive = table.Column<bool>(nullable: true),
                    bIsGoc = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(nullable: true),
                    fGiaTriDuocDuyet = table.Column<double>(nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_LoaiNguonVonID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_ParentId = table.Column<Guid>(nullable: true),
                    iID_PhanBoGoc_ChiPhiID = table.Column<Guid>(nullable: true),
                    iLoai = table.Column<int>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sLoaiDieuChinh = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_PhanBoVon_ChiPhi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_PhanBoVon_ChiPhi_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fGiaTriPheDuyet = table.Column<double>(nullable: true),
                    fGiaTriPheDuyetDC = table.Column<double>(nullable: true),
                    iID_DanhMuc_DT_chi = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinh = table.Column<Guid>(nullable: true),
                    iId_Parent = table.Column<Guid>(nullable: true),
                    iID_PhanBoVon_ChiPhi_ID = table.Column<Guid>(nullable: true),
                    ILoaiDuAn = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sTrangThaiDuAnDangKy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_PhanBoVon_ChiPhi_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TT_DeNghiThanhToan_ChiPhi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bKhoa = table.Column<bool>(nullable: true),
                    bTongHop = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DeNghiThanhToanID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DuAnId = table.Column<Guid>(nullable: true),
                    iID_LoaiNguonVonID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_Parent = table.Column<Guid>(nullable: true),
                    iID_PhanBoVon_ChiPhi_ID = table.Column<Guid>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sGhiChuPheDuyet = table.Column<string>(type: "ntext", nullable: true),
                    sLyDoTuChoi = table.Column<string>(type: "ntext", nullable: true),
                    sNguoiLap = table.Column<string>(nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TT_DeNghiThanhToan_ChiPhi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TT_DeNghiThanhToan_ChiPhi_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fGiaTriDeNghi = table.Column<double>(nullable: true),
                    iID_DeNghiThanhToan_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_NoiDungChi = table.Column<Guid>(nullable: true),
                    sGhiChu = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TT_DeNghiThanhToan_ChiPhi_ChiTiet", x => x.Id);
                });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.0.0.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VDT_KHV_PhanBoVon_ChiPhi");

            migrationBuilder.DropTable(
                name: "VDT_KHV_PhanBoVon_ChiPhi_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_TT_DeNghiThanhToan_ChiPhi");

            migrationBuilder.DropTable(
                name: "VDT_TT_DeNghiThanhToan_ChiPhi_ChiTiet");
        }
    }
}
