using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11271 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.7.1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
