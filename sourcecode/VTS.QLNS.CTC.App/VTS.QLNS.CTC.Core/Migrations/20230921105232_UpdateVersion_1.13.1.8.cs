using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11318 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fTienLDHD_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iSoDuToanDuocDuyet",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "iSoLDHD_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "iTongSoSQ_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iTongSoQNCN_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iTongSo_LuyKeCuoiQuyNay",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iTongSoHSQBS_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iTongSo_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "iTongSoCNVCQP_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_QTC_Nam_CheDoBHXH",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_QTC_Nam_CheDoBHXH",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<bool>(
                name: "bDaTongHop",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "sDSSoChungTuTongHop",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BH_QTC_Quy_KinhPhiQuanLy",
                columns: table => new
                {
                    ID_QTC_Quy_KinhPhiQuanLy = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongTienDeNghiQuyetToanQuyNay = table.Column<double>(nullable: true),
                    fTongTienDuToanDuocGiao = table.Column<double>(nullable: true),
                    fTongTienQuyetToanDaDuyet = table.Column<double>(nullable: true),
                    fTongTienThucChi = table.Column<double>(nullable: true),
                    fTongTienXacNhanQuyetToanQuyNay = table.Column<double>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_TongHopID = table.Column<Guid>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: true),
                    iQuyChungTu = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Quy_KinhPhiQuanLy", x => x.ID_QTC_Quy_KinhPhiQuanLy)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTC_Quy_KinhPhiQuanLy_ChiTiet",
                columns: table => new
                {
                    ID_QTC_Quy_KinhPhiQuanLy_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTienDeNghiQuyetToanQuyNay = table.Column<double>(nullable: true),
                    fTienDuToanDuocGiao = table.Column<double>(nullable: true),
                    fTienQuyetToanDaDuyet = table.Column<double>(nullable: true),
                    fTienThucChi = table.Column<double>(nullable: true),
                    fTienXacNhanQuyetToanQuyNay = table.Column<double>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: true),
                    iID_QTC_Quy_KinhPhiQuanLy = table.Column<Guid>(nullable: true),
                    sM = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true),
                    sTM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Quy_KinhPhiQuanLy_ChiTiet", x => x.ID_QTC_Quy_KinhPhiQuanLy_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTT_BHXH_ChungTu",
                columns: table => new
                {
                    iID_QTT_BHXH_ChungTu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fConLai = table.Column<double>(nullable: true),
                    fDaQuyetToan = table.Column<double>(nullable: true),
                    fDuToan = table.Column<double>(nullable: true),
                    fHSBL = table.Column<double>(nullable: true),
                    fLuongChinh = table.Column<double>(nullable: true),
                    fNghiOm = table.Column<double>(nullable: true),
                    fPCTNNghe = table.Column<double>(nullable: true),
                    fPCTNVuotKhung = table.Column<double>(nullable: true),
                    fPCChucVu = table.Column<double>(nullable: true),
                    fThu_BHTN_NLD = table.Column<double>(nullable: true),
                    fThu_BHTN_NSD = table.Column<double>(nullable: true),
                    fThu_BHXH_NLD = table.Column<double>(nullable: true),
                    fThu_BHXH_NSD = table.Column<double>(nullable: true),
                    fThu_BHYT_NLD = table.Column<double>(nullable: true),
                    fThu_BHYT_NSD = table.Column<double>(nullable: true),
                    fTongCong = table.Column<double>(nullable: true),
                    fTongQTLN = table.Column<double>(nullable: true),
                    fTongSoPhaiThuBHTN = table.Column<double>(nullable: true),
                    fTongSoPhaiThuBHXH = table.Column<double>(nullable: true),
                    fTongSoPhaiThuBHYT = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iQSBQNam = table.Column<int>(nullable: true),
                    iQuyNam = table.Column<int>(nullable: false),
                    iQuyNamLoai = table.Column<int>(nullable: false),
                    sDS_MLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sQuyNamMoTa = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTT_BHXH_ChungTu", x => x.iID_QTT_BHXH_ChungTu)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTT_BHXH_ChungTu_ChiTiet",
                columns: table => new
                {
                    iID_QTT_BHXH_ChungTu_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fConLai = table.Column<double>(nullable: true),
                    fDaQuyetToan = table.Column<double>(nullable: true),
                    fDuToan = table.Column<double>(nullable: true),
                    fHSBL = table.Column<double>(nullable: true),
                    fLuongChinh = table.Column<double>(nullable: true),
                    fNghiOm = table.Column<double>(nullable: true),
                    fPCTNNghe = table.Column<double>(nullable: true),
                    fPCTNVuotKhung = table.Column<double>(nullable: true),
                    fPCChucVu = table.Column<double>(nullable: true),
                    fThu_BHTN_NLD = table.Column<double>(nullable: true),
                    fThu_BHTN_NSD = table.Column<double>(nullable: true),
                    fThu_BHXH_NLD = table.Column<double>(nullable: true),
                    fThu_BHXH_NSD = table.Column<double>(nullable: true),
                    fThu_BHYT_NLD = table.Column<double>(nullable: true),
                    fThu_BHYT_NSD = table.Column<double>(nullable: true),
                    fTongCong = table.Column<double>(nullable: true),
                    fTongQTLN = table.Column<double>(nullable: true),
                    fTongSoPhaiThuBHTN = table.Column<double>(nullable: true),
                    fTongSoPhaiThuBHXH = table.Column<double>(nullable: true),
                    fTongSoPhaiThuBHYT = table.Column<double>(nullable: true),
                    iID_MLNS = table.Column<Guid>(nullable: false),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iQSBQNam = table.Column<int>(nullable: true),
                    iID_QTT_BHXH_ChungTu = table.Column<Guid>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTT_BHXH_ChungTu_ChiTiet", x => x.iID_QTT_BHXH_ChungTu_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.8_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.8_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.8_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.8_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.8_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.8_social_insurance_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_QTC_Quy_KinhPhiQuanLy");

            migrationBuilder.DropTable(
                name: "BH_QTC_Quy_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_QTT_BHXH_ChungTu");

            migrationBuilder.DropTable(
                name: "BH_QTT_BHXH_ChungTu_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fTienLDHD_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iSoDuToanDuocDuyet",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iSoLDHD_ThucChi",
                table: "BH_QTC_Nam_CheDoBHXH_ChiTiet");

            migrationBuilder.DropColumn(
                name: "bDaTongHop",
                table: "BH_QTC_Nam_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "sDSSoChungTuTongHop",
                table: "BH_QTC_Nam_CheDoBHXH");

            migrationBuilder.AlterColumn<int>(
                name: "iTongSoSQ_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iTongSoQNCN_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iTongSo_LuyKeCuoiQuyNay",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iTongSoHSQBS_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iTongSo_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iTongSoCNVCQP_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_QTC_Nam_CheDoBHXH",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_QTC_Nam_CheDoBHXH",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }
    }
}
