using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11441 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SK",
                table: "NS_SKT_MucLuc",
                maxLength: 10,
                nullable: true,
                defaultValueSql: "('')");

            migrationBuilder.AddColumn<string>(
                name: "SL",
                table: "NS_SKT_MucLuc",
                maxLength: 10,
                nullable: true,
                defaultValueSql: "('')");

            migrationBuilder.CreateTable(
                name: "TL_BangLuong_KeHoach_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Gia_Tri = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Ma_CachTL = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CanBo = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_CB = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_DonVi = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_Hieu_CanBo = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_PhuCap = table.Column<string>(maxLength: 50, nullable: true),
                    Nam = table.Column<int>(nullable: false),
                    parent = table.Column<Guid>(nullable: true),
                    Ten_CanBo = table.Column<string>(maxLength: 100, nullable: true),
                    Thang = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_BangLuong_KeHoach_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DS_BangLuong_KeHoach_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    iTrangThai = table.Column<bool>(nullable: true),
                    Ma_CachTL = table.Column<string>(maxLength: 20, nullable: true),
                    Ma_DonVi = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Nam = table.Column<int>(nullable: false),
                    Ten_BangLuong = table.Column<string>(maxLength: 255, nullable: true),
                    Thang = table.Column<int>(nullable: false),
                    UserCreator = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DS_BangLuong_KeHoach_NQ104", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.1_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.1_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.1_budget_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.1_budget_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.1_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.1_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.1_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.1_social_insurance_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_BangLuong_KeHoach_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DS_BangLuong_KeHoach_NQ104");

            migrationBuilder.DropColumn(
                name: "SK",
                table: "NS_SKT_MucLuc");

            migrationBuilder.DropColumn(
                name: "SL",
                table: "NS_SKT_MucLuc");
        }
    }
}
