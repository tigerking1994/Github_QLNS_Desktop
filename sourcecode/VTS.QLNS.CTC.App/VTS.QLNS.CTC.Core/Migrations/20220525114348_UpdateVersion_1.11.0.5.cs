using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11105 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "iID_QDDauTuID",
                table: "NH_DA_QDDauTu_ChiPhi",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.0.5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "iID_QDDauTuID",
                table: "NH_DA_QDDauTu_ChiPhi",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
