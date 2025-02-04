using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11384 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "iTongSo_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoSQ_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoQNCN_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoLuyKeCuoiQuyNay",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoLDHD_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoHSQBS_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iSoCNVCQP_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.4_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.4_social_insurance.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.4_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.4_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.4_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.4_social_insurance_7.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "iTongSo_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoSQ_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoQNCN_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoLuyKeCuoiQuyNay",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoLDHD_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoHSQBS_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iSoCNVCQP_DeNghi",
                table: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
