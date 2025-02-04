using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11364 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet",
                newName: "iNamLamViec");

            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_DTTM_BHYT_ThanNhan",
                newName: "iNamLamViec");

            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_DTC_DuToanChiTrenGiao",
                newName: "iNamLamViec");

            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "iNamLamViec");

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTTM_BHYT_Chung_Tu_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTTM_BHYT_Chung_Tu_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTenPhanHo",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTenCapBac",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTenCanBo",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sMa_Hieu_Can_Bo",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iiD_MaPhanHo",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iiD_MaDonVi",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dDenNgay",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dTuNgay",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sSoSoBHXH",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTC_Nam_KinhPhiQuanLy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_KinhPhiQuanLy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTC_Nam_KinhPhiQuanLy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTC_Nam_KPK_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_KPK_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTC_Nam_KPK_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTTM",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTM",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNoiDung",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNG",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sM",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "INamLamViec",
                table: "BH_KHC_CheDoBHXH_ChiTiet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTC_DuToanChiTrenGiao_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_DTC_DuToanChiTrenGiao_ChiTiet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_DTC_DuToanChiTrenGiao_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sDSXauNoiMa",
                table: "BH_DM_LoaiChi",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaLoaiChi",
                table: "BH_DM_LoaiChi",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.4_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.4_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.4_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.4_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.4_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.4_social_insurance_5.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.4_social_insurance_6.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_QTTM_BHYT_Chung_Tu_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTTM_BHYT_Chung_Tu_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet");

            migrationBuilder.DropColumn(
                name: "dDenNgay",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap");

            migrationBuilder.DropColumn(
                name: "dTuNgay",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap");

            migrationBuilder.DropColumn(
                name: "sSoSoBHXH",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_QTC_Nam_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_QTC_Nam_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_QTC_Nam_KPK_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_KPK_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_QTC_Nam_KPK_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "INamLamViec",
                table: "BH_KHC_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_DTC_DuToanChiTrenGiao_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_DTC_DuToanChiTrenGiao_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_DTC_DuToanChiTrenGiao_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sDSXauNoiMa",
                table: "BH_DM_LoaiChi");

            migrationBuilder.DropColumn(
                name: "sMaLoaiChi",
                table: "BH_DM_LoaiChi");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet",
                newName: "iNamChungTu");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_DTTM_BHYT_ThanNhan",
                newName: "iNamChungTu");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_DTC_DuToanChiTrenGiao",
                newName: "iNamChungTu");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "iNamChungTu");

            migrationBuilder.AlterColumn<string>(
                name: "sTenPhanHo",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTenCapBac",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTenCanBo",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sMa_Hieu_Can_Bo",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iiD_MaPhanHo",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iiD_MaDonVi",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTTM",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTM",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNoiDung",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNG",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sM",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
