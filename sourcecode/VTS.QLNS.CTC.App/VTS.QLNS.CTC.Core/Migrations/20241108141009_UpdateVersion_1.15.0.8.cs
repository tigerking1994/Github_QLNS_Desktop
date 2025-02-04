using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11508 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HT_TableMigrate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Description = table.Column<string>(nullable: true),
                    IsMigrated = table.Column<bool>(nullable: false),
                    MigrateFrequency = table.Column<int>(nullable: false),
                    Object = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HT_TableMigrate", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.8_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.8_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.8_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.0.8_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HT_TableMigrate");
        }
    }
}
