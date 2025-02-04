using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11327 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BH_QTC_Quy_KCB",
                columns: table => new
                {
                    ID_QTC_Quy_KCB = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: false),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(nullable: false),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    FTongTienDeNghiQuyetToanQuyNay = table.Column<double>(nullable: true),
                    fTongTien_DuToanGiaoNamNay = table.Column<double>(nullable: true),
                    fTongTien_DuToanNamTruocChuyenSang = table.Column<double>(nullable: true),
                    FTongTienQuyetToanDaDuyet = table.Column<double>(nullable: true),
                    FTongTienThucChi = table.Column<double>(nullable: true),
                    fTongTien_TongDuToanDuocGiao = table.Column<double>(nullable: true),
                    FTongTienXacNhanQuyetToanQuyNay = table.Column<double>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_TongHopID = table.Column<Guid>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: false),
                    iQuyChungTu = table.Column<int>(nullable: false),
                    sDSSoChungTuTongHop = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Quy_KCB", x => x.ID_QTC_Quy_KCB)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTC_Quy_KCB_ChiTiet",
                columns: table => new
                {
                    ID_QTC_Quy_KCB_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    FTienDeNghiQuyetToanQuyNay = table.Column<double>(nullable: true),
                    fTien_DuToanGiaoNamNay = table.Column<double>(nullable: true),
                    fTien_DuToanNamTruocChuyenSang = table.Column<double>(nullable: true),
                    FTienQuyetToanDaDuyet = table.Column<double>(nullable: true),
                    FTienThucChi = table.Column<double>(nullable: true),
                    fTien_TongDuToanDuocGiao = table.Column<double>(nullable: true),
                    FTienXacNhanQuyetToanQuyNay = table.Column<double>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: false),
                    iID_QTC_Quy_KCB = table.Column<Guid>(nullable: false),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Quy_KCB_ChiTiet", x => x.ID_QTC_Quy_KCB_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.7_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.7_budget_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.7_budget_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_QTC_Quy_KCB");

            migrationBuilder.DropTable(
                name: "BH_QTC_Quy_KCB_ChiTiet");
        }
    }
}
