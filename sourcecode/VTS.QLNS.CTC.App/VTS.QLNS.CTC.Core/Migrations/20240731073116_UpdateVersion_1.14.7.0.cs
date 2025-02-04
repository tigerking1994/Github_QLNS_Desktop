using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11470 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "fTuChi",
                table: "NS_CP_ChungTuChiTiet",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "fHienVat",
                table: "NS_CP_ChungTuChiTiet",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "fDeNghiDonVi",
                table: "NS_CP_ChungTuChiTiet",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.7.0_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.7.0_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "fTuChi",
                table: "NS_CP_ChungTuChiTiet",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "fHienVat",
                table: "NS_CP_ChungTuChiTiet",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "fDeNghiDonVi",
                table: "NS_CP_ChungTuChiTiet",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
