using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11236 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "iID_GoiThauId",
                table: "VDT_QT_DeNghiQuyetToan_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_LoaiQuyetToan",
                table: "VDT_QT_DeNghiQuyetToan",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_QuyetDinh",
                table: "VDT_QT_DeNghiQuyetToan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ten_NganHang",
                table: "TL_DM_PhuCap",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fSoCapPhat",
                table: "NS_CP_ChungTu",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fDonGia",
                table: "NH_DA_HopDong_HangMuc",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_HopDong_NhaThauID",
                table: "NH_DA_HopDong_HangMuc",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_KeHoachDatHang_DanhMucID",
                table: "NH_DA_HopDong_HangMuc",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iSoLuong",
                table: "NH_DA_HopDong_HangMuc",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sDonViTinh",
                table: "NH_DA_HopDong_HangMuc",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sGhiChu",
                table: "NH_DA_HopDong_HangMuc",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_KeHoachDatHangID",
                table: "NH_DA_HopDong",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NH_MSTN_KeHoachDatHang",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsXoa = table.Column<bool>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_MSTN_KeHoachDatHang", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_MSTN_KeHoachDatHang_DanhMuc",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fDonGia_VND = table.Column<double>(nullable: true),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_KeHoachDatHang = table.Column<Guid>(nullable: true),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iSoLuong = table.Column<int>(nullable: true),
                    sDonViTinh = table.Column<string>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sMaOrder = table.Column<string>(nullable: true),
                    sTenDanhMuc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_MSTN_KeHoachDatHang_DanhMuc", x => x.ID);
                });
            // VDT bản cũ vẫn thêm để tránh bị sót
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.3.5_investment.sql");
            // NH
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.3.6.1_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.3.6.2_forex.sql");
            // NS
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.3.6_budget.sql");
            // VDT
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.3.6_investment.sql");
            // LUONG
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.3.6_salary.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NH_MSTN_KeHoachDatHang");

            migrationBuilder.DropTable(
                name: "NH_MSTN_KeHoachDatHang_DanhMuc");

            migrationBuilder.DropColumn(
                name: "iID_GoiThauId",
                table: "VDT_QT_DeNghiQuyetToan_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_LoaiQuyetToan",
                table: "VDT_QT_DeNghiQuyetToan");

            migrationBuilder.DropColumn(
                name: "iID_QuyetDinh",
                table: "VDT_QT_DeNghiQuyetToan");

            migrationBuilder.DropColumn(
                name: "Ten_NganHang",
                table: "TL_DM_PhuCap");

            migrationBuilder.DropColumn(
                name: "fSoCapPhat",
                table: "NS_CP_ChungTu");

            migrationBuilder.DropColumn(
                name: "fDonGia",
                table: "NH_DA_HopDong_HangMuc");

            migrationBuilder.DropColumn(
                name: "iID_HopDong_NhaThauID",
                table: "NH_DA_HopDong_HangMuc");

            migrationBuilder.DropColumn(
                name: "iID_KeHoachDatHang_DanhMucID",
                table: "NH_DA_HopDong_HangMuc");

            migrationBuilder.DropColumn(
                name: "iSoLuong",
                table: "NH_DA_HopDong_HangMuc");

            migrationBuilder.DropColumn(
                name: "sDonViTinh",
                table: "NH_DA_HopDong_HangMuc");

            migrationBuilder.DropColumn(
                name: "sGhiChu",
                table: "NH_DA_HopDong_HangMuc");

            migrationBuilder.DropColumn(
                name: "iID_KeHoachDatHangID",
                table: "NH_DA_HopDong");
        }
    }
}
