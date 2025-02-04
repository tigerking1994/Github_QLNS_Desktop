using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11452 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NS_CauHinh_BaoCao",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iLoaiBaoCao = table.Column<int>(nullable: true),
                    iLoaiCauHinh = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sMaBaoCao = table.Column<string>(nullable: true),
                    sMaGhiChu = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenBaoCao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_CauHinh_BaoCao", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.5.2_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.5.2_salary_1.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NS_CauHinh_BaoCao");
        }
    }
}
