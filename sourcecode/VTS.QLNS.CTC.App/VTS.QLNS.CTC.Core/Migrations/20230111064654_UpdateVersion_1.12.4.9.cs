using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11249 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Mau",
                table: "TL_Map_Column_Config",
                nullable: true,
                defaultValue: 0);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.4.9_salary.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mau",
                table: "TL_Map_Column_Config");
        }
    }
}
