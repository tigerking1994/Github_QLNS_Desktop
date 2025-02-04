using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11416 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BH_DM_CauHinhThamSo",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bTrangThai = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sMa = table.Column<string>(maxLength: 250, nullable: true),
                    sMoTa = table.Column<string>(maxLength: 250, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTen = table.Column<string>(maxLength: 250, nullable: true),
                    fGiaTri = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DM_CauHinhThamSo", x => x.iID);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.6_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.6_salary_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DM_CauHinhThamSo");
        }
    }
}
