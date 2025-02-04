using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11438 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fTienAn",
                table: "NS_MucLucNganSach",
                nullable: false,
                defaultValueSql: "((0))");

            migrationBuilder.CreateTable(
                name: "TL_CanBo_PhuCap_KeHoach_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    DateEnd = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    Gia_Tri = table.Column<decimal>(type: "numeric(17, 3)", nullable: true),
                    HuongPC_SN = table.Column<int>(nullable: true),
                    ISoThang_Huong = table.Column<int>(nullable: true),
                    Ma_CanBo = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_PhuCap = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_CanBo_PhuCap_KeHoach_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DieuChinh_QS_KeHoach_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    Giam_HuuTri = table.Column<int>(nullable: true),
                    Giam_XuatNgu = table.Column<int>(nullable: true),
                    Luong_TuyenSinh = table.Column<double>(nullable: true),
                    Ma_DonVi = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Nam = table.Column<int>(nullable: true),
                    PhuCap_XuatNgu = table.Column<double>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    Tang_NhapNgu = table.Column<int>(nullable: true),
                    Tang_TuyenSinh = table.Column<int>(nullable: true),
                    Ten_DonVi = table.Column<string>(maxLength: 500, nullable: true),
                    Thang = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DieuChinh_QS_KeHoach_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_CanBo_KeHoach_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BHTN = table.Column<bool>(nullable: true),
                    bKhongTinhNTN = table.Column<bool>(unicode: false, nullable: true),
                    bNuocNgoai = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    Cb_KeHoach = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Cccd = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    DiaChi = table.Column<string>(maxLength: 500, nullable: true),
                    DienThoai = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    GTGC = table.Column<decimal>(type: "numeric(15, 2)", nullable: true),
                    HeSoLuong = table.Column<decimal>(nullable: true),
                    HsLuongKeHoach = table.Column<decimal>(type: "numeric(16, 4)", nullable: true),
                    HsLuongTran = table.Column<decimal>(type: "numeric(16, 4)", nullable: true),
                    IsDelete = table.Column<bool>(nullable: true),
                    Is_Nam = table.Column<bool>(nullable: true),
                    Khong_Luong = table.Column<bool>(nullable: true),
                    Loai = table.Column<string>(maxLength: 200, nullable: true),
                    Ma_BL = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CanBo = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CB = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CV = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_DiaBan_HC = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Ma_Hieu_CanBo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    Ma_KhoBac = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Ma_PBan = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    MaSo_DV_SDNS = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    MaSo_VAT = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_TangGiam = table.Column<string>(maxLength: 50, nullable: true),
                    MaTK_LQ = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Nam = table.Column<int>(nullable: true),
                    Nam_TN = table.Column<int>(nullable: true),
                    Nam_VK = table.Column<int>(nullable: true),
                    NgayCap_CMT = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayNhan_CB = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_NN = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_TN = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayTruyLinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_XN = table.Column<DateTime>(type: "datetime", nullable: true),
                    Nhom = table.Column<string>(maxLength: 50, nullable: true),
                    NoiCap_CMT = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    NoiCongTac = table.Column<string>(maxLength: 500, nullable: true),
                    PCCV = table.Column<bool>(nullable: true),
                    Parent = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Readonly = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    So_CMT = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    SoNguoiPhuThuoc = table.Column<decimal>(nullable: true),
                    So_SoLuong = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    So_TaiKhoan = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Splits = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    Ten_CanBo = table.Column<string>(maxLength: 150, nullable: true),
                    Ten_DonVi = table.Column<string>(maxLength: 100, nullable: true),
                    Ten_KhoBac = table.Column<string>(maxLength: 100, nullable: true),
                    Thang = table.Column<int>(nullable: true),
                    Thang_TNN = table.Column<int>(nullable: true),
                    ThoiHan_TangCB = table.Column<int>(nullable: true),
                    TM = table.Column<bool>(nullable: true),
                    UserCreator = table.Column<string>(maxLength: 255, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_CanBo_KeHoach_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_PhuCap_DieuChinh_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    ApDung_Tu = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    GiaTri_Moi = table.Column<decimal>(type: "numeric(17, 3)", nullable: true),
                    Id_PhuCap = table.Column<Guid>(nullable: true),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_PhuCap_DieuChinh_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_QS_KeHoach_ChiTiet_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fPCRQ_BinhNhat = table.Column<decimal>(type: "numeric(15, 2)", nullable: true),
                    fPCRQ_BinhNhi = table.Column<decimal>(type: "numeric(15, 2)", nullable: true),
                    fPCRQ_HaSi = table.Column<decimal>(type: "numeric(15, 2)", nullable: true),
                    fPCRQ_ThuongSi = table.Column<decimal>(type: "numeric(15, 2)", nullable: true),
                    fPCRQ_TrungSi = table.Column<decimal>(type: "numeric(15, 2)", nullable: true),
                    fSoBinhNhat = table.Column<double>(nullable: true),
                    fSoBinhNhi = table.Column<double>(nullable: true),
                    fSoDaiTa = table.Column<double>(nullable: true),
                    fSoDaiUy = table.Column<double>(nullable: true),
                    fSoHaSi = table.Column<double>(nullable: true),
                    fSoQNCN = table.Column<double>(nullable: true),
                    fSoThieuTa = table.Column<double>(nullable: true),
                    fSoThieuUy = table.Column<double>(nullable: true),
                    fSoThuongSi = table.Column<double>(nullable: true),
                    fSoThuongTa = table.Column<double>(nullable: true),
                    fSoThuongUy = table.Column<double>(nullable: true),
                    fSoTrungSi = table.Column<double>(nullable: true),
                    fSoTrungTa = table.Column<double>(nullable: true),
                    fSoTrungUy = table.Column<double>(nullable: true),
                    fSoTuong = table.Column<double>(nullable: true),
                    Ma_DonVi = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Nam = table.Column<int>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    Ten_DonVi = table.Column<string>(maxLength: 500, nullable: true),
                    Thang = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_QS_KeHoach_ChiTiet_NQ104", x => x.Id);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.8_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.8_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.8_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.8_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.8_budget_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.8_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.8_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.8_social_insurance_5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_CanBo_PhuCap_KeHoach_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DieuChinh_QS_KeHoach_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_CanBo_KeHoach_NQ104");

            migrationBuilder.DropTable(
                name: "TL_PhuCap_DieuChinh_NQ104");

            migrationBuilder.DropTable(
                name: "TL_QS_KeHoach_ChiTiet_NQ104");

            migrationBuilder.DropColumn(
                name: "fTienAn",
                table: "NS_MucLucNganSach");
        }
    }
}
