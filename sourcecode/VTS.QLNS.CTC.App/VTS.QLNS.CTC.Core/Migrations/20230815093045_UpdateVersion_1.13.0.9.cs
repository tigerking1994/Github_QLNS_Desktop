using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11309 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bHangCha",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet");

            migrationBuilder.AlterColumn<int>(
                name: "IPhanCap",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iDuLieuNhan",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "BH_DTT_BHXH_DieuChinh",
                columns: table => new
                {
                    iID_DTT_BHXH_DieuChinh = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fThuBHTN_Giam = table.Column<double>(nullable: true),
                    fThuBHTN_NLD = table.Column<double>(nullable: true),
                    fThuBHTN_NLD_Giam = table.Column<double>(nullable: true),
                    fThuBHTN_NLD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHTN_NLD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHTN_NLD_Tang = table.Column<double>(nullable: true),
                    fThuBHTN_NSD = table.Column<double>(nullable: true),
                    fThuBHTN_NSD_Giam = table.Column<double>(nullable: true),
                    fThuBHTN_NSD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHTN_NSD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHTN_NSD_Tang = table.Column<double>(nullable: true),
                    fThuBHTN_Tang = table.Column<double>(nullable: true),
                    fThuBHXH_Giam = table.Column<double>(nullable: true),
                    fThuBHXH_NLD = table.Column<double>(nullable: true),
                    fThuBHXH_NLD_Giam = table.Column<double>(nullable: true),
                    fThuBHXH_NLD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHXH_NLD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHXH_NLD_Tang = table.Column<double>(nullable: true),
                    fThuBHXH_NSD = table.Column<double>(nullable: true),
                    fThuBHXH_NSD_Giam = table.Column<double>(nullable: true),
                    fThuBHXH_NSD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHXH_NSD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHXH_NSD_Tang = table.Column<double>(nullable: true),
                    fThuBHXH_Tang = table.Column<double>(nullable: true),
                    fThuBHYT_Giam = table.Column<double>(nullable: true),
                    fThuBHYT_NLD = table.Column<double>(nullable: true),
                    fThuBHYT_NLD_Giam = table.Column<double>(nullable: true),
                    fThuBHYT_NLD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHYT_NLD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHYT_NLD_Tang = table.Column<double>(nullable: true),
                    fThuBHYT_NSD = table.Column<double>(nullable: true),
                    fThuBHYT_NSD_Giam = table.Column<double>(nullable: true),
                    fThuBHYT_NSD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHYT_NSD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHYT_NSD_Tang = table.Column<double>(nullable: true),
                    fThuBHYT_Tang = table.Column<double>(nullable: true),
                    fTongCong = table.Column<double>(nullable: true),
                    fTongThuBHTN_NLD = table.Column<double>(nullable: true),
                    fTongThuBHTN_NSD = table.Column<double>(nullable: true),
                    fTongThuBHXH_NLD = table.Column<double>(nullable: true),
                    fTongThuBHXH_NSD = table.Column<double>(nullable: true),
                    fTongThuBHYT_NLD = table.Column<double>(nullable: true),
                    fTongThuBHYT_NSD = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_TongHopID = table.Column<Guid>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTT_BHXH_DieuChinh", x => x.iID_DTT_BHXH_DieuChinh);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTT_BHXH_DieuChinh_ChiTiet",
                columns: table => new
                {
                    iID_DTT_BHXH_DieuChinh_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fThuBHTN_Giam = table.Column<double>(nullable: true),
                    fThuBHTN_NLD = table.Column<double>(nullable: true),
                    fThuBHTN_NLD_Giam = table.Column<double>(nullable: true),
                    fThuBHTN_NLD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHTN_NLD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHTN_NLD_Tang = table.Column<double>(nullable: true),
                    fThuBHTN_NSD = table.Column<double>(nullable: true),
                    fThuBHTN_NSD_Giam = table.Column<double>(nullable: true),
                    fThuBHTN_NSD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHTN_NSD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHTN_NSD_Tang = table.Column<double>(nullable: true),
                    fThuBHTN_Tang = table.Column<double>(nullable: true),
                    fThuBHXH_Giam = table.Column<double>(nullable: true),
                    fThuBHXH_NLD = table.Column<double>(nullable: true),
                    fThuBHXH_NLD_Giam = table.Column<double>(nullable: true),
                    fThuBHXH_NLD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHXH_NLD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHXH_NLD_Tang = table.Column<double>(nullable: true),
                    fThuBHXH_NSD = table.Column<double>(nullable: true),
                    fThuBHXH_NSD_Giam = table.Column<double>(nullable: true),
                    fThuBHXH_NSD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHXH_NSD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHXH_NSD_Tang = table.Column<double>(nullable: true),
                    fThuBHXH_Tang = table.Column<double>(nullable: true),
                    fThuBHYT_Giam = table.Column<double>(nullable: true),
                    fThuBHYT_NLD = table.Column<double>(nullable: true),
                    fThuBHYT_NLD_Giam = table.Column<double>(nullable: true),
                    fThuBHYT_NLD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHYT_NLD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHYT_NLD_Tang = table.Column<double>(nullable: true),
                    fThuBHYT_NSD = table.Column<double>(nullable: true),
                    fThuBHYT_NSD_Giam = table.Column<double>(nullable: true),
                    fThuBHYT_NSD_QTCuoiNam = table.Column<double>(nullable: true),
                    fThuBHYT_NSD_QTDauNam = table.Column<double>(nullable: true),
                    fThuBHYT_NSD_Tang = table.Column<double>(nullable: true),
                    fThuBHYT_Tang = table.Column<double>(nullable: true),
                    fTongCong = table.Column<double>(nullable: true),
                    fTongThuBHTN_NLD = table.Column<double>(nullable: true),
                    fTongThuBHTN_NSD = table.Column<double>(nullable: true),
                    fTongThuBHXH_NLD = table.Column<double>(nullable: true),
                    fTongThuBHXH_NSD = table.Column<double>(nullable: true),
                    fTongThuBHYT_NLD = table.Column<double>(nullable: true),
                    fTongThuBHYT_NSD = table.Column<double>(nullable: true),
                    iID_DTT_BHXH_DieuChinh = table.Column<Guid>(nullable: false),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTT_BHXH_DieuChinh_ChiTiet", x => x.iID_DTT_BHXH_DieuChinh_ChiTiet);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTTM_BHYT_ThanNhan_ChiTiet",
                columns: table => new
                {
                    iID_DTTM_BHYT_ThanNhan_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    fDuToan = table.Column<double>(nullable: true),
                    iID_DTTM_BHYT_ThanNhan = table.Column<Guid>(nullable: false),
                    iID_MLNS = table.Column<Guid>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTTM_BHYT_ThanNhan_ChiTiet", x => x.iID_DTTM_BHYT_ThanNhan_ChiTiet);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.9_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.9_social_insurance.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DTT_BHXH_DieuChinh");

            migrationBuilder.DropTable(
                name: "BH_DTT_BHXH_DieuChinh_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_DTTM_BHYT_ThanNhan_ChiTiet");

            migrationBuilder.AlterColumn<int>(
                name: "IPhanCap",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iDuLieuNhan",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bHangCha",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                nullable: false,
                defaultValue: false);
        }
    }
}
