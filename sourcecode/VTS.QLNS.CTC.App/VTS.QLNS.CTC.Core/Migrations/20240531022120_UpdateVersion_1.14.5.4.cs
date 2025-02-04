using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11454 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TL_DM_CapBac_KeHoach_NQ104",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    HsVk = table.Column<decimal>(type: "numeric(16, 4)", nullable: true),
                    Loai = table.Column<string>(maxLength: 50, nullable: true),
                    LoaiNhom = table.Column<string>(maxLength: 100, nullable: true),
                    Ma_BacLuong = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_BacLuong_KeHoach = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_BacLuong_Tran = table.Column<string>(maxLength: 100, nullable: true),
                    Ma_Cb = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_Cb_KeHoach = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    MoTa = table.Column<string>(maxLength: 200, nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    Nhom = table.Column<string>(maxLength: 50, nullable: true),
                    Parent = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    PCRQ_TT = table.Column<double>(nullable: true),
                    Readonly = table.Column<bool>(nullable: true),
                    Splits = table.Column<bool>(nullable: true),
                    Ten_Cb = table.Column<string>(maxLength: 100, nullable: false),
                    Ten_Cb_KeHoach = table.Column<string>(maxLength: 100, nullable: true),
                    Thoi_Han_Tang = table.Column<int>(nullable: true),
                    Tuoi_Huu_Nam = table.Column<int>(nullable: true),
                    Tuoi_Huu_Nu = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_CapBac_KeHoach_NQ104", x => x.ID);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.5.4_salary_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_DM_CapBac_KeHoach_NQ104");
        }
    }
}
