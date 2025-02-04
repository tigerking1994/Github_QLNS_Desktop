using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11195 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "iID_TongHopID",
                table: "NH_QT_QuyetToanNienDo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sTongHopChildID",
                table: "NH_QT_QuyetToanNienDo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NguoiDung_PhanHo",
                columns: table => new
                {
                    iID_NguoiDung_PhanHo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    bPublic = table.Column<bool>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    iID_MaNguoiDung = table.Column<string>(maxLength: 250, nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iSoLanSua = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iSTT = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    sIPSua = table.Column<string>(maxLength: 20, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DT_NguoiDungPhanHo", x => x.iID_NguoiDung_PhanHo);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.9.5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NguoiDung_PhanHo");

            migrationBuilder.DropColumn(
                name: "iID_TongHopID",
                table: "NH_QT_QuyetToanNienDo");

            migrationBuilder.DropColumn(
                name: "sTongHopChildID",
                table: "NH_QT_QuyetToanNienDo");
        }
    }
}
