using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11313 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bHangCha",
                table: "BH_CP_CapBoSung_KCB_BHYT_ChiTiet");

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD",
                table: "NH_QT_QuyetToanNienDo_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_VND",
                table: "NH_QT_QuyetToanNienDo_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD",
                table: "NH_QT_QuyetToanNienDo_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeKinhPhiDaGiaiNganTrongNamNay_VND",
                table: "NH_QT_QuyetToanNienDo_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD",
                table: "NH_QT_QuyetToanNienDo_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_VND",
                table: "NH_QT_QuyetToanNienDo_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD",
                table: "NH_QT_QuyetToanNienDo_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_VND",
                table: "NH_QT_QuyetToanNienDo_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeKinhPhiDaGiaiNganTrongNamNay_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_TongHopID",
                table: "NH_KT_KhoiTaoCapPhat",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true,
                oldDefaultValueSql: "''");

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_TiGiaID",
                table: "NH_KT_KhoiTaoCapPhat",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true,
                oldDefaultValueSql: "''");

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_DonViID",
                table: "NH_KT_KhoiTaoCapPhat",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true,
                oldDefaultValueSql: "''");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.3_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.3_social_insurance_1.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD",
                table: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_VND",
                table: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD",
                table: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeKinhPhiDaGiaiNganTrongNamNay_VND",
                table: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD",
                table: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_VND",
                table: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD",
                table: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_VND",
                table: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNay_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeKinhPhiDaGiaiNganTrongNamNay_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeKinhPhiDaGiaiNganTrongNamNay_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNay_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNay_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_TongHopID",
                table: "NH_KT_KhoiTaoCapPhat",
                nullable: true,
                defaultValueSql: "''",
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_TiGiaID",
                table: "NH_KT_KhoiTaoCapPhat",
                nullable: true,
                defaultValueSql: "''",
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_DonViID",
                table: "NH_KT_KhoiTaoCapPhat",
                nullable: true,
                defaultValueSql: "''",
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bHangCha",
                table: "BH_CP_CapBoSung_KCB_BHYT_ChiTiet",
                nullable: false,
                defaultValue: false);
        }
    }
}
