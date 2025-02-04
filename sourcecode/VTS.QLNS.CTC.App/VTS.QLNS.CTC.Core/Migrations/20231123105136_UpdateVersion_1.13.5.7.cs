using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11357 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.5.7_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.5.7_social_insurance_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
