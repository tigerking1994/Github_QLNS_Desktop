using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11393 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iLoaiKinhPhi",
                table: "BH_CP_CapTamUng_KCB_BHYT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTyLeThu",
                table: "BH_CP_ChungTu",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BH_BaoCao_GhiChu",
                columns: table => new
                {
                    iD = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iLoaiBaoCao = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sMaBaoCao = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenBaoCao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_BaoCao_GhiChu", x => x.iD)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.3_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.3_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.3_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.3_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.3_social_insurance_5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_BaoCao_GhiChu");

            migrationBuilder.DropColumn(
                name: "iLoaiKinhPhi",
                table: "BH_CP_CapTamUng_KCB_BHYT");

            migrationBuilder.DropColumn(
                name: "fTyLeThu",
                table: "BH_CP_ChungTu");
        }
    }
}
