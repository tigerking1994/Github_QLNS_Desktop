using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11474 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NS_MucLucQuyetToanNam",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    iTrangThai = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    sMa = table.Column<string>(nullable: true),
                    sMaCha = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSTT = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DT_MucLucQuyetToanNam", x => x.iID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_MucLucQuyetToanNam_MLNS",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    sMaMLQT = table.Column<string>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DT_MucLucQuyetToanNam_MLNS", x => x.iID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NS_MucLucQuyetToanNam_sMa_sMaCha",
                table: "NS_MucLucQuyetToanNam",
                columns: new[] { "sMa", "sMaCha" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NS_MucLucQuyetToanNam_MLNS");

            migrationBuilder.DropTable(
                name: "NS_MucLucQuyetToanNam");
        }
    }
}
