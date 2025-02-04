using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11306 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iLoaiChungTu",
                table: "BH_DTC_DuToanChiTrenGiao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_DM_MucLucNganSach",
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTTM",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG3",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG2",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG1",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTM",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sNG",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sMoTa",
                table: "BH_DM_MucLucNganSach",
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sM",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DM_MucLucNganSach",
                maxLength: 100,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sL",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sK",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "bHangChaQuyetToan",
                table: "BH_DM_MucLucNganSach",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bHangChaQuyetToan",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaPhuCap",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BH_DanhMucLoaiChi",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false),
                    DNgaySua = table.Column<DateTime>(nullable: true),
                    DNgayTao = table.Column<DateTime>(nullable: true),
                    INamLamViec = table.Column<int>(nullable: false),
                    ITrangThai = table.Column<int>(nullable: true),
                    SMoTa = table.Column<string>(nullable: true),
                    SNguoiSua = table.Column<string>(nullable: true),
                    SNguoiTao = table.Column<string>(nullable: true),
                    STenDanhMucLoaiChi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DanhMucLoaiChi", x => x.iID);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTC_DieuChinhDuToanChi",
                columns: table => new
                {
                    iID_DTC_DieuChinhDuToanChi = table.Column<Guid>(nullable: false),
                    BIsKhoa = table.Column<bool>(nullable: false),
                    DNgayChungTu = table.Column<DateTime>(nullable: true),
                    DNgayQuyetDinh = table.Column<DateTime>(nullable: true),
                    DNgaySua = table.Column<DateTime>(nullable: true),
                    DNgayTao = table.Column<DateTime>(nullable: true),
                    FTienDuToanDuocGiao = table.Column<double>(nullable: true),
                    FTienSoSanhGiam = table.Column<double>(nullable: true),
                    FTienSoSanhTang = table.Column<double>(nullable: true),
                    FTienThucHien06ThangDauNam = table.Column<double>(nullable: true),
                    FTienUocThucHien06ThangCuoiNam = table.Column<double>(nullable: true),
                    FTienUocThucHienCaNam = table.Column<double>(nullable: true),
                    IID_LoaiCap = table.Column<Guid>(nullable: false),
                    IID_MaDonVi = table.Column<string>(nullable: true),
                    IID_TongHopID = table.Column<Guid>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    ILoaiTongHop = table.Column<int>(nullable: false),
                    INamChungTu = table.Column<int>(nullable: true),
                    SLNS = table.Column<string>(nullable: true),
                    SMoTa = table.Column<string>(nullable: true),
                    SNguoiSua = table.Column<string>(nullable: true),
                    SNguoiTao = table.Column<string>(nullable: true),
                    SSoChungTu = table.Column<string>(nullable: true),
                    SSoQuyetDinh = table.Column<string>(nullable: true),
                    STongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTC_DieuChinhDuToanChi", x => x.iID_DTC_DieuChinhDuToanChi);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                columns: table => new
                {
                    iID_BH_DTC_DC_DTC_ChiTiet = table.Column<Guid>(nullable: false),
                    DNgaySua = table.Column<DateTime>(nullable: true),
                    DNgayTao = table.Column<DateTime>(nullable: true),
                    FTienDuToanDuocGiao = table.Column<double>(nullable: true),
                    FTienSoSanhGiam = table.Column<double>(nullable: true),
                    FTienSoSanhTang = table.Column<double>(nullable: true),
                    FTienThucHien06ThangDauNam = table.Column<double>(nullable: true),
                    FTienUocThucHien06ThangCuoiNam = table.Column<double>(nullable: true),
                    FTienUocThucHienCaNam = table.Column<double>(nullable: true),
                    IID_DTC_DieuChinhDuToanChi = table.Column<Guid>(nullable: false),
                    IID_MucLucNganSach = table.Column<Guid>(nullable: false),
                    SM = table.Column<string>(nullable: true),
                    SNguoiSua = table.Column<string>(nullable: true),
                    SNguoiTao = table.Column<string>(nullable: true),
                    SNoiDung = table.Column<string>(nullable: true),
                    STM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTC_DieuChinhDuToanChi_ChiTiet", x => x.iID_BH_DTC_DC_DTC_ChiTiet);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTC_Nhan_PhanBo_Map",
                columns: table => new
                {
                    iID_DTCNhanPhanBoMap = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    dNgayTao = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    iID_BHDTC_NhanPhanBo = table.Column<Guid>(nullable: false),
                    iID_BHDTC_PhanBo = table.Column<Guid>(nullable: false),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTC_Nhan_PhanBo_Map", x => x.iID_DTCNhanPhanBoMap)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTC_PhanBoDuToanChi",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongTien = table.Column<double>(nullable: true),
                    fTongTienHienVat = table.Column<double>(nullable: true),
                    fTongTienTuChi = table.Column<double>(nullable: true),
                    iID_DotNhan = table.Column<string>(nullable: true),
                    iLoaiChungTu = table.Column<int>(nullable: false),
                    iLoaiDotNhanPhanBo = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: false),
                    sDotNhan = table.Column<string>(nullable: true),
                    sID_MaDonVi = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTC_PhanBoDuToanChi", x => x.ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    fTienHienVat = table.Column<double>(nullable: true),
                    fTienTuChi = table.Column<double>(nullable: true),
                    fTongTien = table.Column<double>(nullable: true),
                    iID_DTC_DuToanChiTrenGiao = table.Column<Guid>(nullable: false),
                    iID_DTC_PhanBoDuToanChi = table.Column<Guid>(nullable: false),
                    iID_DonVi = table.Column<Guid>(nullable: false),
                    iID_LoaiCap = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sM = table.Column<string>(nullable: true),
                    sNG = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true),
                    sTM = table.Column<string>(nullable: true),
                    sTTM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTC_PhanBoDuToanChi_ChiTiet", x => x.ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.6_forex_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.6_forex_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.6_forex_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.6_social_insurance.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.6_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DanhMucLoaiChi");

            migrationBuilder.DropTable(
                name: "BH_DTC_DieuChinhDuToanChi");

            migrationBuilder.DropTable(
                name: "BH_DTC_DieuChinhDuToanChi_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_DTC_Nhan_PhanBo_Map");

            migrationBuilder.DropTable(
                name: "BH_DTC_PhanBoDuToanChi");

            migrationBuilder.DropTable(
                name: "BH_DTC_PhanBoDuToanChi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iLoaiChungTu",
                table: "BH_DTC_DuToanChiTrenGiao");

            migrationBuilder.DropColumn(
                name: "sMaPhuCap",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_DM_MucLucNganSach",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTTM",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG3",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG2",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG1",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTM",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sNG",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sMoTa",
                table: "BH_DM_MucLucNganSach",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sM",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DM_MucLucNganSach",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sL",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sK",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<bool>(
                name: "bHangChaQuyetToan",
                table: "BH_DM_MucLucNganSach",
                type: "bHangChaQuyetToan",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
