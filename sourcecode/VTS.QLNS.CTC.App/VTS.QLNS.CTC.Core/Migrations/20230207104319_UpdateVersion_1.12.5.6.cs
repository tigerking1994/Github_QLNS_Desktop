using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11256 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sMaCB",
                table: "NS_MucLucNganSach",
                maxLength: 50,
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.5.6_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.5.6_investment.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sMaCB",
                table: "NS_MucLucNganSach");
        }
    }
}
