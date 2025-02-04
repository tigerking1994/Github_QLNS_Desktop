using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11210 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BActive",
                table: "NH_DM_NhaThau");

            migrationBuilder.DropColumn(
                name: "IThuTu",
                table: "NH_DM_NhaThau");

            migrationBuilder.AlterColumn<decimal>(
                name: "fGiaTriPhuCap_KemTheo",
                table: "TL_DM_PhuCap",
                type: "numeric(17, 3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "fGiaTriNhoNhat",
                table: "TL_DM_PhuCap",
                type: "numeric(17, 3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "fGiaTriLonNhat",
                table: "TL_DM_PhuCap",
                type: "numeric(17, 3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bIsSaveTongHop",
                table: "NH_QT_QuyetToanNienDo_ChiTiet",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NH_TH_TongHop",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsLog = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    fGiaTriUsd = table.Column<double>(nullable: true),
                    fGiaTriVnd = table.Column<double>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: true),
                    iID_TiGia = table.Column<double>(nullable: true),
                    iID_ChungTu = table.Column<Guid>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    iQuyKeHoach = table.Column<int>(nullable: true),
                    iStatus = table.Column<int>(nullable: false),
                    sMaDich = table.Column<string>(maxLength: 100, nullable: true),
                    sMaNguon = table.Column<string>(maxLength: 100, nullable: true),
                    sMaNguonCha = table.Column<string>(maxLength: 100, nullable: true),
                    sMaTienTrinh = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_TH_TongHop", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.1.0.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NH_TH_TongHop");

            migrationBuilder.DropColumn(
                name: "bIsSaveTongHop",
                table: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.AlterColumn<decimal>(
                name: "fGiaTriPhuCap_KemTheo",
                table: "TL_DM_PhuCap",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(17, 3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "fGiaTriNhoNhat",
                table: "TL_DM_PhuCap",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(17, 3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "fGiaTriLonNhat",
                table: "TL_DM_PhuCap",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(17, 3)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BActive",
                table: "NH_DM_NhaThau",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IThuTu",
                table: "NH_DM_NhaThau",
                nullable: true);
        }
    }
}
