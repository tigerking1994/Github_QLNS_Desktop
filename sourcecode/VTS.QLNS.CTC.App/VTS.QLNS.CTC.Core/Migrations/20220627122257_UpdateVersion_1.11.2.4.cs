using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11124 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fUocThucHien",
                table: "NS_DTDauNam_ChungTuChiTiet",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.2.3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fUocThucHien",
                table: "NS_DTDauNam_ChungTuChiTiet");
        }
    }
}
