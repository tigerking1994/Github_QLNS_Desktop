using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11370 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_BH_ThamDinhQuyetToan_ChungTuChiTiet_Id_iID_BH_TDQT_ChungTuChiTiet",
                table: "BH_ThamDinhQuyetToan_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_BH_TDQT_ChungTuChiTiet",
                table: "BH_ThamDinhQuyetToan_ChungTuChiTiet");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BH_ThamDinhQuyetToan_ChungTuChiTiet",
                newName: "iID_BH_TDQT_ChungTuChiTiet");

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_BH_TDQT_ChungTuChiTiet",
                table: "BH_ThamDinhQuyetToan_ChungTuChiTiet",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "sMaLoaiChi",
                table: "BH_DTC_PhanBoDuToanChi",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_CP_ChungTu_ChiTiet",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.0_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.0_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.0_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.0_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.0_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.0_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.0_social_insurance_masterdata.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sMaLoaiChi",
                table: "BH_DTC_PhanBoDuToanChi");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_CP_ChungTu_ChiTiet");

            migrationBuilder.RenameColumn(
                name: "iID_BH_TDQT_ChungTuChiTiet",
                table: "BH_ThamDinhQuyetToan_ChungTuChiTiet",
                newName: "Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BH_ThamDinhQuyetToan_ChungTuChiTiet",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AddColumn<Guid>(
                name: "iID_BH_TDQT_ChungTuChiTiet",
                table: "BH_ThamDinhQuyetToan_ChungTuChiTiet",
                nullable: false,
                defaultValueSql: "(newid())");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BH_ThamDinhQuyetToan_ChungTuChiTiet_Id_iID_BH_TDQT_ChungTuChiTiet",
                table: "BH_ThamDinhQuyetToan_ChungTuChiTiet",
                columns: new[] { "Id", "iID_BH_TDQT_ChungTuChiTiet" });
        }
    }
}
