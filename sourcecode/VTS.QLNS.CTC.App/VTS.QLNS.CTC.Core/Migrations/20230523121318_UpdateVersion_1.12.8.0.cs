using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11280 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fLuongCNVC",
                table: "TL_QT_ChungTuChiTiet_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuongHDLD",
                table: "TL_QT_ChungTuChiTiet_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuongQNCN",
                table: "TL_QT_ChungTuChiTiet_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuongSiQuan",
                table: "TL_QT_ChungTuChiTiet_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fPcCNVC",
                table: "TL_QT_ChungTuChiTiet_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fPcHDLD",
                table: "TL_QT_ChungTuChiTiet_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fPcQNCN",
                table: "TL_QT_ChungTuChiTiet_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fPcSiQuan",
                table: "TL_QT_ChungTuChiTiet_GiaiThich",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CCQP",
                table: "TL_QS_ChungTuChiTiet",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.8.0_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.8.0_budget.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fLuongCNVC",
                table: "TL_QT_ChungTuChiTiet_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fLuongHDLD",
                table: "TL_QT_ChungTuChiTiet_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fLuongQNCN",
                table: "TL_QT_ChungTuChiTiet_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fLuongSiQuan",
                table: "TL_QT_ChungTuChiTiet_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fPcCNVC",
                table: "TL_QT_ChungTuChiTiet_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fPcHDLD",
                table: "TL_QT_ChungTuChiTiet_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fPcQNCN",
                table: "TL_QT_ChungTuChiTiet_GiaiThich");

            migrationBuilder.DropColumn(
                name: "fPcSiQuan",
                table: "TL_QT_ChungTuChiTiet_GiaiThich");

            migrationBuilder.DropColumn(
                name: "CCQP",
                table: "TL_QS_ChungTuChiTiet");
        }
    }
}
