using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11428 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_TL_CanBo_PhuCap_NQ104_TL_DM_CanBo_MA_CBO",
            //    table: "TL_CanBo_PhuCap_NQ104");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_TL_DS_CapNhap_BangLuong_NQ104_TL_DM_DonVi_Ma_CBo",
            //    table: "TL_DS_CapNhap_BangLuong_NQ104");

            migrationBuilder.DropColumn(
                name: "ngay_bao_luu_cb",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "tien_luong_cb_cu",
                table: "TL_DM_CanBo");

            migrationBuilder.CreateTable(
                name: "TL_DM_DonVi_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iTrangThai = table.Column<bool>(nullable: true),
                    Ma_DonVi = table.Column<string>(maxLength: 20, nullable: false),
                    Parent_id = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    Ten_DonVi = table.Column<string>(maxLength: 100, nullable: true),
                    XauNoiMa = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_DonVi_NQ104", x => x.Id);
                    table.UniqueConstraint("AK_TL_DM_DonVi_NQ104_Ma_DonVi", x => x.Ma_DonVi);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_TangGiam_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Loai_TangGiam = table.Column<int>(nullable: true),
                    Ma_TangGiam = table.Column<string>(maxLength: 50, nullable: true),
                    Parent = table.Column<string>(maxLength: 20, nullable: true),
                    Readonly = table.Column<bool>(nullable: true),
                    Splits = table.Column<bool>(nullable: true),
                    Ten_TangGiam = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_TangGiam_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_ThueThuNhapCaNhan_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsThueThang = table.Column<bool>(nullable: false),
                    Loai_Thue = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Readonly = table.Column<bool>(nullable: true),
                    Splits = table.Column<bool>(nullable: true),
                    Ten_Thue = table.Column<string>(maxLength: 50, nullable: true),
                    ThuNhap_Den = table.Column<decimal>(type: "numeric(15, 2)", nullable: true),
                    ThuNhap_Tu = table.Column<decimal>(type: "numeric(15, 2)", nullable: true),
                    Thue_Xuat = table.Column<decimal>(type: "numeric(5, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_ThueThuNhapCaNhan_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_CanBo_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BHTN = table.Column<bool>(nullable: true),
                    bNuocNgoai = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    bTinhBHXH = table.Column<bool>(nullable: true),
                    Cb_KeHoach = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Cccd = table.Column<string>(maxLength: 50, nullable: true),
                    dan_toc = table.Column<string>(maxLength: 100, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Dia_Chi = table.Column<string>(maxLength: 500, nullable: true),
                    dien_quan_ly = table.Column<string>(maxLength: 100, nullable: true),
                    Dien_Thoai = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    GTGC = table.Column<decimal>(type: "numeric(15, 2)", nullable: true),
                    HeSoLuong = table.Column<decimal>(nullable: true),
                    HsLuongKeHoach = table.Column<decimal>(type: "numeric(16, 4)", nullable: true),
                    HsLuongTran = table.Column<decimal>(type: "numeric(16, 4)", nullable: true),
                    iTrangThai = table.Column<int>(nullable: false),
                    IdLuongTran = table.Column<Guid>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: true),
                    IsLock = table.Column<bool>(nullable: true),
                    Is_Nam = table.Column<bool>(nullable: true),
                    is_nang_luong_cb = table.Column<bool>(nullable: false),
                    is_nang_luong_cvd = table.Column<bool>(nullable: false),
                    Khong_Luong = table.Column<bool>(nullable: true),
                    lan_nang_luong_cb = table.Column<int>(nullable: true),
                    lan_nang_luong_cvd = table.Column<int>(nullable: true),
                    loai = table.Column<string>(maxLength: 10, nullable: true),
                    loai_doi_tuong = table.Column<string>(maxLength: 10, nullable: true),
                    ma_bac_luong = table.Column<string>(maxLength: 10, nullable: true),
                    Ma_BL = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CanBo = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_CB = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ma_cb104 = table.Column<string>(maxLength: 10, nullable: true),
                    Ma_CbCu = table.Column<string>(nullable: true),
                    Ma_CV = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ma_cvd104 = table.Column<string>(maxLength: 10, nullable: true),
                    Ma_DiaBan_HC = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Ma_Hieu_CanBo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    Ma_KhoBac = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Ma_PBan = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ma_so_dinh_danh = table.Column<string>(maxLength: 100, nullable: true),
                    MaSo_DV_SDNS = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    MaSo_VAT = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_TangGiam = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_TangGiamCu = table.Column<string>(nullable: true),
                    MaTK_LQ = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Nam = table.Column<int>(nullable: true),
                    nam_bao_luu_cb = table.Column<int>(nullable: true),
                    nam_bao_luu_cvd = table.Column<int>(nullable: true),
                    Nam_TN = table.Column<int>(nullable: true),
                    Nam_VK = table.Column<int>(nullable: true),
                    ngay_bao_luu_cb = table.Column<DateTime>(type: "datetime", nullable: true),
                    ngay_bao_luu_cvd = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayCap_CMT = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_NhanCB = table.Column<DateTime>(type: "datetime", nullable: true),
                    ngay_nhan_cb_den_ngay = table.Column<DateTime>(type: "datetime", nullable: true),
                    ngay_nhan_cb_tu_ngay = table.Column<DateTime>(type: "datetime", nullable: true),
                    ngay_nhan_cvd_den_ngay = table.Column<DateTime>(type: "datetime", nullable: true),
                    ngay_nhan_cvd_tu_ngay = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_NN = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_TN = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayTruyLinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_XN = table.Column<DateTime>(type: "datetime", nullable: true),
                    Nhom = table.Column<string>(maxLength: 50, nullable: true),
                    nhom_chuyen_mon = table.Column<string>(maxLength: 10, nullable: true),
                    nhom_mau = table.Column<string>(maxLength: 100, nullable: true),
                    NoiCap_CMT = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    NoiCongTac = table.Column<string>(maxLength: 500, nullable: true),
                    noi_dang_ky_khai_sinh = table.Column<string>(maxLength: 100, nullable: true),
                    PCCV = table.Column<bool>(nullable: true),
                    Parent = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ParentOld = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    que_quan = table.Column<string>(maxLength: 100, nullable: true),
                    Readonly = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    so_chung_minh_quan_doi = table.Column<string>(maxLength: 100, nullable: true),
                    So_CMT = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    so_nguoi_phu_thuoc = table.Column<int>(nullable: true),
                    So_SoLuong = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    So_TaiKhoan = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    so_thang_tinh_bao_luu_cb = table.Column<int>(nullable: true),
                    so_thang_tinh_bao_luu_cvd = table.Column<int>(nullable: true),
                    Splits = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    Ten_CanBo = table.Column<string>(maxLength: 150, nullable: true),
                    Ten_DonVi = table.Column<string>(maxLength: 100, nullable: true),
                    Ten_KhoBac = table.Column<string>(maxLength: 100, nullable: true),
                    Thang = table.Column<int>(nullable: true),
                    Thang_TNN = table.Column<int>(nullable: true),
                    ThoiHan_TangCb = table.Column<int>(nullable: true),
                    tien_bao_luu_cb = table.Column<decimal>(nullable: true),
                    tien_bao_luu_cvd = table.Column<decimal>(nullable: true),
                    tien_luong_cb = table.Column<decimal>(nullable: true),
                    tien_luong_cb_cu = table.Column<decimal>(nullable: true),
                    tien_luong_cvd = table.Column<decimal>(nullable: true),
                    tien_luong_cvd_cu = table.Column<decimal>(nullable: true),
                    tien_nang_luong_cb = table.Column<decimal>(nullable: true),
                    tien_nang_luong_cvd = table.Column<decimal>(nullable: true),
                    TM = table.Column<bool>(nullable: true),
                    ton_giao = table.Column<string>(maxLength: 100, nullable: true),
                    ty_le_huong_nn = table.Column<decimal>(type: "numeric(16, 4)", nullable: true),
                    UserCreator = table.Column<string>(maxLength: 255, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 255, nullable: true),
                    bKhongTinhNTN = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_CanBo_NQ104", x => x.Id);
                    table.UniqueConstraint("AK_TL_DM_CanBo_NQ104_Ma_CanBo", x => x.Ma_CanBo);
                    //table.ForeignKey(
                    //    name: "FK_TL_DM_CanBo_NQ104_TL_DM_DonVi_NQ104_Parent",
                    //    column: x => x.Parent,
                    //    principalTable: "TL_DM_DonVi_NQ104",
                    //    principalColumn: "Ma_DonVi",
                    //    onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TL_DM_CanBo_NQ104_Parent",
                table: "TL_DM_CanBo_NQ104",
                column: "Parent");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TL_CanBo_PhuCap_NQ104_TL_DM_CanBo_NQ104_MA_CBO",
            //    table: "TL_CanBo_PhuCap_NQ104",
            //    column: "MA_CBO",
            //    principalTable: "TL_DM_CanBo_NQ104",
            //    principalColumn: "Ma_CanBo",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TL_DS_CapNhap_BangLuong_NQ104_TL_DM_DonVi_NQ104_Ma_CBo",
            //    table: "TL_DS_CapNhap_BangLuong_NQ104",
            //    column: "Ma_CBo",
            //    principalTable: "TL_DM_DonVi_NQ104",
            //    principalColumn: "Ma_DonVi",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TL_QT_ChungTu_NQ104_TL_DM_DonVi_NQ104_Ma_DonVi",
            //    table: "TL_QT_ChungTu_NQ104",
            //    column: "Ma_DonVi",
            //    principalTable: "TL_DM_DonVi_NQ104",
            //    principalColumn: "Ma_DonVi",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.8_social_insurance.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.8_salary_0.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.8_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.8_salary_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TL_CanBo_PhuCap_NQ104_TL_DM_CanBo_NQ104_MA_CBO",
                table: "TL_CanBo_PhuCap_NQ104");

            migrationBuilder.DropForeignKey(
                name: "FK_TL_DS_CapNhap_BangLuong_NQ104_TL_DM_DonVi_NQ104_Ma_CBo",
                table: "TL_DS_CapNhap_BangLuong_NQ104");

            migrationBuilder.DropForeignKey(
                name: "FK_TL_QT_ChungTu_NQ104_TL_DM_DonVi_NQ104_Ma_DonVi",
                table: "TL_QT_ChungTu_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_CanBo_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_TangGiam_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_ThueThuNhapCaNhan_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_DonVi_NQ104");

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_bao_luu_cb",
                table: "TL_DM_CanBo",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_luong_cb_cu",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TL_CanBo_PhuCap_NQ104_TL_DM_CanBo_MA_CBO",
                table: "TL_CanBo_PhuCap_NQ104",
                column: "MA_CBO",
                principalTable: "TL_DM_CanBo",
                principalColumn: "Ma_CanBo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TL_DS_CapNhap_BangLuong_NQ104_TL_DM_DonVi_Ma_CBo",
                table: "TL_DS_CapNhap_BangLuong_NQ104",
                column: "Ma_CBo",
                principalTable: "TL_DM_DonVi",
                principalColumn: "Ma_DonVi",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
