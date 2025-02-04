using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11305 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BH_DTC_DuToanChiTrenGiao",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongTien = table.Column<double>(nullable: true),
                    fTongTienHienVat = table.Column<double>(nullable: true),
                    fTongTienTuChi = table.Column<double>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iLoaiDotNhanPhanBo = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: false),
                    sLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTC_DuToanChiTrenGiao", x => x.ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTC_DuToanChiTrenGiao_ChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTienHienVat = table.Column<double>(nullable: true),
                    fTienTuChi = table.Column<double>(nullable: true),
                    fTongTien = table.Column<double>(nullable: true),
                    iID_DTC_DuToanChiTrenGiao = table.Column<Guid>(nullable: false),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: false),
                    sLNS = table.Column<string>(nullable: true),
                    sM = table.Column<string>(nullable: true),
                    sNG = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true),
                    sTM = table.Column<string>(nullable: true),
                    sTTM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTC_DuToanChiTrenGiao_ChiTiet", x => x.ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.5_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.5_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.5_investment_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.5_investment_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.5_investment_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.5_investment_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DTC_DuToanChiTrenGiao");

            migrationBuilder.DropTable(
                name: "BH_DTC_DuToanChiTrenGiao_ChiTiet");
        }
    }
}
