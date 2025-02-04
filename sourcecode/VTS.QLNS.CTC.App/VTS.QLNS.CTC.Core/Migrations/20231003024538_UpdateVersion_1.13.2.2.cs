using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11322 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sKyHieuCu",
                table: "NS_SKT_MucLuc",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iTongSo_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoSQ_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoQNCN_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoLDHD_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoHSQBS_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoDuToanDuocDuyet",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoCNVCQP_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.2_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.2_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.2_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.2_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.2_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.2_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.2_social_insurance_3.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sKyHieuCu",
                table: "NS_SKT_MucLuc");

            migrationBuilder.AlterColumn<int>(
                name: "iTongSo_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoSQ_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoQNCN_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoLDHD_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoHSQBS_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoDuToanDuocDuyet",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoCNVCQP_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
