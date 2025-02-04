using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11506 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HT_ErrorDatabaseLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Description = table.Column<string>(nullable: true),
                    IsFixed = table.Column<bool>(nullable: false),
                    Object = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HT_ErrorDatabaseLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TN_QuyetToan_ChungTuChiTiet_HD4554",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fSoTien = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_TN_QTChungTu = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true),
                    iNguonNganSach = table.Column<int>(nullable: true),
                    iThangQuy = table.Column<int>(nullable: true),
                    iThangQuyLoai = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(maxLength: 50, nullable: true),
                    sK = table.Column<string>(maxLength: 50, nullable: true),
                    sL = table.Column<string>(maxLength: 50, nullable: true),
                    sLNS = table.Column<string>(maxLength: 50, nullable: true),
                    sM = table.Column<string>(maxLength: 50, nullable: true),
                    sNG = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTM = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG1 = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG2 = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG3 = table.Column<string>(maxLength: 50, nullable: true),
                    sTTM = table.Column<string>(maxLength: 50, nullable: true),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN_QuyetToan_ChungTuChiTiet_HD4554", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "TN_QuyetToan_ChungTu_HD4554",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: true),
                    bSent = table.Column<bool>(nullable: true),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongSoTien = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true),
                    iNguonNganSach = table.Column<int>(nullable: true),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    iThangQuy = table.Column<int>(nullable: true),
                    iThangQuyLoai = table.Column<int>(nullable: true),
                    sDSLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    sThangQuy_MoTa = table.Column<string>(maxLength: 50, nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN_QuyetToan_ChungTu_HD4554", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.6_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.6_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.6_budget_5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HT_ErrorDatabaseLog");

            migrationBuilder.DropTable(
                name: "TN_QuyetToan_ChungTuChiTiet_HD4554");

            migrationBuilder.DropTable(
                name: "TN_QuyetToan_ChungTu_HD4554");
        }
    }
}
