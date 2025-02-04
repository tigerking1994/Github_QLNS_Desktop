using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11281 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HuongPC_SN",
                table: "TL_BangLuong_Thang",
                type: "numeric(5, 2)",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.8.1_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HuongPC_SN",
                table: "TL_BangLuong_Thang");
        }
    }
}
