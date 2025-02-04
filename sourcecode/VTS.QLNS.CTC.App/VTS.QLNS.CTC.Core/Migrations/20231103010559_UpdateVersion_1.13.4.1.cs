using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11341 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TL_BangLuong_ThangBHXH",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Gia_Tri = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    HuongPC_SN = table.Column<decimal>(type: "numeric(5, 2)", nullable: true),
                    Loai_BL = table.Column<int>(nullable: true),
                    Ma_CachTL = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ma_CB = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CBo = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CheDo = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_DonVi = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_Hieu_CanBo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    NAM = table.Column<int>(nullable: true),
                    Ngay_HT = table.Column<DateTime>(nullable: true),
                    parent = table.Column<Guid>(nullable: true),
                    So_TT = table.Column<int>(nullable: true),
                    Ten_CachTL = table.Column<string>(maxLength: 100, nullable: true),
                    Ten_Cbo = table.Column<string>(maxLength: 100, nullable: true),
                    THANG = table.Column<int>(nullable: true),
                    User_Name = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_BangLuong_ThangBHXH", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.1_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.1_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.1_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.1_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.1_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_BangLuong_ThangBHXH");
        }
    }
}
