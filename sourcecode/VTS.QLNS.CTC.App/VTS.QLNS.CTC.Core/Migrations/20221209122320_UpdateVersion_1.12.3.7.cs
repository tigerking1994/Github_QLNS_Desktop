using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11237 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "STT",
                table: "NS_DanhMucCongKhai",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bHangCha",
                table: "NS_DanhMucCongKhai",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_DMCongKhai_Cha",
                table: "NS_DanhMucCongKhai",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMa",
                table: "NS_DanhMucCongKhai",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaCha",
                table: "NS_DanhMucCongKhai",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.3.7_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.3.7_budget.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STT",
                table: "NS_DanhMucCongKhai");

            migrationBuilder.DropColumn(
                name: "bHangCha",
                table: "NS_DanhMucCongKhai");

            migrationBuilder.DropColumn(
                name: "iID_DMCongKhai_Cha",
                table: "NS_DanhMucCongKhai");

            migrationBuilder.DropColumn(
                name: "sMa",
                table: "NS_DanhMucCongKhai");

            migrationBuilder.DropColumn(
                name: "sMaCha",
                table: "NS_DanhMucCongKhai");
        }
    }
}
