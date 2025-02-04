using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11207 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "iID_MaDonViID",
                table: "NH_QT_TaiSan",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<int>(
                name: "iLoaiQuyetToan",
                table: "NH_QT_QuyetToanNienDo",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.0.7.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iLoaiQuyetToan",
                table: "NH_QT_QuyetToanNienDo");

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_MaDonViID",
                table: "NH_QT_TaiSan",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
