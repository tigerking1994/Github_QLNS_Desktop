using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11502 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TN_DTDN_ChungTu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    bSent = table.Column<bool>(nullable: true),
                    DNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    DNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    DNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongDuToan_NamKeHoach = table.Column<double>(nullable: true),
                    fTongDuToan_NamNay = table.Column<double>(nullable: true),
                    fTongThucThu_NamTruoc = table.Column<double>(nullable: true),
                    fTongUocThucHien_NamNay = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 500, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    sDSDonViTongHop = table.Column<string>(maxLength: 50, nullable: true),
                    sDSLNS = table.Column<string>(nullable: true),
                    sDSSoChungTuTongHop = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(maxLength: 250, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN_DTDN_ChungTu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TN_DTDN_ChungTuChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    bKhoa = table.Column<bool>(nullable: false),
                    DNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    DNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fDuToan_NamKeHoach = table.Column<double>(nullable: false),
                    fDuToan_NamNay = table.Column<double>(nullable: false),
                    fThucThu_NamTruoc = table.Column<double>(nullable: false),
                    fUocThucHien_NamNay = table.Column<double>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iNamNganSach = table.Column<int>(nullable: false),
                    iD_ChungTu = table.Column<Guid>(nullable: true),
                    sK = table.Column<string>(maxLength: 50, nullable: true),
                    sL = table.Column<string>(maxLength: 50, nullable: true),
                    sLns = table.Column<string>(maxLength: 50, nullable: true),
                    sM = table.Column<string>(maxLength: 50, nullable: true),
                    sNg = table.Column<string>(maxLength: 50, nullable: true),
                    sGhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    sTm = table.Column<string>(maxLength: 50, nullable: true),
                    sTng = table.Column<string>(maxLength: 50, nullable: true),
                    sTng1 = table.Column<string>(maxLength: 50, nullable: true),
                    sTng2 = table.Column<string>(maxLength: 50, nullable: true),
                    sTng3 = table.Column<string>(maxLength: 50, nullable: true),
                    sTtm = table.Column<string>(maxLength: 50, nullable: true),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN_DTDN_ChungTuChiTiet", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.2_budget_5.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.2_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TN_DTDN_ChungTu");

            migrationBuilder.DropTable(
                name: "TN_DTDN_ChungTuChiTiet");
        }
    }
}
