using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11203 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDaChiCacNamTruoc_EUR",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDaChiCacNamTruoc_NgoaiTeKhac",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDaChiCacNamTruoc_USD",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDaChiCacNamTruoc_VND",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDaChiDenCuoiQuyTruoc_EUR",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDaChiDenCuoiQuyTruoc_NgoaiTeKhac",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDaChiDenCuoiQuyTruoc_USD",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDaChiDenCuoiQuyTruoc_VND",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDuocCapCacNamTruoc_EUR",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDuocCapCacNamTruoc_NgoaiTeKhac",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDuocCapCacNamTruoc_USD",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDuocCapCacNamTruoc_VND",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDuocCapDenCuoiQuyTruoc_EUR",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDuocCapDenCuoiQuyTruoc_NgoaiTeKhac",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDuocCapDenCuoiQuyTruoc_USD",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiDuocCapDenCuoiQuyTruoc_VND",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fNoiDungChiUSD",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fNoiDungChiVND",
                table: "NH_NhuCauChiQuy_ChiTiet",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.0.3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fKinhPhiDaChiCacNamTruoc_EUR",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDaChiCacNamTruoc_NgoaiTeKhac",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDaChiCacNamTruoc_USD",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDaChiCacNamTruoc_VND",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDaChiDenCuoiQuyTruoc_EUR",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDaChiDenCuoiQuyTruoc_NgoaiTeKhac",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDaChiDenCuoiQuyTruoc_USD",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDaChiDenCuoiQuyTruoc_VND",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDuocCapCacNamTruoc_EUR",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDuocCapCacNamTruoc_NgoaiTeKhac",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDuocCapCacNamTruoc_USD",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDuocCapCacNamTruoc_VND",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDuocCapDenCuoiQuyTruoc_EUR",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDuocCapDenCuoiQuyTruoc_NgoaiTeKhac",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDuocCapDenCuoiQuyTruoc_USD",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiDuocCapDenCuoiQuyTruoc_VND",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fNoiDungChiUSD",
                table: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fNoiDungChiVND",
                table: "NH_NhuCauChiQuy_ChiTiet");
        }
    }
}
