using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11194 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DNgayTao",
                table: "NH_DM_LoaiTaiSan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SNguoiTao",
                table: "NH_DM_LoaiTaiSan",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NH_DM_NoiDungChi",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    sMaNoiDungChi = table.Column<string>(maxLength: 100, nullable: true),
                    sMoTa = table.Column<string>(type: "ntext", nullable: true),
                    sTenNoiDungChi = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_NoiDungChi", x => x.ID);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.9.4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NH_DM_NoiDungChi");

            migrationBuilder.DropColumn(
                name: "DNgayTao",
                table: "NH_DM_LoaiTaiSan");

            migrationBuilder.DropColumn(
                name: "SNguoiTao",
                table: "NH_DM_LoaiTaiSan");
        }
    }
}
