using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11353 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fGiaTri",
                table: "TL_CanBo_CheDoBHXH",
                newName: "fSoTien");

            migrationBuilder.AlterColumn<string>(
                name: "sTenCheDo",
                table: "TL_CanBo_CheDoBHXH",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sMaCheDo",
                table: "TL_CanBo_CheDoBHXH",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sMaCanBo",
                table: "TL_CanBo_CheDoBHXH",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dNgayQuyetDinh",
                table: "TL_CanBo_CheDoBHXH",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "fGiaTriCanCu",
                table: "TL_CanBo_CheDoBHXH",
                type: "numeric(17, 3)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iThangLuongCanCuDong",
                table: "TL_CanBo_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sSoQuyetDinh",
                table: "TL_CanBo_CheDoBHXH",
                maxLength: 50,
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.5.3_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.5.3_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.5.3_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.5.3_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.5.3_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.5.3_social_insurance_5.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.5.3_social_insurance_6.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.5.3_social_insurance_7.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dNgayQuyetDinh",
                table: "TL_CanBo_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "fGiaTriCanCu",
                table: "TL_CanBo_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "iThangLuongCanCuDong",
                table: "TL_CanBo_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "sSoQuyetDinh",
                table: "TL_CanBo_CheDoBHXH");

            migrationBuilder.RenameColumn(
                name: "fSoTien",
                table: "TL_CanBo_CheDoBHXH",
                newName: "fGiaTri");

            migrationBuilder.AlterColumn<string>(
                name: "sTenCheDo",
                table: "TL_CanBo_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sMaCheDo",
                table: "TL_CanBo_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sMaCanBo",
                table: "TL_CanBo_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
