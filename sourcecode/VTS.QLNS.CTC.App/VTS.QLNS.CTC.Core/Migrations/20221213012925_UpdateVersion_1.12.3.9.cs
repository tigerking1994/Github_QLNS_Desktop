using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11239 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.3.9_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/09_script_mlns_khoi_2023.sql");
            migrationBuilder.RunSqlScript("AppData/_db/08_script_data_dmcongkhai_2023.sql");
            migrationBuilder.RunSqlScript("AppData/_db/06_delete_mlns_useless.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
