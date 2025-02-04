using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11295 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BH_KHT_BHXH_ChiTiet_BhKhtBHXHId",
                table: "BH_KHT_BHXH_ChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_BH_KHC_CheDoBHXH_ChiTiet_BhKhcCheDoBhXhId",
                table: "BH_KHC_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "BhKhtBHXHId",
                table: "BH_KHT_BHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "BhKhcCheDoBhXhId",
                table: "BH_KHC_CheDoBHXH_ChiTiet");

            migrationBuilder.RenameColumn(
                name: "iLoaiTongHop",
                table: "BH_KHT_BHXH",
                newName: "ILoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "IID_TongHop_ID",
                table: "BH_KHT_BHXH",
                newName: "iID_TongHop_ID");

            migrationBuilder.AddColumn<double>(
                name: "fThuBHTN",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThuBHTNNLDDong",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThuBHTNNSDDong",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThuBHXH",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThuBHXHNLDDong",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThuBHXHNSDDong",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThuBHYTNLDDong",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThuBHYTNSDDong",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTong",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongBHYT",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.9.5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fThuBHTN",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fThuBHTNNLDDong",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fThuBHTNNSDDong",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fThuBHXH",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fThuBHXHNLDDong",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fThuBHXHNSDDong",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fThuBHYTNLDDong",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fThuBHYTNSDDong",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fTong",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fTongBHYT",
                table: "BH_KHT_BHXH");

            migrationBuilder.RenameColumn(
                name: "ILoaiTongHop",
                table: "BH_KHT_BHXH",
                newName: "iLoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "iID_TongHop_ID",
                table: "BH_KHT_BHXH",
                newName: "IID_TongHop_ID");

            migrationBuilder.AddColumn<Guid>(
                name: "BhKhtBHXHId",
                table: "BH_KHT_BHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BhKhcCheDoBhXhId",
                table: "BH_KHC_CheDoBHXH_ChiTiet",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BH_KHT_BHXH_ChiTiet_BhKhtBHXHId",
                table: "BH_KHT_BHXH_ChiTiet",
                column: "BhKhtBHXHId");

            migrationBuilder.CreateIndex(
                name: "IX_BH_KHC_CheDoBHXH_ChiTiet_BhKhcCheDoBhXhId",
                table: "BH_KHC_CheDoBHXH_ChiTiet",
                column: "BhKhcCheDoBhXhId");

            migrationBuilder.AddForeignKey(
                name: "FK_BH_KHC_CheDoBHXH_ChiTiet_BH_KHC_CheDoBHXH_BhKhcCheDoBhXhId",
                table: "BH_KHC_CheDoBHXH_ChiTiet",
                column: "BhKhcCheDoBhXhId",
                principalTable: "BH_KHC_CheDoBHXH",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BH_KHT_BHXH_ChiTiet_BH_KHT_BHXH_BhKhtBHXHId",
                table: "BH_KHT_BHXH_ChiTiet",
                column: "BhKhtBHXHId",
                principalTable: "BH_KHT_BHXH",
                principalColumn: "iID_KHT_BHXH",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
