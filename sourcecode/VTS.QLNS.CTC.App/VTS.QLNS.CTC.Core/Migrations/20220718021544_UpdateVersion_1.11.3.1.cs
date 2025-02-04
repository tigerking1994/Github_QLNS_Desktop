using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11131 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NS_DanhMucCongKhai",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Log = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_DanhMucCongKhai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NS_DMCongKhai_MLNS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Log = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    iID_DMCongKhai = table.Column<Guid>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    sNS_XauNoiMa = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_DMCongKhai_MLNS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NS_DMCongKhai_MLNS_NS_DanhMucCongKhai_iID_DMCongKhai",
                        column: x => x.iID_DMCongKhai,
                        principalTable: "NS_DanhMucCongKhai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NS_DMCongKhai_MLNS_iID_DMCongKhai",
                table: "NS_DMCongKhai_MLNS",
                column: "iID_DMCongKhai");

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.3.1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NS_DMCongKhai_MLNS");

            migrationBuilder.DropTable(
                name: "NS_DanhMucCongKhai");
        }
    }
}
