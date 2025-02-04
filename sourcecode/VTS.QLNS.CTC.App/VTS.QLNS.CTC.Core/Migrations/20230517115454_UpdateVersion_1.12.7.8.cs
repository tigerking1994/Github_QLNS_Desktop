using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11278 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fSoCcqp",
                table: "NS_QS_ChungTuChiTiet",
                nullable: true,
                defaultValueSql: "((0))");

            migrationBuilder.AddColumn<string>(
                name: "ThuaUyQuyen1",
                table: "DM_ChuKy",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThuaUyQuyen1_MoTa",
                table: "DM_ChuKy",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThuaUyQuyen2",
                table: "DM_ChuKy",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThuaUyQuyen2_MoTa",
                table: "DM_ChuKy",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThuaUyQuyen3",
                table: "DM_ChuKy",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThuaUyQuyen3_MoTa",
                table: "DM_ChuKy",
                maxLength: 250,
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.7.8.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.7.8_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fSoCcqp",
                table: "NS_QS_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "ThuaUyQuyen1",
                table: "DM_ChuKy");

            migrationBuilder.DropColumn(
                name: "ThuaUyQuyen1_MoTa",
                table: "DM_ChuKy");

            migrationBuilder.DropColumn(
                name: "ThuaUyQuyen2",
                table: "DM_ChuKy");

            migrationBuilder.DropColumn(
                name: "ThuaUyQuyen2_MoTa",
                table: "DM_ChuKy");

            migrationBuilder.DropColumn(
                name: "ThuaUyQuyen3",
                table: "DM_ChuKy");

            migrationBuilder.DropColumn(
                name: "ThuaUyQuyen3_MoTa",
                table: "DM_ChuKy");
        }
    }
}
