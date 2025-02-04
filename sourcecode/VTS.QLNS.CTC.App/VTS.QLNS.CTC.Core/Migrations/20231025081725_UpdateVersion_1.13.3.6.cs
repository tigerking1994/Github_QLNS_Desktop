using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11336 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_KHC_KinhPhiQuanLy_ChiTiet",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_KHC_KinhPhiQuanLy",
                table: "BH_KHC_KinhPhiQuanLy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_KHC_KCB",
                table: "BH_KHC_KCB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_KHC_CheDoBHXH",
                table: "BH_KHC_CheDoBHXH");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_DTC_DieuChinhDuToanChi_ChiTiet",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_DTC_DieuChinhDuToanChi",
                table: "BH_DTC_DieuChinhDuToanChi");

            migrationBuilder.DropColumn(
                name: "ILoaiKCB",
                table: "BH_KHC_KCB");

            migrationBuilder.RenameColumn(
                name: "STongHop",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "sTongHop");

            migrationBuilder.RenameColumn(
                name: "SNguoiTao",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "sNguoiTao");

            migrationBuilder.RenameColumn(
                name: "SNguoiSua",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "sNguoiSua");

            migrationBuilder.RenameColumn(
                name: "ILoaiTongHop",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "iLoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "IID_TongHopID",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "iID_TongHopID");

            migrationBuilder.RenameColumn(
                name: "FTongTienUocThucHienNamTruoc",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "fTongTienUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "FTongTienTaiChinh",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "fTongTienTaiChinh");

            migrationBuilder.RenameColumn(
                name: "FTongTienQuanY",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "fTongTienQuanY");

            migrationBuilder.RenameColumn(
                name: "FTongTienQuanLuc",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "fTongTienQuanLuc");

            migrationBuilder.RenameColumn(
                name: "FTongTienKeHoachThucHienNamNay",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "fTongTienKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "FTongTienDaThucHienNamTruoc",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "fTongTienDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "FTongTienCanBo",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "fTongTienCanBo");

            migrationBuilder.RenameColumn(
                name: "DNgayTao",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "dNgayTao");

            migrationBuilder.RenameColumn(
                name: "DNgaySua",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "dNgaySua");

            migrationBuilder.RenameColumn(
                name: "BIsKhoa",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "bIsKhoa");

            migrationBuilder.RenameColumn(
                name: "STongHop",
                table: "BH_KHC_KCB",
                newName: "sTongHop");

            migrationBuilder.RenameColumn(
                name: "SNguoiTao",
                table: "BH_KHC_KCB",
                newName: "sNguoiTao");

            migrationBuilder.RenameColumn(
                name: "SNguoiSua",
                table: "BH_KHC_KCB",
                newName: "sNguoiSua");

            migrationBuilder.RenameColumn(
                name: "ILoaiTongHop",
                table: "BH_KHC_KCB",
                newName: "iLoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "IID_TongHopID",
                table: "BH_KHC_KCB",
                newName: "iID_TongHopID");

            migrationBuilder.RenameColumn(
                name: "FTongTienUocThucHienNamTruoc",
                table: "BH_KHC_KCB",
                newName: "fTongTienUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "FTongTienKeHoachThucHienNamNay",
                table: "BH_KHC_KCB",
                newName: "fTongTienKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "FTongTienDaThucHienNamTruoc",
                table: "BH_KHC_KCB",
                newName: "fTongTienDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "DNgayTao",
                table: "BH_KHC_KCB",
                newName: "dNgayTao");

            migrationBuilder.RenameColumn(
                name: "DNgaySua",
                table: "BH_KHC_KCB",
                newName: "dNgaySua");

            migrationBuilder.RenameColumn(
                name: "BIsKhoa",
                table: "BH_KHC_KCB",
                newName: "bIsKhoa");

            migrationBuilder.RenameColumn(
                name: "STongHop",
                table: "BH_KHC_CheDoBHXH",
                newName: "sTongHop");

            migrationBuilder.RenameColumn(
                name: "SNguoiTao",
                table: "BH_KHC_CheDoBHXH",
                newName: "sNguoiTao");

            migrationBuilder.RenameColumn(
                name: "SNguoiSua",
                table: "BH_KHC_CheDoBHXH",
                newName: "sNguoiSua");

            migrationBuilder.RenameColumn(
                name: "ITongSoUocThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "ITongSoSQ",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoSQ");

            migrationBuilder.RenameColumn(
                name: "ITongSoQNCN",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoQNCN");

            migrationBuilder.RenameColumn(
                name: "ITongSoLDHD",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoLDHD");

            migrationBuilder.RenameColumn(
                name: "ITongSoKeHoachThucHienNamNay",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "ITongSoHSQBS",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoHSQBS");

            migrationBuilder.RenameColumn(
                name: "ITongSoDaThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "ITongSoCNVQP",
                table: "BH_KHC_CheDoBHXH",
                newName: "iTongSoCNVQP");

            migrationBuilder.RenameColumn(
                name: "ILoaiTongHop",
                table: "BH_KHC_CheDoBHXH",
                newName: "iLoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "IID_TongHopID",
                table: "BH_KHC_CheDoBHXH",
                newName: "iID_TongHopID");

            migrationBuilder.RenameColumn(
                name: "FTongTienUocThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "FTongTienSQ",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienSQ");

            migrationBuilder.RenameColumn(
                name: "FTongTienQNCN",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienQNCN");

            migrationBuilder.RenameColumn(
                name: "FTongTienLDHD",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienLDHD");

            migrationBuilder.RenameColumn(
                name: "FTongTienKeHoachThucHienNamNay",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "FTongTienHSQBS",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienHSQBS");

            migrationBuilder.RenameColumn(
                name: "FTongTienDaThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "FTongTienCNVQP",
                table: "BH_KHC_CheDoBHXH",
                newName: "fTongTienCNVQP");

            migrationBuilder.RenameColumn(
                name: "DNgayTao",
                table: "BH_KHC_CheDoBHXH",
                newName: "dNgayTao");

            migrationBuilder.RenameColumn(
                name: "DNgaySua",
                table: "BH_KHC_CheDoBHXH",
                newName: "dNgaySua");

            migrationBuilder.RenameColumn(
                name: "BIsKhoa",
                table: "BH_KHC_CheDoBHXH",
                newName: "bIsKhoa");

            migrationBuilder.AddColumn<double>(
                name: "fConLaiChuaGiaiNgan_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fConLaiChuaGiaiNgan_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiThuaNopNSNN_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fKinhPhiThuaNopNSNN_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_ChiPhiID",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sDSLNS",
                table: "BH_QTC_Quy_KPK",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fHSBL",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuongChinh",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fNghiOm",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fPCTNNghe",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fPCTNVuotKhung",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fPhuCapChucVu",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongQTLN",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iQSBQNam",
                table: "BH_KHT_BHXH",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_KHC_KinhPhiQuanLy",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_KHC_KinhPhiQuanLy",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayQuyetDinh",
                table: "BH_KHC_KinhPhiQuanLy",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayChungTu",
                table: "BH_KHC_KinhPhiQuanLy",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_KHC_KCB_ChiTiet",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_KHC_KCB_ChiTiet",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_KHC_KCB",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_KHC_KCB",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayQuyetDinh",
                table: "BH_KHC_KCB",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayChungTu",
                table: "BH_KHC_KCB",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_KHC_CheDoBHXH",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_KHC_CheDoBHXH",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayQuyetDinh",
                table: "BH_KHC_CheDoBHXH",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayChungTu",
                table: "BH_KHC_CheDoBHXH",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_DTC_DieuChinhDuToanChi",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_DTC_DieuChinhDuToanChi",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayQuyetDinh",
                table: "BH_DTC_DieuChinhDuToanChi",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayChungTu",
                table: "BH_DTC_DieuChinhDuToanChi",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_KH_KeHoachChi_KinhPhiQuanLy",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                column: "iID_BH_KHC_KinhPhiQuanLy_ChiTiet")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_KHC_KinhPhiQuanLy",
                table: "BH_KHC_KinhPhiQuanLy",
                column: "iID_BH_KHC_KinhPhiQuanLy")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_KHC_KCB",
                table: "BH_KHC_KCB",
                column: "iID_BH_KHC_KCB")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_KH_KeHoachChi",
                table: "BH_KHC_CheDoBHXH",
                column: "id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_DTC_DieuChinhDuToanChi_ChiTiet_1",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                column: "iID_BH_DTC_ChiTiet")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_DTC_DieuChinhDuToanChi",
                table: "BH_DTC_DieuChinhDuToanChi",
                column: "iID_BH_DTC")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateTable(
                name: "BH_KHC_K",
                columns: table => new
                {
                    iID_BH_KHC_K = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongTienDaThucHienNamTruoc = table.Column<double>(nullable: true),
                    fTongTienKeHoachThucHienNamNay = table.Column<double>(nullable: true),
                    fTongTienUocThucHienNamTruoc = table.Column<double>(nullable: true),
                    iIDLoaiChi = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_TongHopID = table.Column<Guid>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_KHC_K", x => x.iID_BH_KHC_K)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_KHC_K_ChiTiet",
                columns: table => new
                {
                    iID_BH_KHC_K_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTienDaThucHienNamTruoc = table.Column<double>(nullable: true),
                    fTienKeHoachThucHienNamNay = table.Column<double>(nullable: true),
                    fTienUocThucHienNamTruoc = table.Column<double>(nullable: true),
                    iID_KHC_K = table.Column<Guid>(nullable: false),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_KHC_K_ChiTiet", x => x.iID_BH_KHC_K_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTC_CapKinhPhi_KCB",
                columns: table => new
                {
                    iID_ChungTu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fConLai = table.Column<double>(nullable: true),
                    fDaQuyetToan = table.Column<double>(nullable: true),
                    fKeHoachCap = table.Column<double>(nullable: true),
                    fQuyetToanQuyNay = table.Column<double>(nullable: true),
                    iLoaiKinhPhi = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iQuy = table.Column<int>(nullable: true),
                    sCoSoYTe = table.Column<string>(nullable: true),
                    sDSLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_CapKinhPhiKCB", x => x.iID_ChungTu)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTC_CapKinhPhi_KCB_ChiTiet",
                columns: table => new
                {
                    iID_ChungTuChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fConLai = table.Column<double>(nullable: true),
                    fDaQuyetToan = table.Column<double>(nullable: true),
                    fKeHoachCap = table.Column<double>(nullable: true),
                    fQuyetToanQuyNay = table.Column<double>(nullable: true),
                    iID_ChungTu = table.Column<Guid>(nullable: false),
                    iID_CoSoYTe = table.Column<Guid>(nullable: false),
                    iID_MLNS = table.Column<Guid>(nullable: false),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sTenCoSoYTe = table.Column<string>(nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true),
                    iID_MaCoSoYTe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_CapKinhPhiKCB_ChiTiet", x => x.iID_ChungTuChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTC_Nam_KPK",
                columns: table => new
                {
                    ID_QTC_Nam_KPK = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    bThucChiTheo4Quy = table.Column<bool>(nullable: true),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTiLeThucHienTrenDuToan = table.Column<double>(nullable: true),
                    fTongTienThieu = table.Column<double>(nullable: true),
                    fTongTienThua = table.Column<double>(nullable: true),
                    fTongTien_DuToanGiaoNamNay = table.Column<double>(nullable: true),
                    fTongTien_DuToanNamTruocChuyenSang = table.Column<double>(nullable: true),
                    fTongTien_ThucChi = table.Column<double>(nullable: true),
                    fTongTien_TongDuToanDuocGiao = table.Column<double>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iID_LoaiChi = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_TongHopID = table.Column<Guid>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Nam_KPK", x => x.ID_QTC_Nam_KPK)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTC_Nam_KPK_ChiTiet",
                columns: table => new
                {
                    ID_QTC_Nam_KPK_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTiLeThucHienTrenDuToan = table.Column<double>(nullable: true),
                    fTienThieu = table.Column<double>(nullable: true),
                    fTienThua = table.Column<double>(nullable: true),
                    fTien_DuToanGiaoNamNay = table.Column<double>(nullable: true),
                    fTien_DuToanNamTruocChuyenSang = table.Column<double>(nullable: true),
                    fTien_ThucChi = table.Column<double>(nullable: true),
                    fTien_TongDuToanDuocGiao = table.Column<double>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: true),
                    iID_QTC_Nam_KPK = table.Column<Guid>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Nam_KPK_ChiTiet", x => x.ID_QTC_Nam_KPK_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTT_BHXH_CTCT_GiaiThich",
                columns: table => new
                {
                    iID_QT_CTCT_GiaiThich = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDenNgay = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dTuNgay = table.Column<DateTime>(type: "datetime", nullable: true),
                    fConPhaiNopTiep = table.Column<double>(nullable: true),
                    fDaNop_TrongQuyNam = table.Column<double>(nullable: true),
                    fPhaiNop_BHXH = table.Column<double>(nullable: true),
                    fPhaiNop_QuyNamTruoc = table.Column<double>(nullable: true),
                    fPhaiNop_TrongQuyNam = table.Column<double>(nullable: true),
                    fQuyTienLuongCanCu = table.Column<double>(nullable: true),
                    fSoConPhaiNop = table.Column<double>(nullable: true),
                    fSoDaNopSau31_12 = table.Column<double>(nullable: true),
                    fSoDaNopTrongNam = table.Column<double>(nullable: true),
                    fSoPhaiThuNop = table.Column<double>(nullable: true),
                    fSoTienGiamDong = table.Column<double>(nullable: true),
                    fTongSoDaNop = table.Column<double>(nullable: true),
                    fTongTruyThu_BHXH = table.Column<double>(nullable: true),
                    fTruyThu_BHTN_NLD = table.Column<double>(nullable: true),
                    fTruyThu_BHTN_NSD = table.Column<double>(nullable: true),
                    fTruyThu_BHTN_TongCong = table.Column<double>(nullable: true),
                    fTruyThu_BHXH_NLD = table.Column<double>(nullable: true),
                    fTruyThu_BHXH_NSD = table.Column<double>(nullable: true),
                    fTruyThu_BHXH_TongCong = table.Column<double>(nullable: true),
                    fTruyThu_BHYT_NLD = table.Column<double>(nullable: true),
                    fTruyThu_BHYT_NSD = table.Column<double>(nullable: true),
                    fTruyThu_BHYT_TongCong = table.Column<double>(nullable: true),
                    fTruyThu_QuyNamTruoc = table.Column<double>(nullable: true),
                    iID_MLNS = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    ILoaiGiaiThich = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iQuanSo = table.Column<int>(nullable: true),
                    iQuyNam = table.Column<int>(nullable: false),
                    iQuyNamLoai = table.Column<int>(nullable: false),
                    iID_QTT_BHXH_ChungTu = table.Column<Guid>(nullable: true),
                    sK = table.Column<string>(nullable: true),
                    sKienNghi = table.Column<string>(nullable: true),
                    sL = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sM = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true),
                    sQuyNamMoTa = table.Column<string>(nullable: true),
                    sTM = table.Column<string>(nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTT_BHXH_CTCT_GiaiThich", x => x.iID_QT_CTCT_GiaiThich)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTT_MucLucGiaiThich",
                columns: table => new
                {
                    iID_MLGT = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iLoai = table.Column<int>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iSTT = table.Column<int>(nullable: false),
                    sNoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTT_MucLucGiaiThich", x => x.iID_MLGT)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_CheDoBHXH",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iLoaiCheDo = table.Column<int>(nullable: true),
                    bTinhTheoCongThuc = table.Column<bool>(nullable: true),
                    sMaCheDo = table.Column<string>(nullable: true),
                    sMaCheDoCha = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenCheDo = table.Column<string>(nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_CheDoBHXH", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.6_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.6_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.6_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.6_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.6_social_insurance_5.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.6_social_insurance_6.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_KHC_K");

            migrationBuilder.DropTable(
                name: "BH_KHC_K_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_QTC_CapKinhPhi_KCB");

            migrationBuilder.DropTable(
                name: "BH_QTC_CapKinhPhi_KCB_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_QTC_Nam_KPK");

            migrationBuilder.DropTable(
                name: "BH_QTC_Nam_KPK_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_QTT_BHXH_CTCT_GiaiThich");

            migrationBuilder.DropTable(
                name: "BH_QTT_MucLucGiaiThich");

            migrationBuilder.DropTable(
                name: "TL_DM_CheDoBHXH");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_KH_KeHoachChi_KinhPhiQuanLy",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_KHC_KinhPhiQuanLy",
                table: "BH_KHC_KinhPhiQuanLy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_KHC_KCB",
                table: "BH_KHC_KCB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_KH_KeHoachChi",
                table: "BH_KHC_CheDoBHXH");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_DTC_DieuChinhDuToanChi_ChiTiet_1",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BH_DTC_DieuChinhDuToanChi",
                table: "BH_DTC_DieuChinhDuToanChi");

            migrationBuilder.DropColumn(
                name: "fConLaiChuaGiaiNgan_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fConLaiChuaGiaiNgan_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiThuaNopNSNN_USD",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fKinhPhiThuaNopNSNN_VND",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "iID_ChiPhiID",
                table: "NH_KT_KhoiTaoCapPhat_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sDSLNS",
                table: "BH_QTC_Quy_KPK");

            migrationBuilder.DropColumn(
                name: "fHSBL",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fLuongChinh",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fNghiOm",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fPCTNNghe",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fPCTNVuotKhung",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fPhuCapChucVu",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "fTongQTLN",
                table: "BH_KHT_BHXH");

            migrationBuilder.DropColumn(
                name: "iQSBQNam",
                table: "BH_KHT_BHXH");

            migrationBuilder.RenameColumn(
                name: "sTongHop",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "STongHop");

            migrationBuilder.RenameColumn(
                name: "sNguoiTao",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "SNguoiTao");

            migrationBuilder.RenameColumn(
                name: "sNguoiSua",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "SNguoiSua");

            migrationBuilder.RenameColumn(
                name: "iLoaiTongHop",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "ILoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "iID_TongHopID",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "IID_TongHopID");

            migrationBuilder.RenameColumn(
                name: "fTongTienUocThucHienNamTruoc",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "FTongTienUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "fTongTienTaiChinh",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "FTongTienTaiChinh");

            migrationBuilder.RenameColumn(
                name: "fTongTienQuanY",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "FTongTienQuanY");

            migrationBuilder.RenameColumn(
                name: "fTongTienQuanLuc",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "FTongTienQuanLuc");

            migrationBuilder.RenameColumn(
                name: "fTongTienKeHoachThucHienNamNay",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "FTongTienKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "fTongTienDaThucHienNamTruoc",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "FTongTienDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "fTongTienCanBo",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "FTongTienCanBo");

            migrationBuilder.RenameColumn(
                name: "dNgayTao",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "DNgayTao");

            migrationBuilder.RenameColumn(
                name: "dNgaySua",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "DNgaySua");

            migrationBuilder.RenameColumn(
                name: "bIsKhoa",
                table: "BH_KHC_KinhPhiQuanLy",
                newName: "BIsKhoa");

            migrationBuilder.RenameColumn(
                name: "sTongHop",
                table: "BH_KHC_KCB",
                newName: "STongHop");

            migrationBuilder.RenameColumn(
                name: "sNguoiTao",
                table: "BH_KHC_KCB",
                newName: "SNguoiTao");

            migrationBuilder.RenameColumn(
                name: "sNguoiSua",
                table: "BH_KHC_KCB",
                newName: "SNguoiSua");

            migrationBuilder.RenameColumn(
                name: "iLoaiTongHop",
                table: "BH_KHC_KCB",
                newName: "ILoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "iID_TongHopID",
                table: "BH_KHC_KCB",
                newName: "IID_TongHopID");

            migrationBuilder.RenameColumn(
                name: "fTongTienUocThucHienNamTruoc",
                table: "BH_KHC_KCB",
                newName: "FTongTienUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "fTongTienKeHoachThucHienNamNay",
                table: "BH_KHC_KCB",
                newName: "FTongTienKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "fTongTienDaThucHienNamTruoc",
                table: "BH_KHC_KCB",
                newName: "FTongTienDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "dNgayTao",
                table: "BH_KHC_KCB",
                newName: "DNgayTao");

            migrationBuilder.RenameColumn(
                name: "dNgaySua",
                table: "BH_KHC_KCB",
                newName: "DNgaySua");

            migrationBuilder.RenameColumn(
                name: "bIsKhoa",
                table: "BH_KHC_KCB",
                newName: "BIsKhoa");

            migrationBuilder.RenameColumn(
                name: "sTongHop",
                table: "BH_KHC_CheDoBHXH",
                newName: "STongHop");

            migrationBuilder.RenameColumn(
                name: "sNguoiTao",
                table: "BH_KHC_CheDoBHXH",
                newName: "SNguoiTao");

            migrationBuilder.RenameColumn(
                name: "sNguoiSua",
                table: "BH_KHC_CheDoBHXH",
                newName: "SNguoiSua");

            migrationBuilder.RenameColumn(
                name: "iTongSoUocThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "iTongSoSQ",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoSQ");

            migrationBuilder.RenameColumn(
                name: "iTongSoQNCN",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoQNCN");

            migrationBuilder.RenameColumn(
                name: "iTongSoLDHD",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoLDHD");

            migrationBuilder.RenameColumn(
                name: "iTongSoKeHoachThucHienNamNay",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "iTongSoHSQBS",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoHSQBS");

            migrationBuilder.RenameColumn(
                name: "iTongSoDaThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "iTongSoCNVQP",
                table: "BH_KHC_CheDoBHXH",
                newName: "ITongSoCNVQP");

            migrationBuilder.RenameColumn(
                name: "iLoaiTongHop",
                table: "BH_KHC_CheDoBHXH",
                newName: "ILoaiTongHop");

            migrationBuilder.RenameColumn(
                name: "iID_TongHopID",
                table: "BH_KHC_CheDoBHXH",
                newName: "IID_TongHopID");

            migrationBuilder.RenameColumn(
                name: "fTongTienUocThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienUocThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "fTongTienSQ",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienSQ");

            migrationBuilder.RenameColumn(
                name: "fTongTienQNCN",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienQNCN");

            migrationBuilder.RenameColumn(
                name: "fTongTienLDHD",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienLDHD");

            migrationBuilder.RenameColumn(
                name: "fTongTienKeHoachThucHienNamNay",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienKeHoachThucHienNamNay");

            migrationBuilder.RenameColumn(
                name: "fTongTienHSQBS",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienHSQBS");

            migrationBuilder.RenameColumn(
                name: "fTongTienDaThucHienNamTruoc",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienDaThucHienNamTruoc");

            migrationBuilder.RenameColumn(
                name: "fTongTienCNVQP",
                table: "BH_KHC_CheDoBHXH",
                newName: "FTongTienCNVQP");

            migrationBuilder.RenameColumn(
                name: "dNgayTao",
                table: "BH_KHC_CheDoBHXH",
                newName: "DNgayTao");

            migrationBuilder.RenameColumn(
                name: "dNgaySua",
                table: "BH_KHC_CheDoBHXH",
                newName: "DNgaySua");

            migrationBuilder.RenameColumn(
                name: "bIsKhoa",
                table: "BH_KHC_CheDoBHXH",
                newName: "BIsKhoa");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DNgayTao",
                table: "BH_KHC_KinhPhiQuanLy",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DNgaySua",
                table: "BH_KHC_KinhPhiQuanLy",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayQuyetDinh",
                table: "BH_KHC_KinhPhiQuanLy",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayChungTu",
                table: "BH_KHC_KinhPhiQuanLy",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_KHC_KCB_ChiTiet",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_KHC_KCB_ChiTiet",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DNgayTao",
                table: "BH_KHC_KCB",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DNgaySua",
                table: "BH_KHC_KCB",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayQuyetDinh",
                table: "BH_KHC_KCB",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayChungTu",
                table: "BH_KHC_KCB",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ILoaiKCB",
                table: "BH_KHC_KCB",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DNgayTao",
                table: "BH_KHC_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DNgaySua",
                table: "BH_KHC_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayQuyetDinh",
                table: "BH_KHC_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayChungTu",
                table: "BH_KHC_CheDoBHXH",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayTao",
                table: "BH_DTC_DieuChinhDuToanChi",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgaySua",
                table: "BH_DTC_DieuChinhDuToanChi",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayQuyetDinh",
                table: "BH_DTC_DieuChinhDuToanChi",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dNgayChungTu",
                table: "BH_DTC_DieuChinhDuToanChi",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_KHC_KinhPhiQuanLy_ChiTiet",
                table: "BH_KHC_KinhPhiQuanLy_ChiTiet",
                column: "iID_BH_KHC_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_KHC_KinhPhiQuanLy",
                table: "BH_KHC_KinhPhiQuanLy",
                column: "iID_BH_KHC_KinhPhiQuanLy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_KHC_KCB",
                table: "BH_KHC_KCB",
                column: "iID_BH_KHC_KCB");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_KHC_CheDoBHXH",
                table: "BH_KHC_CheDoBHXH",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_DTC_DieuChinhDuToanChi_ChiTiet",
                table: "BH_DTC_DieuChinhDuToanChi_ChiTiet",
                column: "iID_BH_DTC_ChiTiet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BH_DTC_DieuChinhDuToanChi",
                table: "BH_DTC_DieuChinhDuToanChi",
                column: "iID_BH_DTC");
        }
    }
}
