using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11315 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BH_QTC_Nam_CheDoBHXH",
                columns: table => new
                {
                    ID_QTC_Nam_CheDoBHXH = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    bThucChiTheo4Quy = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    fTongTienCNVCQP_DeNghi = table.Column<double>(nullable: true),
                    fTongTien_DeNghi = table.Column<double>(nullable: true),
                    fTongTien_DuToanDuyet = table.Column<double>(nullable: true),
                    fTongTienHSQBS_DeNghi = table.Column<double>(nullable: true),
                    fTongTien_LuyKeCuoiQuyNay = table.Column<double>(nullable: true),
                    fTongTien_PheDuyet = table.Column<double>(nullable: true),
                    fTongTienQNCN_DeNghi = table.Column<double>(nullable: true),
                    fTongTienSQ_DeNghi = table.Column<double>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_TongHopID = table.Column<Guid>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: false),
                    iTongSoCNVCQP_DeNghi = table.Column<int>(nullable: false),
                    iTongSo_DeNghi = table.Column<int>(nullable: false),
                    iTongSoHSQBS_DeNghi = table.Column<int>(nullable: false),
                    iTongSo_LuyKeCuoiQuyNay = table.Column<int>(nullable: false),
                    iTongSoQNCN_DeNghi = table.Column<int>(nullable: false),
                    iTongSoSQ_DeNghi = table.Column<int>(nullable: false),
                    sMoTa = table.Column<string>(nullable: true),
                    SNguoiSua = table.Column<string>(nullable: true),
                    SNguoiTao = table.Column<string>(nullable: true),
                    SSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Nam_CheDoBHXH", x => x.ID_QTC_Nam_CheDoBHXH)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                columns: table => new
                {
                    ID_QTC_Nam_CheDoBHXH_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTiLeThucHienTrenDuToan = table.Column<double>(nullable: true),
                    fTienCNVCQP_ThucChi = table.Column<double>(nullable: true),
                    fTienDuToanDuyet = table.Column<double>(nullable: true),
                    fTienHSQBS_ThucChi = table.Column<double>(nullable: true),
                    fTienQNCN_ThucChi = table.Column<double>(nullable: true),
                    fTienSQ_ThucChi = table.Column<double>(nullable: true),
                    fTienThieu = table.Column<double>(nullable: true),
                    FTienThua = table.Column<double>(nullable: true),
                    fTongTien_ThucChi = table.Column<double>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: false),
                    iID_QTC_Nam_CheDoBHXH = table.Column<Guid>(nullable: false),
                    iSoCNVCQP_ThucChi = table.Column<int>(nullable: false),
                    iSoHSQBS_ThucChi = table.Column<int>(nullable: false),
                    iSoQNCN_ThucChi = table.Column<int>(nullable: false),
                    iSoSQ_ThucChi = table.Column<int>(nullable: false),
                    iTongSo_ThucChi = table.Column<int>(nullable: false),
                    sLoaiTroCap = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Nam_CheDoBHXH_ChiTiet", x => x.ID_QTC_Nam_CheDoBHXH_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.5_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.5_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.5_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.5_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_QTC_Nam_CheDoBHXH");

            migrationBuilder.DropTable(
                name: "BH_QTC_Nam_CheDoBHXH_ChiTiet");
        }
    }
}
