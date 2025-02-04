using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11368 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fSoQNCN",
                table: "NS_QS_ChungTuChiTiet",
                newName: "fSoThuongTa_QNCN");

            migrationBuilder.AddColumn<double>(
                name: "fSoDaiUy_QNCN",
                table: "NS_QS_ChungTuChiTiet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fSoThieuTa_QNCN",
                table: "NS_QS_ChungTuChiTiet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fSoThieuUy_QNCN",
                table: "NS_QS_ChungTuChiTiet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fSoTrungUy_QNCN",
                table: "NS_QS_ChungTuChiTiet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fSoThuongUy_QNCN",
                table: "NS_QS_ChungTuChiTiet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fSoTrungTa_QNCN",
                table: "NS_QS_ChungTuChiTiet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "sBangLuongKeHoach",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dNgaySua",
                table: "DM_CoSoYTe",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dNgayTao",
                table: "DM_CoSoYTe",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iTrangThai",
                table: "DM_CoSoYTe",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sNguoiSua",
                table: "DM_CoSoYTe",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sNguoiTao",
                table: "DM_CoSoYTe",
                maxLength: 50,
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.8_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.8_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.8_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.8_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.8_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.8_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.8_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fSoDaiUy_QNCN",
                table: "NS_QS_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "fSoThieuTa_QNCN",
                table: "NS_QS_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "fSoThieuUy_QNCN",
                table: "NS_QS_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "fSoTrungUy_QNCN",
                table: "NS_QS_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "fSoThuongUy_QNCN",
                table: "NS_QS_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "fSoTrungTa_QNCN",
                table: "NS_QS_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "sBangLuongKeHoach",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "dNgaySua",
                table: "DM_CoSoYTe");

            migrationBuilder.DropColumn(
                name: "dNgayTao",
                table: "DM_CoSoYTe");

            migrationBuilder.DropColumn(
                name: "iTrangThai",
                table: "DM_CoSoYTe");

            migrationBuilder.DropColumn(
                name: "sNguoiSua",
                table: "DM_CoSoYTe");

            migrationBuilder.DropColumn(
                name: "sNguoiTao",
                table: "DM_CoSoYTe");

            migrationBuilder.RenameColumn(
                name: "fSoThuongTa_QNCN",
                table: "NS_QS_ChungTuChiTiet",
                newName: "fSoQNCN");
        }
    }
}
