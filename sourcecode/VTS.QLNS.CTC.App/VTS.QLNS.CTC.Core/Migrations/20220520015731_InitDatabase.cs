using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/00_init_data.sql");
            migrationBuilder.RunSqlScript("AppData/_db/01_define_table.sql");
            migrationBuilder.RunSqlScript("AppData/_db/02_function_view.sql");
            migrationBuilder.RunSqlScript("AppData/_db/03_store.sql");
            migrationBuilder.RunSqlScript("AppData/_db/04_indexes.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
