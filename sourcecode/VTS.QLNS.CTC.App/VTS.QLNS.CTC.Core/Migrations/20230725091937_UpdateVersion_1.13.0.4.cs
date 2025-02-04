using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11304 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fGiaTrungThauEUR",
                table: "NH_DA_GoiThau",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fGiaTrungThauUSD",
                table: "NH_DA_GoiThau",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fGiaTrungThauVND",
                table: "NH_DA_GoiThau",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BH_DTT_BHXH_ChungTu",
                columns: table => new
                {
                    iID_DTT_BHXH = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fDuToan = table.Column<double>(nullable: true),
                    fTongBHTN = table.Column<double>(nullable: true),
                    fBHTN_NLD = table.Column<double>(nullable: true),
                    fBHTN_NSD = table.Column<double>(nullable: true),
                    fTongBHXH = table.Column<double>(nullable: true),
                    fBHXH_NLD = table.Column<double>(nullable: true),
                    fBHXH_NSD = table.Column<double>(nullable: true),
                    fBHYT_NLD = table.Column<double>(nullable: true),
                    fBHYT_NSD = table.Column<double>(nullable: true),
                    fTongBHYT = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iLoaiDuToan = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sDSLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTT_BHXH_ChungTu", x => x.iID_DTT_BHXH)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTT_BHXH_ChungTu_ChiTiet",
                columns: table => new
                {
                    iID_DTT_BHXH_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DTT_BHXH = table.Column<Guid>(nullable: false),
                    fThu_BHTN_NLD = table.Column<double>(nullable: true),
                    fThu_BHTN_NSD = table.Column<double>(nullable: true),
                    fThu_BHXH_NLD = table.Column<double>(nullable: true),
                    fThu_BHXH_NSD = table.Column<double>(nullable: true),
                    fThu_BHYT_NLD = table.Column<double>(nullable: true),
                    fThu_BHYT_NSD = table.Column<double>(nullable: true),
                    fTongCong = table.Column<double>(nullable: true),
                    fTongThuBHTN = table.Column<double>(nullable: true),
                    fTongThuBHXH = table.Column<double>(nullable: true),
                    fTongThuBHYT = table.Column<double>(nullable: true),
                    iID_LoaiDoiTuong = table.Column<Guid>(nullable: true),
                    iID_MLNS = table.Column<Guid>(nullable: true),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sK = table.Column<string>(nullable: true),
                    sL = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sLoaiDoiTuong = table.Column<string>(nullable: true),
                    sM = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNG = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sTM = table.Column<string>(nullable: true),
                    sTNG = table.Column<string>(nullable: true),
                    sTng1 = table.Column<string>(nullable: true),
                    sTng2 = table.Column<string>(nullable: true),
                    sTng3 = table.Column<string>(nullable: true),
                    STtm = table.Column<string>(nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTT_BHXH_ChungTu_ChiTiet", x => x.iID_DTT_BHXH_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_KHC_KCB",
                columns: table => new
                {
                    iID_BH_KHC_KCB = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(nullable: true),
                    DNgaySua = table.Column<DateTime>(nullable: true),
                    DNgayTao = table.Column<DateTime>(nullable: true),
                    FTongTienDaThucHienNamTruoc = table.Column<double>(nullable: true),
                    FTongTienKeHoachThucHienNamNay = table.Column<double>(nullable: true),
                    FTongTienUocThucHienNamTruoc = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    IID_TongHopID = table.Column<Guid>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    ILoaiKCB = table.Column<int>(nullable: false),
                    ILoaiTongHop = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    SNguoiSua = table.Column<string>(nullable: true),
                    SNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    STongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_KHC_KCB", x => x.iID_BH_KHC_KCB);
                });

            migrationBuilder.CreateTable(
                name: "BH_KHC_KCB_ChiTiet",
                columns: table => new
                {
                    iID_BH_KHC_KCB_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    fTienDaThucHienNamTruoc = table.Column<double>(nullable: true),
                    fTienKeHoachThucHienNamNay = table.Column<double>(nullable: true),
                    fTienUocThucHienNamTruoc = table.Column<double>(nullable: true),
                    iID_KHC_KCB = table.Column<Guid>(nullable: false),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_KHC_KCB_ChiTiet", x => x.iID_BH_KHC_KCB_ChiTiet);
                });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.4_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.4_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.4_forex.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DTT_BHXH_ChungTu");

            migrationBuilder.DropTable(
                name: "BH_DTT_BHXH_ChungTu_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_KHC_KCB");

            migrationBuilder.DropTable(
                name: "BH_KHC_KCB_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fGiaTrungThauEUR",
                table: "NH_DA_GoiThau");

            migrationBuilder.DropColumn(
                name: "fGiaTrungThauUSD",
                table: "NH_DA_GoiThau");

            migrationBuilder.DropColumn(
                name: "fGiaTrungThauVND",
                table: "NH_DA_GoiThau");
        }
    }
}
