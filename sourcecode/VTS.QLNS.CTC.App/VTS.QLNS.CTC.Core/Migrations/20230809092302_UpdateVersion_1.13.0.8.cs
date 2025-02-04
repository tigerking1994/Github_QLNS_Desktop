using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11308 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_BH_DTT_BHXH_PhanBo_ChungTu_BhDtPhanBoChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_NS_DT_ChungTu_ChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_BhDtPhanBoChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_ChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_DanhMucLoaiChi",
                table: "BH_DanhMucLoaiChi");

            migrationBuilder.DropColumn(
                name: "BhDtPhanBoChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "ChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet");

            migrationBuilder.RenameTable(
                name: "BH_DanhMucLoaiChi",
                newName: "BH_DM_LoaiChi");

            migrationBuilder.RenameColumn(
                name: "bIsKhoa",
                table: "BH_DTC_PhanBoDuToanChi",
                newName: "BIsKhoa");

            migrationBuilder.RenameColumn(
                name: "bIsKhoa",
                table: "BH_DTC_DuToanChiTrenGiao",
                newName: "BIsKhoa");

            migrationBuilder.RenameColumn(
                name: "STM",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "sTM");

            migrationBuilder.RenameColumn(
                name: "SNoiDung",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "sNoiDung");

            migrationBuilder.RenameColumn(
                name: "SNguoiTao",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "sNguoiTao");

            migrationBuilder.RenameColumn(
                name: "SNguoiSua",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "sNguoiSua");

            migrationBuilder.RenameColumn(
                name: "SM",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "sM");

            migrationBuilder.RenameColumn(
                name: "SGhiChu",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "sGhiChu");

            migrationBuilder.RenameColumn(
                name: "IID_MucLucNganSach",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "iID_MucLucNganSach");

            migrationBuilder.RenameColumn(
                name: "FTienUocThucHienCaNam",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "fTienUocThucHienCaNam");

            migrationBuilder.RenameColumn(
                name: "FTienUocThucHien06ThangCuoiNam",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "fTienUocThucHien06ThangCuoiNam");

            migrationBuilder.RenameColumn(
                name: "FTienThucHien06ThangDauNam",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "fTienThucHien06ThangDauNam");

            migrationBuilder.RenameColumn(
                name: "FTienSoSanhTang",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "fTienSoSanhTang");

            migrationBuilder.RenameColumn(
                name: "FTienSoSanhGiam",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "fTienSoSanhGiam");

            migrationBuilder.RenameColumn(
                name: "FTienDuToanDuocGiao",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "fTienDuToanDuocGiao");

            migrationBuilder.RenameColumn(
                name: "DNgayTao",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "dNgayTao");

            migrationBuilder.RenameColumn(
                name: "DNgaySua",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "dNgaySua");

            migrationBuilder.RenameColumn(
                name: "IID_DTC_DieuChinhDuToanChi",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "iID_BH_DTC");

            migrationBuilder.RenameColumn(
                name: "STongHop",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "sTongHop");

            migrationBuilder.RenameColumn(
                name: "SSoQuyetDinh",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "sSoQuyetDinh");

            migrationBuilder.RenameColumn(
                name: "SSoChungTu",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "sSoChungTu");

            migrationBuilder.RenameColumn(
                name: "SNguoiTao",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "sNguoiTao");

            migrationBuilder.RenameColumn(
                name: "SNguoiSua",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "sNguoiSua");

            migrationBuilder.RenameColumn(
                name: "SMoTa",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "sMoTa");

            migrationBuilder.RenameColumn(
                name: "SLNS",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "sLNS");

            migrationBuilder.RenameColumn(
                name: "INamChungTu",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "iNamChungTu");

            migrationBuilder.RenameColumn(
                name: "ILoaiTongHop",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "iLoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "IID_TongHopID",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "iID_TongHopID");

            migrationBuilder.RenameColumn(
                name: "IID_MaDonVi",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "iID_MaDonVi");

            migrationBuilder.RenameColumn(
                name: "IID_LoaiCap",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "iID_LoaiCap");

            migrationBuilder.RenameColumn(
                name: "FTienUocThucHienCaNam",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "fTienUocThucHienCaNam");

            migrationBuilder.RenameColumn(
                name: "FTienUocThucHien06ThangCuoiNam",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "fTienUocThucHien06ThangCuoiNam");

            migrationBuilder.RenameColumn(
                name: "FTienThucHien06ThangDauNam",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "fTienThucHien06ThangDauNam");

            migrationBuilder.RenameColumn(
                name: "FTienSoSanhTang",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "fTienSoSanhTang");

            migrationBuilder.RenameColumn(
                name: "FTienSoSanhGiam",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "fTienSoSanhGiam");

            migrationBuilder.RenameColumn(
                name: "FTienDuToanDuocGiao",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "fTienDuToanDuocGiao");

            migrationBuilder.RenameColumn(
                name: "DNgayTao",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "dNgayTao");

            migrationBuilder.RenameColumn(
                name: "DNgaySua",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "dNgaySua");

            migrationBuilder.RenameColumn(
                name: "DNgayQuyetDinh",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "dNgayQuyetDinh");

            migrationBuilder.RenameColumn(
                name: "DNgayChungTu",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "dNgayChungTu");

            migrationBuilder.RenameColumn(
                name: "BIsKhoa",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "bIsKhoa");

            migrationBuilder.RenameColumn(
                name: "iID_DTC_DieuChinhDuToanChi",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "iID_BH_DTC");

            migrationBuilder.RenameColumn(
                name: "STenDanhMucLoaiChi",
                table: "BH_DM_LoaiChi",
                newName: "sTenDanhMucLoaiChi");

            migrationBuilder.RenameColumn(
                name: "SNguoiTao",
                table: "BH_DM_LoaiChi",
                newName: "sNguoiTao");

            migrationBuilder.RenameColumn(
                name: "SNguoiSua",
                table: "BH_DM_LoaiChi",
                newName: "sNguoiSua");

            migrationBuilder.RenameColumn(
                name: "SMoTa",
                table: "BH_DM_LoaiChi",
                newName: "sMoTa");

            migrationBuilder.RenameColumn(
                name: "ITrangThai",
                table: "BH_DM_LoaiChi",
                newName: "iTrangThai");

            migrationBuilder.RenameColumn(
                name: "INamLamViec",
                table: "BH_DM_LoaiChi",
                newName: "iNamLamViec");

            migrationBuilder.RenameColumn(
                name: "DNgayTao",
                table: "BH_DM_LoaiChi",
                newName: "dNgayTao");

            migrationBuilder.RenameColumn(
                name: "DNgaySua",
                table: "BH_DM_LoaiChi",
                newName: "dNgaySua");

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 100,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTM",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sNG",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sM",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sL",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sK",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<int>(
                name: "iLoaiChungTu",
                table: "BH_DTC_DuToanChiTrenGiao",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_BH_DTC_ChiTiet",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_BH_DTC",
                table: "BH_DTC_DieuChinhDuToanChi",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "iID",
                table: "BH_DM_LoaiChi",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "sLNS",
                table: "BH_DM_LoaiChi",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_DM_LoaiChi",
                table: "BH_DM_LoaiChi",
                column: "iID");

            migrationBuilder.CreateTable(
                name: "BH_DTTM_BHYT_ThanNhan",
                columns: table => new
                {
                    iID_DTTM_BHYT_ThanNhan = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: true),
                    dNgayChungTu = table.Column<DateTime>(nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(nullable: true),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    fDuToan = table.Column<double>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iLoaiDuToan = table.Column<int>(nullable: true),
                    iNamChungTu = table.Column<int>(nullable: true),
                    sDSLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTTM_BHYT_ThanNhan", x => x.iID_DTTM_BHYT_ThanNhan);
                });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.8_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.8_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.8_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.8_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DTTM_BHYT_ThanNhan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_DM_LoaiChi",
                table: "BH_DM_LoaiChi");

            migrationBuilder.DropColumn(
                name: "sLNS",
                table: "BH_DM_LoaiChi");

            migrationBuilder.RenameTable(
                name: "BH_DM_LoaiChi",
                newName: "BH_DanhMucLoaiChi");

            migrationBuilder.RenameColumn(
                name: "BIsKhoa",
                table: "BH_DTC_PhanBoDuToanChi",
                newName: "bIsKhoa");

            migrationBuilder.RenameColumn(
                name: "BIsKhoa",
                table: "BH_DTC_DuToanChiTrenGiao",
                newName: "bIsKhoa");

            migrationBuilder.RenameColumn(
                name: "sTM",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "STM");

            migrationBuilder.RenameColumn(
                name: "sNoiDung",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "SNoiDung");

            migrationBuilder.RenameColumn(
                name: "sNguoiTao",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "SNguoiTao");

            migrationBuilder.RenameColumn(
                name: "sNguoiSua",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "SNguoiSua");

            migrationBuilder.RenameColumn(
                name: "sM",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "SM");

            migrationBuilder.RenameColumn(
                name: "sGhiChu",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "SGhiChu");

            migrationBuilder.RenameColumn(
                name: "iID_MucLucNganSach",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "IID_MucLucNganSach");

            migrationBuilder.RenameColumn(
                name: "fTienUocThucHienCaNam",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "FTienUocThucHienCaNam");

            migrationBuilder.RenameColumn(
                name: "fTienUocThucHien06ThangCuoiNam",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "FTienUocThucHien06ThangCuoiNam");

            migrationBuilder.RenameColumn(
                name: "fTienThucHien06ThangDauNam",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "FTienThucHien06ThangDauNam");

            migrationBuilder.RenameColumn(
                name: "fTienSoSanhTang",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "FTienSoSanhTang");

            migrationBuilder.RenameColumn(
                name: "fTienSoSanhGiam",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "FTienSoSanhGiam");

            migrationBuilder.RenameColumn(
                name: "fTienDuToanDuocGiao",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "FTienDuToanDuocGiao");

            migrationBuilder.RenameColumn(
                name: "dNgayTao",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "DNgayTao");

            migrationBuilder.RenameColumn(
                name: "dNgaySua",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "DNgaySua");

            migrationBuilder.RenameColumn(
                name: "iID_BH_DTC",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                newName: "IID_DTC_DieuChinhDuToanChi");

            migrationBuilder.RenameColumn(
                name: "sTongHop",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "STongHop");

            migrationBuilder.RenameColumn(
                name: "sSoQuyetDinh",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "SSoQuyetDinh");

            migrationBuilder.RenameColumn(
                name: "sSoChungTu",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "SSoChungTu");

            migrationBuilder.RenameColumn(
                name: "sNguoiTao",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "SNguoiTao");

            migrationBuilder.RenameColumn(
                name: "sNguoiSua",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "SNguoiSua");

            migrationBuilder.RenameColumn(
                name: "sMoTa",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "SMoTa");

            migrationBuilder.RenameColumn(
                name: "sLNS",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "SLNS");

            migrationBuilder.RenameColumn(
                name: "iNamChungTu",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "INamChungTu");

            migrationBuilder.RenameColumn(
                name: "iLoaiTongHop",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "ILoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "iID_TongHopID",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "IID_TongHopID");

            migrationBuilder.RenameColumn(
                name: "iID_MaDonVi",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "IID_MaDonVi");

            migrationBuilder.RenameColumn(
                name: "iID_LoaiCap",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "IID_LoaiCap");

            migrationBuilder.RenameColumn(
                name: "fTienUocThucHienCaNam",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "FTienUocThucHienCaNam");

            migrationBuilder.RenameColumn(
                name: "fTienUocThucHien06ThangCuoiNam",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "FTienUocThucHien06ThangCuoiNam");

            migrationBuilder.RenameColumn(
                name: "fTienThucHien06ThangDauNam",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "FTienThucHien06ThangDauNam");

            migrationBuilder.RenameColumn(
                name: "fTienSoSanhTang",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "FTienSoSanhTang");

            migrationBuilder.RenameColumn(
                name: "fTienSoSanhGiam",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "FTienSoSanhGiam");

            migrationBuilder.RenameColumn(
                name: "fTienDuToanDuocGiao",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "FTienDuToanDuocGiao");

            migrationBuilder.RenameColumn(
                name: "dNgayTao",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "DNgayTao");

            migrationBuilder.RenameColumn(
                name: "dNgaySua",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "DNgaySua");

            migrationBuilder.RenameColumn(
                name: "dNgayQuyetDinh",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "DNgayQuyetDinh");

            migrationBuilder.RenameColumn(
                name: "dNgayChungTu",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "DNgayChungTu");

            migrationBuilder.RenameColumn(
                name: "bIsKhoa",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "BIsKhoa");

            migrationBuilder.RenameColumn(
                name: "iID_BH_DTC",
                table: "BH_DTC_DieuChinhDuToanChi",
                newName: "iID_DTC_DieuChinhDuToanChi");

            migrationBuilder.RenameColumn(
                name: "sTenDanhMucLoaiChi",
                table: "BH_DanhMucLoaiChi",
                newName: "STenDanhMucLoaiChi");

            migrationBuilder.RenameColumn(
                name: "sNguoiTao",
                table: "BH_DanhMucLoaiChi",
                newName: "SNguoiTao");

            migrationBuilder.RenameColumn(
                name: "sNguoiSua",
                table: "BH_DanhMucLoaiChi",
                newName: "SNguoiSua");

            migrationBuilder.RenameColumn(
                name: "sMoTa",
                table: "BH_DanhMucLoaiChi",
                newName: "SMoTa");

            migrationBuilder.RenameColumn(
                name: "iTrangThai",
                table: "BH_DanhMucLoaiChi",
                newName: "ITrangThai");

            migrationBuilder.RenameColumn(
                name: "iNamLamViec",
                table: "BH_DanhMucLoaiChi",
                newName: "INamLamViec");

            migrationBuilder.RenameColumn(
                name: "dNgayTao",
                table: "BH_DanhMucLoaiChi",
                newName: "DNgayTao");

            migrationBuilder.RenameColumn(
                name: "dNgaySua",
                table: "BH_DanhMucLoaiChi",
                newName: "DNgaySua");

            migrationBuilder.AlterColumn<string>(
                name: "sXauNoiMa",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTM",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sNG",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sM",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sLNS",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sL",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sK",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AddColumn<Guid>(
                name: "BhDtPhanBoChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iLoaiChungTu",
                table: "BH_DTC_DuToanChiTrenGiao",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_BH_DTC_ChiTiet",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "iID_DTC_DieuChinhDuToanChi",
                table: "BH_DTC_DieuChinhDuToanChi",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "iID",
                table: "BH_DanhMucLoaiChi",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_DanhMucLoaiChi",
                table: "BH_DanhMucLoaiChi",
                column: "iID");

            migrationBuilder.CreateIndex(
                name: "IX_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_BhDtPhanBoChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                column: "BhDtPhanBoChungTuId");

            migrationBuilder.CreateIndex(
                name: "IX_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_ChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                column: "ChungTuId");

            migrationBuilder.AddForeignKey(
                name: "FK_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_BH_DTT_BHXH_PhanBo_ChungTu_BhDtPhanBoChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                column: "BhDtPhanBoChungTuId",
                principalTable: "BH_DTT_BHXH_PhanBo_ChungTu",
                principalColumn: "iID_DTT_BHXH_PhanBo_ChungTu",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BH_DTT_BHXH_PhanBo_ChungTuChiTiet_NS_DT_ChungTu_ChungTuId",
                table: "BH_DTT_BHXH_PhanBo_ChungTuChiTiet",
                column: "ChungTuId",
                principalTable: "NS_DT_ChungTu",
                principalColumn: "iID_DTChungTu",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
