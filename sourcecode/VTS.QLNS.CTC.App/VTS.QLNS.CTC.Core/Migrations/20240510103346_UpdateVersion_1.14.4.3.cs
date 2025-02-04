using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11443 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iSTT",
                table: "BH_DM_ThamDinhQuyetToan",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NS_NC3Y_ChungTuChiTiet",
                columns: table => new
                {
                    iID_CTNC3YChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fDuToan = table.Column<double>(nullable: false, defaultValueSql: "((0))"),
                    fNCNam1 = table.Column<double>(nullable: false, defaultValueSql: "((0))"),
                    fNCNam2 = table.Column<double>(nullable: false, defaultValueSql: "((0))"),
                    fNCNam3 = table.Column<double>(nullable: false, defaultValueSql: "((0))"),
                    fUocTH = table.Column<double>(nullable: false, defaultValueSql: "((0))"),
                    iID_CTSoKiemTra = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true),
                    iID_MLSKT = table.Column<Guid>(nullable: false),
                    iLoai = table.Column<int>(nullable: false),
                    iLoaiChungTu = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iNamNganSach = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sKyHieu = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: false),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NC3Y_ChungTuChiTiet", x => x.iID_CTNC3YChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                    //table.ForeignKey(
                    //    name: "FK_NS_NC3Y_ChungTuChiTiet_NS_SKT_ChungTu_iID_CTSoKiemTra",
                    //    column: x => x.iID_CTSoKiemTra,
                    //    principalTable: "NS_SKT_ChungTu",
                    //    principalColumn: "iID_CTSoKiemTra",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NS_NC3Y_ChungTuChiTiet_iID_CTSoKiemTra",
                table: "NS_NC3Y_ChungTuChiTiet",
                column: "iID_CTSoKiemTra");

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.3_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.3_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.3_budget_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.3_budget_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.3_budget_5.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.3_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.3_social_insurance_9.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NS_NC3Y_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "iSTT",
                table: "BH_DM_ThamDinhQuyetToan");
        }
    }
}
