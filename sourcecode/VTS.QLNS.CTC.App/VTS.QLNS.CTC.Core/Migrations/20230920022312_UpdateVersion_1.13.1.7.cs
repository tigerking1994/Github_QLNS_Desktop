using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11317 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dNgayNoiDung",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fDeNghiChuyenNamSau_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fDeNghiChuyenNamSau_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_KHTT_NhiemVuChiID",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_QuyetDinhKhacID",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iLoaiNoiDung",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaNoiDung",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sTenNoiDung",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_LoaiDanhMucChi",
                table: "BH_DTC_DuToanChiTrenGiao",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.7_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.7_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.7_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.7_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.7_social_insurance_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dNgayNoiDung",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fDeNghiChuyenNamSau_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fDeNghiChuyenNamSau_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_KHTT_NhiemVuChiID",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_QuyetDinhKhacID",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iLoaiNoiDung",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sMaNoiDung",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sTenNoiDung",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_LoaiDanhMucChi",
                table: "BH_DTC_DuToanChiTrenGiao");
        }
    }
}
