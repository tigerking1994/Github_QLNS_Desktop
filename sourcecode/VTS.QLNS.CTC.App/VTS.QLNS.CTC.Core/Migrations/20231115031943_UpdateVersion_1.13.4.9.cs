using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11349 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VDT_TT_ThongTinCanCu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTri = table.Column<double>(nullable: true),
                    iID_DeNghiThanhToanID = table.Column<Guid>(nullable: true),
                    iSTT = table.Column<int>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TT_ThongTinCanCu", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.9_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.9_forex_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.9_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VDT_TT_ThongTinCanCu");
        }
    }
}
