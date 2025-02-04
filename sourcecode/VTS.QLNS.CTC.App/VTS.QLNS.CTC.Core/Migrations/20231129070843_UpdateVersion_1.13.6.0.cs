using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11360 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iNamChungTu",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet");

            migrationBuilder.AddColumn<string>(
                name: "sBanQuanLyDuAn",
                table: "VDT_DA_DuAn",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMlnsBHXH",
                table: "TL_DM_CheDoBHXH",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMaMlnsBHXH",
                table: "TL_DM_CheDoBHXH",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iIDMaDonVi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sLNS",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                columns: table => new
                {
                    iiD_QTC_Quy_CTCT_GiaiThichTroCap = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fSoTien = table.Column<double>(nullable: true),
                    iCapBac = table.Column<int>(nullable: false),
                    iiD_MaDonVi = table.Column<string>(nullable: true),
                    iiD_MaPhanHo = table.Column<string>(nullable: true),
                    iID_QTC_Quy_ChungTu = table.Column<Guid>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iQuy = table.Column<int>(nullable: false),
                    iSoNgayHuong = table.Column<int>(nullable: true),
                    sMa_Hieu_Can_Bo = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTenCanBo = table.Column<string>(nullable: true),
                    sTenCapBac = table.Column<string>(nullable: true),
                    sTenPhanHo = table.Column<string>(nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Quy_CTCT_GiaiThichTroCap", x => x.iiD_QTC_Quy_CTCT_GiaiThichTroCap)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.0_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.0_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.0_investment_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.0_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.0_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.0_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_QTC_Quy_CTCT_GiaiThichTroCap");

            migrationBuilder.DropColumn(
                name: "sBanQuanLyDuAn",
                table: "VDT_DA_DuAn");

            migrationBuilder.DropColumn(
                name: "sMlnsBHXH",
                table: "TL_DM_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "sXauNoiMaMlnsBHXH",
                table: "TL_DM_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "iIDMaDonVi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sLNS",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet");

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamChungTu",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: false,
                defaultValue: 0);
        }
    }
}
