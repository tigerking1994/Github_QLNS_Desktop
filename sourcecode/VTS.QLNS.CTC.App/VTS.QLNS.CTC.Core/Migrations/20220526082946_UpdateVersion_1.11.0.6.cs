using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
               name: "iID_KHTT_NhiemVuChiID",
               table: "NH_DA_KHLCNhaThau",
               nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.0.6.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
