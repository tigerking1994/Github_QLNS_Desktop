using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11367 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iNamChungTu",
                table: "BH_QTC_Nam_KCB_QuanYDonVi");

            migrationBuilder.DropColumn(
                name: "iNamChungTu",
                table: "BH_QTC_Nam_CheDoBHXH");

            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_QTC_Nam_KinhPhiQuanLy",
                newName: "iNamLamViec");

            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_QTC_Nam_KPK",
                newName: "iNamLamViec");

            migrationBuilder.AddColumn<string>(
                name: "iIDMaDonVi",
                table: "BH_QTC_Quy_KPK_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTC_Quy_KPK_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTC_Quy_KPK_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iIDMaDonVi",
                table: "BH_QTC_Quy_KinhPhiQuanLy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTC_Quy_KinhPhiQuanLy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTC_Quy_KinhPhiQuanLy_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonVi",
                table: "BH_QTC_Quy_KCB_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTC_Quy_KCB_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMa",
                table: "BH_QTC_Quy_KCB_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_KCB_QuanYDonVi",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "bDaTongHop",
                table: "BH_QTC_Nam_KinhPhiQuanLy",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "bDaTongHop",
                table: "BH_QTC_Nam_KPK",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BH_DM_MucDongBHXH",
                columns: table => new
                {
                    iD = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTyLe_BHTN_NLD = table.Column<double>(nullable: false),
                    fTyLe_BHTN_NSD = table.Column<double>(nullable: false),
                    fTyLe_BHXH_NLD = table.Column<double>(nullable: false),
                    fTyLe_BHXH_NSD = table.Column<double>(nullable: false),
                    fTyLe_BHYT_NLD = table.Column<double>(nullable: false),
                    fTyLe_BHYT_NSD = table.Column<double>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true),
                    sBH_XauNoiMa = table.Column<string>(maxLength: 150, nullable: true),
                    sMaMucDong = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sNoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DM_MucDongBHXH", x => x.iD)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_DM_ThamDinhQuyetToan",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iKieuChu = table.Column<int>(nullable: false),
                    iMa = table.Column<int>(nullable: true),
                    iMaCha = table.Column<int>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iTrangThai = table.Column<bool>(nullable: false),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sNoiDung = table.Column<string>(maxLength: 200, nullable: true),
                    sSTT = table.Column<string>(maxLength: 50, nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DM_ThamDinhQuyetToan", x => x.iID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_ThamDinhQuyetToan_ChungTu",
                columns: table => new
                {
                    iID_BH_TDQT_ChungTu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: false),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fCNVLDHD = table.Column<double>(nullable: false),
                    fQuanNhan = table.Column<double>(nullable: false),
                    fSoBaoCao = table.Column<double>(nullable: false),
                    fSoThamDinh = table.Column<double>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_ThamDinhQuyetToan", x => x.iID_BH_TDQT_ChungTu)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_ThamDinhQuyetToan_ChungTuChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fCNVLDHD = table.Column<double>(nullable: false),
                    fQuanNhan = table.Column<double>(nullable: false),
                    fSoBaoCao = table.Column<double>(nullable: false),
                    fSoThamDinh = table.Column<double>(nullable: false),
                    iID_BH_TDQT_ChungTu = table.Column<Guid>(nullable: false),
                    iID_BH_TDQT_ChungTuChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iMa = table.Column<int>(nullable: false),
                    iNamLamViec = table.Column<int>(maxLength: 50, nullable: false),
                    sGhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_ThamDinhQuyetToanChiTiet", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.UniqueConstraint("AK_BH_ThamDinhQuyetToan_ChungTuChiTiet_Id_iID_BH_TDQT_ChungTuChiTiet", x => new { x.Id, x.iID_BH_TDQT_ChungTuChiTiet });
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.7_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.7_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.7_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.7_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.7_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.7_social_insurance_5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DM_MucDongBHXH");

            migrationBuilder.DropTable(
                name: "BH_DM_ThamDinhQuyetToan");

            migrationBuilder.DropTable(
                name: "BH_ThamDinhQuyetToan_ChungTu");

            migrationBuilder.DropTable(
                name: "BH_ThamDinhQuyetToan_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "iIDMaDonVi",
                table: "BH_QTC_Quy_KPK_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Quy_KPK_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_QTC_Quy_KPK_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iIDMaDonVi",
                table: "BH_QTC_Quy_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Quy_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_QTC_Quy_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_MaDonVi",
                table: "BH_QTC_Quy_KCB_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Quy_KCB_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sXauNoiMa",
                table: "BH_QTC_Quy_KCB_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_KCB_QuanYDonVi");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "bDaTongHop",
                table: "BH_QTC_Nam_KinhPhiQuanLy");

            migrationBuilder.DropColumn(
                name: "bDaTongHop",
                table: "BH_QTC_Nam_KPK");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_KinhPhiQuanLy",
                newName: "iNamChungTu");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_QTC_Nam_KPK",
                newName: "iNamChungTu");

            migrationBuilder.AddColumn<int>(
                name: "iNamChungTu",
                table: "BH_QTC_Nam_KCB_QuanYDonVi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "iNamChungTu",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: false,
                defaultValue: 0);
        }
    }
}
