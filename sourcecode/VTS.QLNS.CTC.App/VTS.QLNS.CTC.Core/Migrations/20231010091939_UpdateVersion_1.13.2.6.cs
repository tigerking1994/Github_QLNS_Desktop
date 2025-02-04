using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11326 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BH_QTC_Quy_KPK",
                columns: table => new
                {
                    ID_QTC_Quy_KPK = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongTienDeNghiQuyetToanQuyNay = table.Column<double>(nullable: true),
                    fTongTienQuyetToanDaDuyet = table.Column<double>(nullable: true),
                    fTongTienThucChi = table.Column<double>(nullable: true),
                    fTongTienXacNhanQuyetToanQuyNay = table.Column<double>(nullable: true),
                    fTongTien_DuToanGiaoNamNay = table.Column<double>(nullable: true),
                    fTongTien_DuToanNamTruocChuyenSang = table.Column<double>(nullable: true),
                    fTongTien_TongDuToanDuocGiao = table.Column<double>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iID_LoaiChi = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_TongHopID = table.Column<Guid>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: false),
                    iQuyChungTu = table.Column<int>(nullable: false),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Quy_KPK", x => x.ID_QTC_Quy_KPK)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTC_Quy_KPK_ChiTiet",
                columns: table => new
                {
                    ID_QTC_Quy_KPK_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTienDeNghiQuyetToanQuyNay = table.Column<double>(nullable: true),
                    fTienQuyetToanDaDuyet = table.Column<double>(nullable: true),
                    fTienThucChi = table.Column<double>(nullable: true),
                    fTienXacNhanQuyetToanQuyNay = table.Column<double>(nullable: true),
                    fTien_DuToanGiaoNamNay = table.Column<double>(nullable: true),
                    fTien_DuToanNamTruocChuyenSang = table.Column<double>(nullable: true),
                    fTien_TongDuToanDuocGiao = table.Column<double>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: true),
                    iID_QTC_Quy_KPK = table.Column<Guid>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Quy_KPK_ChiTiet", x => x.ID_QTC_Quy_KPK_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.6_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_QTC_Quy_KPK");

            migrationBuilder.DropTable(
                name: "BH_QTC_Quy_KPK_ChiTiet");
        }
    }
}
