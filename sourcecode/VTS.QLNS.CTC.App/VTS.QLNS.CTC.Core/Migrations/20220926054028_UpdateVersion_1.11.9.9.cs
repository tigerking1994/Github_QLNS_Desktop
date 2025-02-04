using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11199 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "fThuaNopTraNSNN_VND",
                table: "NH_QT_ThongTriQuyetToan_ChiTiet",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "fThuaNopTraNSNN_USD",
                table: "NH_QT_ThongTriQuyetToan_ChiTiet",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "fDeNghiQuyetToanNam_VND",
                table: "NH_QT_ThongTriQuyetToan_ChiTiet",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "fDeNghiQuyetToanNam_USD",
                table: "NH_QT_ThongTriQuyetToan_ChiTiet",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_DuAnID",
                table: "NH_QT_QuyetToanNienDo_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_ThanhToan_ChiTietID",
                table: "NH_QT_QuyetToanNienDo_ChiTiet",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.9.9.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iID_DuAnID",
                table: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_ThanhToan_ChiTietID",
                table: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.AlterColumn<float>(
                name: "fThuaNopTraNSNN_VND",
                table: "NH_QT_ThongTriQuyetToan_ChiTiet",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "fThuaNopTraNSNN_USD",
                table: "NH_QT_ThongTriQuyetToan_ChiTiet",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "fDeNghiQuyetToanNam_VND",
                table: "NH_QT_ThongTriQuyetToan_ChiTiet",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "fDeNghiQuyetToanNam_USD",
                table: "NH_QT_ThongTriQuyetToan_ChiTiet",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
