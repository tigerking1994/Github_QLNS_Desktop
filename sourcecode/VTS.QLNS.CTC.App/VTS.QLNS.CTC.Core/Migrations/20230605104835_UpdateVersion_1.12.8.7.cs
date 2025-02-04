using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11287 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "iID_CTDuToan_Nhan",
                table: "NS_DT_ChungTuChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.8.7_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.8.7_salary.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "iID_CTDuToan_Nhan",
                table: "NS_DT_ChungTuChiTiet",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
