using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11355 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_Sent",
                table: "NS_SKT_ChungTu",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_Sent",
                table: "NS_QT_ChungTu",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_Sent",
                table: "NS_DTDauNam_ChungTu",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.5.5_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_Sent",
                table: "NS_SKT_ChungTu");

            migrationBuilder.DropColumn(
                name: "is_Sent",
                table: "NS_QT_ChungTu");

            migrationBuilder.DropColumn(
                name: "is_Sent",
                table: "NS_DTDauNam_ChungTu");
        }
    }
}
