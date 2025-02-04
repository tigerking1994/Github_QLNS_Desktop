using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11371 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.1_fix_update.sql");
            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_KHTM_BHYT",
                newName: "iNamLamViec");

            migrationBuilder.RenameColumn(
                name: "ILoaiTongHop",
                table: "BH_KHT_BHXH",
                newName: "iLoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "INamChungTu",
                table: "BH_KHT_BHXH",
                newName: "iNamLamViec");

            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_CP_CapBoSung_KCB_BHYT",
                newName: "iNamLamViec");

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTTM_BHYT_Chung_Tu_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTTM_BHYT_Chung_Tu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_QTTM_BHYT_Chung_Tu",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sQuyNamMoTa",
                table: "BH_QTTM_BHYT_Chung_Tu",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_QTTM_BHYT_Chung_Tu",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_QTTM_BHYT_Chung_Tu",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTTM_BHYT_Chung_Tu",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTM",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sQuyNamMoTa",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNoiDung",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sM",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sL",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sK",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_QTT_BHXH_ChungTu",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sQuyNamMoTa",
                table: "BH_QTT_BHXH_ChungTu",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_QTT_BHXH_ChungTu",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_QTT_BHXH_ChungTu",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTT_BHXH_ChungTu",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTenDonVi",
                table: "BH_KHTM_BHYT_ChiTiet",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_KHTM_BHYT_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_KHTM_BHYT_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHTM_BHYT_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_KHTM_BHYT_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sLNS",
                table: "BH_KHTM_BHYT_ChiTiet",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_KHTM_BHYT_ChiTiet",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_KHTM_BHYT",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_KHTM_BHYT",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_KHTM_BHYT",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_KHTM_BHYT",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHTM_BHYT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTenDonVi",
                table: "BH_KHT_BHXH_ChiTiet",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_KHT_BHXH_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_KHT_BHXH_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHT_BHXH_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_KHT_BHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sLNS",
                table: "BH_KHT_BHXH_ChiTiet",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_KHT_BHXH_ChiTiet",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_KHT_BHXH",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_KHT_BHXH",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_KHT_BHXH",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_KHT_BHXH",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHT_BHXH",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_DTTM_BHYT_ThanNhan",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_DTTM_BHYT_ThanNhan",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTTM_BHYT_ThanNhan",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTTM_BHYT_ThanNhan",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTTM_BHYT_ThanNhan",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTng3",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTng2",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTng1",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTM",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNG",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sMoTa",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sM",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sL",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sK",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_DTT_BHXH_ChungTu",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_DTT_BHXH_ChungTu",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTT_BHXH_ChungTu",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTT_BHXH_ChungTu",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTT_BHXH_ChungTu",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iMaCha",
                table: "BH_DM_ThamDinhQuyetToan",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sGhiChu",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_DTT_BHXH_DieuChinh",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTT_BHXH_DieuChinh",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTT_BHXH_DieuChinh",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTT_BHXH_DieuChinh",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaCoSoYTe",
                table: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_CP_CapTamUng_KCB_BHYT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_CP_CapBoSung_KCB_BHYT_ChiTiet",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sDSID_MaDonVi",
                table: "BH_CP_CapBoSung_KCB_BHYT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.1_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.1_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.1_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.1_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_KHTM_BHYT_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sLNS",
                table: "BH_KHTM_BHYT_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_KHTM_BHYT_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_KHT_BHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sLNS",
                table: "BH_KHT_BHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_KHT_BHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_CP_CapTamUng_KCB_BHYT");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_KHTM_BHYT",
                newName: "iNamChungTu");

            migrationBuilder.RenameColumn(
                name: "iLoaiTongHop",
                table: "BH_KHT_BHXH",
                newName: "ILoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_KHT_BHXH",
                newName: "INamChungTu");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_CP_CapBoSung_KCB_BHYT",
                newName: "iNamChungTu");

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTTM_BHYT_Chung_Tu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTTM_BHYT_Chung_Tu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_QTTM_BHYT_Chung_Tu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sQuyNamMoTa",
                table: "BH_QTTM_BHYT_Chung_Tu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_QTTM_BHYT_Chung_Tu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_QTTM_BHYT_Chung_Tu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTTM_BHYT_Chung_Tu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTM",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sQuyNamMoTa",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNoiDung",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sM",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sL",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sK",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_QTT_BHXH_ChungTu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sQuyNamMoTa",
                table: "BH_QTT_BHXH_ChungTu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_QTT_BHXH_ChungTu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_QTT_BHXH_ChungTu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTT_BHXH_ChungTu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTTM_BHYT_ThanNhan_PhanBo",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTenDonVi",
                table: "BH_KHTM_BHYT_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_KHTM_BHYT_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_KHTM_BHYT_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHTM_BHYT_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_KHTM_BHYT",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_KHTM_BHYT",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_KHTM_BHYT",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_KHTM_BHYT",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHTM_BHYT",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTenDonVi",
                table: "BH_KHT_BHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_KHT_BHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_KHT_BHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHT_BHXH_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_KHT_BHXH",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_KHT_BHXH",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_KHT_BHXH",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_KHT_BHXH",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_KHT_BHXH",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTTM_BHYT_ThanNhan_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_DTTM_BHYT_ThanNhan",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_DTTM_BHYT_ThanNhan",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTTM_BHYT_ThanNhan",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTTM_BHYT_ThanNhan",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTTM_BHYT_ThanNhan",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTng3",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTng2",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTng1",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTM",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNG",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sMoTa",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sM",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sL",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sK",
                table: "BH_DTT_BHXH_ChungTu_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoQuyetDinh",
                table: "BH_DTT_BHXH_ChungTu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_DTT_BHXH_ChungTu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTT_BHXH_ChungTu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTT_BHXH_ChungTu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTT_BHXH_ChungTu",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iMaCha",
                table: "BH_DM_ThamDinhQuyetToan",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sGhiChu",
                table: "BH_DTT_BHXH_DieuChinh_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sSoChungTu",
                table: "BH_DTT_BHXH_DieuChinh",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiTao",
                table: "BH_DTT_BHXH_DieuChinh",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNguoiSua",
                table: "BH_DTT_BHXH_DieuChinh",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_DTT_BHXH_DieuChinh",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaCoSoYTe",
                table: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_CP_CapBoSung_KCB_BHYT_ChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sDSID_MaDonVi",
                table: "BH_CP_CapBoSung_KCB_BHYT",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
