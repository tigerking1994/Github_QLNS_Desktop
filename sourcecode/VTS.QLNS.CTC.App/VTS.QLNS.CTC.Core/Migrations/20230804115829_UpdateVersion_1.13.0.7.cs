using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11307 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "iID_BH_DTC_DC_DTC_ChiTiet",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "iID_BH_DTC_ChiTiet");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<string>(
                name: "SGhiChu",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DNgayChungTu",
                table: "BH_DTC_DieuChinhDuToanChi",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BH_DTT_BHXH_PhanBo_ChungTu",
                columns: table => new
                {
                    iID_DTT_BHXH_PhanBo_ChungTu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bKhoa = table.Column<bool>(nullable: false),
                    bLuongNhanDuLieu = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fThuBHTN_NLD = table.Column<double>(nullable: true),
                    fThuBHTN_NSD = table.Column<double>(nullable: true),
                    fThuBHXH_NLD = table.Column<double>(nullable: true),
                    fThuBHXH_NSD = table.Column<double>(nullable: true),
                    fThuBHYT_NLD = table.Column<double>(nullable: true),
                    fThuBHYT_NSD = table.Column<double>(nullable: true),
                    fTongBHTN = table.Column<double>(nullable: true),
                    fTongBHXH = table.Column<double>(nullable: true),
                    fTongBHYT = table.Column<double>(nullable: true),
                    fTongDuToan = table.Column<double>(nullable: true),
                    iID_DotNhan = table.Column<string>(nullable: true),
                    iLoaiDuToan = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    sDonViNhanDuLieu = table.Column<string>(nullable: true),
                    sDSID_MaDonVi = table.Column<string>(maxLength: 500, nullable: true),
                    sDSLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(maxLength: 250, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTT_BHXH_PhanBo_ChungTu", x => x.iID_DTT_BHXH_PhanBo_ChungTu);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTT_Nhan_Phan_Bo_Map",
                columns: table => new
                {
                    iID_DTTNhanPhanBoMap = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    dNgayTao = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    iID_CTDuToan_Nhan = table.Column<Guid>(nullable: false),
                    iID_CTDuToan_PhanBo = table.Column<Guid>(nullable: false),
                    sNgaySua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTT_Nhan_Phan_Bo_Map", x => x.iID_DTTNhanPhanBoMap);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                columns: table => new
                {
                    iID_DTT_BHXH_ChungTu_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    BhDtPhanBoChungTuId = table.Column<Guid>(nullable: true),
                    ChungTuId = table.Column<Guid>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fBHTN_NLD = table.Column<double>(nullable: true),
                    fBHTN_NSD = table.Column<double>(nullable: true),
                    fBHXH_NLD = table.Column<double>(nullable: true),
                    fBHXH_NSD = table.Column<double>(nullable: true),
                    fBHYT_NLD = table.Column<double>(nullable: true),
                    fBHYT_NSD = table.Column<double>(nullable: true),
                    fThuBHTN = table.Column<double>(nullable: true),
                    fThuBHXH = table.Column<double>(nullable: true),
                    fThuBHYT = table.Column<double>(nullable: true),
                    fTongCong = table.Column<double>(nullable: true),
                    iDuLieuNhan = table.Column<int>(nullable: false),
                    iID_CTDuToan_Nhan = table.Column<Guid>(nullable: true),
                    iID_DTT_BHXH_ChungTu = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MLNS = table.Column<Guid>(nullable: true),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    IPhanCap = table.Column<int>(nullable: false),
                    sK = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sL = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sLNS = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true, defaultValueSql: "('')"),
                    sNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    sTNG1 = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG2 = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG3 = table.Column<string>(maxLength: 50, nullable: true),
                    sTTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTT_BHXH_PhanBo_ChungTuChiTiet", x => x.iID_DTT_BHXH_ChungTu_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_BH_DTT_BHXH_PhanBo_ChungTu_BhDtPhanBoChungTuId",
                        column: x => x.BhDtPhanBoChungTuId,
                        principalTable: "BH_DTT_BHXH_PhanBo_ChungTu",
                        principalColumn: "iID_DTT_BHXH_PhanBo_ChungTu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_NS_DT_ChungTu_ChungTuId",
                        column: x => x.ChungTuId,
                        principalTable: "NS_DT_ChungTu",
                        principalColumn: "iID_DTChungTu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_BhDtPhanBoChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                column: "BhDtPhanBoChungTuId");

            migrationBuilder.CreateIndex(
                name: "IX_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_ChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                column: "ChungTuId");

            migrationBuilder.CreateIndex(
                name: "IX_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_iID_DTT_BHXH_ChungTu_iNamLamViec",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                columns: new[] { "iID_DTT_BHXH_ChungTu", "iNamLamViec" });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.7_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.7_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.7_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.7_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.7_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "BH_DTT_Nhan_Phan_Bo_Map");

            migrationBuilder.DropTable(
                name: "BH_DTT_BHXH_PhanBo_ChungTu");

            migrationBuilder.DropColumn(
                name: "SGhiChu",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet");

            migrationBuilder.RenameColumn(
                name: "iID_BH_DTC_ChiTiet",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "iID_BH_DTC_DC_DTC_ChiTiet");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DNgayChungTu",
                table: "BH_DTC_DieuChinhDuToanChi",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
