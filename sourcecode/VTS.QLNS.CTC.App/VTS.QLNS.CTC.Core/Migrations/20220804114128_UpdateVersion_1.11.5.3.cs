using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11153 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iLoaiNganSach",
                table: "NS_MucLucNganSach",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.5.3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iLoaiNganSach",
                table: "NS_MucLucNganSach");
        }
    }
}
