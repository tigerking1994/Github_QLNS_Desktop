using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11290 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "bKhongTinhNTN",
                table: "TL_DM_CanBo_KeHoach",
                unicode: false,
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.9.0_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bKhongTinhNTN",
                table: "TL_DM_CanBo_KeHoach");
        }
    }
}
