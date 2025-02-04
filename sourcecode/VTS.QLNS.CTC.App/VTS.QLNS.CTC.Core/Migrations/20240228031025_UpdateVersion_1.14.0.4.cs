using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11404 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sQuyNamMoTa",
                table: "BH_QTC_CapKinhPhi_KCB",
                newName: "SQuyNamMoTa");

            migrationBuilder.AddColumn<string>(
                name: "dan_toc",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "dien_quan_ly",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_nang_luong_cb",
                table: "TL_DM_CanBo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_nang_luong_cvd",
                table: "TL_DM_CanBo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "lang_nang_luong_cb",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lang_nang_luong_cvd",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loai",
                table: "TL_DM_CanBo",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loai_doi_tuong",
                table: "TL_DM_CanBo",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ma_bac_luong",
                table: "TL_DM_CanBo",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ma_cb104",
                table: "TL_DM_CanBo",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ma_cvd104",
                table: "TL_DM_CanBo",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ma_so_dinh_danh",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "nam_bao_luu_cb",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "nam_bao_luu_cvd",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_nhan_cb_den_ngay",
                table: "TL_DM_CanBo",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_nhan_cb_tu_ngay",
                table: "TL_DM_CanBo",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_nhan_cvd_den_ngay",
                table: "TL_DM_CanBo",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_nhan_cvd_tu_ngay",
                table: "TL_DM_CanBo",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nhom_chuyen_mon",
                table: "TL_DM_CanBo",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nhom_mau",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "noi_dang_ky_khai_sinh",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "que_quan",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "so_chung_minh_quan_doi",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "so_nguoi_phu_thuoc",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "so_thang_tinh_bao_luu_cb",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "so_thang_tinh_bao_luu_cvd",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_bao_luu_cb",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_bao_luu_cvd",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_luong_cb",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_luong_cvd",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_nang_luong_cb",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_nang_luong_cvd",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ton_giao",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SQuyNamMoTa",
                table: "BH_QTC_CapKinhPhi_KCB",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "BH_CP_ChungTu_ChiTiet",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TL_BangLuong_ThangBHXH_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    nGiaTri = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    nHuongPCSN = table.Column<decimal>(type: "numeric(5, 2)", nullable: true),
                    iLoaiBL = table.Column<int>(nullable: true),
                    sMaCachTL = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    sMaCB = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    sMaCBo = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    sMaCheDo = table.Column<string>(maxLength: 50, nullable: true),
                    sMaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    sMaHieuCanBo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    iNam = table.Column<int>(nullable: true),
                    dNgayHT = table.Column<DateTime>(nullable: true),
                    iID_Parent = table.Column<Guid>(nullable: true),
                    iSoTT = table.Column<int>(nullable: true),
                    sTenCachTL = table.Column<string>(maxLength: 100, nullable: true),
                    sTenCbo = table.Column<string>(maxLength: 100, nullable: true),
                    iThang = table.Column<int>(nullable: true),
                    sUserName = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_BangLuong_ThangBHXH_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_BangLuong_Thang_Bridge_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    gia_tri = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    ma_can_bo = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ma_don_vi = table.Column<string>(maxLength: 50, nullable: true),
                    ma_hieu_can_bo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    ma_phu_cap = table.Column<string>(maxLength: 50, nullable: true),
                    parent = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_BangLuong_Thang_Bridge_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_BangLuong_Thang_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    data = table.Column<string>(nullable: true),
                    Gia_Tri = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    HuongPC_SN = table.Column<decimal>(type: "numeric(5, 2)", nullable: true),
                    Loai_BL = table.Column<int>(nullable: true),
                    Ma_CachTL = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ma_CB = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CBo = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_DonVi = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_Hieu_CanBo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    Ma_PhuCap = table.Column<string>(maxLength: 50, nullable: true),
                    NAM = table.Column<int>(nullable: true),
                    Ngay_HT = table.Column<DateTime>(nullable: true),
                    parent = table.Column<Guid>(nullable: true),
                    So_TT = table.Column<int>(nullable: true),
                    Ten_CachTL = table.Column<string>(maxLength: 100, nullable: true),
                    Ten_Cbo = table.Column<string>(maxLength: 100, nullable: true),
                    THANG = table.Column<int>(nullable: true),
                    User_Name = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_BangLuong_Thang_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_CanBo_PhuCap_Bridge_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    gia_tri = table.Column<decimal>(type: "numeric(17, 4)", nullable: true),
                    ma_can_bo = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    ma_phu_cap = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    ngay_huong_phu_cap = table.Column<decimal>(type: "numeric(5, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_CanBo_PhuCap_Bridge_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_CanBo_PhuCap_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bCapNhat = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bSaoChep = table.Column<bool>(nullable: true),
                    CHON = table.Column<bool>(nullable: true),
                    CONG_THUC = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    data = table.Column<string>(nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    flag = table.Column<bool>(nullable: true),
                    GIA_TRI = table.Column<decimal>(type: "numeric(17, 4)", nullable: true),
                    HE_SO = table.Column<decimal>(type: "numeric(8, 4)", nullable: true),
                    HuongPC_SN = table.Column<decimal>(type: "numeric(5, 2)", nullable: true),
                    ISoThang_Huong = table.Column<int>(nullable: true),
                    MA_CBO = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    MA_KMCP = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    MA_PHUCAP = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    PHANTRAM_CT = table.Column<decimal>(type: "numeric(17, 4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_CanBo_PhuCap_NQ104", x => x.Id);
                    //table.ForeignKey(
                    //    name: "FK_TL_CanBo_PhuCap_NQ104_TL_DM_CanBo_MA_CBO",
                    //    column: x => x.MA_CBO,
                    //    principalTable: "TL_DM_CanBo",
                    //    principalColumn: "Ma_CanBo",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_Cach_TinhLuong_BaoHiem_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    CongThuc = table.Column<string>(type: "ntext", nullable: true),
                    Ma_CachTL = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ma_Cot = table.Column<string>(maxLength: 100, nullable: true),
                    Ma_KMCP = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_KMCP1 = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Nam = table.Column<int>(nullable: true),
                    NoiDung = table.Column<string>(maxLength: 500, nullable: true),
                    Ten_CachTL = table.Column<string>(maxLength: 500, nullable: true),
                    Ten_Cot = table.Column<string>(maxLength: 500, nullable: true),
                    Thang = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_Cach_TinhLuong_BaoHiem_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_Cach_TinhLuong_Chuan_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    CongThuc = table.Column<string>(type: "ntext", nullable: true),
                    Ma_CachTL = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ma_Cot = table.Column<string>(maxLength: 100, nullable: true),
                    Ma_KMCP = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_KMCP1 = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Nam = table.Column<int>(nullable: true),
                    NoiDung = table.Column<string>(maxLength: 500, nullable: true),
                    Ten_CachTL = table.Column<string>(maxLength: 500, nullable: true),
                    Ten_Cot = table.Column<string>(maxLength: 500, nullable: true),
                    Thang = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_Cach_TinhLuong_Chuan_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_Cach_TinhLuong_TruyLinh_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    CongThuc = table.Column<string>(type: "ntext", nullable: true),
                    Ma_CachTL = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ma_Cot = table.Column<string>(maxLength: 100, nullable: true),
                    Ma_KMCP = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_KMCP1 = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Nam = table.Column<int>(nullable: true),
                    NoiDung = table.Column<string>(maxLength: 500, nullable: true),
                    Ten_CachTL = table.Column<string>(maxLength: 500, nullable: true),
                    Ten_Cot = table.Column<string>(maxLength: 500, nullable: true),
                    Thang = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_Cach_TinhLuong_TruyLinh_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_CapBac_Luong_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bhcs_cq = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    bhtn_cq = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    bhxh_cq = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    bhyt_cq = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    hs_bhcs = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    hs_bhtn = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    hs_bhxh = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    hs_bhyt = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    hs_kpcd = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    hs_tro_cap_om_dau = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    is_readonly = table.Column<bool>(nullable: false),
                    kpcd_cq = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    lht_hs = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    loai = table.Column<string>(maxLength: 10, nullable: true),
                    loai_doi_tuong = table.Column<string>(maxLength: 20, nullable: true),
                    ma_dm = table.Column<string>(maxLength: 10, nullable: true),
                    nhom = table.Column<string>(maxLength: 10, nullable: true),
                    nhom_doi_tuong = table.Column<int>(nullable: true),
                    phu_cap_ra_quan = table.Column<decimal>(type: "numeric(16, 0)", nullable: true),
                    ten_dm = table.Column<string>(maxLength: 100, nullable: true),
                    ti_le_huong = table.Column<decimal>(type: "numeric(5, 2)", nullable: true),
                    tien_luong = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_CapBac_Luong_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_CapBac_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    is_readonly = table.Column<bool>(nullable: true),
                    ma_cb = table.Column<string>(maxLength: 10, nullable: true),
                    note = table.Column<string>(maxLength: 100, nullable: true),
                    parent = table.Column<string>(maxLength: 20, nullable: true),
                    ten_cb = table.Column<string>(maxLength: 50, nullable: true),
                    xau_noi_ma = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_CapBac_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_ChucVu_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    loai = table.Column<bool>(nullable: true),
                    ma = table.Column<string>(maxLength: 10, nullable: true),
                    ten = table.Column<string>(maxLength: 50, nullable: true),
                    tien_luong = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_ChucVu_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_PhuCap_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bGiaTri = table.Column<bool>(nullable: true),
                    bHuongPc_Sn = table.Column<bool>(nullable: true),
                    bSaoChep = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    Chon = table.Column<bool>(nullable: true),
                    Cong_Thuc = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    Dinh_Dang = table.Column<bool>(nullable: true),
                    fGiaTriLonNhat = table.Column<decimal>(type: "numeric(17, 3)", nullable: true),
                    fGiaTriNhoNhat = table.Column<decimal>(type: "numeric(17, 3)", nullable: true),
                    fGiaTriPhuCap_KemTheo = table.Column<decimal>(type: "numeric(17, 3)", nullable: true),
                    Gia_Tri = table.Column<decimal>(type: "numeric(17, 3)", nullable: true),
                    He_So = table.Column<decimal>(type: "numeric(8, 4)", nullable: true),
                    HuongPC_SN = table.Column<decimal>(type: "numeric(5, 2)", nullable: true),
                    iDinhDang = table.Column<int>(nullable: true),
                    iId_Ma_PhuCap_KemTheo = table.Column<string>(maxLength: 20, nullable: true),
                    iId_PhuCap_KemTheo = table.Column<Guid>(nullable: true),
                    iLoai = table.Column<int>(nullable: true),
                    Is_Formula = table.Column<bool>(nullable: true),
                    Is_Readonly = table.Column<bool>(nullable: true),
                    IThang_ToiDa = table.Column<int>(nullable: true),
                    Ma_KMCP = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_PhuCap = table.Column<string>(maxLength: 20, nullable: true),
                    Ma_TTM_Ng = table.Column<string>(maxLength: 20, nullable: true),
                    Numeric_Scale = table.Column<int>(nullable: true),
                    Parent = table.Column<string>(maxLength: 20, nullable: true),
                    PhanTram_CT = table.Column<decimal>(type: "numeric(17, 4)", nullable: true),
                    Readonly = table.Column<bool>(nullable: true),
                    Splits = table.Column<bool>(nullable: true),
                    Ten_Ngan = table.Column<string>(maxLength: 127, nullable: true),
                    Ten_NganHang = table.Column<string>(maxLength: 255, nullable: true),
                    Ten_PhuCap = table.Column<string>(maxLength: 100, nullable: true),
                    Tinh_BHXH = table.Column<bool>(nullable: true, defaultValueSql: "('TRUE')"),
                    Tinh_TNCN = table.Column<bool>(nullable: true),
                    Xau_Noi_Ma = table.Column<string>(maxLength: 255, nullable: true),
                    XSort = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_PhuCap_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DS_CapNhap_BangLuong_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Den_Ngay = table.Column<DateTime>(nullable: true),
                    IsTongHop = table.Column<bool>(nullable: true),
                    KhoaBangLuong = table.Column<bool>(nullable: true),
                    Loai_DS_CNBLuong = table.Column<int>(nullable: true),
                    Ma_CachTL = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CBo = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_PBan = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Nam = table.Column<decimal>(type: "numeric(18, 0)", nullable: true),
                    NgayTao_BL = table.Column<DateTime>(nullable: true),
                    NguoiTao = table.Column<string>(maxLength: 255, nullable: true),
                    Note = table.Column<string>(maxLength: 255, nullable: true),
                    So_TT = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    Ten_DS_CNBLuong = table.Column<string>(maxLength: 255, nullable: true),
                    Thang = table.Column<decimal>(type: "numeric(18, 0)", nullable: true),
                    Tu_Ngay = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DS_CapNhap_BangLuong_NQ104", x => x.Id);
                    //table.ForeignKey(
                    //    name: "FK_TL_DS_CapNhap_BangLuong_NQ104_TL_DM_DonVi_Ma_CBo",
                    //    column: x => x.Ma_CBo,
                    //    principalTable: "TL_DM_DonVi",
                    //    principalColumn: "Ma_DonVi",
                    //    onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TL_PhuCap_MLNS_NQ104",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    ChiTietToi = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    iTrangThai = table.Column<string>(maxLength: 20, nullable: true),
                    idCachTinhLuong = table.Column<Guid>(nullable: true),
                    idMlns = table.Column<Guid>(nullable: true),
                    idNguonNganSach = table.Column<Guid>(nullable: true),
                    idPhuCap = table.Column<Guid>(nullable: true),
                    K = table.Column<string>(maxLength: 50, nullable: true),
                    L = table.Column<string>(maxLength: 50, nullable: true),
                    LNS = table.Column<string>(maxLength: 50, nullable: true),
                    M = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_CachTL = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_Cb = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ma_NguonNganSach = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_PhuCap = table.Column<string>(maxLength: 50, nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    Nam = table.Column<int>(nullable: true),
                    NG = table.Column<string>(maxLength: 50, nullable: true),
                    NguonNganSach = table.Column<string>(nullable: true),
                    sTNG = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG1 = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG2 = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG3 = table.Column<string>(maxLength: 50, nullable: true),
                    Ten_PhuCap = table.Column<string>(maxLength: 100, nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: true),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true),
                    XauNoiMa = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_PhuCap_MLNS_NQ104", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TL_QT_ChungTuChiTiet_GiaiThich_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fKinhPhi_An = table.Column<double>(nullable: true),
                    fKinhPhi_LuongPC_Khac = table.Column<double>(nullable: true),
                    fKinhPhi_PhuCap_HSQBS = table.Column<double>(nullable: true),
                    fLuongBHXH_CNVQP_Tru = table.Column<double>(nullable: true),
                    fLuongBHXH_HD_Tru = table.Column<double>(nullable: true),
                    fLuongBHXH_QNCN_Tru = table.Column<double>(nullable: true),
                    fLuongBHXH_SiQuan_Tru = table.Column<double>(nullable: true),
                    fLuongCNVC = table.Column<double>(nullable: true),
                    fLuong_CNVQP_Tru = table.Column<double>(nullable: true),
                    fLuongHDLD = table.Column<double>(nullable: true),
                    fLuong_HD_Tru = table.Column<double>(nullable: true),
                    fLuongQNCN = table.Column<double>(nullable: true),
                    fLuong_QNCN_Tru = table.Column<double>(nullable: true),
                    fLuongSiQuan = table.Column<double>(nullable: true),
                    fLuong_SiQuan_Tru = table.Column<double>(nullable: true),
                    fNgayAn = table.Column<double>(nullable: true),
                    fNgayAn_Cong = table.Column<double>(nullable: true),
                    fNgayAn_QT = table.Column<double>(nullable: true),
                    fNgayAn_Tru = table.Column<double>(nullable: true),
                    fPcCNVC = table.Column<double>(nullable: true),
                    fPcHDLD = table.Column<double>(nullable: true),
                    fPcQNCN = table.Column<double>(nullable: true),
                    fPcSiQuan = table.Column<double>(nullable: true),
                    fPhuCapBHXH_CNVQP_Tru = table.Column<double>(nullable: true),
                    fPhuCapBHXH_HD_Tru = table.Column<double>(nullable: true),
                    fPhuCapBHXH_QNCN_Tru = table.Column<double>(nullable: true),
                    fPhuCapBHXH_SiQuan_Tru = table.Column<double>(nullable: true),
                    fPhuCap_CNVQP_Tru = table.Column<double>(nullable: true),
                    fPhuCap_HD_Tru = table.Column<double>(nullable: true),
                    fPhuCap_QNCN_Tru = table.Column<double>(nullable: true),
                    fPhuCap_SiQuan_Tru = table.Column<double>(nullable: true),
                    iHuu_HSQBS = table.Column<int>(nullable: true),
                    iHuu_Khac = table.Column<int>(nullable: true),
                    iHuu_QNCN = table.Column<int>(nullable: true),
                    iHuu_SiQuan = table.Column<int>(nullable: true),
                    iID_QTChungTu = table.Column<Guid>(nullable: false),
                    iMa_DonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iNam = table.Column<int>(nullable: true),
                    iThang = table.Column<int>(nullable: true),
                    iThoiViec_HSQBS = table.Column<int>(nullable: true),
                    iThoiViec_Khac = table.Column<int>(nullable: true),
                    iThoiViec_QNCN = table.Column<int>(nullable: true),
                    iThoiViec_SiQuan = table.Column<int>(nullable: true),
                    iXuatNgu_HSQBS = table.Column<int>(nullable: true),
                    iXuatNgu_Khac = table.Column<int>(nullable: true),
                    iXuatNgu_QNCN = table.Column<int>(nullable: true),
                    iXuatNgu_SiQuan = table.Column<int>(nullable: true),
                    sMoTa_KienNghi = table.Column<string>(nullable: true),
                    sMoTa_TinhHinh = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_QT_ChungTuChiTiet_GiaiThich_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_QT_ChungTuChiTiet_KeHoach_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BHangCha = table.Column<bool>(nullable: true),
                    ChiTietToi = table.Column<string>(maxLength: 50, nullable: true),
                    Chuong = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    DieuChinh = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    iThangQuy = table.Column<int>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true),
                    Id_ChungTu = table.Column<Guid>(nullable: true),
                    Id_DonVi = table.Column<string>(maxLength: 50, nullable: false),
                    K = table.Column<string>(maxLength: 50, nullable: false),
                    L = table.Column<string>(maxLength: 50, nullable: false),
                    LNS = table.Column<string>(maxLength: 50, nullable: false),
                    M = table.Column<string>(maxLength: 50, nullable: false),
                    MaPhuCap = table.Column<string>(maxLength: 50, nullable: true),
                    MLNS_Id = table.Column<Guid>(nullable: false),
                    MLNS_Id_Parent = table.Column<Guid>(nullable: true),
                    MoTa = table.Column<string>(maxLength: 500, nullable: true),
                    MucAn = table.Column<double>(nullable: true),
                    NamLamViec = table.Column<int>(nullable: false),
                    NamNganSach = table.Column<int>(nullable: false),
                    NG = table.Column<string>(maxLength: 50, nullable: false),
                    Ngach = table.Column<string>(maxLength: 50, nullable: true),
                    NguonNganSach = table.Column<int>(nullable: false),
                    SoNgay = table.Column<int>(nullable: true),
                    SoNguoi = table.Column<int>(nullable: true),
                    TenDonVi = table.Column<string>(maxLength: 500, nullable: false),
                    Thang = table.Column<int>(nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: false),
                    TNG = table.Column<string>(maxLength: 50, nullable: false),
                    TNG1 = table.Column<string>(maxLength: 50, nullable: true),
                    TNG2 = table.Column<string>(maxLength: 50, nullable: true),
                    TNG3 = table.Column<string>(maxLength: 50, nullable: true),
                    TongCong = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    TongNamTruoc = table.Column<decimal>(nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: false),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true),
                    XauNoiMa = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_QT_ChungTuChiTiet_KeHoach_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_QT_ChungTuChiTiet_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BHangCha = table.Column<bool>(nullable: true),
                    ChiTietToi = table.Column<string>(maxLength: 50, nullable: true),
                    Chuong = table.Column<string>(maxLength: 50, nullable: true),
                    DDuToan = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    DieuChinh = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    iThangQuy = table.Column<int>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true),
                    Id_ChungTu = table.Column<Guid>(nullable: false),
                    Id_DonVi = table.Column<string>(maxLength: 50, nullable: false),
                    K = table.Column<string>(maxLength: 50, nullable: false),
                    L = table.Column<string>(maxLength: 50, nullable: false),
                    LNS = table.Column<string>(maxLength: 50, nullable: false),
                    M = table.Column<string>(maxLength: 50, nullable: false),
                    MaCachTl = table.Column<string>(maxLength: 50, nullable: true),
                    MaCb = table.Column<string>(nullable: true),
                    MLNS_Id = table.Column<Guid>(nullable: false),
                    MLNS_Id_Parent = table.Column<Guid>(nullable: true),
                    MoTa = table.Column<string>(maxLength: 500, nullable: true),
                    MucAn = table.Column<double>(nullable: true),
                    NamLamViec = table.Column<int>(nullable: false),
                    NamNganSach = table.Column<int>(nullable: false),
                    NG = table.Column<string>(maxLength: 50, nullable: false),
                    NguonNganSach = table.Column<int>(nullable: false),
                    SoNgay = table.Column<int>(nullable: true),
                    SoNguoi = table.Column<int>(nullable: true),
                    TenDonVi = table.Column<string>(maxLength: 500, nullable: false),
                    TM = table.Column<string>(maxLength: 50, nullable: false),
                    TNG = table.Column<string>(maxLength: 50, nullable: false),
                    TNG1 = table.Column<string>(maxLength: 50, nullable: true),
                    TNG2 = table.Column<string>(maxLength: 50, nullable: true),
                    TNG3 = table.Column<string>(maxLength: 50, nullable: true),
                    TongCong = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: false),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true),
                    XauNoiMa = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_QT_ChungTuChiTiet_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_QT_ChungTu_NQ104",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    bNganSachNhanDuLieu = table.Column<bool>(nullable: true),
                    ChungTuIndex = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    Id_ChungTu = table.Column<int>(nullable: false),
                    IIdChungTuDuToan = table.Column<string>(nullable: true),
                    LNS = table.Column<string>(maxLength: 50, nullable: false),
                    Ma_DonVi = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    MoTa = table.Column<string>(maxLength: 200, nullable: true),
                    Nam = table.Column<int>(nullable: false),
                    Ngay_tao = table.Column<DateTime>(type: "date", nullable: false),
                    sTongHop = table.Column<string>(nullable: true),
                    So_ChungTu = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ten_DonVi = table.Column<string>(maxLength: 100, nullable: true),
                    Thang = table.Column<int>(nullable: false),
                    TrangThai = table.Column<int>(nullable: true),
                    UserCreated = table.Column<string>(maxLength: 50, nullable: true),
                    UserModified = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_QT_ChungTu_NQ104", x => x.ID);
                    //table.ForeignKey(
                    //    name: "FK_TL_QT_ChungTu_NQ104_TL_DM_DonVi_Ma_DonVi",
                    //    column: x => x.Ma_DonVi,
                    //    principalTable: "TL_DM_DonVi",
                    //    principalColumn: "Ma_DonVi",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TL_CanBo_PhuCap_NQ104_MA_CBO",
                table: "TL_CanBo_PhuCap_NQ104",
                column: "MA_CBO");

            migrationBuilder.CreateIndex(
                name: "IX_TL_DS_CapNhap_BangLuong_NQ104_Ma_CBo",
                table: "TL_DS_CapNhap_BangLuong_NQ104",
                column: "Ma_CBo");

            migrationBuilder.CreateIndex(
                name: "IX_TL_QT_ChungTu_NQ104_Ma_DonVi",
                table: "TL_QT_ChungTu_NQ104",
                column: "Ma_DonVi");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.4_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.4_salary_new_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.4_salary_new_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.4_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.4_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_BangLuong_ThangBHXH_NQ104");

            migrationBuilder.DropTable(
                name: "TL_BangLuong_Thang_Bridge_NQ104");

            migrationBuilder.DropTable(
                name: "TL_BangLuong_Thang_NQ104");

            migrationBuilder.DropTable(
                name: "TL_CanBo_PhuCap_Bridge_NQ104");

            migrationBuilder.DropTable(
                name: "TL_CanBo_PhuCap_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_Cach_TinhLuong_BaoHiem_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_Cach_TinhLuong_Chuan_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_Cach_TinhLuong_TruyLinh_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_CapBac_Luong_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_CapBac_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_ChucVu_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DM_PhuCap_NQ104");

            migrationBuilder.DropTable(
                name: "TL_DS_CapNhap_BangLuong_NQ104");

            migrationBuilder.DropTable(
                name: "TL_PhuCap_MLNS_NQ104");

            migrationBuilder.DropTable(
                name: "TL_QT_ChungTuChiTiet_GiaiThich_NQ104");

            migrationBuilder.DropTable(
                name: "TL_QT_ChungTuChiTiet_KeHoach_NQ104");

            migrationBuilder.DropTable(
                name: "TL_QT_ChungTuChiTiet_NQ104");

            migrationBuilder.DropTable(
                name: "TL_QT_ChungTu_NQ104");

            migrationBuilder.DropColumn(
                name: "dan_toc",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "dien_quan_ly",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "is_nang_luong_cb",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "is_nang_luong_cvd",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "lang_nang_luong_cb",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "lang_nang_luong_cvd",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "loai",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "loai_doi_tuong",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "ma_bac_luong",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "ma_cb104",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "ma_cvd104",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "ma_so_dinh_danh",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "nam_bao_luu_cb",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "nam_bao_luu_cvd",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "ngay_nhan_cb_den_ngay",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "ngay_nhan_cb_tu_ngay",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "ngay_nhan_cvd_den_ngay",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "ngay_nhan_cvd_tu_ngay",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "nhom_chuyen_mon",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "nhom_mau",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "noi_dang_ky_khai_sinh",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "que_quan",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "so_chung_minh_quan_doi",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "so_nguoi_phu_thuoc",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "so_thang_tinh_bao_luu_cb",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "so_thang_tinh_bao_luu_cvd",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "tien_bao_luu_cb",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "tien_bao_luu_cvd",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "tien_luong_cb",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "tien_luong_cvd",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "tien_nang_luong_cb",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "tien_nang_luong_cvd",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "ton_giao",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "BH_CP_ChungTu_ChiTiet");

            migrationBuilder.RenameColumn(
                name: "SQuyNamMoTa",
                table: "BH_QTC_CapKinhPhi_KCB",
                newName: "sQuyNamMoTa");

            migrationBuilder.AlterColumn<string>(
                name: "sQuyNamMoTa",
                table: "BH_QTC_CapKinhPhi_KCB",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
