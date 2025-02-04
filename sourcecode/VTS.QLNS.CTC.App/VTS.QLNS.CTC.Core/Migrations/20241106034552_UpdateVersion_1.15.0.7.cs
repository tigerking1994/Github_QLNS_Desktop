using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11507 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "sGhiChu",
                table: "TN_QuyetToan_ChungTuChiTiet_HD4554",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.7_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.7_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.7_budget_5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "sGhiChu",
                table: "TN_QuyetToan_ChungTuChiTiet_HD4554",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
