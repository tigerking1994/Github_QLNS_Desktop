using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11301 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "iID_DonVi",
                table: "BH_KHTM_BHYT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BhKhcKinhphiQuanlys",
                columns: table => new
                {
                    iID_BH_KHC_KinhPhiQuanLy = table.Column<Guid>(nullable: false),
                    BIsKhoa = table.Column<bool>(nullable: false),
                    DNgayChungTu = table.Column<DateTime>(nullable: true),
                    DNgayQuyetDinh = table.Column<DateTime>(nullable: true),
                    DNgaySua = table.Column<DateTime>(nullable: true),
                    DNgayTao = table.Column<DateTime>(nullable: true),
                    FTongTienCanBo = table.Column<double>(nullable: true),
                    FTongTienDaThucHienNamTruoc = table.Column<double>(nullable: true),
                    FTongTienKeHoachThucHienNamNay = table.Column<double>(nullable: true),
                    FTongTienQuanLuc = table.Column<double>(nullable: true),
                    FTongTienQuanY = table.Column<double>(nullable: true),
                    FTongTienTaiChinh = table.Column<double>(nullable: true),
                    FTongTienUocThucHienNamTruoc = table.Column<double>(nullable: true),
                    IID_MaDonVi = table.Column<string>(nullable: true),
                    IID_TongHopID = table.Column<Guid>(nullable: true),
                    IIdDonViId = table.Column<Guid>(nullable: true),
                    ILoaiTongHop = table.Column<int>(nullable: false),
                    INamChungTu = table.Column<int>(nullable: true),
                    SMoTa = table.Column<string>(nullable: true),
                    SNguoiSua = table.Column<string>(nullable: true),
                    SNguoiTao = table.Column<string>(nullable: true),
                    SSoChungTu = table.Column<string>(nullable: true),
                    SSoQuyetDinh = table.Column<string>(nullable: true),
                    STongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BhKhcKinhphiQuanlys", x => x.iID_BH_KHC_KinhPhiQuanLy);
                });

            migrationBuilder.CreateTable(
                name: "BhKhcKinhphiQuanlyChiTiets",
                columns: table => new
                {
                    iID_BH_KHC_KinhPhiQuanLy_ChiTiet = table.Column<Guid>(nullable: false),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    fTienCanBo = table.Column<double>(nullable: true),
                    fTienDaThucHienNamTruoc = table.Column<double>(nullable: true),
                    fTienKeHoachThucHienNamNay = table.Column<double>(nullable: true),
                    fTienQuanLuc = table.Column<double>(nullable: true),
                    fTienQuanY = table.Column<double>(nullable: true),
                    fTienTaiChinh = table.Column<double>(nullable: true),
                    fTienUocThucHienNamTruoc = table.Column<double>(nullable: true),
                    iID_KHC_KinhPhiQuanLy = table.Column<Guid>(nullable: false),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sM = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true),
                    sTM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BhKhcKinhphiQuanlyChiTiets", x => x.iID_BH_KHC_KinhPhiQuanLy_ChiTiet);
                });

            migrationBuilder.CreateTable(
                name: "BH_KHTM_BHYT_ChiTiet",
                columns: table => new
                {
                    iID_KHTM_BHYT_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fDinhMuc = table.Column<float>(nullable: true),
                    fThanhTien = table.Column<float>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_NoiDung = table.Column<Guid>(nullable: false),
                    iSoNguoi = table.Column<int>(nullable: true),
                    iSoThang = table.Column<int>(nullable: true),
                    iID_KHTM_BHYT = table.Column<Guid>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sTenDonVi = table.Column<string>(nullable: true),
                    sTenNoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_KHTM_BHYT_ChiTiet", x => x.iID_KHTM_BHYT_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });
            //migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.1_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.1_budget.sql");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BhKhcKinhphiQuanlys");

            migrationBuilder.DropTable(
                name: "BhKhcKinhphiQuanlyChiTiets");

            migrationBuilder.DropTable(
                name: "BH_KHTM_BHYT_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_DonVi",
                table: "BH_KHTM_BHYT");
        }
    }
}
