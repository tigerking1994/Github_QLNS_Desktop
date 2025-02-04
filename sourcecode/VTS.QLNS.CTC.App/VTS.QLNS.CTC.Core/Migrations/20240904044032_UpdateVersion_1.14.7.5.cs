using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11475 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fRutKBNN",
                table: "NS_DT_ChungTuChiTiet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fTongRutKBNN",
                table: "NS_DT_ChungTu",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RutKBNN",
                table: "IMP_DuToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fHSBL",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuongChinh",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fNghiOm",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fPCChucVu",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fPCTNNghe",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fPCTNVuotKhung",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BH_DuToan_CTCT_KPQL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fSoTien = table.Column<double>(nullable: true),
                    iID_ChungTu = table.Column<Guid>(nullable: true),
                    iID_ChungTuChiTiet = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    sLNS = table.Column<string>(maxLength: 50, nullable: true),
                    sM = table.Column<string>(maxLength: 50, nullable: true),
                    sNG = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sNoiDung = table.Column<string>(nullable: true),
                    sTM = table.Column<string>(maxLength: 50, nullable: true),
                    sTMM = table.Column<string>(maxLength: 50, nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DuToan_CTCT_KPQL", x => x.ID)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.7.5_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.7.5_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.7.5_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.7.5_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DuToan_CTCT_KPQL");

            migrationBuilder.DropColumn(
                name: "fRutKBNN",
                table: "NS_DT_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "fTongRutKBNN",
                table: "NS_DT_ChungTu");

            migrationBuilder.DropColumn(
                name: "RutKBNN",
                table: "IMP_DuToan");

            migrationBuilder.DropColumn(
                name: "fHSBL",
                table: "BH_QTT_BHXH_CTCT_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fLuongChinh",
                table: "BH_QTT_BHXH_CTCT_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fNghiOm",
                table: "BH_QTT_BHXH_CTCT_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fPCChucVu",
                table: "BH_QTT_BHXH_CTCT_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fPCTNNghe",
                table: "BH_QTT_BHXH_CTCT_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fPCTNVuotKhung",
                table: "BH_QTT_BHXH_CTCT_GiaiThich");

            migrationBuilder.AddColumn<Guid>(
                name: "NsMucLucQuyetToanNamId",
                table: "NS_MucLucQuyetToanNam_MLNS",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NS_MucLucQuyetToanNam_MLNS_NsMucLucQuyetToanNamId",
                table: "NS_MucLucQuyetToanNam_MLNS",
                column: "NsMucLucQuyetToanNamId");

            migrationBuilder.AddForeignKey(
                name: "FK_NS_MucLucQuyetToanNam_MLNS_NS_MucLucQuyetToanNam_NsMucLucQuyetToanNamId",
                table: "NS_MucLucQuyetToanNam_MLNS",
                column: "NsMucLucQuyetToanNamId",
                principalTable: "NS_MucLucQuyetToanNam",
                principalColumn: "iID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
