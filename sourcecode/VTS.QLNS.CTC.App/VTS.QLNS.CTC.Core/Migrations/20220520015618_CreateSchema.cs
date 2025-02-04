using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class CreateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "VDT_DM_LoaiCongTrinh_SEQ",
                minValue: 1L,
                maxValue: 999999999999999999L);

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(maxLength: 50, nullable: true),
                    dNgayCanCu = table.Column<DateTime>(type: "date", nullable: true),
                    FileName = table.Column<string>(maxLength: 500, nullable: false),
                    FilePath = table.Column<string>(maxLength: 2000, nullable: false),
                    iLoaiCanCu = table.Column<int>(nullable: true),
                    ModuleType = table.Column<int>(nullable: false),
                    ObjectId = table.Column<Guid>(nullable: false),
                    sSoCanCu = table.Column<string>(maxLength: 250, nullable: true),
                    UploadType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BH_DM_CheDoBHXH",
                columns: table => new
                {
                    iID_CheDo = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    iID_CheDo_Cha = table.Column<Guid>(nullable: true),
                    iID_MaCheDo = table.Column<string>(maxLength: 50, nullable: false),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: false),
                    sTen_CheDo = table.Column<string>(maxLength: 200, nullable: false),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DM_CheDoBHXH", x => x.iID_CheDo);
                });

            migrationBuilder.CreateTable(
                name: "BH_DM_KinhPhi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    MaKinhPhi = table.Column<string>(maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(maxLength: 500, nullable: true),
                    NamLamViec = table.Column<int>(nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    NguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(maxLength: 50, nullable: false),
                    SapXep = table.Column<int>(nullable: true),
                    TenKinhPhi = table.Column<string>(maxLength: 200, nullable: false),
                    TrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DM_KinhPhi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CP_DanhMuc",
                columns: table => new
                {
                    iId_CP_DanhMuc = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_MaDMCapPhat = table.Column<string>(maxLength: 50, nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    LNS = table.Column<string>(nullable: true),
                    Log = table.Column<string>(nullable: true),
                    OrderIndex = table.Column<int>(nullable: true, defaultValueSql: "((999))"),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTen = table.Column<string>(maxLength: 250, nullable: false),
                    sTenThongTriCap = table.Column<string>(maxLength: 500, nullable: true),
                    sTenThongTriThu = table.Column<string>(maxLength: 250, nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CP_DanhMuc", x => x.iId_CP_DanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    iID_DanhMuc = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_MaDanhMuc = table.Column<string>(maxLength: 50, nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iThuTu = table.Column<int>(nullable: true, defaultValueSql: "((999))"),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    Log = table.Column<string>(nullable: true),
                    NganSachNganh = table.Column<bool>(nullable: true),
                    sGiaTri = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(maxLength: 250, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTen = table.Column<string>(maxLength: 250, nullable: false),
                    sType = table.Column<string>(maxLength: 50, nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.iID_DanhMuc)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "DM_BQuanLy",
                columns: table => new
                {
                    iID_BQuanLy = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    iID_MaBQuanLy = table.Column<string>(maxLength: 10, nullable: false),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    sKyHieu = table.Column<string>(maxLength: 20, nullable: true),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenBQuanLy = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_PhongBan", x => x.iID_BQuanLy);
                });

            migrationBuilder.CreateTable(
                name: "DM_ChuDauTu",
                columns: table => new
                {
                    iID_DonVi = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    ChiNhanhNuocNgoai = table.Column<string>(maxLength: 500, nullable: true),
                    ChiNhanhTrongNuoc = table.Column<string>(maxLength: 500, nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DonViCha = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true),
                    Loai = table.Column<string>(maxLength: 50, nullable: true),
                    MaSoDVSDNS = table.Column<string>(maxLength: 500, nullable: true),
                    sKyHieu = table.Column<string>(maxLength: 20, nullable: true),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    STKNuocNgoai = table.Column<string>(maxLength: 500, nullable: true),
                    STKTrongNuoc = table.Column<string>(maxLength: 500, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_ChuDauTu", x => x.iID_DonVi);
                });

            migrationBuilder.CreateTable(
                name: "DM_ChuKy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDanhSach = table.Column<bool>(nullable: true),
                    ChucDanh1 = table.Column<string>(maxLength: 50, nullable: true),
                    ChucDanh1_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    ChucDanh2 = table.Column<string>(maxLength: 50, nullable: true),
                    ChucDanh2_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    ChucDanh3 = table.Column<string>(maxLength: 50, nullable: true),
                    ChucDanh3_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    ChucDanh4 = table.Column<string>(maxLength: 50, nullable: true),
                    ChucDanh4_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    ChucDanh5 = table.Column<string>(maxLength: 50, nullable: true),
                    ChucDanh5_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    ChucDanh6 = table.Column<string>(maxLength: 50, nullable: true),
                    ChucDanh6_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    INamLamViec = table.Column<int>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    Id_Code = table.Column<string>(maxLength: 50, nullable: false),
                    Id_Old_Type = table.Column<string>(nullable: true),
                    Id_Type = table.Column<string>(maxLength: 100, nullable: true),
                    KyHieu = table.Column<string>(maxLength: 50, nullable: true),
                    Log = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(maxLength: 500, nullable: true),
                    sKinhGuiCQTTBQP = table.Column<string>(nullable: true),
                    sKinhGuiKBNN = table.Column<string>(nullable: true),
                    sLoai = table.Column<string>(maxLength: 50, nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    Ten = table.Column<string>(maxLength: 250, nullable: true),
                    Ten1 = table.Column<string>(maxLength: 50, nullable: true),
                    Ten1_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    Ten2 = table.Column<string>(maxLength: 50, nullable: true),
                    Ten2_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    Ten3 = table.Column<string>(maxLength: 50, nullable: true),
                    Ten3_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    Ten4 = table.Column<string>(maxLength: 50, nullable: true),
                    Ten4_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    Ten5 = table.Column<string>(maxLength: 50, nullable: true),
                    Ten5_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    Ten6 = table.Column<string>(maxLength: 50, nullable: true),
                    Ten6_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    ThuaLenh1 = table.Column<string>(maxLength: 50, nullable: true),
                    ThuaLenh1_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    ThuaLenh2 = table.Column<string>(maxLength: 50, nullable: true),
                    ThuaLenh2_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    ThuaLenh3 = table.Column<string>(maxLength: 50, nullable: true),
                    ThuaLenh3_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    ThuaLenh4 = table.Column<string>(maxLength: 50, nullable: true),
                    ThuaLenh4_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    ThuaLenh5 = table.Column<string>(maxLength: 50, nullable: true),
                    ThuaLenh5_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    ThuaLenh6 = table.Column<string>(maxLength: 50, nullable: true),
                    ThuaLenh6_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    TieuDe1 = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "((1))"),
                    TieuDe1_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    TieuDe2 = table.Column<string>(maxLength: 50, nullable: true),
                    TieuDe2_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    TieuDe3_MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_ChuKy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DM_DeTai",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    SMa = table.Column<string>(maxLength: 100, nullable: true),
                    SMota = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_DeTai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonVi",
                columns: table => new
                {
                    iID_DonVi = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bCoNSNganh = table.Column<bool>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    iTrangThai = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iID_Parent = table.Column<Guid>(nullable: true),
                    IsPhongBan = table.Column<bool>(nullable: true),
                    iKhoi = table.Column<string>(maxLength: 250, nullable: true),
                    sKyHieu = table.Column<string>(maxLength: 20, nullable: true),
                    iLoai = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "((0))"),
                    LoaiNganSach = table.Column<int>(nullable: true),
                    Log = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 500, nullable: true),
                    iCapDonVi = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_DonVi", x => x.iID_DonVi);
                    //table.ForeignKey(
                    //    name: "FK_DonVi_DonVi_iID_Parent",
                    //    column: x => x.iID_Parent,
                    //    principalTable: "DonVi",
                    //    principalColumn: "iID_DonVi",
                    //    onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HT_App_Update_Log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date_Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Date_Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    File = table.Column<string>(maxLength: 250, nullable: true),
                    iCount = table.Column<int>(nullable: false),
                    iProccessed = table.Column<bool>(nullable: false),
                    iSuccess = table.Column<bool>(nullable: false),
                    Id_Update = table.Column<string>(maxLength: 50, nullable: false),
                    Id_Version = table.Column<string>(maxLength: 50, nullable: false),
                    Index3 = table.Column<int>(nullable: true),
                    Index4 = table.Column<int>(nullable: true),
                    Index6 = table.Column<int>(nullable: true),
                    Index7 = table.Column<int>(nullable: true),
                    Log = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Priority_Type = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    Update_Type = table.Column<int>(nullable: false),
                    User_Creator = table.Column<string>(maxLength: 50, nullable: true),
                    User_Modifier = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HT_App_Update_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HT_App_Version",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DESCRIPTION = table.Column<string>(nullable: true),
                    FILENAME = table.Column<string>(nullable: true),
                    FILESIZE = table.Column<long>(nullable: true),
                    FILESTREAM = table.Column<byte[]>(nullable: true),
                    STATUS = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    VERSION = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HT_App_Version", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HT_ChucNang",
                columns: table => new
                {
                    iID_MaChucNang = table.Column<string>(maxLength: 100, nullable: false),
                    BHangCha = table.Column<bool>(nullable: false),
                    iID_ChucNangCha = table.Column<Guid>(nullable: true),
                    ITrangThai = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    iID_ChucNang = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    sSTT = table.Column<string>(maxLength: 500, nullable: true),
                    sTenChucNang = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HT_ChucNang", x => x.iID_MaChucNang);
                });

            migrationBuilder.CreateTable(
                name: "HT_LoaiQuyen",
                columns: table => new
                {
                    iID_LoaiQuyen = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    sTenLoaiQuyen = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_AUTHORITY_TYPE", x => x.iID_LoaiQuyen);
                });

            migrationBuilder.CreateTable(
                name: "HT_NguoiDung",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    ACTIVATION_KEY = table.Column<string>(nullable: true),
                    bKichHoat = table.Column<bool>(nullable: false),
                    dNgayCaiLai = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    RESET_KEY = table.Column<string>(nullable: true),
                    sDuongDanAnh = table.Column<string>(nullable: true),
                    sEmail = table.Column<string>(nullable: true),
                    sHo = table.Column<string>(nullable: true),
                    sMatKhau = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sTaiKhoan = table.Column<string>(maxLength: 250, nullable: false),
                    sTen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HT_NguoiDung", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HT_NhatKy_CapNhat_DuLieu",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ACCOUNT = table.Column<string>(maxLength: 200, nullable: true),
                    ACTION_NAME = table.Column<string>(maxLength: 200, nullable: true),
                    APPLICATION_CODE = table.Column<string>(maxLength: 200, nullable: true),
                    DURATION = table.Column<int>(nullable: true),
                    END_TIME = table.Column<DateTime>(type: "datetime", nullable: true),
                    ERROR_CODE = table.Column<decimal>(type: "numeric(10, 0)", nullable: true),
                    ERROR_DESCRIPTION = table.Column<string>(maxLength: 200, nullable: true),
                    IP_PORT_CURRENT_NODE = table.Column<string>(maxLength: 200, nullable: true),
                    IP_PORT_PARENT_NODE = table.Column<string>(maxLength: 200, nullable: true),
                    REQUEST_CONTENT = table.Column<string>(maxLength: 1000, nullable: true),
                    RESPONSE_CONTENT = table.Column<string>(maxLength: 1000, nullable: true),
                    SERVICE_CODE = table.Column<string>(maxLength: 200, nullable: true),
                    SESSION_ID = table.Column<string>(maxLength: 100, nullable: true),
                    START_TIME = table.Column<DateTime>(type: "datetime", nullable: true),
                    TRANSACTION_STATUS = table.Column<bool>(nullable: true),
                    USER_NAME = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HT_NhatKy_CapNhat_DuLieu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HT_Nhom",
                columns: table => new
                {
                    iID_Nhom = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bKichHoat = table.Column<bool>(nullable: false),
                    sTenNhom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_GROUP", x => x.iID_Nhom);
                });

            migrationBuilder.CreateTable(
                name: "IMP_CP_ChungTuChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    Chuong = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    HienVat = table.Column<double>(nullable: false),
                    iLoai = table.Column<string>(maxLength: 10, nullable: true, defaultValueSql: "((0))"),
                    iTrangThai = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    Id_ChungTu = table.Column<Guid>(nullable: true),
                    Id_DonVi = table.Column<string>(maxLength: 50, nullable: true),
                    Id_PhongBan = table.Column<string>(maxLength: 10, nullable: true),
                    Id_PhongBanDich = table.Column<string>(maxLength: 10, nullable: true),
                    K = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    L = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    LNS = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    Log = table.Column<string>(nullable: true),
                    M = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    MLNS_Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    MLNS_Id_Parent = table.Column<Guid>(nullable: true),
                    MoTa = table.Column<string>(maxLength: 500, nullable: true, defaultValueSql: "('')"),
                    NamLamViec = table.Column<int>(nullable: true),
                    NG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    NguonNganSach = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    TenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    TNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    TTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    TuChi = table.Column<double>(nullable: false),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true),
                    XauNoiMa = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMP_CP_ChungTuChiTiet", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "IMP_DuToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    GhiChu = table.Column<string>(nullable: true),
                    HangMua = table.Column<double>(nullable: true),
                    HangNhap = table.Column<double>(nullable: true),
                    HienVat = table.Column<double>(nullable: true),
                    ImportId = table.Column<Guid>(nullable: true),
                    K = table.Column<string>(maxLength: 50, nullable: true),
                    L = table.Column<string>(maxLength: 50, nullable: true),
                    LNS = table.Column<string>(maxLength: 50, nullable: true),
                    Loai = table.Column<string>(maxLength: 10, nullable: true),
                    M = table.Column<string>(maxLength: 50, nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    NG = table.Column<string>(maxLength: 50, nullable: true),
                    PhanCap = table.Column<double>(nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: true),
                    TNG = table.Column<string>(maxLength: 50, nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: true),
                    TuChi = table.Column<double>(nullable: true),
                    XauNoiMa = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMP_DuToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IMP_History",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    FileName = table.Column<string>(maxLength: 250, nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    ServiceCode = table.Column<string>(maxLength: 100, nullable: true),
                    TableName = table.Column<string>(maxLength: 100, nullable: true),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMP_History", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IMP_QuyetToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    ChiTieu = table.Column<double>(nullable: true),
                    ConLai = table.Column<double>(nullable: true),
                    DaQuyetToan = table.Column<double>(nullable: true),
                    DeNghi = table.Column<double>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true),
                    ImportId = table.Column<Guid>(nullable: true),
                    K = table.Column<string>(maxLength: 50, nullable: true),
                    L = table.Column<string>(maxLength: 50, nullable: true),
                    LNS = table.Column<string>(maxLength: 50, nullable: true),
                    Loai = table.Column<string>(maxLength: 10, nullable: true),
                    M = table.Column<string>(maxLength: 50, nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    NG = table.Column<string>(maxLength: 50, nullable: true),
                    PheDuyet = table.Column<double>(nullable: true),
                    SoLuot = table.Column<double>(nullable: true),
                    SoNgay = table.Column<double>(nullable: true),
                    SoNguoi = table.Column<double>(nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: true),
                    TNG = table.Column<string>(maxLength: 50, nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: true),
                    TuChi = table.Column<double>(nullable: true),
                    XauNoiMa = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMP_QuyetToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IMP_SKT_SoLieuChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    ChuaPhanCap = table.Column<double>(nullable: false),
                    Chuong = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    DuPhong = table.Column<double>(nullable: false),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    HangMua = table.Column<double>(nullable: false),
                    HangNhap = table.Column<double>(nullable: false),
                    HienVat = table.Column<double>(nullable: false),
                    IGuiNhan = table.Column<int>(nullable: false),
                    iLoai = table.Column<int>(nullable: false),
                    iTrangThai = table.Column<int>(nullable: true),
                    Id_DonVi = table.Column<string>(maxLength: 50, nullable: true),
                    Id_DonViTao = table.Column<string>(maxLength: 50, nullable: true),
                    IsLocked = table.Column<bool>(nullable: true),
                    K = table.Column<string>(maxLength: 50, nullable: false),
                    L = table.Column<string>(maxLength: 50, nullable: false),
                    LNS = table.Column<string>(maxLength: 50, nullable: false),
                    LoaiChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    Log = table.Column<string>(nullable: true),
                    M = table.Column<string>(maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(maxLength: 500, nullable: true),
                    NamLamViec = table.Column<int>(nullable: false),
                    NamNganSach = table.Column<int>(nullable: false),
                    NG = table.Column<string>(maxLength: 50, nullable: false),
                    NguonNganSach = table.Column<int>(nullable: false),
                    PhanCap = table.Column<double>(nullable: false),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    TenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: false),
                    TNG = table.Column<string>(maxLength: 50, nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: false),
                    TuChi = table.Column<double>(nullable: false),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true),
                    XauNoiMa = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMP_SKT_SoLieuChiTiet", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "IMP_TN_DT_ThuNop",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bHangCha = table.Column<bool>(nullable: false),
                    Chuong = table.Column<string>(maxLength: 50, nullable: true),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    iPhanCap = table.Column<int>(nullable: false),
                    iTrangThai = table.Column<int>(nullable: true),
                    Id_ChungTu = table.Column<Guid>(nullable: true),
                    Id_DonVi = table.Column<string>(maxLength: 50, nullable: true),
                    Id_DotNhan = table.Column<Guid>(nullable: true),
                    ILoai = table.Column<int>(nullable: true),
                    ImportId = table.Column<Guid>(nullable: true),
                    K = table.Column<string>(maxLength: 50, nullable: false),
                    L = table.Column<string>(maxLength: 50, nullable: false),
                    LNS = table.Column<string>(maxLength: 50, nullable: false),
                    M = table.Column<string>(maxLength: 50, nullable: false),
                    MLNS_Id = table.Column<Guid>(nullable: true),
                    MLNS_Id_Parent = table.Column<Guid>(nullable: true),
                    NamLamViec = table.Column<int>(nullable: true),
                    NamNganSach = table.Column<int>(nullable: false),
                    NG = table.Column<string>(maxLength: 50, nullable: false),
                    NguonNganSach = table.Column<int>(nullable: true),
                    NoiDung = table.Column<string>(maxLength: 500, nullable: true),
                    TenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: false),
                    TNG = table.Column<string>(maxLength: 50, nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: false),
                    TuChi = table.Column<double>(nullable: false),
                    XauNoiMa = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMP_TN_DT_ThuNop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IMP_TN_QT_ThuNop",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bLaHangCha = table.Column<bool>(nullable: false),
                    bThoaiThu = table.Column<bool>(nullable: true),
                    ChenhLech = table.Column<double>(nullable: true),
                    ChiPhiKhac = table.Column<double>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    iGuiNhan = table.Column<int>(nullable: true),
                    iLoai = table.Column<string>(maxLength: 10, nullable: true),
                    iThangQuy = table.Column<int>(nullable: true),
                    iThangQuyLoai = table.Column<int>(nullable: false),
                    iTrangThai = table.Column<int>(nullable: true),
                    Id_ChungTu = table.Column<Guid>(nullable: true),
                    Id_DonVi = table.Column<string>(maxLength: 50, nullable: true),
                    Id_DonViTao = table.Column<string>(maxLength: 50, nullable: true),
                    ID_MaLoaiHinh = table.Column<Guid>(nullable: false),
                    ID_MaLoaiHinh_Cha = table.Column<Guid>(nullable: true),
                    Id_PhongBan = table.Column<string>(maxLength: 10, nullable: true),
                    Id_PhongBanDich = table.Column<string>(maxLength: 10, nullable: true),
                    ImportId = table.Column<Guid>(nullable: true),
                    LNS = table.Column<string>(maxLength: 50, nullable: true),
                    Log = table.Column<string>(nullable: true),
                    NamLamViec = table.Column<int>(nullable: true),
                    NamNganSach = table.Column<int>(nullable: false),
                    NguonNganSach = table.Column<int>(nullable: false),
                    Noidung = table.Column<string>(maxLength: 50, nullable: false),
                    NSNN_Khac = table.Column<double>(nullable: true),
                    NSNN_Khac_BQP = table.Column<double>(nullable: true),
                    Phi_LePhi = table.Column<double>(nullable: true),
                    PP_BoSungKinhPhi = table.Column<double>(nullable: true),
                    PP_NopNSQP = table.Column<double>(nullable: true),
                    PP_SoChuaPhanPhoi = table.Column<double>(nullable: true),
                    PP_TrichCacQuy = table.Column<double>(nullable: true),
                    QT_KhauHaoTSCĐ = table.Column<double>(nullable: false),
                    QT_QTNSKhac = table.Column<double>(nullable: false),
                    QT_TienLuong = table.Column<double>(nullable: false),
                    QT_TongSoQTNS = table.Column<double>(nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    TenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    ThueGTGT = table.Column<double>(nullable: false),
                    ThueTNDN = table.Column<double>(nullable: true),
                    ThueTNDN_BQP = table.Column<double>(nullable: true),
                    TongSoChiPhi = table.Column<double>(nullable: true),
                    TongSoThu = table.Column<double>(nullable: true),
                    TongnopNSNN = table.Column<double>(nullable: false),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMP_TN_QT_ThuNop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung_DonVi",
                columns: table => new
                {
                    iID_NguoiDung_DonVi = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    bPublic = table.Column<bool>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    iID_MaNguoiDung = table.Column<string>(maxLength: 250, nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iSoLanSua = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iSTT = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    sIPSua = table.Column<string>(maxLength: 20, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DT_NguoiDungDonVi", x => x.iID_NguoiDung_DonVi);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_ChuTruongDauTu",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    bIsXoa = table.Column<bool>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_CapPheDuyetID = table.Column<Guid>(nullable: true),
                    iID_ChuDauTuID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_MaChuDauTu = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_EURID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_NgoaiTeKhacID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_VNDID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    sDiaDiem = table.Column<string>(maxLength: 300, nullable: true),
                    sKetThuc = table.Column<string>(maxLength: 50, nullable: true),
                    sKhoiCong = table.Column<string>(maxLength: 50, nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sMota = table.Column<string>(nullable: true),
                    sMucTieu = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sQuyMo = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_ChuTruongDauTu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_ChuTruongDauTu_HangMuc",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_ChuTruongDauTuID = table.Column<Guid>(nullable: true),
                    iID_DuAn_HangMucID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    sMaHangMuc = table.Column<string>(maxLength: 50, nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 50, nullable: true),
                    sTenHangMuc = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_ChuTruongDauTu_HangMuc", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_ChuTruongDauTu_NguonVon",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_ChuTruongDauTuID = table.Column<Guid>(nullable: false),
                    iID_DuAn_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_ChuTruongDauTu_NguonVon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_DuAn",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsDuPhong = table.Column<bool>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fEUR = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fNgoaiTeKhac = table.Column<double>(nullable: true),
                    fUSD = table.Column<double>(nullable: true),
                    fVND = table.Column<double>(nullable: true),
                    iID_CapPheDuyetID = table.Column<Guid>(nullable: true),
                    iID_ChuDauTuID = table.Column<Guid>(nullable: true),
                    iID_ChuTruongDauTuID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_MaChuDauTu = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_EURID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_NgoaiTeKhacID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_VNDID = table.Column<Guid>(nullable: true),
                    sDiaDiem = table.Column<string>(maxLength: 300, nullable: true),
                    sKetThuc = table.Column<string>(maxLength: 50, nullable: true),
                    sKhoiCong = table.Column<string>(maxLength: 50, nullable: true),
                    sMaDuAn = table.Column<string>(maxLength: 100, nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sMucTieu = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sQuyMo = table.Column<string>(nullable: true),
                    sTenDuAn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_DuAn", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_DuAn_HangMuc",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    sMaHangMuc = table.Column<string>(maxLength: 50, nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 255, nullable: true),
                    sTenHangMuc = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_DuAn_HangMuc", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_DuAn_NguonVon",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    fGiaTriEUR = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_DuAn_NguonVon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_DuToan",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    bIsXoa = table.Column<bool>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_DuToanGocID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTuID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_EURID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_NgoaiTeKhacID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_VNDID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sMota = table.Column<string>(type: "ntext", nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHDuToan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_DuToan_ChiPhi",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_DuToanID = table.Column<Guid>(nullable: true),
                    iID_DuToan_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTu_ChiPhiID = table.Column<Guid>(nullable: true),
                    sMaChiPhi = table.Column<string>(maxLength: 100, nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 50, nullable: true),
                    sTenChiPhi = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DuToan_ChiPhi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_DuToan_HangMuc",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_DuToan_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_HangMucPhanChiaID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTu_HangMucID = table.Column<Guid>(nullable: true),
                    sMaHangMuc = table.Column<string>(maxLength: 50, nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 50, nullable: true),
                    sTenHangMuc = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DuToan_HangMuc", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_DuToan_NguonVon",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_DuToanID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_QDDauTu_NguonVonID = table.Column<Guid>(nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_DuToan_NguonVon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_GoiThau",
                columns: table => new
                {
                    iID_GoiThauID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    bIsGoc = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    bIsKhoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bIsXoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    dBatDauChonNhaThau = table.Column<DateTime>(type: "datetime", nullable: true),
                    dKetThucChonNhaThau = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "0"),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaGoiThauEUR = table.Column<double>(nullable: true),
                    fGiaGoiThauNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaGoiThauUSD = table.Column<double>(nullable: true),
                    fGiaGoiThauVND = table.Column<double>(nullable: true),
                    fGiaQuyetDinhChiTietEUR = table.Column<double>(nullable: true),
                    fGiaQuyetDinhChiTietNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaQuyetDinhChiTietUSD = table.Column<double>(nullable: true),
                    fGiaQuyetDinhChiTietVND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinhID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_GoiThauGocID = table.Column<Guid>(nullable: true),
                    iID_HinhThucChonNhaThauID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iId_KHLCNhaThau = table.Column<Guid>(nullable: true),
                    iID_LoaiHopDongID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    iID_ParentAdjustID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_PhuongAnNhapKhauID = table.Column<Guid>(nullable: true),
                    iID_PhuongThucDauThauID = table.Column<Guid>(nullable: true),
                    iID_QuyetDinhChiTietID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_EURID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_NgoaiTeKhacID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_VNDID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    iLoai = table.Column<int>(nullable: true),
                    iThoiGianThucHien = table.Column<int>(nullable: true),
                    LoaiGoiThau = table.Column<string>(maxLength: 200, nullable: true),
                    sMaGoiThau = table.Column<string>(type: "varchar(100)", nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(type: "varchar(255)", nullable: true),
                    sTenGoiThau = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_GoiThau", x => x.iID_GoiThauID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_GoiThau_ChiPhi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    fTienGoiThau_EUR = table.Column<double>(nullable: true),
                    fTienGoiThau_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fTienGoiThau_USD = table.Column<double>(nullable: true),
                    fTienGoiThau_VND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinh_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_DuToan_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_GoiThauID = table.Column<Guid>(nullable: true),
                    iID_GoiThau_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTu_ChiPhiID = table.Column<Guid>(nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 50, nullable: true),
                    sTenChiPhi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_GoiThau_ChiPhi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_GoiThau_HangMuc",
                columns: table => new
                {
                    iID_GoiThau_HangMucID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTienGoiThau_EUR = table.Column<double>(nullable: true),
                    fTienGoiThau_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fTienGoiThau_USD = table.Column<double>(nullable: true),
                    fTienGoiThau_VND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinh_HangMucID = table.Column<Guid>(nullable: true),
                    iID_DuToan_HangMucID = table.Column<Guid>(nullable: true),
                    iID_GoiThau_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTu_HangMucID = table.Column<Guid>(nullable: true),
                    sMaHangMuc = table.Column<string>(maxLength: 50, nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 50, nullable: true),
                    sTenHangMuc = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_GoiThau_HangMuc", x => x.iID_GoiThau_HangMucID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_GoiThau_NguonVon",
                columns: table => new
                {
                    iID_GoiThau_NguonVonID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTienGoiThau_EUR = table.Column<double>(nullable: true),
                    fTienGoiThau_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fTienGoiThau_USD = table.Column<double>(nullable: true),
                    fTienGoiThau_VND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinh_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_ChuTruongDauTu_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_DuAn_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_DuToan_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_GoiThauID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_QDDauTu_NguonVonID = table.Column<Guid>(nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_GoiThau_NguonVon", x => x.iID_GoiThau_NguonVonID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_HopDong_CacQuyetDinh",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fQDHD_EUR = table.Column<double>(nullable: true),
                    fQDHD_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fQDHD_USD = table.Column<double>(nullable: true),
                    fQDHD_VND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinhID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_HopDong_CacQuyetDinh", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_HopDong_ChiPhi",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    fTienHopDong_EUR = table.Column<double>(nullable: true),
                    fTienHopDong_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fTienHopDong_USD = table.Column<double>(nullable: true),
                    fTienHopDong_VND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinh_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_CacQuyetDinhID = table.Column<Guid>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_GoiThau_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_HopDongGoiThauNhaThauID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_HopDong_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 50, nullable: true),
                    sTenChiPhi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_HopDong_ChiPhi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_HopDong_GoiThau_NhaThau",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_GoiThauID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    iThoiGianThucHien = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_HopDong_GoiThau_NhaThau", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_HopDong_HangMuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTienHopDong_EUR = table.Column<double>(nullable: true),
                    fTienHopDong_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fTienHopDong_USD = table.Column<double>(nullable: true),
                    fTienHopDong_VND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinh_HangMucID = table.Column<Guid>(nullable: true),
                    iID_GoiThau_HangMucID = table.Column<Guid>(nullable: true),
                    iID_HopDong_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    sMaHangMuc = table.Column<string>(nullable: true),
                    sMaOrder = table.Column<string>(nullable: true),
                    sTenHangMuc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_HopDong_HangMuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_HopDong_NguonVon",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    fTienHopDong_EUR = table.Column<double>(nullable: true),
                    fTienHopDong_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fTienHopDong_USD = table.Column<double>(nullable: true),
                    fTienHopDong_VND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinhID = table.Column<Guid>(nullable: true),
                    iID_CacQuyetDinh_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_GoiThau_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: false),
                    sMaOrder = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_HopDong_NguonVon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_KHLCNhaThau",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    bIsXoa = table.Column<bool>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "date", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_DuToanID = table.Column<Guid>(nullable: true),
                    iID_LCNhaThauGocID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTuID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_EURID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_NgoaiTeKhacID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_VNDID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_KHLCNhaThau", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_QDDauTu",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    bIsXoa = table.Column<bool>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_ChuDauTuID = table.Column<Guid>(nullable: true),
                    iID_ChuTruongDauTuID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_MaChuDauTu = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    sDiaDiem = table.Column<string>(maxLength: 300, nullable: true),
                    sKetThuc = table.Column<string>(maxLength: 50, nullable: true),
                    sKhoiCong = table.Column<string>(maxLength: 50, nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_QDDauTu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_QDDauTu_ChiPhi",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTu_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_QDDauTuID = table.Column<Guid>(nullable: false),
                    sMaOrder = table.Column<string>(maxLength: 50, nullable: true),
                    sTenChiPhi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_QDDauTu_ChiPhi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_QDDauTu_HangMuc",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_ChuTruongDauTu_HangMucID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTu_ChiPhiID = table.Column<Guid>(nullable: true),
                    sMaHangMuc = table.Column<string>(maxLength: 50, nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 50, nullable: true),
                    sTenHangMuc = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_QDDauTu_HangMuc", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_QDDauTu_NguonVon",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_ChuTruongDauTu_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_QDDauTuID = table.Column<Guid>(nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_QDDauTu_NguonVon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_ChiPhi",
                columns: table => new
                {
                    iID_ChiPhi = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iThuTu = table.Column<int>(nullable: false),
                    sID_MaNguoiDungTao = table.Column<string>(maxLength: 200, nullable: true),
                    sMaChiPhi = table.Column<string>(maxLength: 50, nullable: false),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true),
                    sTenChiPhi = table.Column<string>(maxLength: 300, nullable: false),
                    sTenVietTat = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_ChiPhi", x => x.iID_ChiPhi);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_DonViTinh",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    sMaDonViTinh = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenDonViTinh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_DonViTinh", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_HinhThucChonNhaThau",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iThuTu = table.Column<int>(nullable: true),
                    sMaHinhThucChonNhaThau = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenHinhThucChonNhaThau = table.Column<string>(nullable: true),
                    sTenVietTat = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_HinhThucChonNhaThau", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_LoaiCongTrinh",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_Parent = table.Column<Guid>(nullable: true),
                    iSoLanSua = table.Column<int>(nullable: true),
                    iThuTu = table.Column<int>(nullable: true),
                    K = table.Column<string>(maxLength: 200, nullable: true),
                    L = table.Column<string>(maxLength: 200, nullable: true),
                    LNS = table.Column<string>(maxLength: 200, nullable: true),
                    M = table.Column<string>(maxLength: 200, nullable: true),
                    NG = table.Column<string>(maxLength: 200, nullable: true),
                    sID_MaNguoiDungSua = table.Column<string>(maxLength: 200, nullable: true),
                    sID_MaNguoiDungTao = table.Column<string>(maxLength: 200, nullable: true),
                    sIPSua = table.Column<string>(maxLength: 20, nullable: true),
                    sMaLoaiCongTrinh = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenLoaiCongTrinh = table.Column<string>(maxLength: 300, nullable: true),
                    sTenVietTat = table.Column<string>(maxLength: 100, nullable: true),
                    TM = table.Column<string>(maxLength: 200, nullable: true),
                    TNG = table.Column<string>(maxLength: 200, nullable: true),
                    TNG1 = table.Column<string>(maxLength: 200, nullable: true),
                    TNG2 = table.Column<string>(maxLength: 200, nullable: true),
                    TNG3 = table.Column<string>(maxLength: 200, nullable: true),
                    TTM = table.Column<string>(maxLength: 200, nullable: true),
                    XAUNOIMA = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_LoaiCongTrinh", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_LoaiHopDong",
                columns: table => new
                {
                    iID_LoaiHopDongID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iThuTu = table.Column<int>(nullable: true),
                    sMaLoaiHopDong = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenLoaiHopDong = table.Column<string>(nullable: true),
                    sTenVietTat = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_LoaiHopDong", x => x.iID_LoaiHopDongID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_LoaiTaiSan",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    sMaLoaiTaiSan = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenLoaiTaiSan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_LoaiTaiSan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_LoaiTienTe",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    sMaTienTe = table.Column<string>(maxLength: 10, nullable: true),
                    sMoTaChiTiet = table.Column<string>(nullable: true),
                    sTenTienTe = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_LoaiTienTe", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_NhaThau",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgayCapCMND = table.Column<DateTime>(nullable: true),
                    iLoai = table.Column<int>(nullable: true),
                    sChucVu = table.Column<string>(nullable: true),
                    sDaiDien = table.Column<string>(nullable: true),
                    sDiaChi = table.Column<string>(nullable: true),
                    sDienThoai = table.Column<string>(nullable: true),
                    sDienThoaiLienHe = table.Column<string>(nullable: true),
                    sEmail = table.Column<string>(nullable: true),
                    sFax = table.Column<string>(nullable: true),
                    sMaNganHang = table.Column<string>(nullable: true),
                    sMaNhaThau = table.Column<string>(nullable: true),
                    sMaSoThue = table.Column<string>(nullable: true),
                    sNganHang = table.Column<string>(nullable: true),
                    sNguoiLienHe = table.Column<string>(nullable: true),
                    sNoiCapCMND = table.Column<string>(nullable: true),
                    sSoCMND = table.Column<string>(nullable: true),
                    sSoTaiKhoan = table.Column<string>(nullable: true),
                    sTenNhaThau = table.Column<string>(nullable: true),
                    sWebsite = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_NhaThau", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_NhaThau_NganHang",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    sMaNganHang = table.Column<string>(maxLength: 100, nullable: true),
                    sSoTaiKhoan = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sTenNganHang = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_NhaThau_NganHang", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_NhaThau_NguoiNhan",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgayCapCMND = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    sChucVu = table.Column<string>(maxLength: 100, nullable: true),
                    sDienThoai = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sEmail = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sFax = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sNoiCapCMND = table.Column<string>(maxLength: 255, nullable: true),
                    sSoCMND = table.Column<string>(maxLength: 50, nullable: true),
                    sTenNguoiNhan = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_NhaThau_NguoiNhan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_NhiemVuChi",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iLoaiNhiemVuChi = table.Column<int>(nullable: true, defaultValueSql: "1"),
                    sMaNhiemVuChi = table.Column<string>(maxLength: 100, nullable: false),
                    sMaOrder = table.Column<string>(maxLength: 255, nullable: true),
                    sMoTaChiTiet = table.Column<string>(nullable: true),
                    sTenNhiemVuChi = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_NhiemVuChi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_PhanCapPheDuyet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true),
                    iThuTu = table.Column<int>(nullable: true),
                    sMa = table.Column<string>(maxLength: 100, nullable: true),
                    sMoTa = table.Column<string>(type: "ntext", nullable: true),
                    sTen = table.Column<string>(maxLength: 300, nullable: true),
                    sTenVietTat = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_PhanCapPheDuyet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_PhuongThucChonNhaThau",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iThuTu = table.Column<int>(nullable: true),
                    sMaPhuongThucChonNhaThau = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenPhuongThucChonNhaThau = table.Column<string>(nullable: true),
                    sTenVietTat = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_PhuongThucChonNhaThau", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_TiGia",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    iID_TienTeGocID = table.Column<Guid>(nullable: true),
                    sMaTiGia = table.Column<string>(nullable: true),
                    sMaTienTeGoc = table.Column<string>(nullable: true),
                    sMoTaTiGia = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sTenTiGia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_TiGia", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_TiGia_ChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: false),
                    iID_TienTeID = table.Column<Guid>(nullable: false),
                    sMaTienTeQuyDoi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_TiGia_ChiTiet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_XuatXu",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    sMaXuatXu = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenXuatXu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_XuatXu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_HDNK_CacQuyetDinh",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsKhoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bIsXoa = table.Column<bool>(nullable: true, defaultValueSql: "0"),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_DonViQuanLy = table.Column<Guid>(nullable: true),
                    iID_DonViThucHien = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_GocID = table.Column<Guid>(nullable: true),
                    iID_KHTongThe_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_ParentAdjustID = table.Column<Guid>(nullable: true),
                    iID_ParentId = table.Column<Guid>(nullable: true),
                    iID_PhuongAnNhapKhauID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    iLoai = table.Column<int>(nullable: true),
                    iLoaiQuyetDinh = table.Column<int>(nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTaChiTiet_QuyetDinh = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_HDNK_CacQuyetDinh", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_HDNK_CacQuyetDinh_ChiPhi",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinhID = table.Column<Guid>(nullable: false),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTu_ChiPhiID = table.Column<Guid>(nullable: true),
                    sMaOrder = table.Column<string>(nullable: true),
                    sTenChiPhi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_HDNK_CacQuyetDinh_ChiPhi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_HDNK_CacQuyetDinh_ChiPhi_HangMuc",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinh_ChiPhiID = table.Column<Guid>(nullable: false),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTu_HangMucID = table.Column<Guid>(nullable: true),
                    sMaHangMuc = table.Column<string>(nullable: true),
                    sMaOrder = table.Column<string>(nullable: true),
                    sTenHangMuc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_HDNK_CacQuyetDinh_ChiPhi_HangMuc", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_HDNK_CacQuyetDinh_NguonVon",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinhID = table.Column<Guid>(nullable: false),
                    iID_NguonVonID = table.Column<int>(nullable: false),
                    iID_QDDauTu_NguonVonID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_HDNK_CacQuyetDinh_NguonVon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_HDNK_PhuongAnNhapKhau",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsKhoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bIsXoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "date", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_ChuTruongDauTuID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_PhuongAnNhapKhauGocID = table.Column<Guid>(nullable: true),
                    iID_QDDauTuID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    iLoai = table.Column<int>(nullable: true),
                    sLoaiSoCu = table.Column<string>(nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_HDNK_PhuongAnNhapKhau", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_HopDong",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHieuLuc = table.Column<bool>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_TiGiaUSD_NgoaiTeKhacID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_VNDID = table.Column<Guid>(nullable: true),
                    sSoHopDong = table.Column<string>(nullable: true),
                    sTenHopDong = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_HopDong", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_KHChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsActive = table.Column<bool>(nullable: false),
                    bIsGoc = table.Column<bool>(nullable: false),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayKeHoach = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_GocID = table.Column<Guid>(nullable: true),
                    iID_ParentAdjustID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: false),
                    sMoTaChiTiet = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoKeHoach = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_KHChiTiet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_KHChiTiet_HopDong",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_KHChiTietID = table.Column<Guid>(nullable: true),
                    iID_KHTongThe_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_NH_HopDongID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_NgoaiTeKhacID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_VNDID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_KHChiTiet_HopDong", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_KHTongThe",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsActive = table.Column<bool>(nullable: false),
                    bIsGoc = table.Column<bool>(nullable: false),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayKeHoachBQP = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    dNgayKeHoachTTCP = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fTongGiaTri_KHBQP = table.Column<double>(nullable: false),
                    fTongGiaTri_KHTTCP = table.Column<double>(nullable: false),
                    iGiaiDoanDen = table.Column<int>(nullable: true),
                    iGiaiDoanTu = table.Column<int>(nullable: true),
                    iID_GocID = table.Column<Guid>(maxLength: 50, nullable: true),
                    iID_ParentAdjustID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(maxLength: 50, nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: false),
                    ILoai = table.Column<int>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sMoTaChiTiet_KHBQP = table.Column<string>(nullable: true),
                    sMoTaChiTiet_KHTTCP = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoKeHoachBQP = table.Column<string>(maxLength: 100, nullable: true),
                    sSoKeHoachTTCP = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_KHTongThe", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_NhuCauChiQuy",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsKhoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bIsXoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    dNgayDeNghi = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_GocID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "''"),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    iQuy = table.Column<int>(nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiLap = table.Column<string>(maxLength: 255, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoDeNghi = table.Column<string>(maxLength: 100, nullable: true),
                    sTongHop = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_NhuCauChiQuy", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_NhuCauChiQuy_ChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    fNhuCauQuyNay_EUR = table.Column<double>(nullable: true, defaultValueSql: "0"),
                    fNhuCauQuyNay_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fNhuCauQuyNay_USD = table.Column<double>(nullable: true),
                    fNhuCauQuyNay_VND = table.Column<double>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_NhuCauChiQuyID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_NhuCauChiQuy_ChiTiet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_QuyetToanDAHT",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsKhoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bIsXoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bTongHop = table.Column<bool>(nullable: true),
                    dNgayDeNghi = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fCPKhongTaoTaiSan_EUR = table.Column<double>(nullable: true),
                    fCPKhongTaoTaiSan_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fCPKhongTaoTaiSan_USD = table.Column<double>(nullable: true),
                    fCPKhongTaoTaiSan_VND = table.Column<double>(nullable: true),
                    fCPThietHai_EUR = table.Column<double>(nullable: true),
                    fCPThietHai_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fCPThietHai_USD = table.Column<double>(nullable: true),
                    fCPThietHai_VND = table.Column<double>(nullable: true),
                    fDeNghiQuyetToan_EUR = table.Column<double>(nullable: true),
                    fDeNghiQuyetToan_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fDeNghiQuyetToan_USD = table.Column<double>(nullable: true),
                    fDeNghiQuyetToan_VND = table.Column<double>(nullable: true),
                    fTaiSanDaiHan_EUR = table.Column<double>(nullable: true),
                    fTaiSanDaiHan_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fTaiSanDaiHan_USD = table.Column<double>(nullable: true),
                    fTaiSanDaiHan_VND = table.Column<double>(nullable: true),
                    fTaiSanNganHan_EUR = table.Column<double>(nullable: true),
                    fTaiSanNganHan_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fTaiSanNganHan_USD = table.Column<double>(nullable: true),
                    fTaiSanNganHan_VND = table.Column<double>(nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_GocID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "''"),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iID_TiGiaPheDuyetID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    iID_Parent = table.Column<Guid>(nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoDeNghi = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_QuyetToanDAHT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_QuyetToanDAHT_ChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    fDeNghiQuyetToan_EUR = table.Column<double>(nullable: true),
                    fDeNghiQuyetToan_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fDeNghiQuyetToan_USD = table.Column<double>(nullable: true),
                    fDeNghiQuyetToan_VND = table.Column<double>(nullable: true),
                    fDeNghiSoVoiDuToan_EUR = table.Column<double>(nullable: true),
                    fDeNghiSoVoiDuToan_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fDeNghiSoVoiDuToan_USD = table.Column<double>(nullable: true),
                    fDeNghiSoVoiDuToan_VND = table.Column<double>(nullable: true),
                    fDeNghiSoVoiKetQuaKiemToan_EUR = table.Column<double>(nullable: true),
                    fDeNghiSoVoiKetQuaKiemToan_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fDeNghiSoVoiKetQuaKiemToan_USD = table.Column<double>(nullable: true),
                    fDeNghiSoVoiKetQuaKiemToan_VND = table.Column<double>(nullable: true),
                    fDeNghiSoVoiQuyetToanAB_EUR = table.Column<double>(nullable: true),
                    fDeNghiSoVoiQuyetToanAB_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fDeNghiSoVoiQuyetToanAB_USD = table.Column<double>(nullable: true),
                    fDeNghiSoVoiQuyetToanAB_VND = table.Column<double>(nullable: true),
                    fGiaTriPheDuyetQuyetToan_EUR = table.Column<double>(nullable: true),
                    fGiaTriPheDuyetQuyetToan_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriPheDuyetQuyetToan_USD = table.Column<double>(nullable: true),
                    fGiaTriPheDuyetQuyetToan_VND = table.Column<double>(nullable: true),
                    fGiaTriQuyetToanAB_EUR = table.Column<double>(nullable: true),
                    fGiaTriQuyetToanAB_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriQuyetToanAB_USD = table.Column<double>(nullable: true),
                    fGiaTriQuyetToanAB_VND = table.Column<double>(nullable: true),
                    fGiaTriThamTra_EUR = table.Column<double>(nullable: true),
                    fGiaTriThamTra_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriThamTra_USD = table.Column<double>(nullable: true),
                    fGiaTriThamTra_VND = table.Column<double>(nullable: true),
                    fKetQuaKiemToan_EUR = table.Column<double>(nullable: true),
                    fKetQuaKiemToan_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fKetQuaKiemToan_USD = table.Column<double>(nullable: true),
                    fKetQuaKiemToan_VND = table.Column<double>(nullable: true),
                    fPheDuyetSoVoiDeNghi_EUR = table.Column<double>(nullable: true),
                    fPheDuyetSoVoiDeNghi_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fPheDuyetSoVoiDeNghi_USD = table.Column<double>(nullable: true),
                    fPheDuyetSoVoiDeNghi_VND = table.Column<double>(nullable: true),
                    fPheDuyetSoVoiDuToan_EUR = table.Column<double>(nullable: true),
                    fPheDuyetSoVoiDuToan_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fPheDuyetSoVoiDuToan_USD = table.Column<double>(nullable: true),
                    fPheDuyetSoVoiDuToan_VND = table.Column<double>(nullable: true),
                    iid_HM_CP = table.Column<Guid>(nullable: true),
                    iID_DeNghiQuyetToanDAHT_ID = table.Column<Guid>(nullable: true),
                    IType = table.Column<int>(nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_QuyetToanDAHT_ChiTiet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_QuyetToanDAHT_NguonVon",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    iID_DeNghiQuyetToanDAHT_ID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_QuyetToanDAHT_NguonVon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_QuyetToanNienDo",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsKhoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bIsXoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    dNgayDeNghi = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    iCoQuanThanhToan = table.Column<int>(nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_GocID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "''"),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    iLoaiThanhToan = table.Column<int>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoDeNghi = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_QuyetToanNienDo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_QuyetToanNienDo_ChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    fDeNghiChuyenNamSau_EUR = table.Column<double>(nullable: true, defaultValueSql: "0"),
                    fDeNghiChuyenNamSau_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fDeNghiChuyenNamSau_USD = table.Column<double>(nullable: true),
                    fDeNghiChuyenNamSau_VND = table.Column<double>(nullable: true),
                    fDeNghiQTNamNay_EUR = table.Column<double>(nullable: true, defaultValueSql: "0"),
                    fDeNghiQTNamNay_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fDeNghiQTNamNay_USD = table.Column<double>(nullable: true),
                    fDeNghiQTNamNay_VND = table.Column<double>(nullable: true),
                    fHopDong_USD = table.Column<double>(nullable: true),
                    fHopDong_VND = table.Column<double>(nullable: true),
                    fKeHoach_BQP_USD = table.Column<double>(nullable: true),
                    fKeHoach_BQP_VND = table.Column<double>(nullable: true),
                    fKeHoachChuaGiaiNgan_USD = table.Column<double>(nullable: true),
                    fKeHoachChuaGiaiNgan_VND = table.Column<double>(nullable: true),
                    fKeHoach_TTCP_USD = table.Column<double>(nullable: true),
                    fKeHoach_TTCP_VND = table.Column<double>(nullable: true),
                    fLuyKeKinhPhiDuocCap_EUR = table.Column<double>(nullable: true, defaultValueSql: "0"),
                    fLuyKeKinhPhiDuocCap_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fLuyKeKinhPhiDuocCap_USD = table.Column<double>(nullable: true),
                    fLuyKeKinhPhiDuocCap_VND = table.Column<double>(nullable: true),
                    fQTKinhPhiDuocCap_NamNay_EUR = table.Column<double>(nullable: true),
                    fQTKinhPhiDuocCap_NamNay_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fQTKinhPhiDuocCap_NamNay_USD = table.Column<double>(nullable: true),
                    fQTKinhPhiDuocCap_NamNay_VND = table.Column<double>(nullable: true, defaultValueSql: "0"),
                    fQTKinhPhiDuocCap_NamTruocChuyenSang_EUR = table.Column<double>(nullable: true),
                    fQTKinhPhiDuocCap_NamTruocChuyenSang_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fQTKinhPhiDuocCap_NamTruocChuyenSang_USD = table.Column<double>(nullable: true),
                    fQTKinhPhiDuocCap_NamTruocChuyenSang_VND = table.Column<double>(nullable: true, defaultValueSql: "0"),
                    fQTKinhPhiDuocCap_TongSo_EUR = table.Column<double>(nullable: true),
                    fQTKinhPhiDuocCap_TongSo_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fQTKinhPhiDuocCap_TongSo_USD = table.Column<double>(nullable: true),
                    fQTKinhPhiDuocCap_TongSo_VND = table.Column<double>(nullable: true, defaultValueSql: "0"),
                    fQTKinhPhiDuyetCacNamTruoc_EUR = table.Column<double>(nullable: true, defaultValueSql: "0"),
                    fQTKinhPhiDuyetCacNamTruoc_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fQTKinhPhiDuyetCacNamTruoc_USD = table.Column<double>(nullable: true),
                    fQTKinhPhiDuyetCacNamTruoc_VND = table.Column<double>(nullable: true),
                    fThuaNopNSNN_EUR = table.Column<double>(nullable: true, defaultValueSql: "0"),
                    fThuaNopNSNN_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fThuaNopNSNN_USD = table.Column<double>(nullable: true),
                    fThuaNopNSNN_VND = table.Column<double>(nullable: true),
                    fThuaThieuKinhPhiTrongNam_EUR = table.Column<double>(nullable: true, defaultValueSql: "0"),
                    fThuaThieuKinhPhiTrongNam_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fThuaThieuKinhPhiTrongNam_USD = table.Column<double>(nullable: true),
                    fThuaThieuKinhPhiTrongNam_VND = table.Column<double>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_MLNS_ID = table.Column<Guid>(nullable: true),
                    iID_MucLucNganSachID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QuyetToanNienDoID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_QuyetToanNienDo_ChiTiet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_TaiSan",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgayBatDauSuDung = table.Column<DateTime>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_LoaiTaiSanID = table.Column<Guid>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true),
                    SMaTaiSan = table.Column<string>(nullable: true),
                    sMoTaTaiSan = table.Column<string>(nullable: true),
                    sTenTaiSan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_TaiSan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_QT_TaiSan_ChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_DonViTinhID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_TaiSanID = table.Column<Guid>(nullable: true),
                    iID_XuatXuID = table.Column<Guid>(nullable: true),
                    iSoLuong = table.Column<int>(nullable: true),
                    sHangMuc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_QT_TaiSan_ChiTiet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_TT_ThanhToan",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    bIsKhoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bIsXoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bTongHop = table.Column<bool>(nullable: true),
                    dNgayCapCMND = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayDeNghi = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayPheDuyet = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fChuyenKhoan_BangSo = table.Column<double>(nullable: true),
                    fSoDuTamUng = table.Column<double>(nullable: true),
                    fThuHoiTamUng_BangChu = table.Column<string>(nullable: true),
                    fThuHoiTamUng_BangSo = table.Column<double>(nullable: true),
                    fThuHoiTamUngPheDuyet_BangChu = table.Column<string>(nullable: true),
                    fThuHoiTamUngPheDuyet_BangSo = table.Column<double>(nullable: true),
                    fTienMat_BangSo = table.Column<double>(nullable: true),
                    fTongDeNghi_BangSo = table.Column<double>(nullable: true),
                    fTongPheDuyet_BangSo = table.Column<double>(nullable: true),
                    fTraDonViThuHuong_BangChu = table.Column<string>(nullable: true),
                    fTraDonViThuHuong_BangSo = table.Column<double>(nullable: true),
                    fTraDonViThuHuongPheDuyet_BangChu = table.Column<string>(nullable: true),
                    fTraDonViThuHuongPheDuyet_BangSo = table.Column<double>(nullable: true),
                    fTuChoiThanhToan_BangSo = table.Column<double>(nullable: true),
                    iCoQuanThanhToan = table.Column<int>(nullable: true),
                    iID_ChuDauTuID = table.Column<Guid>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iID_DonViCapTren = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_KHTongTheID = table.Column<Guid>(nullable: true),
                    iID_MaChuDauTu = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "''"),
                    iID_MaDonViCapTren = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "''"),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    iID_NhaThau_NganHangID = table.Column<Guid>(nullable: true),
                    iID_NhaThau_NguoiNhanID = table.Column<Guid>(nullable: true),
                    iID_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iID_TiGiaPheDuyetID = table.Column<Guid>(nullable: true),
                    iLoaiDeNghi = table.Column<int>(nullable: true),
                    iLoaiNoiDungChi = table.Column<int>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true),
                    iID_Parent = table.Column<Guid>(nullable: true),
                    sCanCu = table.Column<string>(nullable: true),
                    sChuyenKhoan_BangChu = table.Column<string>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sKinhGui = table.Column<string>(maxLength: 100, nullable: true),
                    sLyDoTuChoi = table.Column<string>(nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sNganHang = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiLienHe = table.Column<string>(maxLength: 300, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sNoiCapCMND = table.Column<string>(maxLength: 255, nullable: true),
                    sSoCMND = table.Column<string>(maxLength: 50, nullable: true),
                    sSoDeNghi = table.Column<string>(maxLength: 50, nullable: true),
                    sSoTaiKhoan = table.Column<string>(type: "varchar(100)", nullable: true),
                    sThuTruongDonVi = table.Column<string>(maxLength: 255, nullable: true),
                    sTienMat_BangChu = table.Column<string>(nullable: true),
                    sTongDeNghi_BangChu = table.Column<string>(nullable: true),
                    sTongPheDuyet_BangChu = table.Column<string>(nullable: true),
                    sTruongPhong = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_TT_ThanhToan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_TT_ThanhToan_ChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fDeNghiCapKyNay_EUR = table.Column<double>(nullable: true),
                    fDeNghiCapKyNay_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fDeNghiCapKyNay_USD = table.Column<double>(nullable: true),
                    fDeNghiCapKyNay_VND = table.Column<double>(nullable: true),
                    fPheDuyetCapKyNay_EUR = table.Column<double>(nullable: true),
                    fPheDuyetCapKyNay_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fPheDuyetCapKyNay_USD = table.Column<double>(nullable: true),
                    fPheDuyetCapKyNay_VND = table.Column<double>(nullable: true),
                    fTiLeThanhToan = table.Column<double>(nullable: true),
                    fTongGiaTriTheoHoaDon_EUR = table.Column<double>(nullable: true),
                    fTongGiaTriTheoHoaDon_NgoaiTeKhac = table.Column<double>(nullable: true),
                    fTongGiaTriTheoHoaDon_USD = table.Column<double>(nullable: true),
                    fTongGiaTriTheoHoaDon_VND = table.Column<double>(nullable: true),
                    iID_DeNghiThanhToanID = table.Column<Guid>(nullable: true),
                    iID_LoaiNoiDungChiID = table.Column<Guid>(nullable: true),
                    iID_MLNS_ID = table.Column<Guid>(nullable: true),
                    iID_MLNS_MucID = table.Column<Guid>(nullable: true),
                    iID_MLNS_TietMucID = table.Column<Guid>(nullable: true),
                    iID_MLNS_TieuMucID = table.Column<Guid>(nullable: true),
                    iID_MucID = table.Column<Guid>(nullable: true),
                    iID_MucLucNganSachID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_PhuLucHopDongID = table.Column<Guid>(nullable: true),
                    iID_TietMucID = table.Column<Guid>(nullable: true),
                    iID_TieuMucID = table.Column<Guid>(nullable: true),
                    sTenNoiDungChi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_TT_ThanhToan_ChiTiet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_TT_ThongTriCapPhat",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    bIsKhoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bIsXoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    dNgayGhiSo = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayLapThongTri = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongGiaTriEUR = table.Column<double>(nullable: true),
                    fTongGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fTongGiaTriUSD = table.Column<double>(nullable: true),
                    fTongGiaTriVND = table.Column<double>(nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_MaDonViID = table.Column<string>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_NgoaiTeKhacID = table.Column<Guid>(nullable: true),
                    iID_TiGiaUSD_VNDID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    iNamThucHien = table.Column<int>(nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sMaThongTri = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoCT1 = table.Column<string>(maxLength: 50, nullable: true),
                    sSoCT2 = table.Column<string>(maxLength: 50, nullable: true),
                    sTK1 = table.Column<string>(maxLength: 50, nullable: true),
                    sTK2 = table.Column<string>(maxLength: 50, nullable: true),
                    sTongGiaTri_BangChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_TT_ThongTriCapPhat", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_TT_ThongTriCapPhat_ChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    iID_PheDuyetThanhToanID = table.Column<Guid>(nullable: true),
                    iID_ThongTriCapPhatID = table.Column<Guid>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_TT_ThongTriCapPhat_ChiTiet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NS_BK_ChungTu",
                columns: table => new
                {
                    iID_BKChungTu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongHienVat = table.Column<double>(nullable: false),
                    fTongTuChi = table.Column<double>(nullable: false),
                    iID_DeTaiId = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: false, defaultValueSql: "((2))"),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    iThangQuy = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    iThangQuyLoai = table.Column<int>(nullable: false),
                    sLoai = table.Column<string>(maxLength: 10, nullable: true, defaultValueSql: "((0))"),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sNoiDung = table.Column<string>(maxLength: 500, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 50, nullable: true),
                    sThangQuy_MoTa = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "(N'T')"),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BK_ChungTu", x => x.iID_BKChungTu);
                });

            migrationBuilder.CreateTable(
                name: "NS_CauHinh_CanCu",
                columns: table => new
                {
                    iID_CauHinh_CanCu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bChinhSua = table.Column<bool>(nullable: true),
                    iID_MaChucNang = table.Column<string>(maxLength: 250, nullable: true),
                    iNamCanCu = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iThietLap = table.Column<int>(nullable: true),
                    sModule = table.Column<string>(maxLength: 250, nullable: true),
                    sTenCot = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHinh_CanCu", x => x.iID_CauHinh_CanCu);
                });

            migrationBuilder.CreateTable(
                name: "NS_CP_ChungTu",
                columns: table => new
                {
                    iID_CTCapPhat = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongHienVat = table.Column<double>(nullable: true),
                    fTongTuChi = table.Column<double>(nullable: true),
                    iID_MaDMCapPhat = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iLoai = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    iType_MoTa = table.Column<string>(maxLength: 50, nullable: true),
                    nChiTietToi = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "((0))"),
                    sDSSoChungTuTongHop = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_CP_ChungTu", x => x.iID_CTCapPhat);
                });

            migrationBuilder.CreateTable(
                name: "NS_CP_ChungTuChiTiet",
                columns: table => new
                {
                    iID_CTCapPhatChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fDeNghiDonVi = table.Column<double>(nullable: false),
                    fHienVat = table.Column<double>(nullable: false),
                    fTuChi = table.Column<double>(nullable: false),
                    iID_CTCapPhat = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iID_MLNS = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iLoai = table.Column<string>(maxLength: 10, nullable: true, defaultValueSql: "((0))"),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true),
                    sChuong = table.Column<string>(maxLength: 50, nullable: true),
                    sGhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    sK = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sL = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sLNS = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true, defaultValueSql: "('')"),
                    sNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    sTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CP_ChungTuChiTiet", x => x.iID_CTCapPhatChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_DC_ChungTu",
                columns: table => new
                {
                    iID_DCChungTu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: false),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fDieuChinh = table.Column<double>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: false),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true),
                    iLoaiChungTu = table.Column<int>(nullable: false),
                    iLoaiDuKien = table.Column<int>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    sDSLNS = table.Column<string>(nullable: false),
                    sGhiChu = table.Column<string>(maxLength: 1000, nullable: true),
                    sMoTa = table.Column<string>(maxLength: 250, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: false),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_DC_ChungTu", x => x.iID_DCChungTu);
                });

            migrationBuilder.CreateTable(
                name: "NS_DC_ChungTuChiTiet",
                columns: table => new
                {
                    iID_DCCTChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fDuKienQtCuoiNam = table.Column<double>(nullable: true),
                    fDuKienQtDauNam = table.Column<double>(nullable: true),
                    iID_DCChungTu = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iID_MLNS = table.Column<Guid>(nullable: false),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true, defaultValueSql: "((2))"),
                    sGhiChu = table.Column<string>(maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_NS_DC_ChungTuChiTiet", x => x.iID_DCCTChiTiet);
                });

            migrationBuilder.CreateTable(
                name: "NS_DT_ChungTu",
                columns: table => new
                {
                    iID_DTChungTu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bKhoa = table.Column<bool>(nullable: false),
                    bLuongNhanDuLieu = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongDuPhong = table.Column<double>(nullable: false),
                    fTongHangMua = table.Column<double>(nullable: false),
                    fTongHangNhap = table.Column<double>(nullable: false),
                    fTongHienVat = table.Column<double>(nullable: false),
                    fTongPhanCap = table.Column<double>(nullable: false),
                    fTongTonKho = table.Column<double>(nullable: false),
                    fTongTuChi = table.Column<double>(nullable: false),
                    iID_DotNhan = table.Column<string>(nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iLoai = table.Column<int>(nullable: false),
                    iLoaiChungTu = table.Column<int>(nullable: true),
                    iLoaiDuToan = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true, defaultValueSql: "((2))"),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    sDSID_MaDonVi = table.Column<string>(maxLength: 500, nullable: true),
                    sDSLNS = table.Column<string>(nullable: true),
                    sGhiChu = table.Column<string>(maxLength: 1000, nullable: true),
                    sMoTa = table.Column<string>(maxLength: 250, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DT_ChungTu", x => x.iID_DTChungTu);
                });

            migrationBuilder.CreateTable(
                name: "NS_DT_ChungTu_CanCu",
                columns: table => new
                {
                    iID_DTCTCanCu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_CTDuToan = table.Column<Guid>(nullable: false),
                    iID_CTNSNganh = table.Column<Guid>(nullable: false),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DT_ChungTu_CanCu", x => x.iID_DTCTCanCu);
                });

            migrationBuilder.CreateTable(
                name: "NS_DTDauNam_ChungTu",
                columns: table => new
                {
                    iID_CTDTDauNam = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongHangMua = table.Column<double>(nullable: true),
                    fTongHangNhap = table.Column<double>(nullable: true),
                    fTongHienVat = table.Column<double>(nullable: true),
                    fTongPhanCap = table.Column<double>(nullable: true),
                    fTongTuChi = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 500, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true),
                    iLoaiChungTu = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true),
                    iSoChungTuIndex = table.Column<int>(nullable: false),
                    sDSDonViTongHop = table.Column<string>(maxLength: 50, nullable: true),
                    sDSSoChungTuTongHop = table.Column<string>(nullable: true),
                    sDSLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(maxLength: 250, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_SoLieuChungTu", x => x.iID_CTDTDauNam);
                });

            migrationBuilder.CreateTable(
                name: "NS_DTDauNam_ChungTuChiTiet",
                columns: table => new
                {
                    iID_CTDTDauNamChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    bKhoa = table.Column<bool>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fChuaPhanCap = table.Column<double>(nullable: false, defaultValueSql: "((0.000000000000000e+000))"),
                    fDuPhong = table.Column<double>(nullable: false),
                    fHangMua = table.Column<double>(nullable: false),
                    fHangNhap = table.Column<double>(nullable: false),
                    fHienVat = table.Column<double>(nullable: false),
                    fPhanCap = table.Column<double>(nullable: false),
                    fTuChi = table.Column<double>(nullable: false),
                    iID_CTDTDauNam = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    iLoai = table.Column<int>(nullable: false),
                    iLoaiChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iNamNganSach = table.Column<int>(nullable: false, defaultValueSql: "((2))"),
                    sChuong = table.Column<string>(maxLength: 50, nullable: true),
                    sGhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    sK = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sL = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sLNS = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true, defaultValueSql: "('')"),
                    sNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    sTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    sTTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_SoLieuChiTiet", x => x.iID_CTDTDauNamChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_DTDauNam_ChungTuChiTiet_CanCu",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    bKhoa = table.Column<bool>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fChuaPhanCap = table.Column<double>(nullable: false, defaultValueSql: "((0.000000000000000e+000))"),
                    fHangMua = table.Column<double>(nullable: false),
                    fHangNhap = table.Column<double>(nullable: false),
                    fHienVat = table.Column<double>(nullable: false),
                    fPhanCap = table.Column<double>(nullable: false),
                    fTuChi = table.Column<double>(nullable: false),
                    iID_CanCu = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    iLoaiChungTu = table.Column<int>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iNamNganSach = table.Column<int>(nullable: false, defaultValueSql: "((2))"),
                    sGhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    sK = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sL = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sLNS = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true, defaultValueSql: "('')"),
                    sNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    sTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    sTTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_SoLieuChiTiet_Data_CanCu", x => x.iID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_DTDauNam_ChungTu_ChungTuCanCu",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_CanCu = table.Column<Guid>(nullable: true),
                    iID_CTCanCu = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iLoaiChungTu = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_SoLieuChiTiet_CanCu", x => x.iID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_DTDauNam_PhanCap",
                columns: table => new
                {
                    iID_DTDauNam_PhanCap = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTuChi = table.Column<double>(nullable: true),
                    iID_CTDTDauNamChiTiet = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MLNS = table.Column<Guid>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_SoLieuChiTiet_PhanCap", x => x.iID_DTDauNam_PhanCap);
                });

            migrationBuilder.CreateTable(
                name: "NS_MLSKT_MLNS",
                columns: table => new
                {
                    iID_MLSKT_MLNS = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    Log = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sNS_XauNoiMa = table.Column<string>(maxLength: 50, nullable: true),
                    sSKT_KyHieu = table.Column<string>(maxLength: 50, nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_MucLuc_Map", x => x.iID_MLSKT_MLNS)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_MucLucNganSach",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDuPhong = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    bHangCha = table.Column<bool>(nullable: false),
                    bHangChaDuToan = table.Column<bool>(nullable: true),
                    bHangChaQuyetToan = table.Column<bool>(nullable: true),
                    bHangMua = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    bHangNhap = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    bHienVat = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    bNgay = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    bPhanCap = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    bSoNguoi = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    bTonKho = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    bTuChi = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    sChiTietToi = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "(N'NG')"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    iLoai = table.Column<string>(maxLength: 50, nullable: true),
                    iLock = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    iTrangThai = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaBQuanLy = table.Column<string>(maxLength: 10, nullable: true),
                    sK = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sL = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sLNS = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    Log = table.Column<string>(nullable: true),
                    sM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    iID_MLNS = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    sMoTa = table.Column<string>(nullable: false, defaultValueSql: "('')"),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sCPChiTietToi = table.Column<string>(maxLength: 50, nullable: true),
                    sDuToanChiTietToi = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sNhapTheoTruong = table.Column<string>(maxLength: 200, nullable: true),
                    sQuyetToanChiTietToi = table.Column<string>(maxLength: 50, nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    sTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG1 = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG2 = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG3 = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DT_MucLuc", x => x.iID)
                        .Annotation("SqlServer:Clustered", false);
                    //table.UniqueConstraint("AK_NS_MucLucNganSach_iID_MLNS", x => x.iID_MLNS);
                    //table.ForeignKey(
                    //    name: "FK_NS_MucLucNganSach_NS_MucLucNganSach_iID_MLNS_Cha",
                    //    column: x => x.iID_MLNS_Cha,
                    //    principalTable: "NS_MucLucNganSach",
                    //    principalColumn: "iID_MLNS",
                    //    onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NS_MucLucNganSach_Nganh",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bPublic = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    iID_MaNganh = table.Column<string>(maxLength: 10, nullable: false),
                    iID_MaNganhMLNS = table.Column<string>(maxLength: 100, nullable: true),
                    iID_MaNhomNguoiDung_DuocGiao = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNhomNguoiDung_Public = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    iSoLanSua = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iTrangThai = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    sID_MaNguoiDung_DuocGiao = table.Column<string>(maxLength: 200, nullable: true),
                    sID_MaNguoiDungSua = table.Column<string>(maxLength: 200, nullable: true),
                    sID_MaNguoiDungTao = table.Column<string>(maxLength: 200, nullable: true),
                    sIPSua = table.Column<string>(maxLength: 20, nullable: true),
                    sMaNguoiQuanLy = table.Column<string>(maxLength: 500, nullable: true, defaultValueSql: "('')"),
                    sTenNganh = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_MucLucNganSach_Nganh", x => x.iID);
                });

            migrationBuilder.CreateTable(
                name: "NS_Nganh_ChungTu",
                columns: table => new
                {
                    iID_CTNganh = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fDuphong = table.Column<double>(nullable: true),
                    fTongHangMua = table.Column<double>(nullable: true),
                    fTongHangNhap = table.Column<double>(nullable: true),
                    fTongHienVat = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    fTongPhanCap = table.Column<double>(nullable: true),
                    fTongTuChi = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 500, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iLoaiChungTu = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    sDSLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(maxLength: 250, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    sSoCongVan = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LB_ChungTu", x => x.iID_CTNganh);
                });

            migrationBuilder.CreateTable(
                name: "NS_Nganh_ChungTuChiTiet",
                columns: table => new
                {
                    iID_CTNganhChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fChuaPhanCap = table.Column<double>(nullable: false),
                    fHangMua = table.Column<double>(nullable: false),
                    fHangNhap = table.Column<double>(nullable: false),
                    fPhanCap = table.Column<double>(nullable: false),
                    fTuChi = table.Column<double>(nullable: false),
                    iID_CTNganh = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iID_MLNS = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_Parent_MLNS = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true),
                    sChuong = table.Column<string>(maxLength: 50, nullable: true),
                    sGhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    sK = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sL = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sLNS = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true, defaultValueSql: "('')"),
                    sNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    sTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LB_ChungTuChiTiet", x => x.iID_CTNganhChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_Nganh_ChungTuChiTiet_PhanCap",
                columns: table => new
                {
                    iID_CTNganhChiTiet_PhanCap = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fHienVat = table.Column<double>(nullable: true),
                    fPhanCap = table.Column<double>(nullable: true),
                    iID_CTNganhChiTiet = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MLNS = table.Column<Guid>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LB_ChungTuChiTiet_PhanCap", x => x.iID_CTNganhChiTiet_PhanCap);
                });

            migrationBuilder.CreateTable(
                name: "NS_NguoiDung_LNS",
                columns: table => new
                {
                    iID_NguoiDung_LNS = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iNamLamViec = table.Column<int>(nullable: false),
                    sLNS = table.Column<string>(maxLength: 250, nullable: false),
                    sMaNguoiDung = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_NguoiDung_LNS", x => x.iID_NguoiDung_LNS);
                });

            migrationBuilder.CreateTable(
                name: "NguonNganSach",
                columns: table => new
                {
                    iID_MaNguonNganSach = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    bPublic = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    iSTT = table.Column<int>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 200, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 200, nullable: true),
                    sTen = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_NguonNganSach", x => x.iID_MaNguonNganSach);
                });

            migrationBuilder.CreateTable(
                name: "NS_PhongBan_DonVi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    Id_DonVi = table.Column<string>(maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    Id_PhongBan = table.Column<string>(maxLength: 10, nullable: false),
                    Log = table.Column<string>(nullable: true),
                    NamLamViec = table.Column<int>(nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_PhongBan_DonVi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NS_QS_ChungTu",
                columns: table => new
                {
                    iID_QSChungTu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iLoai = table.Column<int>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    iThangQuy = table.Column<int>(nullable: false),
                    sMoTa = table.Column<string>(maxLength: 250, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QTQS_ChungTu", x => x.iID_QSChungTu);
                });

            migrationBuilder.CreateTable(
                name: "NS_QS_ChungTuChiTiet",
                columns: table => new
                {
                    iID_QSCTChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fSoBinhNhat = table.Column<double>(nullable: false),
                    fSoBinhNhi = table.Column<double>(nullable: false),
                    fSoCNVQP = table.Column<double>(nullable: false),
                    fSoCNVQP_KH = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fSoCNVQPCT = table.Column<double>(nullable: false),
                    fSoCY_H = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fSoCY_KT = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fSoDaiTa = table.Column<double>(nullable: false),
                    fSoDaiUy = table.Column<double>(nullable: false),
                    fSoHaSi = table.Column<double>(nullable: false),
                    fSoHSQBS_KH = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fSoLDHD = table.Column<double>(nullable: false),
                    fSoLDHD_KH = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fSoQNCN = table.Column<double>(nullable: false),
                    fSoQNCN_KH = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fSoQNVQPHD = table.Column<double>(nullable: false),
                    fSoSQ_KH = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fSoThieuTa = table.Column<double>(nullable: false),
                    fSoThieuUy = table.Column<double>(nullable: false),
                    fSoThuongSi = table.Column<double>(nullable: false),
                    fSoThuongTa = table.Column<double>(nullable: false),
                    fSoThuongUy = table.Column<double>(nullable: false),
                    fSoTrungSi = table.Column<double>(nullable: false),
                    fSoTrungTa = table.Column<double>(nullable: false),
                    fSoTrungUy = table.Column<double>(nullable: false),
                    fSoTSQ = table.Column<double>(nullable: false),
                    fSoTuong = table.Column<double>(nullable: false),
                    fSoVCQP = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    fTongSo = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MLNS = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iID_QSChungTu = table.Column<Guid>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iThangQuy = table.Column<int>(nullable: false),
                    sGhiChu = table.Column<string>(type: "ntext", nullable: true),
                    sKyHieu = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    sNgaySua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QS_ChungTuChiTiet", x => x.iID_QSCTChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_QS_MucLuc",
                columns: table => new
                {
                    iID_QSMucLuc = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    iID_MLNS = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iThuTu = table.Column<int>(nullable: false),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    sHienThi = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sKyHieu = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sM = table.Column<string>(maxLength: 10, nullable: false, defaultValueSql: "('')"),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    sTM = table.Column<string>(maxLength: 10, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_MucLucQuanSo", x => x.iID_QSMucLuc)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_QT_ChungTu",
                columns: table => new
                {
                    iID_QTChungTu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongTuChi_DeNghi = table.Column<double>(nullable: false),
                    fTongTuChi_PheDuyet = table.Column<double>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: false, defaultValueSql: "((2))"),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    iThangQuy = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    iThangQuyLoai = table.Column<int>(nullable: false),
                    sDSLNS = table.Column<string>(nullable: true),
                    sLoai = table.Column<string>(maxLength: 10, nullable: true, defaultValueSql: "((0))"),
                    sMoTa = table.Column<string>(maxLength: 250, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    sThangQuy_MoTa = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "(N'T')"),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QT_ChungTu", x => x.iID_QTChungTu);
                });

            migrationBuilder.CreateTable(
                name: "NS_QT_ChungTuChiTiet",
                columns: table => new
                {
                    iID_QTCTChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fSoLuot = table.Column<double>(nullable: false),
                    fSoNgay = table.Column<double>(nullable: false),
                    fSoNguoi = table.Column<double>(nullable: false),
                    fTuChi_DeNghi = table.Column<double>(nullable: false),
                    fTuChi_PheDuyet = table.Column<double>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    iID_MLNS = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iID_QTChungTu = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: false, defaultValueSql: "((2))"),
                    iThangQuy = table.Column<int>(nullable: true),
                    iThangQuyLoai = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    sK = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sL = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sLNS = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true, defaultValueSql: "('')"),
                    sNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG1 = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG2 = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG3 = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_QT_ChungTuChiTiet", x => x.iID_QTCTChiTiet);
                });

            migrationBuilder.CreateTable(
                name: "NS_QT_ChungTuChiTiet_GiaiThich",
                columns: table => new
                {
                    iID_QTCTCTGiaiThich = table.Column<Guid>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fKinhPhi_An = table.Column<double>(nullable: false),
                    fKinhPhi_LuongPC_Khac = table.Column<double>(nullable: false),
                    fKinhPhi_PhuCap_HSQBS = table.Column<double>(nullable: false),
                    fLuongBHXH_CNVQP_Tru = table.Column<double>(nullable: false),
                    fLuongBHXH_HD_Tru = table.Column<double>(nullable: false),
                    fLuongBHXH_QNCN_Tru = table.Column<double>(nullable: false),
                    fLuongBHXH_SiQuan_Tru = table.Column<double>(nullable: false),
                    fLuong_CNVQP = table.Column<double>(nullable: false),
                    fLuong_CNVQP_QT = table.Column<double>(nullable: false),
                    fLuong_CNVQP_Tru = table.Column<double>(nullable: false),
                    fLuong_HD = table.Column<double>(nullable: false),
                    fLuong_HD_QT = table.Column<double>(nullable: false),
                    fLuong_HD_Tru = table.Column<double>(nullable: false),
                    fLuong_QNCN = table.Column<double>(nullable: false),
                    fLuong_QNCN_QT = table.Column<double>(nullable: false),
                    fLuong_QNCN_Tru = table.Column<double>(nullable: false),
                    fLuong_SiQuan = table.Column<double>(nullable: false),
                    fLuong_SiQuan_QT = table.Column<double>(nullable: false),
                    fLuong_SiQuan_Tru = table.Column<double>(nullable: false),
                    fNgayAn = table.Column<double>(nullable: false),
                    fNgayAn_Cong = table.Column<double>(nullable: false),
                    fNgayAn_QT = table.Column<double>(nullable: false),
                    fNgayAn_Tru = table.Column<double>(nullable: false),
                    fPhuCapBHXH_CNVQP_Tru = table.Column<double>(nullable: false),
                    fPhuCapBHXH_HD_Tru = table.Column<double>(nullable: false),
                    fPhuCapBHXH_QNCN_Tru = table.Column<double>(nullable: false),
                    fPhuCapBHXH_SiQuan_Tru = table.Column<double>(nullable: false),
                    fPhuCap_CNVQP = table.Column<double>(nullable: false),
                    fPhuCap_CNVQP_QT = table.Column<double>(nullable: false),
                    fPhuCap_CNVQP_Tru = table.Column<double>(nullable: false),
                    fPhuCap_HD = table.Column<double>(nullable: false),
                    fPhuCap_HD_QT = table.Column<double>(nullable: false),
                    fPhuCap_HD_Tru = table.Column<double>(nullable: false),
                    fPhuCap_QNCN = table.Column<double>(nullable: false),
                    fPhuCap_QNCN_QT = table.Column<double>(nullable: false),
                    fPhuCap_QNCN_Tru = table.Column<double>(nullable: false),
                    fPhuCap_SiQuan = table.Column<double>(nullable: false),
                    fPhuCap_SiQuan_QT = table.Column<double>(nullable: false),
                    fPhuCap_SiQuan_Tru = table.Column<double>(nullable: false),
                    fRaQuan_CNVQP_Nguoi_Huu = table.Column<double>(nullable: false),
                    fRaQuan_CNVQP_Nguoi_ThoiViec = table.Column<double>(nullable: false),
                    fRaQuan_CNVQP_Nguoi_XuatNgu = table.Column<double>(nullable: false),
                    fRaQuan_CNVQP_Tien_Huu = table.Column<double>(nullable: false),
                    fRaQuan_CNVQP_Tien_ThoiViec = table.Column<double>(nullable: false),
                    fRaQuan_CNVQP_Tien_XuatNgu = table.Column<double>(nullable: false),
                    fRaQuan_HSQCS_Nguoi_Huu = table.Column<double>(nullable: false),
                    fRaQuan_HSQCS_Nguoi_ThoiViec = table.Column<double>(nullable: false),
                    fRaQuan_HSQCS_Nguoi_XuatNgu = table.Column<double>(nullable: false),
                    fRaQuan_HSQCS_Tien_Huu = table.Column<double>(nullable: false),
                    fRaQuan_HSQCS_Tien_ThoiViec = table.Column<double>(nullable: false),
                    fRaQuan_HSQCS_Tien_XuatNgu = table.Column<double>(nullable: false),
                    fRaQuan_QNCN_Nguoi_Huu = table.Column<double>(nullable: false),
                    fRaQuan_QNCN_Nguoi_ThoiViec = table.Column<double>(nullable: false),
                    fRaQuan_QNCN_Nguoi_XuatNgu = table.Column<double>(nullable: false),
                    fRaQuan_QNCN_Tien_Huu = table.Column<double>(nullable: false),
                    fRaQuan_QNCN_Tien_ThoiViec = table.Column<double>(nullable: false),
                    fRaQuan_QNCN_Tien_XuatNgu = table.Column<double>(nullable: false),
                    fRaQuan_SiQuan_Nguoi_Huu = table.Column<double>(nullable: false),
                    fRaQuan_SiQuan_Nguoi_ThoiViec = table.Column<double>(nullable: false),
                    fRaQuan_SiQuan_Nguoi_XuatNgu = table.Column<double>(nullable: false),
                    fRaQuan_SiQuan_Tien_Huu = table.Column<double>(nullable: false),
                    fRaQuan_SiQuan_Tien_ThoiViec = table.Column<double>(nullable: false),
                    fRaQuan_SiQuan_Tien_XuatNgu = table.Column<double>(nullable: false),
                    iID_GiaiThich = table.Column<string>(maxLength: 50, nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_QTChungTu = table.Column<Guid>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iThangQuy = table.Column<int>(nullable: false),
                    iThangQuyLoai = table.Column<int>(nullable: false),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true),
                    sMoTa_KienNghi = table.Column<string>(nullable: true),
                    sMoTa_TinhHinh = table.Column<string>(nullable: true),
                    sNgaySua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.QT_ChungTuChiTiet_GiaiThich", x => x.iID_QTCTCTGiaiThich);
                });

            migrationBuilder.CreateTable(
                name: "NS_QT_ChungTuChiTiet_GiaiThich_LuongTru",
                columns: table => new
                {
                    iID_QTCTCTGiaiThich_LuongTru = table.Column<Guid>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fLuongCapBac = table.Column<double>(nullable: false),
                    fLuongPhuCapCongVu = table.Column<double>(nullable: false),
                    fLuongPhuCapKhac = table.Column<double>(nullable: false),
                    fLuongPhuCapKhacBH = table.Column<double>(nullable: false),
                    fLuongThamNien = table.Column<double>(nullable: false),
                    fLuongThang = table.Column<double>(nullable: false),
                    fNgayNghi = table.Column<double>(nullable: false),
                    fSoNguoi = table.Column<double>(nullable: false),
                    fTong_BaoHiem = table.Column<double>(nullable: false),
                    iID_DoiTuong = table.Column<string>(maxLength: 20, nullable: false),
                    iID_GiaiThich = table.Column<string>(maxLength: 50, nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_QTChungTu = table.Column<Guid>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iStatus = table.Column<int>(nullable: false),
                    iThangQuy = table.Column<int>(nullable: false),
                    iThangQuyLoai = table.Column<int>(nullable: false),
                    sHoTen = table.Column<string>(maxLength: 250, nullable: true),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.QT_ChungTuChiTiet_GiaiThich_LuongTru", x => x.iID_QTCTCTGiaiThich_LuongTru);
                });

            migrationBuilder.CreateTable(
                name: "NS_Session",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    Log = table.Column<string>(nullable: true),
                    NamLamViec = table.Column<int>(nullable: false),
                    NamNganSach = table.Column<int>(nullable: false),
                    NguonNganSach = table.Column<int>(nullable: false),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    ThangLamViec = table.Column<int>(nullable: true),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true),
                    UserName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_Session", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NS_SKT_ChungTu",
                columns: table => new
                {
                    iID_CTSoKiemTra = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongMuaHangCapHienVat = table.Column<double>(nullable: true),
                    fTongPhanCap = table.Column<double>(nullable: true),
                    fTongTuChi = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 10, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true),
                    iLoai = table.Column<int>(nullable: false),
                    iLoaiChungTu = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iNamNganSach = table.Column<int>(nullable: true),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    sDSSoChungTuTongHop = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: false),
                    sSoQuyetDinh = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_ChungTu", x => x.iID_CTSoKiemTra)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_SKT_ChungTuChiTiet_CanCu",
                columns: table => new
                {
                    iID_ChungTuChiTiet_CanCu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fHienVat = table.Column<double>(nullable: true),
                    fHuyDongTonKho = table.Column<double>(nullable: false),
                    fMuaHangCapHienVat = table.Column<double>(nullable: false),
                    fPhanCap = table.Column<double>(nullable: false),
                    fTuChi = table.Column<double>(nullable: false),
                    iID_CanCu = table.Column<Guid>(nullable: false),
                    iID_MLSKT = table.Column<Guid>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iiID_CTSoKiemTra = table.Column<Guid>(nullable: false),
                    sKyHieu = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_ChungTuChiTiet_CanCu", x => x.iID_ChungTuChiTiet_CanCu);
                });

            migrationBuilder.CreateTable(
                name: "NS_SKT_ChungTu_ChungTuCanCu",
                columns: table => new
                {
                    iID_ChungTu_CanCu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_CanCu = table.Column<Guid>(nullable: false),
                    iID_CTCanCu = table.Column<Guid>(nullable: false),
                    iiID_CTSoKiemTra = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_ChungTuChiTiet_CanCu_ChungTu", x => x.iID_ChungTu_CanCu);
                });

            migrationBuilder.CreateTable(
                name: "NS_SKT_MucLuc",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    dNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    dNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MLSKT = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_MLSKTCha = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    iTrangThai = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    KyHieuCha = table.Column<string>(maxLength: 12, nullable: true),
                    Log = table.Column<string>(nullable: true),
                    Muc = table.Column<string>(maxLength: 10, nullable: false, defaultValueSql: "('')"),
                    sKyHieu = table.Column<string>(maxLength: 50, nullable: true),
                    sLoaiNhap = table.Column<string>(maxLength: 100, nullable: true, defaultValueSql: "((1))"),
                    sM = table.Column<string>(maxLength: 10, nullable: false),
                    sMoTa = table.Column<string>(nullable: false),
                    sNG_Cha = table.Column<string>(maxLength: 3, nullable: false),
                    sNG = table.Column<string>(maxLength: 2, nullable: false),
                    sSTT = table.Column<string>(maxLength: 10, nullable: true, defaultValueSql: "('')"),
                    sSTTBC = table.Column<string>(maxLength: 12, nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_MucLuc", x => x.iID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_SKT_NganhThamDinh",
                columns: table => new
                {
                    iID_CTNganhThamDinh = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongHienVat_CTC = table.Column<double>(nullable: true),
                    fTongHienVat_Nganh = table.Column<double>(nullable: true),
                    fTongTuChi_CTC = table.Column<double>(nullable: true),
                    fTongTuChi_Nganh = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 10, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: false),
                    iLoai = table.Column<int>(nullable: false),
                    iLoaiChungTu = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iNamNganSach = table.Column<int>(nullable: false),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    iTongHop = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: false),
                    sSoQuyetDinh = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_NganhThamDinh", x => x.iID_CTNganhThamDinh)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_SKT_NganhThamDinhChiTiet",
                columns: table => new
                {
                    iID_CTNganhThamDinhChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fChiDacThuNganhPhanCap = table.Column<double>(nullable: false),
                    fSuDungTonKho = table.Column<double>(nullable: false),
                    fTuChi = table.Column<double>(nullable: false),
                    iID_CTNganhThamDinh = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: false),
                    iID_MucLuc = table.Column<Guid>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iNamNganSach = table.Column<int>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sM = table.Column<string>(maxLength: 10, nullable: false),
                    sMoTa = table.Column<string>(nullable: false),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_NganhThamDinhChiTiet", x => x.iID_CTNganhThamDinhChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NS_SKT_NganhThamDinhChiTiet_SKT",
                columns: table => new
                {
                    iID_CTNganhThamDinhChiTiet_SKT = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fChiDacThuNganhPhanCap = table.Column<double>(nullable: true),
                    fSuDungTonKho = table.Column<double>(nullable: true),
                    fTuChi = table.Column<double>(nullable: true),
                    iID_CTSoKiemTra = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: false),
                    iID_MucLuc = table.Column<Guid>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iNamNganSach = table.Column<int>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sM = table.Column<string>(maxLength: 10, nullable: false),
                    sMoTa = table.Column<string>(nullable: false),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NS_SKT_NganhThamDinhChiTiet_SKT", x => x.iID_CTNganhThamDinhChiTiet_SKT);
                });

            migrationBuilder.CreateTable(
                name: "TL_BangLuong_KeHoach",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Gia_Tri = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Ma_CachTL = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CanBo = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_CB = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_DonVi = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_Hieu_CanBo = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_PhuCap = table.Column<string>(maxLength: 50, nullable: true),
                    Nam = table.Column<int>(nullable: false),
                    parent = table.Column<Guid>(nullable: true),
                    Ten_CanBo = table.Column<string>(maxLength: 100, nullable: true),
                    Thang = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_BangLuong_KeHoach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_BangLuong_Thang",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Gia_Tri = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
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
                    table.PrimaryKey("PK_TL_BangLuong_Thang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_Bao_Cao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newsequentialid())"),
                    IsParent = table.Column<bool>(nullable: true),
                    Ma_BaoCao = table.Column<string>(maxLength: 20, nullable: false),
                    Ma_Parent = table.Column<string>(maxLength: 20, nullable: true),
                    Note = table.Column<string>(maxLength: 255, nullable: true),
                    Ten_BaoCao = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_Bao_Cao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_CanBo_PhuCap_KeHoach",
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
                    table.PrimaryKey("PK_TL_CanBo_PhuCap_KeHoach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DieuChinh_QS_KeHoach",
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
                    table.PrimaryKey("PK_TL_DieuChinh_QS_KeHoach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_Cach_TinhLuong_BaoHiem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    CongThuc = table.Column<string>(type: "ntext", nullable: true),
                    Ma_CachTL = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ma_Cot = table.Column<string>(maxLength: 100, nullable: true),
                    Ma_KMCP = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_KMCP1 = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    NoiDung = table.Column<string>(maxLength: 500, nullable: true),
                    Ten_CachTL = table.Column<string>(maxLength: 500, nullable: true),
                    Ten_Cot = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_Cach_TinhLuong_BaoHiem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_Cach_TinhLuong_Chuan",
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
                    table.PrimaryKey("PK_TL_DM_Cach_TinhLuong_Chuan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_Cach_TinhLuong_TruyLinh",
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
                    table.PrimaryKey("PK_TL_DM_Cach_TinhLuong_TruyLinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_CapBac",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Bhcs_Cq = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Bhtn_Cq = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Bhxh_Cq = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Bhyt_Cq = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Hs_Bhcs = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Hs_Bhtn = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Hs_Bhxh = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Hs_Bhyt = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Hs_Kpcd = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Kpcd_Cq = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Lht_Hs = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Ma_Cb = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Note = table.Column<string>(maxLength: 200, nullable: true),
                    Parent = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    PhuCapRaQuan = table.Column<decimal>(type: "numeric(16, 0)", nullable: true),
                    Readonly = table.Column<bool>(nullable: true),
                    Splits = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    Ten_Cb = table.Column<string>(maxLength: 50, nullable: true),
                    TiLeHuong = table.Column<decimal>(type: "numeric(5, 2)", nullable: true, defaultValueSql: "((1))"),
                    XauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_CapBac", x => x.Id);
                    table.UniqueConstraint("AK_TL_DM_CapBac_Ma_Cb", x => x.Ma_Cb);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_CapBac_KeHoach",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BHTN_CN = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    BHTN_CQ = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    BHXH_CN = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    BHXH_CQ = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    BHYT_CN = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    BHYT_CQ = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    HsLuongKeHoach = table.Column<decimal>(type: "numeric(16, 4)", nullable: true),
                    HsLuongTran = table.Column<decimal>(type: "numeric(16, 4)", nullable: true),
                    HsVk = table.Column<decimal>(type: "numeric(16, 4)", nullable: true),
                    IdHslHienTai = table.Column<Guid>(nullable: true),
                    IdHslKeHoach = table.Column<Guid>(nullable: true),
                    IdHslTran = table.Column<Guid>(nullable: true),
                    KPCD_CN = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    KPCD_CQ = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    LHT_HS = table.Column<decimal>(type: "numeric(15, 4)", nullable: true),
                    Ma_Cb = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_Cb_KeHoach = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    MaCbTran = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    MoTa = table.Column<string>(maxLength: 200, nullable: true),
                    MoTa_KeHoach = table.Column<string>(maxLength: 200, nullable: true),
                    MoTaLuongTran = table.Column<string>(maxLength: 200, nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    Nhom = table.Column<string>(maxLength: 50, nullable: true),
                    Parent = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    PCRQ_TT = table.Column<double>(nullable: true),
                    Readonly = table.Column<bool>(nullable: true),
                    Splits = table.Column<bool>(nullable: true),
                    Ten_Cb = table.Column<string>(maxLength: 100, nullable: false),
                    Ten_Cb_KeHoach = table.Column<string>(maxLength: 100, nullable: true),
                    Thoi_Han_Tang = table.Column<int>(nullable: true),
                    Tuoi_Huu_Nam = table.Column<int>(nullable: true),
                    Tuoi_Huu_Nu = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_CapBac_KeHoach", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_ChucVu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    HeSo_Cv = table.Column<decimal>(type: "numeric(5, 2)", nullable: true),
                    Ma_Cv = table.Column<string>(maxLength: 20, nullable: false),
                    Ten_Cv = table.Column<string>(maxLength: 50, nullable: true),
                    ThanhTien_Cv = table.Column<decimal>(type: "numeric(14, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_ChucVu", x => x.Id);
                    table.UniqueConstraint("AK_TL_DM_ChucVu_Ma_Cv", x => x.Ma_Cv);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_DonVi",
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
                    table.PrimaryKey("PK_TL_DM_DonVi", x => x.Id);
                    table.UniqueConstraint("AK_TL_DM_DonVi_Ma_DonVi", x => x.Ma_DonVi);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_HSL_KeHoach",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Lht_hs_kh = table.Column<decimal>(type: "numeric(16, 4)", nullable: true),
                    MaCb = table.Column<string>(maxLength: 20, nullable: true),
                    MoTa = table.Column<string>(maxLength: 200, nullable: true),
                    Ngach = table.Column<string>(maxLength: 20, nullable: true),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_HSL_KeHoach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_MapPC_Detail",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    giatri = table.Column<decimal>(type: "numeric(17, 4)", nullable: true),
                    id_phuCap = table.Column<Guid>(nullable: true),
                    ma_phuCap = table.Column<string>(maxLength: 50, nullable: true),
                    old_value = table.Column<string>(maxLength: 50, nullable: true),
                    ten_phuCap = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_MapPC_Detail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_NangLuong",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    LHT_HS_HT = table.Column<decimal>(type: "numeric(5, 2)", nullable: true),
                    LHT_HS_KH = table.Column<decimal>(type: "numeric(5, 2)", nullable: true),
                    Ma_Cb_HT = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_Cb_KH = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Note = table.Column<string>(maxLength: 200, nullable: true),
                    Parent = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ten_Cb_HT = table.Column<string>(maxLength: 50, nullable: true),
                    Ten_Cb_KH = table.Column<string>(maxLength: 50, nullable: true),
                    Thoi_Han_Tang = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_NangLuong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_PhuCap",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bGiaTri = table.Column<bool>(nullable: true),
                    bHuongPc_Sn = table.Column<bool>(nullable: true),
                    bSaoChep = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    Chon = table.Column<bool>(nullable: true),
                    Cong_Thuc = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    Dinh_Dang = table.Column<bool>(nullable: true),
                    Gia_Tri = table.Column<decimal>(type: "numeric(17, 3)", nullable: true),
                    He_So = table.Column<decimal>(type: "numeric(8, 4)", nullable: true),
                    HuongPC_SN = table.Column<decimal>(type: "numeric(5, 2)", nullable: true),
                    iDinhDang = table.Column<int>(nullable: true),
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
                    Ten_PhuCap = table.Column<string>(maxLength: 100, nullable: true),
                    Tinh_BHXH = table.Column<bool>(nullable: true, defaultValueSql: "('TRUE')"),
                    Tinh_TNCN = table.Column<bool>(nullable: true),
                    Xau_Noi_Ma = table.Column<string>(maxLength: 255, nullable: true),
                    XSort = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_PhuCap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_PhuCap_KeHoach",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Chon = table.Column<string>(maxLength: 50, nullable: true),
                    Gia_Tri_Cu = table.Column<decimal>(type: "numeric(17, 3)", nullable: true),
                    Gia_Tri_Moi = table.Column<decimal>(type: "numeric(17, 3)", nullable: false),
                    Is_Formula = table.Column<string>(maxLength: 50, nullable: true),
                    Is_Readonly = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_PhuCap = table.Column<string>(maxLength: 50, nullable: false),
                    NgayApDung = table.Column<DateTime>(type: "date", nullable: false),
                    Parent = table.Column<string>(maxLength: 50, nullable: false),
                    Splits = table.Column<string>(maxLength: 50, nullable: true),
                    Ten_PhuCap = table.Column<string>(maxLength: 100, nullable: false),
                    Xau_Noi_Ma = table.Column<string>(maxLength: 50, nullable: true),
                    XSort = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_PhuCap_KeHoach", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_TangGiam",
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
                    table.PrimaryKey("PK_TL_DM_TangGiam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_ThemCachTinhLuong",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Ma_ThemCachTL = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ten_ThemCachTL = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_ThemCachTinhLuong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_ThueThuNhapCaNhan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
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
                    table.PrimaryKey("PK_TL_DM_ThueThuNhapCaNhan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_TietTieuMuc_Nganh",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Ma_TTM_Ng = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ten_TTM_Ng = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_TietTieuMuc_Nganh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DS_BangLuong_KeHoach",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    iTrangThai = table.Column<bool>(nullable: true),
                    Ma_CachTL = table.Column<string>(maxLength: 20, nullable: true),
                    Ma_DonVi = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Nam = table.Column<int>(nullable: false),
                    Ten_BangLuong = table.Column<string>(maxLength: 255, nullable: true),
                    Thang = table.Column<int>(nullable: false),
                    UserCreator = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DS_BangLuong_KeHoach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DS_SoSanhLuong",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Ma_Bang = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_DonVi = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    NamSS_1 = table.Column<int>(nullable: false),
                    NamSS_2 = table.Column<int>(nullable: false),
                    Ten_Bang = table.Column<string>(maxLength: 150, nullable: false),
                    Ten_DonVi = table.Column<string>(maxLength: 100, nullable: true),
                    ThangSS_1 = table.Column<int>(nullable: false),
                    ThangSS_2 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DS_SoSanhLuong", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TL_GT_Tai_Chinh",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    CapPhatTiep_Nam = table.Column<int>(nullable: false),
                    CapPhatTiep_Thang = table.Column<int>(nullable: false),
                    LoPhi_DuocCap = table.Column<int>(nullable: true),
                    LoPhi_ThanhToan = table.Column<int>(nullable: true),
                    Ma_CanBo = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_Cb = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_CV = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_GiayGTTC = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Nam = table.Column<int>(nullable: false),
                    NganHang = table.Column<string>(maxLength: 500, nullable: true),
                    NgayKy = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKy_QD = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ngay_NN = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_TN = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_XN = table.Column<DateTime>(type: "datetime", nullable: true),
                    NoiChuyenDen = table.Column<string>(maxLength: 500, nullable: false),
                    So_QD = table.Column<string>(maxLength: 100, nullable: false),
                    So_SoLuong = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    SoTaiKhoan = table.Column<string>(maxLength: 50, nullable: true),
                    Ten_CanBo = table.Column<string>(maxLength: 150, nullable: false),
                    Ten_CapBac = table.Column<string>(maxLength: 200, nullable: true),
                    Ten_Cv = table.Column<string>(maxLength: 150, nullable: true),
                    Thang = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_GT_Tai_Chinh", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TL_Map_Column_Config",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Is_Map_PhuCap = table.Column<bool>(nullable: true),
                    Is_Map_Value = table.Column<bool>(nullable: true),
                    Map_Expression = table.Column<string>(maxLength: 200, nullable: true),
                    New_Column = table.Column<string>(maxLength: 100, nullable: true),
                    Old_Column = table.Column<string>(maxLength: 100, nullable: true),
                    Use_PhuCap_Value = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_Map_Column_Config", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TL_PhuCap_DieuChinh",
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
                    table.PrimaryKey("PK_TL_PhuCap_DieuChinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_PhuCap_MLNS",
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
                    Ten_PhuCap = table.Column<string>(maxLength: 100, nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: true),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true),
                    XauNoiMa = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_PhuCap_MLNS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TL_QS_ChungTuChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BinhNhat = table.Column<double>(nullable: true),
                    BinhNhi = table.Column<double>(nullable: true),
                    CNQP = table.Column<double>(nullable: true),
                    DaiTa = table.Column<double>(nullable: true),
                    DaiUy = table.Column<double>(nullable: true),
                    DaiUyCn = table.Column<double>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    HaSi = table.Column<double>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true),
                    Id_ChungTu = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Id_DonVi = table.Column<string>(maxLength: 50, nullable: false),
                    LDHD = table.Column<double>(nullable: true),
                    MLNS_Id = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    MLNS_Id_Parent = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    MoTa = table.Column<string>(maxLength: 500, nullable: true),
                    NamLamViec = table.Column<int>(nullable: false),
                    QNCN = table.Column<double>(nullable: true),
                    TenDonVi = table.Column<string>(maxLength: 500, nullable: false),
                    Thang = table.Column<int>(nullable: true),
                    ThieuTa = table.Column<double>(nullable: true),
                    ThieuTaCn = table.Column<double>(nullable: true),
                    ThieuUy = table.Column<double>(nullable: true),
                    ThieuUyCn = table.Column<double>(nullable: true),
                    ThuongSi = table.Column<double>(nullable: true),
                    ThuongTa = table.Column<double>(nullable: true),
                    ThuongTaCn = table.Column<double>(nullable: true),
                    ThuongUy = table.Column<double>(nullable: true),
                    ThuongUyCn = table.Column<double>(nullable: true),
                    TongSo = table.Column<double>(nullable: true),
                    TrungSi = table.Column<double>(nullable: true),
                    TrungTa = table.Column<double>(nullable: true),
                    TrungTaCn = table.Column<double>(nullable: true),
                    TrungUy = table.Column<double>(nullable: true),
                    TrungUyCn = table.Column<double>(nullable: true),
                    Tuong = table.Column<double>(nullable: true),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true),
                    VCQP = table.Column<double>(nullable: true),
                    XauNoiMa = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_QS_ChungTuChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_QS_KeHoach_ChiTiet",
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
                    table.PrimaryKey("PK_TL_QS_KeHoach_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_QT_ChungTuChiTiet",
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
                    table.PrimaryKey("PK_TL_QT_ChungTuChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_QT_ChungTuChiTiet_GiaiThich",
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
                    fLuong_CNVQP_Tru = table.Column<double>(nullable: true),
                    fLuong_HD_Tru = table.Column<double>(nullable: true),
                    fLuong_QNCN_Tru = table.Column<double>(nullable: true),
                    fLuong_SiQuan_Tru = table.Column<double>(nullable: true),
                    fNgayAn = table.Column<double>(nullable: true),
                    fNgayAn_Cong = table.Column<double>(nullable: true),
                    fNgayAn_QT = table.Column<double>(nullable: true),
                    fNgayAn_Tru = table.Column<double>(nullable: true),
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
                    table.PrimaryKey("PK_TL_QT_ChungTuChiTiet_GiaiThich", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_QT_ChungTuChiTiet_KeHoach",
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
                    table.PrimaryKey("PK_TL_QT_ChungTuChiTiet_KeHoach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TN_DanhMucLoaiHinh",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bLaHangCha = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "('')"),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_MaNhomNguoiDung_DuocGiao = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNhomNguoiDung_Public = table.Column<string>(maxLength: 50, nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iSoLanSua = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iSTT = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "((1))"),
                    iTrangThai = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    ID_MaLoaiHinh = table.Column<Guid>(nullable: true),
                    ID_MaLoaiHinh_Cha = table.Column<Guid>(nullable: true),
                    ID_PhongBan = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "((0))"),
                    LNS = table.Column<string>(maxLength: 250, nullable: true, defaultValueSql: "((0))"),
                    MoTa = table.Column<string>(nullable: true),
                    sID_MaNguoiDung_DuocGiao = table.Column<string>(maxLength: 200, nullable: true),
                    sIPSua = table.Column<string>(maxLength: 20, nullable: true),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN_DanhMucLoaiHinh", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TN_DT_ChungTu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    iDot = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    iGuiNhan = table.Column<int>(nullable: true),
                    iKiemDuyet = table.Column<int>(nullable: true),
                    iLoai = table.Column<int>(nullable: false),
                    iTongHop = table.Column<string>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    Id_DonVi = table.Column<string>(maxLength: 500, nullable: true),
                    Id_DonViTao = table.Column<string>(maxLength: 50, nullable: false),
                    Id_DotNhan = table.Column<string>(nullable: true),
                    IsLocked = table.Column<bool>(nullable: false),
                    LNS = table.Column<string>(nullable: true),
                    LoaiChungTu = table.Column<int>(nullable: true),
                    LoaiNganSach = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    Log = table.Column<string>(nullable: true),
                    MoTaChiTiet = table.Column<string>(maxLength: 1000, nullable: true),
                    NamLamViec = table.Column<int>(nullable: true),
                    NamNganSach = table.Column<int>(nullable: true, defaultValueSql: "((2))"),
                    NgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguonNganSach = table.Column<int>(nullable: true),
                    SoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    SoChungTuIndex = table.Column<int>(nullable: true),
                    Solieunhap = table.Column<string>(name: "So lieu nhap", maxLength: 50, nullable: true),
                    SoQuyetDinh = table.Column<string>(maxLength: 50, nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    TenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    TuChiSum = table.Column<double>(nullable: false),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN_DT_ChungTu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TN_DT_ChungTuChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    B = table.Column<string>(maxLength: 50, nullable: true),
                    bHangCha = table.Column<bool>(nullable: false),
                    Chuong = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    iPhanCap = table.Column<int>(nullable: false),
                    iTrangThai = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    Id_ChungTu = table.Column<Guid>(nullable: true),
                    Id_DonVi = table.Column<string>(maxLength: 50, nullable: true),
                    Id_DotNhan = table.Column<Guid>(nullable: true),
                    Id_PhongBan = table.Column<string>(maxLength: 10, nullable: true),
                    Id_PhongBanDich = table.Column<string>(maxLength: 10, nullable: true),
                    K = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    L = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    LNS = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    Log = table.Column<string>(nullable: true),
                    M = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    MLNS_Id = table.Column<Guid>(nullable: true, defaultValueSql: "(newid())"),
                    MLNS_Id_Parent = table.Column<Guid>(nullable: true),
                    NamLamViec = table.Column<int>(nullable: true),
                    NamNganSach = table.Column<int>(nullable: false, defaultValueSql: "((2))"),
                    NG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    NguonNganSach = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    NoiDung = table.Column<string>(maxLength: 500, nullable: true, defaultValueSql: "('')"),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    TenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    TNG = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    TNG1 = table.Column<string>(maxLength: 50, nullable: true),
                    TNG2 = table.Column<string>(maxLength: 50, nullable: true),
                    TNG3 = table.Column<string>(maxLength: 50, nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    TuChi = table.Column<double>(nullable: false),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true),
                    XauNoiMa = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN_DT_ChungTuChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TN_QT_ChungTu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    iKiemDuyet = table.Column<int>(nullable: true),
                    iThangQuy = table.Column<int>(nullable: true),
                    iThangQuyLoai = table.Column<int>(nullable: false),
                    iThangQuy_MoTa = table.Column<string>(maxLength: 50, nullable: true),
                    iTongHop = table.Column<string>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    Id_DonVi = table.Column<string>(maxLength: 500, nullable: true),
                    Id_DonViTao = table.Column<string>(maxLength: 50, nullable: true),
                    Id_PhongBan = table.Column<string>(maxLength: 50, nullable: true),
                    IsLocked = table.Column<bool>(nullable: false),
                    LNS = table.Column<string>(nullable: true),
                    Log = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(maxLength: 250, nullable: true),
                    MoTaChiTiet = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    NamLamViec = table.Column<int>(nullable: true),
                    NamNganSach = table.Column<int>(nullable: false, defaultValueSql: "((2))"),
                    NgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguonNganSach = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    Noidung = table.Column<string>(nullable: true),
                    SoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    SoChungTuIndex = table.Column<int>(nullable: true),
                    SoQuyetDinh = table.Column<string>(maxLength: 50, nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    TenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    TongSoChiPhiSum = table.Column<double>(nullable: false),
                    TongSoThuSum = table.Column<double>(nullable: false),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN_QT_ChungTu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TN_QT_ChungTuChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bLaHangCha = table.Column<bool>(nullable: false),
                    bThoaiThu = table.Column<bool>(nullable: true),
                    ChenhLech = table.Column<double>(nullable: true),
                    ChiPhiKhac = table.Column<double>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    iGuiNhan = table.Column<int>(nullable: true),
                    iLoai = table.Column<string>(maxLength: 10, nullable: true, defaultValueSql: "((0))"),
                    iThangQuy = table.Column<int>(nullable: true, defaultValueSql: "('')"),
                    iThangQuyLoai = table.Column<int>(nullable: false, defaultValueSql: "('')"),
                    iTrangThai = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    Id_ChungTu = table.Column<Guid>(nullable: true),
                    Id_DonVi = table.Column<string>(maxLength: 50, nullable: true),
                    Id_DonViTao = table.Column<string>(maxLength: 50, nullable: true),
                    ID_MaLoaiHinh = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    ID_MaLoaiHinh_Cha = table.Column<Guid>(nullable: true),
                    Id_PhongBan = table.Column<string>(maxLength: 10, nullable: true),
                    Id_PhongBanDich = table.Column<string>(maxLength: 10, nullable: true),
                    LNS = table.Column<string>(maxLength: 50, nullable: true),
                    Log = table.Column<string>(nullable: true),
                    NamLamViec = table.Column<int>(nullable: true),
                    NamNganSach = table.Column<int>(nullable: false, defaultValueSql: "((2))"),
                    NguonNganSach = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    Noidung = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    NSNN_Khac = table.Column<double>(nullable: true),
                    NSNN_Khac_BQP = table.Column<double>(nullable: true),
                    Phi_LePhi = table.Column<double>(nullable: true),
                    PP_BoSungKinhPhi = table.Column<double>(nullable: true),
                    PP_NopNSQP = table.Column<double>(nullable: true),
                    PP_SoChuaPhanPhoi = table.Column<double>(nullable: true),
                    PP_TrichCacQuy = table.Column<double>(nullable: true),
                    QT_KhauHaoTSCĐ = table.Column<double>(nullable: false),
                    QT_QTNSKhac = table.Column<double>(nullable: false),
                    QT_TienLuong = table.Column<double>(nullable: false),
                    QT_TongSoQTNS = table.Column<double>(nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    TenDonVi = table.Column<string>(maxLength: 250, nullable: true),
                    ThueGTGT = table.Column<double>(nullable: false),
                    ThueTNDN = table.Column<double>(nullable: true),
                    ThueTNDN_BQP = table.Column<double>(nullable: true),
                    TongSoChiPhi = table.Column<double>(nullable: true),
                    TongSoThu = table.Column<double>(nullable: true),
                    TongnopNSNN = table.Column<double>(nullable: false),
                    UserCreator = table.Column<string>(maxLength: 50, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN_QT_ChungTuChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_ChuTruongDauTu",
                columns: table => new
                {
                    iID_ChuTruongDauTuID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: false),
                    bIsDelete = table.Column<bool>(nullable: true),
                    bIsGoc = table.Column<bool>(nullable: true),
                    BKhoa = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayThamDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayToTrinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTMDTDuKienPheDuyet = table.Column<double>(nullable: true),
                    fTMDTDuKienThamDinh = table.Column<double>(nullable: true),
                    fTMDTDuKienToTrinh = table.Column<double>(nullable: true),
                    iID_CapPheDuyetID = table.Column<Guid>(nullable: true),
                    iID_ChuDauTuID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DonViThucHienID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_HinhThucDauTuID = table.Column<Guid>(nullable: true),
                    iID_HinhThucQuanLyID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_LoaiDuAn = table.Column<Guid>(nullable: true),
                    iID_MaChuDauTuID = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    iID_NhomDuAnID = table.Column<Guid>(nullable: true),
                    iID_NhomQuanLyID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    sCoQuanPheDuyet = table.Column<string>(maxLength: 300, nullable: true),
                    sCoQuanThamDinh = table.Column<string>(maxLength: 300, nullable: true),
                    sDiaDiem = table.Column<string>(maxLength: 300, nullable: true),
                    sDienTichSuDungDat = table.Column<string>(nullable: true),
                    sHoanThanh = table.Column<string>(maxLength: 50, nullable: true),
                    sKhoiCong = table.Column<string>(maxLength: 50, nullable: true),
                    sLoaiDieuChinh = table.Column<string>(maxLength: 50, nullable: true),
                    sMota = table.Column<string>(nullable: true),
                    sMucTieu = table.Column<string>(nullable: true),
                    sNguoiKy = table.Column<string>(maxLength: 300, nullable: true),
                    sNguonGocSuDungDat = table.Column<string>(nullable: true),
                    sQuyMo = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true),
                    sSoThamDinh = table.Column<string>(maxLength: 300, nullable: true),
                    sSoToTrinh = table.Column<string>(maxLength: 100, nullable: true),
                    sSuCanThietDauTu = table.Column<string>(type: "ntext", nullable: true),
                    sTenDuAn = table.Column<string>(maxLength: 500, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuTruongDauTu", x => x.iID_ChuTruongDauTuID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_ChuTruongDauTu_ChiPhi",
                columns: table => new
                {
                    iID_ChuTruongDauTu_ChiPhiID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    fTienThamDinh = table.Column<double>(nullable: true),
                    fTienToTrinh = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: false),
                    iID_ChuTruongDauTuID = table.Column<Guid>(nullable: false),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuTruongDauTu_ChiPhi", x => x.iID_ChuTruongDauTu_ChiPhiID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_ChuTruongDauTu_DM_HangMuc",
                columns: table => new
                {
                    iID_ChuTruongDauTu_DM_HangMucID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienHangMuc = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    sMaHangMuc = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sQuyMo = table.Column<string>(maxLength: 255, nullable: true),
                    sTenHangMuc = table.Column<string>(maxLength: 300, nullable: true),
                    smaOrder = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DA_ChuTruongDauTu_DM_HangMuc", x => x.iID_ChuTruongDauTu_DM_HangMucID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_ChuTruongDauTu_HangMuc",
                columns: table => new
                {
                    iID_ChuTruongDauTu_HangMucID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    iID_ChuTruongDauTuID = table.Column<Guid>(nullable: false),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_HangMucID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuTruongDauTu_HangMuc", x => x.iID_ChuTruongDauTu_HangMucID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_ChuTruongDauTu_NguonVon",
                columns: table => new
                {
                    iID_ChuTruongDauTu_NguonVonID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    DuAnId = table.Column<Guid>(nullable: true),
                    FGiaTriDieuChinh = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    fTienThamDinh = table.Column<double>(nullable: true),
                    fTienToTrinh = table.Column<double>(nullable: true),
                    iID_ChuTruongDauTuID = table.Column<Guid>(nullable: false),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuTruongDauTu_Nguonvon", x => x.iID_ChuTruongDauTu_NguonVonID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_DuAn",
                columns: table => new
                {
                    iID_DuAnID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsCanBoDuyet = table.Column<bool>(nullable: true),
                    bIsDeleted = table.Column<bool>(nullable: true),
                    bIsDuPhong = table.Column<bool>(nullable: true),
                    bIsDuyet = table.Column<bool>(nullable: true),
                    bIsKetThuc = table.Column<bool>(nullable: true),
                    bLaDuAnChinhThuc = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dKetThucThucTe = table.Column<DateTime>(type: "datetime", nullable: true),
                    dKhoiCongThucTe = table.Column<DateTime>(type: "datetime", nullable: true),
                    fHanMucDauTu = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTongMucDauTu = table.Column<double>(nullable: true),
                    fTongMucDauTuDuKien = table.Column<double>(nullable: true),
                    fTongMucDauTuThamDinh = table.Column<double>(nullable: true),
                    iID_CapPheDuyetID = table.Column<Guid>(nullable: true),
                    iID_ChuDauTuID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DonViThucHienDuAnID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_HinhThucDauTuID = table.Column<Guid>(nullable: true),
                    iID_HinhThucQuanLyID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_LoaiDuAnId = table.Column<Guid>(nullable: true),
                    iID_MaChuDauTuID = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaDonViThucHienDuAnID = table.Column<string>(nullable: true),
                    iID_NganhDuAnID = table.Column<Guid>(nullable: true),
                    iID_NhomDuAnID = table.Column<Guid>(nullable: true),
                    iID_NhomQuanLyID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iMaDuAnIndex = table.Column<int>(nullable: true),
                    Id_DuAnKhthDeXuat = table.Column<Guid>(nullable: true),
                    sCanBoPhuTrach = table.Column<string>(maxLength: 250, nullable: true),
                    sDiaDiem = table.Column<string>(maxLength: 300, nullable: true),
                    sDiaDiemMoTaiKhoan = table.Column<string>(maxLength: 300, nullable: true),
                    sDienTichSuDungDat = table.Column<string>(nullable: true),
                    sKetThuc = table.Column<string>(maxLength: 50, nullable: true),
                    sKhoiCong = table.Column<string>(maxLength: 50, nullable: true),
                    sMaDuAn = table.Column<string>(maxLength: 100, nullable: true),
                    sMaKetNoi = table.Column<string>(maxLength: 100, nullable: true),
                    sMucTieu = table.Column<string>(nullable: true),
                    sNguonGocSuDungDat = table.Column<string>(nullable: true),
                    sQuyMo = table.Column<string>(nullable: true),
                    sSoTaiKhoan = table.Column<string>(maxLength: 50, nullable: true),
                    sSuCanThietDauTu = table.Column<string>(type: "ntext", nullable: true),
                    sTenDuAn = table.Column<string>(nullable: true),
                    sTrangThaiDuAn = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duan", x => x.iID_DuAnID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_DuAn_ChiPhi",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    fTienThamDinh = table.Column<double>(nullable: true),
                    fTienToTrinh = table.Column<double>(nullable: true),
                    iID_DMDuAn_ChiPhi = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: false),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("iID", x => x.iID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_DuAn_HangMuc",
                columns: table => new
                {
                    iID_DuAn_HangMucID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienHangMuc = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    IdChuTruong = table.Column<Guid>(nullable: true),
                    IdLoaiCongTrinh = table.Column<Guid>(nullable: true),
                    maOrder = table.Column<string>(maxLength: 255, nullable: true),
                    sMaHangMuc = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sQuyMo = table.Column<string>(maxLength: 255, nullable: true),
                    sTenHangMuc = table.Column<string>(maxLength: 300, nullable: true),
                    fHanMucDauTu = table.Column<double>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    indexMaHangMuc = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuAn_HangMuc", x => x.iID_DuAn_HangMucID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_DuToan",
                columns: table => new
                {
                    iID_DuToanID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: false),
                    bIsGoc = table.Column<bool>(nullable: true),
                    bIsKhoiTao = table.Column<bool>(nullable: true),
                    BKhoa = table.Column<bool>(nullable: true),
                    bLaThayThe = table.Column<bool>(nullable: false),
                    bLaTongDuToan = table.Column<bool>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayThamDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayToTrinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTiGia = table.Column<double>(nullable: false),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTongDuToanPheDuyet = table.Column<double>(nullable: true),
                    fTongDuToanThamDinh = table.Column<double>(nullable: true),
                    fTongDuToanToTrinh = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: false),
                    iID_DuToanGocID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTuID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TuVanKhaoSatID = table.Column<Guid>(nullable: true),
                    iID_TuVanThietKeID = table.Column<Guid>(nullable: true),
                    sCoQuanPheDuyet = table.Column<string>(maxLength: 300, nullable: true),
                    sCoQuanThamDinh = table.Column<string>(maxLength: 300, nullable: true),
                    sMoTa = table.Column<string>(type: "ntext", nullable: true),
                    sNguoiKy = table.Column<string>(maxLength: 300, nullable: true),
                    sNoiDung = table.Column<string>(type: "ntext", nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: false),
                    sSoThamDinh = table.Column<string>(maxLength: 100, nullable: true),
                    sSoToTrinh = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true),
                    TenDuToan = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuToan", x => x.iID_DuToanID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_DuToan_ChiPhi",
                columns: table => new
                {
                    iID_DuToan_ChiPhiID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriDieuChinh = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    fTienPheDuyetQDDT = table.Column<double>(nullable: true),
                    fTienThamDinh = table.Column<double>(nullable: true),
                    fTienToTrinh = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: false),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAn_ChiPhi = table.Column<Guid>(nullable: true),
                    iID_DuToanID = table.Column<Guid>(nullable: false),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuToan_ChiPhi", x => x.iID_DuToan_ChiPhiID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_DuToan_DM_HangMuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsEdit = table.Column<bool>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienHangMuc = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_HangMucPhanChia = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    IdLoaiCongTrinh = table.Column<Guid>(nullable: true),
                    maOrder = table.Column<string>(maxLength: 255, nullable: true),
                    sMaHangMuc = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sQuyMo = table.Column<string>(maxLength: 255, nullable: true),
                    sTenHangMuc = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DA_DuToan_DM_HangMuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_DuToan_HangMuc",
                columns: table => new
                {
                    iID_DuToan_HangMuciID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriDieuChinh = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    fTienPheDuyetQDDT = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAn_ChiPhi = table.Column<Guid>(nullable: true),
                    iID_DuToanID = table.Column<Guid>(nullable: true),
                    iID_HangMucID = table.Column<Guid>(nullable: true),
                    iID_NguonVon = table.Column<int>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuToan_HangMuc", x => x.iID_DuToan_HangMuciID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_DuToan_Nguonvon",
                columns: table => new
                {
                    iID_DuToan_NguonVonID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriDieuChinh = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    fTienPheDuyetQDDT = table.Column<double>(nullable: true),
                    fTienThamDinh = table.Column<double>(nullable: true),
                    fTienToTrinh = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuToanID = table.Column<Guid>(nullable: false),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuToan_Nguonvon", x => x.iID_DuToan_NguonVonID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_GoiThau",
                columns: table => new
                {
                    iID_GoiThauID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true),
                    bIsGoc = table.Column<bool>(nullable: true),
                    bIsKhoiTao = table.Column<bool>(nullable: true),
                    BKhoa = table.Column<bool>(nullable: true),
                    dBatDauChonNhaThau = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dKetThucChonNhaThau = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayLap = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTri = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienTrungThau = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_GoiThauGocID = table.Column<Guid>(nullable: true),
                    iId_KHLCNhaThau = table.Column<Guid>(nullable: true),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    LoaiGoiThau = table.Column<string>(maxLength: 200, nullable: true),
                    NgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    sHinhThucChonNhaThau = table.Column<string>(maxLength: 300, nullable: true),
                    sHinhThucHopDong = table.Column<string>(maxLength: 300, nullable: true),
                    sMaGoiThau = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sPhuongThucDauThau = table.Column<string>(maxLength: 300, nullable: true),
                    sTenGoiThau = table.Column<string>(maxLength: 300, nullable: true),
                    sThoiGianThucHien = table.Column<string>(nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true),
                    SoQuyetDinh = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DA_GoiThau", x => x.iID_GoiThauID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_GoiThau_ChiPhi",
                columns: table => new
                {
                    iID_GoiThau_ChiPhiID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienGoiThau = table.Column<double>(nullable: true),
                    iID_DuToanID = table.Column<Guid>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_GoiThauID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thau.GoiThau_ChiPhi_1", x => x.iID_GoiThau_ChiPhiID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_GoiThau_HangMuc",
                columns: table => new
                {
                    iID_GoiThau_HangMucID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienGoiThau = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_DuToanID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_GoiThauID = table.Column<Guid>(nullable: true),
                    iID_HangMucID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoiThau_HangMuc", x => x.iID_GoiThau_HangMucID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_GoiThau_NguonVon",
                columns: table => new
                {
                    iID_GoiThau_NguonVonID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienGoiThau = table.Column<double>(nullable: true),
                    iID_DuToanID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_GoiThauID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thau.GoiThau_NguonVon", x => x.iID_GoiThau_NguonVonID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_HopDong_DM_HangMuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_ChiPhiID = table.Column<Guid>(nullable: false),
                    iID_HopDongGoiThauNhaThauID = table.Column<Guid>(nullable: false),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienHangMuc = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    maOrder = table.Column<string>(maxLength: 255, nullable: true),
                    sMaHangMuc = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenHangMuc = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DA_HopDong_DM_HangMuc", x => new { x.Id, x.iID_ChiPhiID, x.iID_HopDongGoiThauNhaThauID });
                    table.UniqueConstraint("AK_VDT_DA_HopDong_DM_HangMuc_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_HopDong_GoiThau_ChiPhi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTri = table.Column<double>(nullable: true),
                    fGiaTriTruocDC = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_HopDongGoiThauNhaThauID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DA_HopDong_GoiThau_ChiPhi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_HopDong_GoiThau_HangMuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDateCreate = table.Column<DateTime>(nullable: true),
                    dDateUpdate = table.Column<DateTime>(nullable: true),
                    fGiaTri = table.Column<double>(nullable: true),
                    fGiaTriDuocDuyet = table.Column<double>(nullable: true),
                    fGiaTriTrungThau = table.Column<double>(nullable: true),
                    fGiaTriTruocDC = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_HangMucID = table.Column<Guid>(nullable: true),
                    iID_HopDongGoiThauNhaThauID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DA_HopDong_GoiThau_HangMuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_HopDong_GoiThau_NhaThau",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDateCreate = table.Column<DateTime>(nullable: true),
                    dDateUpdate = table.Column<DateTime>(nullable: true),
                    fGiaTri = table.Column<double>(nullable: true),
                    fGiaTriHopDong = table.Column<double>(nullable: false),
                    fGiaTriHopDongTruocDC = table.Column<double>(nullable: true),
                    fGiaTriTrungThau = table.Column<double>(nullable: true),
                    fGiaTriTrungThauTruocDC = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_GoiThauID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DA_HopDong_GoiThau_NhaThau", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_NguonVon",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fThanhTien = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAn = table.Column<Guid>(nullable: false),
                    iID_HangMucID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DA_NguonVon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_QDDauTu",
                columns: table => new
                {
                    iID_QDDauTuID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true),
                    bIsGoc = table.Column<bool>(nullable: true),
                    bIsKhoiTao = table.Column<bool>(nullable: true),
                    BKhoa = table.Column<bool>(nullable: true),
                    bLaThayThe = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayThamDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayToTrinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriDieuChinh = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTongMucDauTuPheDuyet = table.Column<double>(nullable: true),
                    fTongMucDauTuThamDinh = table.Column<double>(nullable: true),
                    fTongMucDauTuToTrinh = table.Column<double>(nullable: true),
                    iID_ChuTruongDauTuID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_HinhThucQuanLyID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iLoaiQuyetDinh = table.Column<int>(nullable: true),
                    sCoQuanPheDuyet = table.Column<string>(maxLength: 300, nullable: true),
                    sCoQuanThamDinh = table.Column<string>(maxLength: 300, nullable: true),
                    sDiaDiem = table.Column<string>(maxLength: 300, nullable: true),
                    sKetThuc = table.Column<string>(maxLength: 50, nullable: true),
                    sKhoiCong = table.Column<string>(maxLength: 50, nullable: true),
                    SMoTa = table.Column<string>(nullable: true),
                    sNguoiKy = table.Column<string>(maxLength: 300, nullable: true),
                    sSoBuocThietKe = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true),
                    sSoThamDinh = table.Column<string>(maxLength: 300, nullable: true),
                    sSoToTrinh = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QDDauTu", x => x.iID_QDDauTuID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_QDDauTu_ChiPhi",
                columns: table => new
                {
                    iID_QDDauTu_ChiPhiID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriDieuChinh = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    fTienThamDinh = table.Column<double>(nullable: true),
                    fTienToTrinh = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAn_ChiPhi = table.Column<Guid>(nullable: true),
                    iID_QDDauTuID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QDDauTu_ChiPhi", x => x.iID_QDDauTu_ChiPhiID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_QDDauTu_DM_HangMuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsEdit = table.Column<bool>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienHangMuc = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    IdLoaiCongTrinh = table.Column<Guid>(nullable: true),
                    maOrder = table.Column<string>(maxLength: 255, nullable: true),
                    sMaHangMuc = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sQuyMo = table.Column<string>(maxLength: 255, nullable: true),
                    sTenHangMuc = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DA_QDDauTu_DM_HangMuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_QDDauTu_HangMuc",
                columns: table => new
                {
                    iID_QDDauTu_HangMuciID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriDieuChinh = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAn_ChiPhi = table.Column<Guid>(nullable: true),
                    iID_HangMucID = table.Column<Guid>(nullable: false),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_NguonVon = table.Column<int>(nullable: true),
                    iID_QDDauTuID = table.Column<Guid>(nullable: false),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QDDauTu_HangMuc", x => x.iID_QDDauTu_HangMuciID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_QDDauTu_NguonVon",
                columns: table => new
                {
                    iID_QDDauTu_NguonVonID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    DuAnId = table.Column<Guid>(nullable: true),
                    fGiaTriDieuChinh = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    fTienPheDuyetCTDT = table.Column<double>(nullable: true),
                    fTienThamDinh = table.Column<double>(nullable: true),
                    fTienToTrinh = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_QDDauTuID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QDDauTu_Nguonvon", x => x.iID_QDDauTu_NguonVonID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DA_TT_HopDong",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true),
                    bIsGoc = table.Column<bool>(nullable: true),
                    BKhoa = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dKetThucDuKien = table.Column<DateTime>(type: "datetime", nullable: true),
                    dKhoiCongDuKien = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayHopDong = table.Column<DateTime>(type: "datetime", nullable: false),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienHopDong = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_GoiThauID = table.Column<Guid>(nullable: true),
                    iID_HopDongGocID = table.Column<Guid>(nullable: true),
                    iID_LoaiHopDongID = table.Column<Guid>(nullable: true),
                    iID_NhaThauThucHienID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iLandieuchinh = table.Column<int>(nullable: true),
                    iThoiGianThucHien = table.Column<int>(nullable: false),
                    iTinhTrangHopDong = table.Column<int>(nullable: false),
                    NoiDungHopDong = table.Column<string>(maxLength: 500, nullable: true),
                    sHinhThucHopDong = table.Column<string>(maxLength: 500, nullable: true),
                    sNganHang = table.Column<string>(maxLength: 100, nullable: true),
                    sSoHopDong = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    sSoTaiKhoan = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    STenHopDong = table.Column<string>(nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DA_TT_HopDong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_ChiPhi",
                columns: table => new
                {
                    iID_ChiPhi = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_ChiPhi_Parent = table.Column<Guid>(nullable: true),
                    iSoLanSua = table.Column<int>(nullable: true),
                    iThuTu = table.Column<int>(nullable: false),
                    sID_MaNguoiDungSua = table.Column<string>(maxLength: 200, nullable: true),
                    sID_MaNguoiDungTao = table.Column<string>(maxLength: 200, nullable: true),
                    sIPSua = table.Column<string>(maxLength: 20, nullable: true),
                    sMaChiPhi = table.Column<string>(maxLength: 50, nullable: false),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true),
                    sTenChiPhi = table.Column<string>(maxLength: 300, nullable: false),
                    sTenVietTat = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DM_ChiPhi", x => x.iID_ChiPhi);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_DonViThucHienDuAn",
                columns: table => new
                {
                    iID_DonVi = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BHangCha = table.Column<bool>(nullable: false),
                    iCapDonVi = table.Column<int>(nullable: true),
                    iID_MaDonViNS = table.Column<string>(nullable: true),
                    iID_DonViCha = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 20, nullable: true),
                    sDiaChi = table.Column<string>(nullable: true),
                    sKyHieu = table.Column<string>(nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_DonViThucHienDuAn", x => x.iID_DonVi);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_DuAn_ChiPhi",
                columns: table => new
                {
                    iID_DuAn_ChiPhi = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_ChiPhi = table.Column<Guid>(nullable: true),
                    iID_ChiPhi_Parent = table.Column<Guid>(nullable: true),
                    iSoLanSua = table.Column<int>(nullable: true),
                    iThuTu = table.Column<int>(nullable: false),
                    sID_MaNguoiDungSua = table.Column<string>(maxLength: 200, nullable: true),
                    sID_MaNguoiDungTao = table.Column<string>(maxLength: 200, nullable: true),
                    sIPSua = table.Column<string>(maxLength: 20, nullable: true),
                    sMaChiPhi = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true),
                    sTenChiPhi = table.Column<string>(maxLength: 300, nullable: true),
                    sTenVietTat = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DM_DuAn_ChiPhi", x => x.iID_DuAn_ChiPhi);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_HinhThucQuanLy",
                columns: table => new
                {
                    iID_HinhThucQuanLyID = table.Column<Guid>(nullable: false),
                    iThuTu = table.Column<int>(nullable: true),
                    sMaHinhThucQuanLy = table.Column<string>(maxLength: 50, nullable: false),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenHinhThucQuanLy = table.Column<string>(maxLength: 300, nullable: false),
                    sTenVietTat = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhThucQuanLy", x => x.iID_HinhThucQuanLyID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_KieuThongTri",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    iID_LoaiThongTriID = table.Column<Guid>(nullable: false),
                    sMaKieuThongTri = table.Column<string>(maxLength: 50, nullable: true),
                    sTenKieuThongTri = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DM_KieuThongTri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_LoaiCongTrinh",
                columns: table => new
                {
                    iID_LoaiCongTrinh = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_Parent = table.Column<Guid>(nullable: true),
                    iSoLanSua = table.Column<int>(nullable: true),
                    iThuTu = table.Column<int>(nullable: true),
                    K = table.Column<string>(maxLength: 200, nullable: true),
                    L = table.Column<string>(maxLength: 200, nullable: true),
                    LNS = table.Column<string>(maxLength: 200, nullable: true),
                    M = table.Column<string>(maxLength: 200, nullable: true),
                    NG = table.Column<string>(maxLength: 200, nullable: true),
                    sID_MaNguoiDungSua = table.Column<string>(maxLength: 200, nullable: true),
                    sID_MaNguoiDungTao = table.Column<string>(maxLength: 200, nullable: true),
                    sIPSua = table.Column<string>(maxLength: 20, nullable: true),
                    sMaLoaiCongTrinh = table.Column<string>(maxLength: 50, nullable: false),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenLoaiCongTrinh = table.Column<string>(maxLength: 300, nullable: false),
                    sTenVietTat = table.Column<string>(maxLength: 100, nullable: false),
                    TM = table.Column<string>(maxLength: 200, nullable: true),
                    TNG = table.Column<string>(maxLength: 200, nullable: true),
                    TNG1 = table.Column<string>(maxLength: 200, nullable: true),
                    TNG2 = table.Column<string>(maxLength: 200, nullable: true),
                    TNG3 = table.Column<string>(maxLength: 200, nullable: true),
                    TTM = table.Column<string>(maxLength: 200, nullable: true),
                    XAUNOIMA = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VDT_DM_L__1D2E4069D1992AB9", x => x.iID_LoaiCongTrinh);
                    //table.ForeignKey(
                    //    name: "FK_VDT_DM_LoaiCongTrinh_VDT_DM_LoaiCongTrinh_iID_Parent",
                    //    column: x => x.iID_Parent,
                    //    principalTable: "VDT_DM_LoaiCongTrinh",
                    //    principalColumn: "iID_LoaiCongTrinh",
                    //    onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_LoaiDuAn",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    iNamThucHien = table.Column<int>(nullable: false),
                    sMaLoaiDuAn = table.Column<string>(maxLength: 50, nullable: false),
                    sMoTa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DM_LoaiDuAn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_LoaiHopDong",
                columns: table => new
                {
                    iID_LoaiHopDongID = table.Column<Guid>(nullable: false),
                    iThuTu = table.Column<int>(nullable: true),
                    sMaLoaiHopDong = table.Column<string>(maxLength: 50, nullable: false),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenLoaiHopDong = table.Column<string>(maxLength: 300, nullable: false),
                    sTenVietTat = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHopDong", x => x.iID_LoaiHopDongID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_LoaiThongTri",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    iKieuLoaiThongTri = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    sTenLoaiThongTri = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DM_LoaiThongTri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_NhaThau",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    sChucVu = table.Column<string>(maxLength: 100, nullable: true),
                    sDaiDien = table.Column<string>(maxLength: 100, nullable: true),
                    sDiaChi = table.Column<string>(maxLength: 300, nullable: true),
                    sDienThoai = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sDienThoaiLienHe = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sEmail = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sFax = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sMaNganHang = table.Column<string>(maxLength: 100, nullable: true),
                    sMaNhaThau = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sMaSoThue = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sNganHang = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiLienHe = table.Column<string>(maxLength: 300, nullable: true),
                    sSoTaiKhoan = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    sTenNhaThau = table.Column<string>(maxLength: 300, nullable: true),
                    sWebsite = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DM_NhaThau", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_NhomDuAn",
                columns: table => new
                {
                    iID_NhomDuAnID = table.Column<Guid>(nullable: false),
                    iNamThucHien = table.Column<int>(nullable: false),
                    iThuTu = table.Column<int>(nullable: true),
                    mTien = table.Column<decimal>(type: "money", nullable: true),
                    sMaNhomDuAn = table.Column<string>(maxLength: 50, nullable: false),
                    sMoTa = table.Column<string>(nullable: true),
                    sTenNhomDuAn = table.Column<string>(maxLength: 300, nullable: false),
                    sTenVietTat = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomDuAn", x => x.iID_NhomDuAnID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_NhomQuanLy",
                columns: table => new
                {
                    iID_NhomQuanLyID = table.Column<Guid>(nullable: false),
                    iThuTu = table.Column<int>(nullable: true),
                    sMaNhomQuanLy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    sTenNhomQuanLy = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM.NhomQuanLy", x => x.iID_NhomQuanLyID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_DM_PhanCapDuAn",
                columns: table => new
                {
                    iID_PhanCapID = table.Column<Guid>(nullable: false),
                    bActive = table.Column<bool>(nullable: true),
                    iThuTu = table.Column<int>(nullable: true),
                    sMa = table.Column<string>(maxLength: 100, nullable: true),
                    sMoTa = table.Column<string>(type: "ntext", nullable: true),
                    sTen = table.Column<string>(maxLength: 300, nullable: true),
                    sTenVietTat = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VDT_DM_P__0466A1C8AF5F02A7", x => x.iID_PhanCapID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_KeHoach5Nam",
                columns: table => new
                {
                    iID_KeHoach5NamID = table.Column<Guid>(nullable: false),
                    bActive = table.Column<bool>(nullable: false),
                    bIsGoc = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriDuocDuyet = table.Column<double>(nullable: true),
                    iGiaiDoanDen = table.Column<int>(nullable: false),
                    iGiaiDoanTu = table.Column<int>(nullable: false),
                    iID_KhthDeXuat = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: false),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_ParentId = table.Column<Guid>(nullable: true),
                    ILoai = table.Column<int>(nullable: true),
                    MoTaChiTiet = table.Column<string>(nullable: true),
                    NamLamViec = table.Column<int>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true),
                    sTrangThai = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PheDuyet5Nam", x => x.iID_KeHoach5NamID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_KeHoach5Nam_ChiTiet",
                columns: table => new
                {
                    iID_KeHoach5Nam_ChiTietID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true),
                    fGiaTriBoTri = table.Column<double>(nullable: true),
                    fGiaTriBoTriDc = table.Column<double>(nullable: true),
                    FGiaTriDeXuat = table.Column<double>(nullable: true),
                    fGiaTriKeHoach = table.Column<double>(nullable: true),
                    fHanMucDauTu = table.Column<double>(nullable: true),
                    fVonBoTriTuNamDenNam = table.Column<double>(nullable: true),
                    fVonBoTriTuNamDenNamDc = table.Column<double>(nullable: true),
                    fVonDaGiao = table.Column<double>(nullable: true),
                    fVonDaGiaoDc = table.Column<double>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: false),
                    iID_KeHoach5NamID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sTen = table.Column<string>(maxLength: 255, nullable: true),
                    sTrangThai = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PheDuyet5Nam_ChiTiet", x => x.iID_KeHoach5Nam_ChiTietID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_KeHoach5Nam_DeXuat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bActive = table.Column<bool>(nullable: false),
                    bIsGoc = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriKeHoach = table.Column<double>(nullable: true),
                    iGiaiDoanDen = table.Column<int>(nullable: false),
                    iGiaiDoanTu = table.Column<int>(nullable: false),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: false),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_ParentId = table.Column<Guid>(nullable: true),
                    iID_TongHopParent = table.Column<Guid>(nullable: true),
                    ILoai = table.Column<int>(nullable: true),
                    MoTaChiTiet = table.Column<string>(nullable: true),
                    NamLamViec = table.Column<int>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true),
                    sTongHop = table.Column<string>(nullable: true),
                    sTrangThai = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_KeHoach5Nam_DeXuat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true),
                    fGiaTriBoTri = table.Column<double>(nullable: true),
                    fGiaTriBoTriDC = table.Column<double>(nullable: true),
                    fGiaTriKeHoach = table.Column<double>(nullable: false),
                    fGiaTriNamThuBa = table.Column<double>(nullable: true),
                    fGiaTriNamThuBaDC = table.Column<double>(nullable: true),
                    fGiaTriNamThuHai = table.Column<double>(nullable: true),
                    fGiaTriNamThuHaiDC = table.Column<double>(nullable: true),
                    fGiaTriNamThuNam = table.Column<double>(nullable: true),
                    fGiaTriNamThuNamDC = table.Column<double>(nullable: true),
                    fGiaTriNamThuNhat = table.Column<double>(nullable: true),
                    fGiaTriNamThuNhatDC = table.Column<double>(nullable: true),
                    fGiaTriNamThuTu = table.Column<double>(nullable: true),
                    fGiaTriNamThuTuDC = table.Column<double>(nullable: true),
                    fHanMucDauTu = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: false),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fVonBoTriTuNamDenNam = table.Column<double>(nullable: true),
                    fVonDaGiao = table.Column<double>(nullable: true),
                    IGiaiDoanDen = table.Column<int>(nullable: true),
                    IGiaiDoanTu = table.Column<int>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_KeHoach5NamID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TongHop = table.Column<Guid>(nullable: true),
                    IdParent = table.Column<Guid>(nullable: true),
                    iID_ParentModified = table.Column<Guid>(nullable: true),
                    IdReference = table.Column<Guid>(nullable: true),
                    indexCode = table.Column<int>(nullable: true),
                    IsParent = table.Column<bool>(nullable: false),
                    IsStatus = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: true),
                    SDiaDiem = table.Column<string>(maxLength: 255, nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    SMaOrder = table.Column<string>(maxLength: 200, nullable: true),
                    STT = table.Column<string>(maxLength: 20, nullable: true),
                    sTen = table.Column<string>(maxLength: 255, nullable: true),
                    sTrangThai = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_KeHoachVonUng",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriUng = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_KeHoachUngDeXuatID = table.Column<Guid>(nullable: true),
                    iID_LoaiNguonVonID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_NhomQuanLyID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sKhoanNganSach = table.Column<string>(maxLength: 50, nullable: true),
                    sLoaiNganSach = table.Column<string>(maxLength: 50, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_KeHoachVonUng", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_KeHoachVonUng_DX",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bKhoa = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayDeNghi = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriUng = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_NhomQuanLyID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sSoDeNghi = table.Column<string>(maxLength: 100, nullable: true),
                    sTongHop = table.Column<string>(nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_KeHoachVonUng_DX", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_KeHoachVonUng_DX_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriDeNghi = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_KeHoachUngID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    sGhiChu = table.Column<string>(type: "ntext", nullable: true),
                    sTrangThaiDuAnDangKy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_KeHoachVonUng_DX_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_PhanBoVon",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bActive = table.Column<bool>(nullable: true),
                    bIsCanBoDuyet = table.Column<bool>(nullable: true),
                    bIsDuyet = table.Column<bool>(nullable: true),
                    bIsGoc = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    bLaThayThe = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    fCapPhatBangLenhChi = table.Column<double>(nullable: true),
                    fCapPhatTaiKhoBac = table.Column<double>(nullable: true),
                    fGiaTrDeNghi = table.Column<double>(nullable: true),
                    fGiaTrPhanBo = table.Column<double>(nullable: true),
                    fGiaTriThuHoi = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamTruocKhoBac = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamTruocLenhChi = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_KhoanNganSachID = table.Column<Guid>(nullable: true),
                    iID_LoaiNganSachID = table.Column<Guid>(nullable: true),
                    iID_LoaiNguonVonID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_NhomQuanLyID = table.Column<Guid>(nullable: true),
                    iID_ParentId = table.Column<Guid>(nullable: true),
                    iID_PhanBoGocID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TongHopSoLieuID = table.Column<Guid>(nullable: true),
                    iLoai = table.Column<int>(nullable: true),
                    iLoaiDuToan = table.Column<int>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sLoaiDieuChinh = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_PhanBoVon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_PhanBoVon_DonVi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    bIsCanBoDuyet = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    bIsDuyet = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    bIsGoc = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    bKhoa = table.Column<bool>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "date", nullable: false),
                    fThanhToan = table.Column<double>(nullable: true),
                    fThuHoiVonUngTruoc = table.Column<double>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: false),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: false),
                    iID_NguonVonID = table.Column<int>(nullable: false),
                    iID_ParentId = table.Column<Guid>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: false),
                    sNguoiLap = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: false),
                    sTongHop = table.Column<string>(nullable: true),
                    sTruongPhong = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: false),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_PhanBoVon_DonVi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_PhanBoVon_DonVi_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true),
                    fKeHoachVonDuocDuyetNamNay = table.Column<double>(nullable: true),
                    fLuyKeVonNamTruoc = table.Column<double>(nullable: true),
                    fThanhToan = table.Column<double>(nullable: true),
                    fThanhToanDC = table.Column<double>(nullable: true),
                    fThuHoiVonUngTruoc = table.Column<double>(nullable: true),
                    fThuHoiVonUngTruocDC = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTongMucDauTuDuocDuyet = table.Column<double>(nullable: true),
                    fUocThucHien = table.Column<double>(nullable: true),
                    fUocThucHienDC = table.Column<double>(nullable: true),
                    fVonKeoDaiCacNamTruoc = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: false),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_ParentId = table.Column<Guid>(nullable: true),
                    iId_PhanBoVon_DonVi = table.Column<Guid>(nullable: false),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    ILoaiDuAn = table.Column<int>(nullable: true),
                    sMaDuAn = table.Column<string>(maxLength: 100, nullable: false),
                    sTrangThaiDuAnDangKy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_PhanBoVon_DonVi_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_PhanBoVon_DonVi_PheDuyet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bActive = table.Column<bool>(nullable: true),
                    bIsGoc = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: true),
                    bLaThayThe = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTrPhanBo = table.Column<double>(nullable: true),
                    fGiaTriThuHoi = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_LoaiNguonVonID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_ParentId = table.Column<Guid>(nullable: true),
                    iID_PhanBoGocID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TongHopSoLieuID = table.Column<Guid>(nullable: true),
                    iID_VonNamDeXuatID = table.Column<Guid>(nullable: true),
                    iLoai = table.Column<int>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sLoaiDieuChinh = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_PhanBoVon_DonVi_PheDuyet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KT_KhoiTao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsDuAnCu = table.Column<bool>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriConPhaiUng = table.Column<double>(nullable: true),
                    fKHVonUng = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fVonUngDaCap = table.Column<double>(nullable: true),
                    fVonUngDaThuHoi = table.Column<double>(nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_DuToanID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_QDDauTuID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iNamKhoiTao = table.Column<int>(nullable: false),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KT_KhoiTao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KT_KhoiTao_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fKHVonHetNamTruoc = table.Column<double>(nullable: true),
                    fLuyKeThanhToanKLHT = table.Column<double>(nullable: true),
                    fLuyKeThanhToanTamUng = table.Column<double>(nullable: true),
                    fSoChuyenChiTieuChuaCap = table.Column<double>(nullable: true),
                    fSoChuyenChiTieuDaCap = table.Column<double>(nullable: true),
                    fTamUngQuaKB = table.Column<double>(nullable: true),
                    fThanhToanQuaKB = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_KhoiTaoID = table.Column<Guid>(nullable: false),
                    iID_LoaiNguonVonID = table.Column<Guid>(nullable: true),
                    iID_MucID = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NganhID = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: false),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TietMucID = table.Column<string>(maxLength: 50, nullable: true),
                    iID_TieuMucID = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KT_KhoiTao_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KT_KhoiTao_DuLieu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayKhoiTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iNamKhoiTao = table.Column<int>(nullable: false),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KT_KhoiTao_DuLieu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KT_KhoiTao_DuLieu_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fKHUT_KeHoachUngTruocChuaThuHoi = table.Column<double>(nullable: true),
                    fKHUT_KeHoachUngTruocKeoDaiSangNam = table.Column<double>(nullable: true),
                    fKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc = table.Column<double>(nullable: true),
                    fKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi = table.Column<double>(nullable: true),
                    fKHUT_VonBoTriHetNamTruoc = table.Column<double>(nullable: true),
                    fKHVN_KeHoachVonKeoDaiSangNam = table.Column<double>(nullable: true),
                    fKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc = table.Column<double>(nullable: true),
                    fKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc = table.Column<double>(nullable: true),
                    fKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi = table.Column<double>(nullable: true),
                    fKHVN_VonBoTriHetNamTruoc = table.Column<double>(nullable: true),
                    iCoQuanThanhToan = table.Column<int>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_KhoiTaoDuLieuID = table.Column<Guid>(nullable: false),
                    sMaDuAn = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KT_KhoiTao_DuLieu_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fLuyKeTUChuaThuHoiNN_KHVN = table.Column<double>(nullable: true),
                    fLuyKeTUChuaThuHoiNN_KHVU = table.Column<double>(nullable: true),
                    fLuyKeTUChuaThuHoiTN_KHVN = table.Column<double>(nullable: true),
                    fLuyKeTUChuaThuHoiTN_KHVU = table.Column<double>(nullable: true),
                    fLuyKeTTKLHTNN_KHVN = table.Column<double>(nullable: true),
                    fLuyKeTTKLHTNN_KHVU = table.Column<double>(nullable: true),
                    fLuyKeTTKLHTTN_KHVN = table.Column<double>(nullable: true),
                    fLuyKeTTKLHTTN_KHVU = table.Column<double>(nullable: true),
                    iID_HopDongId = table.Column<Guid>(nullable: true),
                    iId_KhoiTaoDuLieuChiTietId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_NC_NhuCauChi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayDeNghi = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    iQuy = table.Column<int>(nullable: true),
                    sNguoiLap = table.Column<string>(maxLength: 255, nullable: true),
                    sNoiDung = table.Column<string>(nullable: true),
                    sSoDeNghi = table.Column<string>(maxLength: 50, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_NC_NhuCauChi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_NC_NhuCauChi_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriDeNghi = table.Column<double>(nullable: true),
                    iID_DuAnId = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinhId = table.Column<Guid>(nullable: true),
                    iID_NhuCauChiId = table.Column<Guid>(nullable: true),
                    sGhiChu = table.Column<string>(type: "ntext", nullable: true),
                    sLoaiThanhToan = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_NC_NhuCauChi_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QDDT_KHLCNhaThau",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bActive = table.Column<bool>(nullable: true),
                    bIsGoc = table.Column<bool>(nullable: true),
                    BKhoa = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "date", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "date", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "date", nullable: true),
                    iID_ChuTruongDauTuID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_DuToanID = table.Column<Guid>(nullable: true),
                    iID_LCNhaThauGocID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QDDauTuID = table.Column<Guid>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_QDDT_KHLCNhaThau", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_BCQuyetToanNienDo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsCanBoDuyet = table.Column<bool>(nullable: true),
                    bIsDuyet = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayDeNghi = table.Column<DateTime>(type: "datetime", nullable: true),
                    iCoQuanThanhToan = table.Column<int>(nullable: false),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iLoaiThanhToan = table.Column<int>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sSoDeNghi = table.Column<string>(maxLength: 100, nullable: true),
                    sTongHop = table.Column<string>(nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_QT_BCQuyetToanNienDo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VdtQtBcquyetToanNienDoChiTiets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BIsCanBoDuyet = table.Column<bool>(nullable: true),
                    BIsDuyet = table.Column<bool>(nullable: true),
                    DDateCreate = table.Column<DateTime>(nullable: true),
                    DDateDelete = table.Column<DateTime>(nullable: true),
                    DDateUpdate = table.Column<DateTime>(nullable: true),
                    FGiaTriNamNayChuyenNamSau = table.Column<double>(nullable: true),
                    FGiaTriNamTruocChuyenNamSau = table.Column<double>(nullable: true),
                    IIdBcquyetToanNienDoId = table.Column<Guid>(nullable: true),
                    IIdDuAnId = table.Column<Guid>(nullable: true),
                    SUserCreate = table.Column<string>(nullable: true),
                    SUserDelete = table.Column<string>(nullable: true),
                    SUserUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VdtQtBcquyetToanNienDoChiTiets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_BCQuyetToanNienDo_ChiTiet_01",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    fChiTieuNamNay = table.Column<double>(nullable: true),
                    fChiTieuNamTrcChuyenSang = table.Column<double>(nullable: true),
                    fGiaTriNamNayChuyenNamSau = table.Column<double>(nullable: true),
                    fGiaTriNamTruocChuyenNamSau = table.Column<double>(nullable: true),
                    fGiaTriTamUngDieuChinhGiam = table.Column<double>(nullable: true),
                    fGiaTriThuHoiTheoGiaiNganThucTe = table.Column<double>(nullable: true),
                    fGiaTriUngChuyenNamSau = table.Column<double>(nullable: true),
                    fKHUngNamNay = table.Column<double>(nullable: true),
                    fKHUngTrcChuaThuHoiTrcNamQuyetToan = table.Column<double>(nullable: true),
                    fLKThanhToanDenTrcNamQuyetToan = table.Column<double>(nullable: true),
                    fLKThanhToanDenTrcNamQuyetToan_KHUng = table.Column<double>(nullable: true),
                    fTamUngChuaThuHoi_CTNamNay = table.Column<double>(nullable: true),
                    fTamUngChuaThuHoi_CTNamTrcChuyenSang = table.Column<double>(nullable: true),
                    fTamUngChuaThuHoiTrcNamQuyetToan = table.Column<double>(nullable: true),
                    fThanhToan_KHUngNamNay = table.Column<double>(nullable: true),
                    fThanhToan_KHUngNamTrcChuyenSang = table.Column<double>(nullable: true),
                    fThanhToanKLHT_CTNamNay = table.Column<double>(nullable: true),
                    fThanhToanKLHT_CTNamTrcChuyenSang = table.Column<double>(nullable: true),
                    fThuHoiUngNamTrc = table.Column<double>(nullable: true),
                    fThuHoiUngTruoc = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: false),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iCoQuanThanhToan = table.Column<int>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_BCQuyetToanNienDo = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: false),
                    iID_TienTeID = table.Column<Guid>(nullable: false),
                    sTrangThaiDuAnDangKy = table.Column<string>(maxLength: 50, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_QT_BCQuyetToanNienDo_ChiTiet_01", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_BCQuyetToanNienDo_PhanTich",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    fChiTieuNamNayKB = table.Column<double>(nullable: true),
                    fChiTieuNamNayLC = table.Column<double>(nullable: true),
                    fDnQuyetToanNamNay = table.Column<double>(nullable: true),
                    fDnQuyetToanNamTrc = table.Column<double>(nullable: true),
                    fDuToanCNSChuaGiaiNganTaiCuc = table.Column<double>(nullable: true),
                    fDuToanCNSChuaGiaiNganTaiDV = table.Column<double>(nullable: true),
                    fDuToanCNSChuaGiaiNganTaiKB = table.Column<double>(nullable: true),
                    fDuToanThuHoi = table.Column<double>(nullable: true),
                    fSoCapNamNay = table.Column<double>(nullable: true),
                    fSoCapNamTrcCS = table.Column<double>(nullable: true),
                    fTuChuaThuHoiTaiCuc = table.Column<double>(nullable: true),
                    fTuChuaThuHoiTaiDonVi = table.Column<double>(nullable: true),
                    iID_BCQuyetToanNienDo = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_QT_BCQuyetToanNienDo_PhanTich", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_DeNghiQuyetToan",
                columns: table => new
                {
                    iID_DeNghiQuyetToanID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bKhoa = table.Column<bool>(nullable: false),
                    bTongHop = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dThoiGianHoanThanh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dThoiGianKhoiCong = table.Column<DateTime>(type: "datetime", nullable: true),
                    dThoiGianLapBaoCao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dThoiGianNhanBaoCao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fChiPhiKhongTaoNenTaiSan = table.Column<double>(nullable: true),
                    fChiPhiThietHai = table.Column<double>(nullable: true),
                    fGiaTriDeNghiQuyetToan = table.Column<double>(nullable: true),
                    fTaiSanDaiHanDonViKhacQuanLy = table.Column<double>(nullable: true),
                    fTaiSanDaiHanThuocCDTQuanLy = table.Column<double>(nullable: true),
                    fTaiSanNganHanDonViKhacQuanLy = table.Column<double>(nullable: true),
                    fTaiSanNganHanThuocCDTQuanLy = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_Parent = table.Column<Guid>(nullable: true),
                    sMoTa = table.Column<string>(type: "ntext", nullable: true),
                    sNguoiLap = table.Column<string>(maxLength: 255, nullable: true),
                    sNguoiNhan = table.Column<string>(maxLength: 255, nullable: true),
                    sSoBaoCao = table.Column<string>(maxLength: 50, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QT.DeNghiQuyetToan", x => x.iID_DeNghiQuyetToanID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_DeNghiQuyetToan_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriDeNghiQuyetToan = table.Column<double>(nullable: true),
                    fGiaTriKiemToan = table.Column<double>(nullable: true),
                    fGiaTriQuyetToanAB = table.Column<double>(nullable: true),
                    iID_ChiPhiId = table.Column<Guid>(nullable: true),
                    iID_DeNghiQuyetToanID = table.Column<Guid>(nullable: false),
                    iID_HangMucId = table.Column<Guid>(nullable: true),
                    sMaOrder = table.Column<string>(maxLength: 200, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_QT_DeNghiQuyetToan_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_DeNghiQuyetToan_Nguonvon",
                columns: table => new
                {
                    iID_DeNghiQuyetToan_NguonVonID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienToTrinh = table.Column<double>(nullable: true),
                    iID_DeNghiQuyetToanID = table.Column<Guid>(nullable: false),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeNghiQuyetToan_Nguonvon", x => x.iID_DeNghiQuyetToan_NguonVonID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_DeNghiQuyetToanNienDo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayDeNghi = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_MaDonViDeNghi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_DonViDeNghiID = table.Column<Guid>(nullable: true),
                    iID_LoaiNguonVonID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sNguoiDeNghi = table.Column<string>(maxLength: 255, nullable: true),
                    sSoDeNghi = table.Column<string>(maxLength: 50, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_QT_DeNghiQuyetToanNienDo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_DeNghiQuyetToanNienDo_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fGiaTriNamNayChuyenNamSau = table.Column<double>(nullable: true),
                    fGiaTriNamTruocChuyenNamSau = table.Column<double>(nullable: true),
                    fGiaTriQuyetToanNamNay = table.Column<double>(nullable: true),
                    fGiaTriQuyetToanNamNayDonVi = table.Column<double>(nullable: true),
                    fGiaTriQuyetToanNamTruoc = table.Column<double>(nullable: true),
                    fGiaTriQuyetToanNamTruocDonVi = table.Column<double>(nullable: true),
                    fGiaTriTamUngNamNayChuaThuHoi = table.Column<double>(nullable: true),
                    fGiaTriTamUngNamTruocChuaThuHoi = table.Column<double>(nullable: true),
                    iID_DeNghiQuyetToanNienDoID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_MucID = table.Column<Guid>(nullable: true),
                    iID_NganhID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TietMucID = table.Column<Guid>(nullable: true),
                    iID_TieuMucID = table.Column<Guid>(nullable: true),
                    K = table.Column<string>(maxLength: 50, nullable: true),
                    L = table.Column<string>(maxLength: 50, nullable: true),
                    LNS = table.Column<string>(maxLength: 50, nullable: true),
                    M = table.Column<string>(maxLength: 50, nullable: true),
                    mTiGia = table.Column<double>(nullable: true),
                    mTiGiaDonVi = table.Column<double>(nullable: true),
                    NG = table.Column<string>(maxLength: 50, nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_QT_DeNghiQuyetToanNienDo_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_QuyetToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bKhoa = table.Column<bool>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dKetThucThucTe = table.Column<DateTime>(type: "datetime", nullable: true),
                    dKhoiCongThucTe = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayThamDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayToTrinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    fChiPhiKhongTaoNenTaiSan = table.Column<double>(nullable: true),
                    fChiPhiThietHai = table.Column<double>(nullable: true),
                    fTaiSanDaiHanDonViKhacQuanLy = table.Column<double>(nullable: true),
                    fTaiSanDaiHanThuocCDTQuanLy = table.Column<double>(nullable: true),
                    fTaiSanNganHanDonViKhacQuanLy = table.Column<double>(nullable: true),
                    fTaiSanNganHanThuocCDTQuanLy = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienQuyetToanPheDuyet = table.Column<double>(nullable: true),
                    fTienQuyetToanThamDinh = table.Column<double>(nullable: true),
                    fTienQuyetToanToTrinh = table.Column<double>(nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: false),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    sCoQuanPheDuyet = table.Column<string>(maxLength: 100, nullable: true),
                    sCoQuanThamDinh = table.Column<string>(maxLength: 300, nullable: true),
                    sNguoiKy = table.Column<string>(maxLength: 100, nullable: true),
                    sNoiDung = table.Column<string>(type: "ntext", nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: false),
                    sSoThamDinh = table.Column<string>(maxLength: 100, nullable: true),
                    sSoToTrinh = table.Column<string>(maxLength: 100, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_QT_QuyetToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_QuyetToan_ChiPhi",
                columns: table => new
                {
                    iID_QuyetToan_ChiPhiID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    fTienThamDinh = table.Column<double>(nullable: true),
                    fTienToTrinh = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: false),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_QuyetToanID = table.Column<Guid>(nullable: false),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyetToan_ChiPhi", x => x.iID_QuyetToan_ChiPhiID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_QuyetToan_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriQuyetToan = table.Column<double>(nullable: true),
                    fGiaTriThamTra = table.Column<double>(nullable: true),
                    iID_ChiPhiId = table.Column<Guid>(nullable: true),
                    iID_HangMucId = table.Column<Guid>(nullable: true),
                    iID_QuyetToanID = table.Column<Guid>(nullable: false),
                    sMaOrder = table.Column<string>(maxLength: 200, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_QT_QuyetToan_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_QuyetToan_Nguonvon",
                columns: table => new
                {
                    iID_QuyetToan_NguonVonID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    fTienThamDinh = table.Column<double>(nullable: true),
                    fTienToTrinh = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: false),
                    iID_QuyetToanID = table.Column<Guid>(nullable: false),
                    iID_TienTeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyetToan_Nguonvon", x => x.iID_QuyetToan_NguonVonID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_QuyetToan_NguonVon_ChenhLech",
                columns: table => new
                {
                    iID_QuyetToan_NguonVonCL = table.Column<Guid>(nullable: false),
                    fTienChenhLech = table.Column<double>(nullable: true),
                    fTienPheDuyet = table.Column<double>(nullable: true),
                    fTienThanhToan = table.Column<double>(nullable: true),
                    iID_NguonVonID = table.Column<Guid>(nullable: true),
                    iID_QuyetToanID = table.Column<Guid>(nullable: true),
                    sTenNguonVonCL = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QT.QuyetToan_NguonVon_ChenhLech", x => x.iID_QuyetToan_NguonVonCL);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_TongHopSoLieu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bIsCanBoDuyet = table.Column<bool>(nullable: true),
                    bIsDuyet = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayLap = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NguonVonID = table.Column<Guid>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_QT_TongHopSoLieu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_QT_XuLySoLieu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayLap = table.Column<DateTime>(type: "datetime", nullable: true),
                    fBuTruThuaThieu = table.Column<double>(nullable: true),
                    fCapThanhKhoan = table.Column<double>(nullable: true),
                    fGiaTriChuyenNamSauChuaCap = table.Column<double>(nullable: true),
                    fGiaTriChuyenNamSauDaCap = table.Column<double>(nullable: true),
                    fGiaTriNamTruocChuyenNamSauChuaCap = table.Column<double>(nullable: true),
                    fGiaTriNamTruocChuyenNamSauDaCap = table.Column<double>(nullable: true),
                    fThuLaiKeHoachNamNay = table.Column<double>(nullable: true),
                    fThuLaiKeHoachNamTruoc = table.Column<double>(nullable: true),
                    fThuThanhKhoan = table.Column<double>(nullable: true),
                    fThuThanhKhoanNamTruoc = table.Column<double>(nullable: true),
                    fThuUng = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: false),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: false),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_LoaiNguonVonID = table.Column<Guid>(nullable: true),
                    iID_MucID = table.Column<Guid>(nullable: true),
                    iID_NganhID = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: false),
                    iID_TietMucID = table.Column<Guid>(nullable: true),
                    iID_TieuMucID = table.Column<Guid>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sTrangThaiDuAnDangKy = table.Column<string>(maxLength: 50, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_QT_XuLySoLieu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_ThongTri",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bIsCanBoDuyet = table.Column<bool>(nullable: true),
                    bIsDuyet = table.Column<bool>(nullable: true),
                    bThanhToan = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayThongTri = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true),
                    iID_LoaiThongTriID = table.Column<Guid>(nullable: true),
                    iID_NhomQuanLyID = table.Column<Guid>(nullable: true),
                    iLoaiThongTri = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true),
                    iNamThongTri = table.Column<int>(nullable: true),
                    sMaLoaiCongTrinh = table.Column<string>(maxLength: 300, nullable: true),
                    sMaNguonVon = table.Column<string>(maxLength: 50, nullable: true),
                    sMaThongTri = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiLap = table.Column<string>(maxLength: 50, nullable: true),
                    sThuTruongDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    sTruongPhong = table.Column<string>(maxLength: 50, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true),
                    iID_MaDonViID = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_ThongTri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_ThongTri_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fSoTien = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_CapPheDuyetID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_KieuThongTriID = table.Column<Guid>(nullable: false),
                    iID_LoaiCongTrinhID = table.Column<Guid>(nullable: true),
                    iID_LoaiNguonVonID = table.Column<Guid>(nullable: true),
                    iID_MucID = table.Column<Guid>(nullable: true),
                    iID_NganhID = table.Column<Guid>(nullable: true),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    iID_ThongTriID = table.Column<Guid>(nullable: false),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TietMucID = table.Column<Guid>(nullable: true),
                    iID_TieuMucID = table.Column<Guid>(nullable: true),
                    sDonViThuHuong = table.Column<string>(nullable: true),
                    sSoThongTri = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_ThongTri_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TongHop_NguonNSDauTu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsLog = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    bKeHoach = table.Column<bool>(nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTri = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_MucID = table.Column<Guid>(nullable: true),
                    iID_NganhID = table.Column<Guid>(nullable: true),
                    iID_TietMucID = table.Column<Guid>(nullable: true),
                    iID_TieuMucID = table.Column<Guid>(nullable: true),
                    iID_ChungTu = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iId_MaNguonCha = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iLoaiUng = table.Column<int>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    iStatus = table.Column<int>(nullable: false),
                    iThuHoiTUCheDo = table.Column<int>(nullable: true),
                    K = table.Column<string>(nullable: true),
                    L = table.Column<string>(nullable: true),
                    LNS = table.Column<string>(nullable: true),
                    M = table.Column<string>(nullable: true),
                    NG = table.Column<string>(nullable: true),
                    sMaDich = table.Column<string>(maxLength: 100, nullable: true),
                    sMaNguon = table.Column<string>(maxLength: 100, nullable: true),
                    sMaNguonCha = table.Column<string>(maxLength: 100, nullable: true),
                    sMaTienTrinh = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true),
                    TM = table.Column<string>(nullable: true),
                    TTM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TongHop_NguonNSDauTu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TT_DeNghiThanhToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bHoanTraUngTruoc = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    bThanhToanTheoHopDong = table.Column<bool>(nullable: true),
                    bTongHop = table.Column<bool>(nullable: true),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayBangKLHT = table.Column<DateTime>(nullable: true),
                    dNgayDeNghi = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayPheDuyet = table.Column<DateTime>(type: "datetime", nullable: true),
                    fChuyenTienBaoHanh = table.Column<double>(nullable: true),
                    fChuyenTienBaoHanhDuocDuyet = table.Column<double>(nullable: true),
                    fGiaTriThanhToanNN = table.Column<double>(nullable: true),
                    fGiaTriThanhToanTN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiTN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiUngTruocNN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiUngTruocTN = table.Column<double>(nullable: true),
                    fLuyKeGiaTriNghiemThuKLHT = table.Column<double>(nullable: true),
                    fThueGiaTriGiaTang = table.Column<double>(nullable: true),
                    fThueGiaTriGiaTangDuocDuyet = table.Column<double>(nullable: true),
                    iID_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DuAnId = table.Column<Guid>(nullable: true),
                    iID_HopDongId = table.Column<Guid>(nullable: true),
                    iID_LoaiNguonVonID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_NhaThauId = table.Column<Guid>(nullable: true),
                    iID_NhomQuanLyID = table.Column<Guid>(nullable: true),
                    iID_PhanBoVonID = table.Column<Guid>(nullable: true),
                    iID_ThongTriThanhToanID = table.Column<Guid>(nullable: true),
                    iLoaiThanhToan = table.Column<int>(nullable: false),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    iID_Parent = table.Column<Guid>(nullable: true),
                    sGhiChu = table.Column<string>(type: "ntext", nullable: true),
                    sGhiChuPheDuyet = table.Column<string>(type: "ntext", nullable: true),
                    sLyDoTuChoi = table.Column<string>(type: "ntext", nullable: true),
                    sMaNganHang = table.Column<string>(nullable: true),
                    sNguoiLap = table.Column<string>(maxLength: 255, nullable: true),
                    sSoBangKLHT = table.Column<string>(nullable: true),
                    sSoDeNghi = table.Column<string>(maxLength: 50, nullable: true),
                    sSoTaiKhoanNhaThau = table.Column<string>(nullable: true),
                    sTenDonViThuHuong = table.Column<string>(nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true),
                    iCoQuanThanhToan = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TT_DeNghiThanhToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TT_DeNghiThanhToan_KHV",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_DeNghiThanhToanID = table.Column<Guid>(nullable: false),
                    iID_KeHoachVonID = table.Column<Guid>(nullable: true),
                    iLoai = table.Column<int>(nullable: true),
                    iStt = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeNghiThanhToan_KHV", x => x.iID);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TT_DeNghiThanhToanUng",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayDeNghi = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriTamUng = table.Column<double>(nullable: true),
                    fGiaTriThanhToan = table.Column<double>(nullable: true),
                    fGiaTriThuHoi = table.Column<double>(nullable: true),
                    fGiaTriThuHoiUngNgoaiChiTieu = table.Column<double>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_KhoanNganSach = table.Column<Guid>(nullable: true),
                    iID_LoaiNganSach = table.Column<Guid>(nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_NhomQuanLyID = table.Column<Guid>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(type: "ntext", nullable: true),
                    sNguoiLap = table.Column<string>(maxLength: 255, nullable: true),
                    sSoDeNghi = table.Column<string>(maxLength: 50, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TT_DeNghiThanhToanUng", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TT_PheDuyetThanhToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DeNghiThanhToan = table.Column<Guid>(nullable: false),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: false),
                    iID_HopDongID = table.Column<Guid>(nullable: false),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_NhomQuanLyID = table.Column<Guid>(nullable: true),
                    iLoaiThanhToan = table.Column<int>(nullable: false),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(type: "ntext", nullable: true),
                    sNguoiLap = table.Column<string>(maxLength: 255, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 50, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TT_PheDuyetThanhToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TT_PheDuyetThanhToan_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fGiaTriThanhToanNN = table.Column<double>(nullable: true),
                    fGiaTriThanhToanTN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamNayNN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamNayTN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamTruocNN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamTruocTN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiUngTruocNamNayNN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiUngTruocNamNayTN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiUngTruocNamTruocNN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiUngTruocNamTruocTN = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DeNghiThanhToanID = table.Column<Guid>(nullable: true),
                    iID_KeHoachVonID = table.Column<Guid>(nullable: true),
                    iId_DeNghiTamUng = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_MucID = table.Column<Guid>(nullable: true),
                    iID_NganhID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TietMucID = table.Column<Guid>(nullable: true),
                    iID_TieuMucID = table.Column<Guid>(nullable: true),
                    iLoaiKeHoachVon = table.Column<int>(nullable: true),
                    M = table.Column<string>(maxLength: 50, nullable: true),
                    NG = table.Column<string>(maxLength: 50, nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TT_PheDuyetThanhToan_ChiTiet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TT_ThanhToanQuaKhoBac",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    dDateCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    dDateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayThanhToan = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriTamUng = table.Column<double>(nullable: true),
                    fGiaTriThanhToan = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViNhanThanhToanID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_KhoanNganSach = table.Column<Guid>(nullable: true),
                    iID_LoaiNganSach = table.Column<Guid>(nullable: true),
                    iID_LoaiNguonVonID = table.Column<Guid>(nullable: true),
                    iId_MaDonViNhanThanhToanID = table.Column<string>(maxLength: 50, nullable: true),
                    iId_MaDonViQuanLyID = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NguonVonID = table.Column<int>(nullable: true),
                    iID_NhomQuanLyID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iNamKeHoach = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(type: "ntext", nullable: true),
                    sNguoiLap = table.Column<string>(maxLength: 50, nullable: true),
                    sSoThanhToan = table.Column<string>(maxLength: 50, nullable: true),
                    sUserCreate = table.Column<string>(maxLength: 100, nullable: true),
                    sUserDelete = table.Column<string>(maxLength: 100, nullable: true),
                    sUserUpdate = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TT_ThanhToanQuaKhoBac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HT_Quyen",
                columns: table => new
                {
                    iID_MaQuyen = table.Column<string>(maxLength: 250, nullable: false),
                    iID_LoaiQuyen = table.Column<Guid>(nullable: false),
                    iID_Quyen = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    sTenQuyen = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_AUTHORITY", x => x.iID_MaQuyen);
                    table.ForeignKey(
                        name: "FK_HT_Quyen_HT_LoaiQuyen_iID_LoaiQuyen",
                        column: x => x.iID_LoaiQuyen,
                        principalTable: "HT_LoaiQuyen",
                        principalColumn: "iID_LoaiQuyen",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HT_Nhom_NguoiDung ",
                columns: table => new
                {
                    iID_Nhom = table.Column<Guid>(nullable: false),
                    iID_MaNguoiDung = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.iID_Nhom, x.iID_MaNguoiDung });
                    table.ForeignKey(
                        name: "FK_HT_Nhom_NguoiDung _HT_NguoiDung_iID_MaNguoiDung",
                        column: x => x.iID_MaNguoiDung,
                        principalTable: "HT_NguoiDung",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HT_Nhom_NguoiDung _HT_Nhom_iID_Nhom",
                        column: x => x.iID_Nhom,
                        principalTable: "HT_Nhom",
                        principalColumn: "iID_Nhom",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NH_KHTongThe_NhiemVuChi",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriKH_BQP = table.Column<double>(nullable: true),
                    fGiaTriKH_TTCP = table.Column<double>(nullable: true),
                    iID_DonViThuHuongID = table.Column<Guid>(nullable: false),
                    iID_KHTongTheID = table.Column<Guid>(nullable: false),
                    iID_NhiemVuChiID = table.Column<Guid>(nullable: false),
                    iID_MaDonViThuHuong = table.Column<string>(nullable: true),
                    sMaOrder = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_KHTongThe_NhiemVuChi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NH_KHTongThe_NhiemVuChi_NH_DM_NhiemVuChi_iID_NhiemVuChiID",
                        column: x => x.iID_NhiemVuChiID,
                        principalTable: "NH_DM_NhiemVuChi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NS_BK_ChungTuChiTiet",
                columns: table => new
                {
                    iID_BKCTChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongHienVat = table.Column<double>(nullable: false),
                    fTongTuChi = table.Column<double>(nullable: false),
                    iID_BKChungTu = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    iID_MLNS = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: false, defaultValueSql: "((2))"),
                    iThangQuy = table.Column<int>(nullable: true),
                    iThangQuyLoai = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    sK = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sL = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sLNS = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sLoai = table.Column<string>(maxLength: 10, nullable: true, defaultValueSql: "((0))"),
                    sM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true, defaultValueSql: "('')"),
                    sNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    sTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTNG = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sTTM = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    sXauNoiMa = table.Column<string>(maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BK_ChungTuChiTiet", x => x.iID_BKCTChiTiet);
                    table.ForeignKey(
                        name: "FK_BK_ChungTuChiTiet_BK_ChungTu",
                        column: x => x.iID_BKChungTu,
                        principalTable: "NS_BK_ChungTu",
                        principalColumn: "iID_BKChungTu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NS_DT_ChungTuChiTiet",
                columns: table => new
                {
                    iID_DTCTChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fDuPhong = table.Column<double>(nullable: false),
                    fHangMua = table.Column<double>(nullable: false),
                    fHangNhap = table.Column<double>(nullable: false),
                    fHienVat = table.Column<double>(nullable: false),
                    fPhanCap = table.Column<double>(nullable: false),
                    fTonKho = table.Column<double>(nullable: false),
                    fTuChi = table.Column<double>(nullable: false),
                    iDuLieuNhan = table.Column<int>(nullable: false),
                    iID_CTDuToan_Nhan = table.Column<string>(nullable: true),
                    iID_DTChungTu = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    iID_MLNS = table.Column<Guid>(nullable: true),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iNamNganSach = table.Column<int>(nullable: true, defaultValueSql: "((2))"),
                    iPhanCap = table.Column<int>(nullable: false),
                    sGhiChu = table.Column<string>(maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_DT_ChungTuChiTiet", x => x.iID_DTCTChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_NS_DT_ChungTuChiTiet_NS_DT_ChungTu_iID_DTChungTu",
                        column: x => x.iID_DTChungTu,
                        principalTable: "NS_DT_ChungTu",
                        principalColumn: "iID_DTChungTu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NS_DT_Nhan_PhanBo_Map",
                columns: table => new
                {
                    iID_DTNhanPhanBoMap = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    dNgayTao = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    iID_CTDuToan_Nhan = table.Column<Guid>(nullable: false),
                    iID_CTDuToan_PhanBo = table.Column<Guid>(nullable: false),
                    sNgaySua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DT_ChungTu_Map", x => x.iID_DTNhanPhanBoMap);
                    //table.ForeignKey(
                    //    name: "FK_NS_DT_Nhan_PhanBo_Map_NS_DT_ChungTu_iID_CTDuToan_Nhan",
                    //    column: x => x.iID_CTDuToan_Nhan,
                    //    principalTable: "NS_DT_ChungTu",
                    //    principalColumn: "iID_DTChungTu",
                    //    onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_NS_DT_Nhan_PhanBo_Map_NS_DT_ChungTu_iID_CTDuToan_PhanBo",
                    //    column: x => x.iID_CTDuToan_PhanBo,
                    //    principalTable: "NS_DT_ChungTu",
                    //    principalColumn: "iID_DTChungTu",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NS_SKT_ChungTuChiTiet",
                columns: table => new
                {
                    iID_CTSoKiemTraChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fHienVat = table.Column<double>(nullable: false),
                    fHuyDongTonKho = table.Column<double>(nullable: false),
                    fMuaHangCapHienVat = table.Column<double>(nullable: false, defaultValueSql: "((0))"),
                    fPhanCap = table.Column<double>(nullable: false),
                    fThongBaoDonVi = table.Column<double>(nullable: false),
                    fTonKhoDenNgay = table.Column<double>(nullable: false),
                    fTuChi = table.Column<double>(nullable: false),
                    fTuChiDeNghi = table.Column<double>(nullable: false),
                    iID_CTSoKiemTra = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    iID_MaNguonNganSach = table.Column<int>(nullable: true),
                    iID_MLSKT = table.Column<Guid>(nullable: false),
                    iLoai = table.Column<int>(nullable: false),
                    iLoaiChungTu = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iNamNganSach = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sKyHieu = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: false),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKT_ChungTuChiTiet", x => x.iID_CTSoKiemTraChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_NS_SKT_ChungTuChiTiet_NS_SKT_ChungTu_iID_CTSoKiemTra",
                        column: x => x.iID_CTSoKiemTra,
                        principalTable: "NS_SKT_ChungTu",
                        principalColumn: "iID_CTSoKiemTra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_CanBo_KeHoach",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BHTN = table.Column<bool>(nullable: true),
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
                    table.PrimaryKey("PK_TL_DM_CanBo_KeHoach", x => x.Id);
                    //table.ForeignKey(
                    //    name: "FK_TL_DM_CanBo_KeHoach_TL_DM_CapBac_Ma_CB",
                    //    column: x => x.Ma_CB,
                    //    principalTable: "TL_DM_CapBac",
                    //    principalColumn: "Ma_Cb",
                    //    onDelete: ReferentialAction.Restrict);
                    //table.ForeignKey(
                    //    name: "FK_TL_DM_CanBo_KeHoach_TL_DM_ChucVu_Ma_CV",
                    //    column: x => x.Ma_CV,
                    //    principalTable: "TL_DM_ChucVu",
                    //    principalColumn: "Ma_Cv",
                    //    onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TL_DM_CanBo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BHTN = table.Column<bool>(nullable: true),
                    bNuocNgoai = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    Cb_KeHoach = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Cccd = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Dia_Chi = table.Column<string>(maxLength: 500, nullable: true),
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
                    Khong_Luong = table.Column<bool>(nullable: true),
                    Ma_BL = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CanBo = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Ma_CB = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CbCu = table.Column<string>(nullable: true),
                    Ma_CV = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_DiaBan_HC = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Ma_Hieu_CanBo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    Ma_KhoBac = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Ma_PBan = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    MaSo_DV_SDNS = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    MaSo_VAT = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_TangGiam = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_TangGiamCu = table.Column<string>(nullable: true),
                    MaTK_LQ = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Nam = table.Column<int>(nullable: true),
                    Nam_TN = table.Column<int>(nullable: true),
                    Nam_VK = table.Column<int>(nullable: true),
                    NgayCap_CMT = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_NhanCB = table.Column<DateTime>(type: "datetime", nullable: true),
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
                    So_SoLuong = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    So_TaiKhoan = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Splits = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    Ten_CanBo = table.Column<string>(maxLength: 150, nullable: true),
                    Ten_DonVi = table.Column<string>(maxLength: 100, nullable: true),
                    Ten_KhoBac = table.Column<string>(maxLength: 100, nullable: true),
                    Thang = table.Column<int>(nullable: true),
                    Thang_TNN = table.Column<int>(nullable: true),
                    ThoiHan_TangCb = table.Column<int>(nullable: true),
                    TM = table.Column<bool>(nullable: true),
                    UserCreator = table.Column<string>(maxLength: 255, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DM_CanBo", x => x.Id);
                    table.UniqueConstraint("AK_TL_DM_CanBo_Ma_CanBo", x => x.Ma_CanBo);
                    //table.ForeignKey(
                    //    name: "FK_TL_DM_CanBo_TL_DM_CapBac_Ma_CB",
                    //    column: x => x.Ma_CB,
                    //    principalTable: "TL_DM_CapBac",
                    //    principalColumn: "Ma_Cb",
                    //    onDelete: ReferentialAction.Restrict);
                    //table.ForeignKey(
                    //    name: "FK_TL_DM_CanBo_TL_DM_ChucVu_Ma_CV",
                    //    column: x => x.Ma_CV,
                    //    principalTable: "TL_DM_ChucVu",
                    //    principalColumn: "Ma_Cv",
                    //    onDelete: ReferentialAction.Restrict);
                    //table.ForeignKey(
                    //    name: "FK_TL_DM_CanBo_TL_DM_DonVi_Parent",
                    //    column: x => x.Parent,
                    //    principalTable: "TL_DM_DonVi",
                    //    principalColumn: "Ma_DonVi",
                    //    onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TL_DS_CapNhap_BangLuong",
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
                    table.PrimaryKey("PK_TL_DS_CapNhap_BangLuong", x => x.Id);
                    //table.ForeignKey(
                    //    name: "FK_TL_DS_CapNhap_BangLuong_TL_DM_DonVi_Ma_CBo",
                    //    column: x => x.Ma_CBo,
                    //    principalTable: "TL_DM_DonVi",
                    //    principalColumn: "Ma_DonVi",
                    //    onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TL_QS_ChungTu",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bNganSachNhanDuLieu = table.Column<bool>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    Id_ChungTu = table.Column<int>(nullable: false),
                    IsLock = table.Column<bool>(nullable: true),
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
                    table.PrimaryKey("PK_TL_QS_ChungTu", x => x.ID);
                    //table.ForeignKey(
                    //    name: "FK_TL_QS_ChungTu_TL_DM_DonVi_Ma_DonVi",
                    //    column: x => x.Ma_DonVi,
                    //    principalTable: "TL_DM_DonVi",
                    //    principalColumn: "Ma_DonVi",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TL_QT_ChungTu",
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
                    table.PrimaryKey("PK_TL_QT_ChungTu", x => x.ID);
                    //table.ForeignKey(
                    //    name: "FK_TL_QT_ChungTu_TL_DM_DonVi_Ma_DonVi",
                    //    column: x => x.Ma_DonVi,
                    //    principalTable: "TL_DM_DonVi",
                    //    principalColumn: "Ma_DonVi",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_KeHoachVonUng_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fCapPhatBangLenhChi = table.Column<double>(nullable: true),
                    fCapPhatTaiKhoBac = table.Column<double>(nullable: true),
                    fGiaTriUng = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_KeHoachUngID = table.Column<Guid>(nullable: true),
                    iID_MucID = table.Column<Guid>(nullable: true),
                    iID_NganhID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TietMucID = table.Column<Guid>(nullable: true),
                    iID_TieuMucID = table.Column<Guid>(nullable: true),
                    sGhiChu = table.Column<string>(type: "ntext", nullable: true),
                    sTrangThaiDuAnDangKy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_KeHoachVonUng_ChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeHoachVonUng_ChiTiet_KeHoachVonUng",
                        column: x => x.iID_KeHoachUngID,
                        principalTable: "VDT_KHV_KeHoachVonUng",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_PhanBoVon_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bActive = table.Column<bool>(nullable: true),
                    fCapPhatBangLenhChi = table.Column<double>(nullable: true),
                    fCapPhatBangLenhChiDC = table.Column<double>(nullable: true),
                    fCapPhatTaiKhoBac = table.Column<double>(nullable: true),
                    fCapPhatTaiKhoBacDC = table.Column<double>(nullable: true),
                    fGiaTrDeNghi = table.Column<double>(nullable: true),
                    fGiaTrPhanBo = table.Column<double>(nullable: true),
                    fGiaTriThuHoi = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamTruocKhoBac = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamTruocKhoBacDC = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamTruocLenhChi = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamTruocLenhChiDC = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinh = table.Column<Guid>(nullable: true),
                    iID_MucID = table.Column<Guid>(nullable: true),
                    iID_NganhID = table.Column<Guid>(nullable: true),
                    iId_Parent = table.Column<Guid>(nullable: true),
                    iID_PhanBoVonID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TietMucID = table.Column<Guid>(nullable: true),
                    iID_TieuMucID = table.Column<Guid>(nullable: true),
                    ILoaiDuAn = table.Column<int>(nullable: true),
                    K = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    L = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    LNS = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    M = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    NG = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    sGhiChu = table.Column<string>(nullable: true),
                    sTrangThaiDuAnDangKy = table.Column<string>(maxLength: 100, nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    TTM = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_PhanBoVon_ChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhanBoVon_ChiTiet_PhanBoVon",
                        column: x => x.iID_PhanBoVonID,
                        principalTable: "VDT_KHV_PhanBoVon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    bActive = table.Column<bool>(nullable: true),
                    fGiaTrPhanBo = table.Column<double>(nullable: true),
                    fGiaTriThuHoi = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_LoaiCongTrinh = table.Column<Guid>(nullable: true),
                    iId_Parent = table.Column<Guid>(nullable: true),
                    iID_PhanBoVon_DonVi_PheDuyet_ID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    ILoaiDuAn = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet_VDT_KHV_PhanBoVon_DonVi_PheDuyet_iID_PhanBoVon_DonVi_PheDuyet_ID",
                        column: x => x.iID_PhanBoVon_DonVi_PheDuyet_ID,
                        principalTable: "VDT_KHV_PhanBoVon_DonVi_PheDuyet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TT_DeNghiThanhToan_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fGiaTriTamUngKhac = table.Column<double>(nullable: true),
                    fGiaTriThanhToanKhac = table.Column<double>(nullable: true),
                    fGiaTriThanhToanNN = table.Column<double>(nullable: true),
                    fGiaTriThanhToanTN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiKhac = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamNayNN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamNayTN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamTruocNN = table.Column<double>(nullable: true),
                    fGiaTriThuHoiNamTruocTN = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DeNghiThanhToanID = table.Column<Guid>(nullable: true),
                    iID_DonViKhacID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_MucID = table.Column<Guid>(nullable: true),
                    iID_NganhID = table.Column<Guid>(nullable: true),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TietMucID = table.Column<Guid>(nullable: true),
                    iID_TieuMucID = table.Column<Guid>(nullable: true),
                    M = table.Column<string>(maxLength: 50, nullable: true),
                    NG = table.Column<string>(maxLength: 50, nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TT_DeNghiThanhToan_ChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VDT_TT_DeNghiThanhToan_ChiTiet_VDT_TT_DeNghiThanhToan_iID_DeNghiThanhToanID",
                        column: x => x.iID_DeNghiThanhToanID,
                        principalTable: "VDT_TT_DeNghiThanhToan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TT_DeNghiThanhToanUng_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fGiaTriTamUng = table.Column<double>(nullable: true),
                    fGiaTriTamUngKhac = table.Column<double>(nullable: true),
                    fGiaTriThanhToan = table.Column<double>(nullable: true),
                    fGiaTriThanhToanKhac = table.Column<double>(nullable: true),
                    fGiaTriThuHoi = table.Column<double>(nullable: true),
                    fGiaTriThuHoiKhac = table.Column<double>(nullable: true),
                    fGiaTriThuHoiUngNgoaiChiTieu = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DeNghiThanhToanID = table.Column<Guid>(nullable: true),
                    iID_DonViKhacID = table.Column<Guid>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_MucID = table.Column<Guid>(nullable: true),
                    iID_NganhID = table.Column<Guid>(nullable: true),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TietMucID = table.Column<Guid>(nullable: true),
                    iID_TieuMucID = table.Column<Guid>(nullable: true),
                    M = table.Column<string>(maxLength: 50, nullable: true),
                    NG = table.Column<string>(maxLength: 50, nullable: true),
                    sGhiChu = table.Column<string>(maxLength: 1200, nullable: true),
                    TM = table.Column<string>(maxLength: 50, nullable: true),
                    TTM = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TT_DeNghiThanhToanUng_ChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TT.DeNghiThanhToanUng_ChiTiet_TT.DeNghiThanhToanUng",
                        column: x => x.iID_DeNghiThanhToanID,
                        principalTable: "VDT_TT_DeNghiThanhToanUng",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VDT_TT_ThanhToanQuaKhoBac_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fGiaTriTamUng = table.Column<double>(nullable: true),
                    fGiaTriThanhToan = table.Column<double>(nullable: true),
                    fTiGia = table.Column<double>(nullable: true),
                    fTiGiaDonVi = table.Column<double>(nullable: true),
                    iID_DonViTienTeID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_MucID = table.Column<Guid>(nullable: true),
                    iID_NganhID = table.Column<Guid>(nullable: true),
                    iID_NhaThauID = table.Column<Guid>(nullable: true),
                    iID_ThanhToanID = table.Column<Guid>(nullable: true),
                    iID_TienTeID = table.Column<Guid>(nullable: true),
                    iID_TietMucID = table.Column<Guid>(nullable: true),
                    iID_TieuMucID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_TT_ThanhToanQuaKhoBac_ChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TT.ThanhToanQuaKhoBac_ChiTiet_TT.ThanhToanQuaKhoBac",
                        column: x => x.iID_ThanhToanID,
                        principalTable: "VDT_TT_ThanhToanQuaKhoBac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HT_Nhom_Quyen",
                columns: table => new
                {
                    iID_MaQuyen = table.Column<string>(maxLength: 250, nullable: false),
                    iID_Nhom = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorityGroup", x => new { x.iID_MaQuyen, x.iID_Nhom });
                    table.ForeignKey(
                        name: "FK_HT_Nhom_Quyen_HT_Quyen_iID_MaQuyen",
                        column: x => x.iID_MaQuyen,
                        principalTable: "HT_Quyen",
                        principalColumn: "iID_MaQuyen",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HT_Nhom_Quyen_HT_Nhom_iID_Nhom",
                        column: x => x.iID_Nhom,
                        principalTable: "HT_Nhom",
                        principalColumn: "iID_Nhom",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HT_Quyen_ChucNang",
                columns: table => new
                {
                    iID_MaChucNang = table.Column<string>(maxLength: 100, nullable: false),
                    iID_MaQuyen = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HT_Quyen_ChucNang", x => new { x.iID_MaChucNang, x.iID_MaQuyen });
                    table.ForeignKey(
                        name: "FK_HT_Quyen_ChucNang_HT_ChucNang_iID_MaChucNang",
                        column: x => x.iID_MaChucNang,
                        principalTable: "HT_ChucNang",
                        principalColumn: "iID_MaChucNang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HT_Quyen_ChucNang_HT_Quyen_iID_MaQuyen",
                        column: x => x.iID_MaQuyen,
                        principalTable: "HT_Quyen",
                        principalColumn: "iID_MaQuyen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_HopDong",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsActive = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    bIsGoc = table.Column<bool>(nullable: true, defaultValueSql: "1"),
                    bIsKhoa = table.Column<bool>(nullable: true, defaultValueSql: "0"),
                    bIsXoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    dKetThucDuKien = table.Column<DateTime>(type: "datetime", nullable: true),
                    dKhoiCongDuKien = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayHopDong = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriEUR = table.Column<double>(nullable: true),
                    fGiaTriHopDongEUR = table.Column<double>(nullable: true),
                    fGiaTriHopDongNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriHopDongUSD = table.Column<double>(nullable: true),
                    fGiaTriHopDongVND = table.Column<double>(nullable: true),
                    fGiaTriNgoaiTeKhac = table.Column<double>(nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_CacQuyetDinhID = table.Column<Guid>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_GoiThauID = table.Column<Guid>(nullable: true),
                    iID_HopDongGocID = table.Column<Guid>(nullable: true),
                    iID_KHTongThe_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_LoaiHopDongID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true),
                    iID_NhaThauThucHienID = table.Column<Guid>(nullable: true),
                    iID_ParentAdjustID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: false),
                    iLoai = table.Column<int>(nullable: true),
                    iThoiGianThucHien = table.Column<int>(nullable: true),
                    sHinhThucHopDong = table.Column<string>(maxLength: 300, nullable: true),
                    sMaNgoaiTeKhac = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoHopDong = table.Column<string>(type: "varchar(100)", nullable: true),
                    sTenHopDong = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_HopDong", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NH_DA_HopDong_NH_KHTongThe_NhiemVuChi_iID_KHTongThe_NhiemVuChiID",
                        column: x => x.iID_KHTongThe_NhiemVuChiID,
                        principalTable: "NH_KHTongThe_NhiemVuChi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TL_CanBo_PhuCap",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bSaoChep = table.Column<bool>(nullable: true),
                    CHON = table.Column<bool>(nullable: true),
                    CONG_THUC = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
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
                    table.PrimaryKey("PK_TL_CanBo_PhuCap", x => x.Id);
                    //table.ForeignKey(
                    //    name: "FK_TL_CanBo_PhuCap_TL_DM_CanBo_MA_CBO",
                    //    column: x => x.MA_CBO,
                    //    principalTable: "TL_DM_CanBo",
                    //    principalColumn: "Ma_CanBo",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ID_PARENT_INDEX",
                table: "DM_ChuDauTu",
                column: "iID_DonViCha");

            migrationBuilder.CreateIndex(
                name: "IX_DonVi_iID_Parent",
                table: "DonVi",
                column: "iID_Parent");

            migrationBuilder.CreateIndex(
                name: "UQ_LoginUnique",
                table: "HT_NguoiDung",
                column: "sTaiKhoan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HT_Nhom_NguoiDung _iID_MaNguoiDung",
                table: "HT_Nhom_NguoiDung ",
                column: "iID_MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_HT_Nhom_Quyen_iID_Nhom",
                table: "HT_Nhom_Quyen",
                column: "iID_Nhom");

            migrationBuilder.CreateIndex(
                name: "IX_HT_Quyen_iID_LoaiQuyen",
                table: "HT_Quyen",
                column: "iID_LoaiQuyen");

            migrationBuilder.CreateIndex(
                name: "IX_HT_Quyen_ChucNang_iID_MaQuyen",
                table: "HT_Quyen_ChucNang",
                column: "iID_MaQuyen");

            migrationBuilder.CreateIndex(
                name: "IX_NH_DA_HopDong_iID_KHTongThe_NhiemVuChiID",
                table: "NH_DA_HopDong",
                column: "iID_KHTongThe_NhiemVuChiID");

            migrationBuilder.CreateIndex(
                name: "IX_NH_KHTongThe_NhiemVuChi_iID_NhiemVuChiID",
                table: "NH_KHTongThe_NhiemVuChi",
                column: "iID_NhiemVuChiID");

            migrationBuilder.CreateIndex(
                name: "IX_NS_BK_ChungTuChiTiet_iID_BKChungTu",
                table: "NS_BK_ChungTuChiTiet",
                column: "iID_BKChungTu");

            migrationBuilder.CreateIndex(
                name: "IX_NS_DT_ChungTuChiTiet_iID_DTChungTu_iNamNganSach_iID_MaNguonNganSach_iNamLamViec",
                table: "NS_DT_ChungTuChiTiet",
                columns: new[] { "iID_DTChungTu", "iNamNganSach", "iID_MaNguonNganSach", "iNamLamViec" });

            migrationBuilder.CreateIndex(
                name: "IX_NS_DT_Nhan_PhanBo_Map_iID_CTDuToan_Nhan",
                table: "NS_DT_Nhan_PhanBo_Map",
                column: "iID_CTDuToan_Nhan");

            migrationBuilder.CreateIndex(
                name: "IX_NS_DT_Nhan_PhanBo_Map_iID_CTDuToan_PhanBo",
                table: "NS_DT_Nhan_PhanBo_Map",
                column: "iID_CTDuToan_PhanBo");

            migrationBuilder.CreateIndex(
                name: "IX_NS_MucLucNganSach_iID_MLNS_iNamLamViec",
                table: "NS_MucLucNganSach",
                columns: new[] { "iID_MLNS", "iNamLamViec" });

            migrationBuilder.CreateIndex(
                name: "IX_NS_MucLucNganSach_iNamLamViec_sLNS",
                table: "NS_MucLucNganSach",
                columns: new[] { "iNamLamViec", "sLNS" });

            migrationBuilder.CreateIndex(
                name: "IX_NS_MucLucNganSach_iID_iID_MLNS_iID_MLNS_Cha",
                table: "NS_MucLucNganSach",
                columns: new[] { "iID", "iID_MLNS", "iID_MLNS_Cha" });

            migrationBuilder.CreateIndex(
                name: "IX_NS_MucLucNganSach_iID_MLNS_Cha_sL_iNamLamViec",
                table: "NS_MucLucNganSach",
                columns: new[] { "iID_MLNS_Cha", "sL", "iNamLamViec" });

            migrationBuilder.CreateIndex(
                name: "IX_NS_MucLucNganSach_sL_sK_sM_sTM_sTTM_sNG_iNamLamViec_sLNS",
                table: "NS_MucLucNganSach",
                columns: new[] { "sL", "sK", "sM", "sTM", "sTTM", "sNG", "iNamLamViec", "sLNS" });

            migrationBuilder.CreateIndex(
                name: "NguoiDung_LNS_sLNS_Index",
                table: "NS_NguoiDung_LNS",
                columns: new[] { "sMaNguoiDung", "iNamLamViec" });

            migrationBuilder.CreateIndex(
                name: "IX_NS_SKT_ChungTuChiTiet_iID_CTSoKiemTra",
                table: "NS_SKT_ChungTuChiTiet",
                column: "iID_CTSoKiemTra");

            migrationBuilder.CreateIndex(
                name: "UQ__Tl_Bao_C__FBD635CD7CC29D66",
                table: "TL_Bao_Cao",
                column: "Ma_BaoCao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TL_CanBo_PhuCap_MA_CBO",
                table: "TL_CanBo_PhuCap",
                column: "MA_CBO");

            migrationBuilder.CreateIndex(
                name: "IX_TL_DM_CanBo_Ma_CB",
                table: "TL_DM_CanBo",
                column: "Ma_CB");

            migrationBuilder.CreateIndex(
                name: "IX_TL_DM_CanBo_Ma_CV",
                table: "TL_DM_CanBo",
                column: "Ma_CV");

            migrationBuilder.CreateIndex(
                name: "IX_TL_DM_CanBo_Parent",
                table: "TL_DM_CanBo",
                column: "Parent");

            migrationBuilder.CreateIndex(
                name: "IX_TL_DM_CanBo_KeHoach_Ma_CB",
                table: "TL_DM_CanBo_KeHoach",
                column: "Ma_CB");

            migrationBuilder.CreateIndex(
                name: "IX_TL_DM_CanBo_KeHoach_Ma_CV",
                table: "TL_DM_CanBo_KeHoach",
                column: "Ma_CV");

            migrationBuilder.CreateIndex(
                name: "IX_TL_DS_CapNhap_BangLuong_Ma_CBo",
                table: "TL_DS_CapNhap_BangLuong",
                column: "Ma_CBo");

            migrationBuilder.CreateIndex(
                name: "IX_TL_QS_ChungTu_Ma_DonVi",
                table: "TL_QS_ChungTu",
                column: "Ma_DonVi");

            migrationBuilder.CreateIndex(
                name: "IX_TL_QT_ChungTu_Ma_DonVi",
                table: "TL_QT_ChungTu",
                column: "Ma_DonVi");

            migrationBuilder.CreateIndex(
                name: "IX_VDT_DM_LoaiCongTrinh_iID_Parent",
                table: "VDT_DM_LoaiCongTrinh",
                column: "iID_Parent");

            migrationBuilder.CreateIndex(
                name: "IX_VDT_KHV_KeHoachVonUng_ChiTiet_iID_KeHoachUngID",
                table: "VDT_KHV_KeHoachVonUng_ChiTiet",
                column: "iID_KeHoachUngID");

            migrationBuilder.CreateIndex(
                name: "IX_VDT_KHV_PhanBoVon_ChiTiet_iID_PhanBoVonID",
                table: "VDT_KHV_PhanBoVon_ChiTiet",
                column: "iID_PhanBoVonID");

            migrationBuilder.CreateIndex(
                name: "IX_VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet_iID_PhanBoVon_DonVi_PheDuyet_ID",
                table: "VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet",
                column: "iID_PhanBoVon_DonVi_PheDuyet_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VDT_TT_DeNghiThanhToan_ChiTiet_iID_DeNghiThanhToanID",
                table: "VDT_TT_DeNghiThanhToan_ChiTiet",
                column: "iID_DeNghiThanhToanID");

            migrationBuilder.CreateIndex(
                name: "IX_VDT_TT_DeNghiThanhToanUng_ChiTiet_iID_DeNghiThanhToanID",
                table: "VDT_TT_DeNghiThanhToanUng_ChiTiet",
                column: "iID_DeNghiThanhToanID");

            migrationBuilder.CreateIndex(
                name: "IX_VDT_TT_ThanhToanQuaKhoBac_ChiTiet_iID_ThanhToanID",
                table: "VDT_TT_ThanhToanQuaKhoBac_ChiTiet",
                column: "iID_ThanhToanID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "BH_DM_CheDoBHXH");

            migrationBuilder.DropTable(
                name: "BH_DM_KinhPhi");

            migrationBuilder.DropTable(
                name: "CP_DanhMuc");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "DM_BQuanLy");

            migrationBuilder.DropTable(
                name: "DM_ChuDauTu");

            migrationBuilder.DropTable(
                name: "DM_ChuKy");

            migrationBuilder.DropTable(
                name: "DM_DeTai");

            migrationBuilder.DropTable(
                name: "DonVi");

            migrationBuilder.DropTable(
                name: "HT_App_Update_Log");

            migrationBuilder.DropTable(
                name: "HT_App_Version");

            migrationBuilder.DropTable(
                name: "HT_NhatKy_CapNhat_DuLieu");

            migrationBuilder.DropTable(
                name: "HT_Nhom_NguoiDung ");

            migrationBuilder.DropTable(
                name: "HT_Nhom_Quyen");

            migrationBuilder.DropTable(
                name: "HT_Quyen_ChucNang");

            migrationBuilder.DropTable(
                name: "IMP_CP_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "IMP_DuToan");

            migrationBuilder.DropTable(
                name: "IMP_History");

            migrationBuilder.DropTable(
                name: "IMP_QuyetToan");

            migrationBuilder.DropTable(
                name: "IMP_SKT_SoLieuChiTiet");

            migrationBuilder.DropTable(
                name: "IMP_TN_DT_ThuNop");

            migrationBuilder.DropTable(
                name: "IMP_TN_QT_ThuNop");

            migrationBuilder.DropTable(
                name: "NguoiDung_DonVi");

            migrationBuilder.DropTable(
                name: "NH_DA_ChuTruongDauTu");

            migrationBuilder.DropTable(
                name: "NH_DA_ChuTruongDauTu_HangMuc");

            migrationBuilder.DropTable(
                name: "NH_DA_ChuTruongDauTu_NguonVon");

            migrationBuilder.DropTable(
                name: "NH_DA_DuAn");

            migrationBuilder.DropTable(
                name: "NH_DA_DuAn_HangMuc");

            migrationBuilder.DropTable(
                name: "NH_DA_DuAn_NguonVon");

            migrationBuilder.DropTable(
                name: "NH_DA_DuToan");

            migrationBuilder.DropTable(
                name: "NH_DA_DuToan_ChiPhi");

            migrationBuilder.DropTable(
                name: "NH_DA_DuToan_HangMuc");

            migrationBuilder.DropTable(
                name: "NH_DA_DuToan_NguonVon");

            migrationBuilder.DropTable(
                name: "NH_DA_GoiThau");

            migrationBuilder.DropTable(
                name: "NH_DA_GoiThau_ChiPhi");

            migrationBuilder.DropTable(
                name: "NH_DA_GoiThau_HangMuc");

            migrationBuilder.DropTable(
                name: "NH_DA_GoiThau_NguonVon");

            migrationBuilder.DropTable(
                name: "NH_DA_HopDong");

            migrationBuilder.DropTable(
                name: "NH_DA_HopDong_CacQuyetDinh");

            migrationBuilder.DropTable(
                name: "NH_DA_HopDong_ChiPhi");

            migrationBuilder.DropTable(
                name: "NH_DA_HopDong_GoiThau_NhaThau");

            migrationBuilder.DropTable(
                name: "NH_DA_HopDong_HangMuc");

            migrationBuilder.DropTable(
                name: "NH_DA_HopDong_NguonVon");

            migrationBuilder.DropTable(
                name: "NH_DA_KHLCNhaThau");

            migrationBuilder.DropTable(
                name: "NH_DA_QDDauTu");

            migrationBuilder.DropTable(
                name: "NH_DA_QDDauTu_ChiPhi");

            migrationBuilder.DropTable(
                name: "NH_DA_QDDauTu_HangMuc");

            migrationBuilder.DropTable(
                name: "NH_DA_QDDauTu_NguonVon");

            migrationBuilder.DropTable(
                name: "NH_DM_ChiPhi");

            migrationBuilder.DropTable(
                name: "NH_DM_DonViTinh");

            migrationBuilder.DropTable(
                name: "NH_DM_HinhThucChonNhaThau");

            migrationBuilder.DropTable(
                name: "NH_DM_LoaiCongTrinh");

            migrationBuilder.DropTable(
                name: "NH_DM_LoaiHopDong");

            migrationBuilder.DropTable(
                name: "NH_DM_LoaiTaiSan");

            migrationBuilder.DropTable(
                name: "NH_DM_LoaiTienTe");

            migrationBuilder.DropTable(
                name: "NH_DM_NhaThau");

            migrationBuilder.DropTable(
                name: "NH_DM_NhaThau_NganHang");

            migrationBuilder.DropTable(
                name: "NH_DM_NhaThau_NguoiNhan");

            migrationBuilder.DropTable(
                name: "NH_DM_PhanCapPheDuyet");

            migrationBuilder.DropTable(
                name: "NH_DM_PhuongThucChonNhaThau");

            migrationBuilder.DropTable(
                name: "NH_DM_TiGia");

            migrationBuilder.DropTable(
                name: "NH_DM_TiGia_ChiTiet");

            migrationBuilder.DropTable(
                name: "NH_DM_XuatXu");

            migrationBuilder.DropTable(
                name: "NH_HDNK_CacQuyetDinh");

            migrationBuilder.DropTable(
                name: "NH_HDNK_CacQuyetDinh_ChiPhi");

            migrationBuilder.DropTable(
                name: "NH_HDNK_CacQuyetDinh_ChiPhi_HangMuc");

            migrationBuilder.DropTable(
                name: "NH_HDNK_CacQuyetDinh_NguonVon");

            migrationBuilder.DropTable(
                name: "NH_HDNK_PhuongAnNhapKhau");

            migrationBuilder.DropTable(
                name: "NH_HopDong");

            migrationBuilder.DropTable(
                name: "NH_KHChiTiet");

            migrationBuilder.DropTable(
                name: "NH_KHChiTiet_HopDong");

            migrationBuilder.DropTable(
                name: "NH_KHTongThe");

            migrationBuilder.DropTable(
                name: "NH_NhuCauChiQuy");

            migrationBuilder.DropTable(
                name: "NH_NhuCauChiQuy_ChiTiet");

            migrationBuilder.DropTable(
                name: "NH_QT_QuyetToanDAHT");

            migrationBuilder.DropTable(
                name: "NH_QT_QuyetToanDAHT_ChiTiet");

            migrationBuilder.DropTable(
                name: "NH_QT_QuyetToanDAHT_NguonVon");

            migrationBuilder.DropTable(
                name: "NH_QT_QuyetToanNienDo");

            migrationBuilder.DropTable(
                name: "NH_QT_QuyetToanNienDo_ChiTiet");

            migrationBuilder.DropTable(
                name: "NH_QT_TaiSan");

            migrationBuilder.DropTable(
                name: "NH_QT_TaiSan_ChiTiet");

            migrationBuilder.DropTable(
                name: "NH_TT_ThanhToan");

            migrationBuilder.DropTable(
                name: "NH_TT_ThanhToan_ChiTiet");

            migrationBuilder.DropTable(
                name: "NH_TT_ThongTriCapPhat");

            migrationBuilder.DropTable(
                name: "NH_TT_ThongTriCapPhat_ChiTiet");

            migrationBuilder.DropTable(
                name: "NS_BK_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "NS_CauHinh_CanCu");

            migrationBuilder.DropTable(
                name: "NS_CP_ChungTu");

            migrationBuilder.DropTable(
                name: "NS_CP_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "NS_DC_ChungTu");

            migrationBuilder.DropTable(
                name: "NS_DC_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "NS_DT_ChungTu_CanCu");

            migrationBuilder.DropTable(
                name: "NS_DT_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "NS_DTDauNam_ChungTu");

            migrationBuilder.DropTable(
                name: "NS_DTDauNam_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "NS_DTDauNam_ChungTuChiTiet_CanCu");

            migrationBuilder.DropTable(
                name: "NS_DTDauNam_ChungTu_ChungTuCanCu");

            migrationBuilder.DropTable(
                name: "NS_DTDauNam_PhanCap");

            migrationBuilder.DropTable(
                name: "NS_DT_Nhan_PhanBo_Map");

            migrationBuilder.DropTable(
                name: "NS_MLSKT_MLNS");

            migrationBuilder.DropTable(
                name: "NS_MucLucNganSach");

            migrationBuilder.DropTable(
                name: "NS_MucLucNganSach_Nganh");

            migrationBuilder.DropTable(
                name: "NS_Nganh_ChungTu");

            migrationBuilder.DropTable(
                name: "NS_Nganh_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "NS_Nganh_ChungTuChiTiet_PhanCap");

            migrationBuilder.DropTable(
                name: "NS_NguoiDung_LNS");

            migrationBuilder.DropTable(
                name: "NguonNganSach");

            migrationBuilder.DropTable(
                name: "NS_PhongBan_DonVi");

            migrationBuilder.DropTable(
                name: "NS_QS_ChungTu");

            migrationBuilder.DropTable(
                name: "NS_QS_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "NS_QS_MucLuc");

            migrationBuilder.DropTable(
                name: "NS_QT_ChungTu");

            migrationBuilder.DropTable(
                name: "NS_QT_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "NS_QT_ChungTuChiTiet_GiaiThich");

            migrationBuilder.DropTable(
                name: "NS_QT_ChungTuChiTiet_GiaiThich_LuongTru");

            migrationBuilder.DropTable(
                name: "NS_Session");

            migrationBuilder.DropTable(
                name: "NS_SKT_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "NS_SKT_ChungTuChiTiet_CanCu");

            migrationBuilder.DropTable(
                name: "NS_SKT_ChungTu_ChungTuCanCu");

            migrationBuilder.DropTable(
                name: "NS_SKT_MucLuc");

            migrationBuilder.DropTable(
                name: "NS_SKT_NganhThamDinh");

            migrationBuilder.DropTable(
                name: "NS_SKT_NganhThamDinhChiTiet");

            migrationBuilder.DropTable(
                name: "NS_SKT_NganhThamDinhChiTiet_SKT");

            migrationBuilder.DropTable(
                name: "TL_BangLuong_KeHoach");

            migrationBuilder.DropTable(
                name: "TL_BangLuong_Thang");

            migrationBuilder.DropTable(
                name: "TL_Bao_Cao");

            migrationBuilder.DropTable(
                name: "TL_CanBo_PhuCap");

            migrationBuilder.DropTable(
                name: "TL_CanBo_PhuCap_KeHoach");

            migrationBuilder.DropTable(
                name: "TL_DieuChinh_QS_KeHoach");

            migrationBuilder.DropTable(
                name: "TL_DM_Cach_TinhLuong_BaoHiem");

            migrationBuilder.DropTable(
                name: "TL_DM_Cach_TinhLuong_Chuan");

            migrationBuilder.DropTable(
                name: "TL_DM_Cach_TinhLuong_TruyLinh");

            migrationBuilder.DropTable(
                name: "TL_DM_CanBo_KeHoach");

            migrationBuilder.DropTable(
                name: "TL_DM_CapBac_KeHoach");

            migrationBuilder.DropTable(
                name: "TL_DM_HSL_KeHoach");

            migrationBuilder.DropTable(
                name: "TL_DM_MapPC_Detail");

            migrationBuilder.DropTable(
                name: "TL_DM_NangLuong");

            migrationBuilder.DropTable(
                name: "TL_DM_PhuCap");

            migrationBuilder.DropTable(
                name: "TL_DM_PhuCap_KeHoach");

            migrationBuilder.DropTable(
                name: "TL_DM_TangGiam");

            migrationBuilder.DropTable(
                name: "TL_DM_ThemCachTinhLuong");

            migrationBuilder.DropTable(
                name: "TL_DM_ThueThuNhapCaNhan");

            migrationBuilder.DropTable(
                name: "TL_DM_TietTieuMuc_Nganh");

            migrationBuilder.DropTable(
                name: "TL_DS_BangLuong_KeHoach");

            migrationBuilder.DropTable(
                name: "TL_DS_CapNhap_BangLuong");

            migrationBuilder.DropTable(
                name: "TL_DS_SoSanhLuong");

            migrationBuilder.DropTable(
                name: "TL_GT_Tai_Chinh");

            migrationBuilder.DropTable(
                name: "TL_Map_Column_Config");

            migrationBuilder.DropTable(
                name: "TL_PhuCap_DieuChinh");

            migrationBuilder.DropTable(
                name: "TL_PhuCap_MLNS");

            migrationBuilder.DropTable(
                name: "TL_QS_ChungTu");

            migrationBuilder.DropTable(
                name: "TL_QS_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "TL_QS_KeHoach_ChiTiet");

            migrationBuilder.DropTable(
                name: "TL_QT_ChungTu");

            migrationBuilder.DropTable(
                name: "TL_QT_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "TL_QT_ChungTuChiTiet_GiaiThich");

            migrationBuilder.DropTable(
                name: "TL_QT_ChungTuChiTiet_KeHoach");

            migrationBuilder.DropTable(
                name: "TN_DanhMucLoaiHinh");

            migrationBuilder.DropTable(
                name: "TN_DT_ChungTu");

            migrationBuilder.DropTable(
                name: "TN_DT_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "TN_QT_ChungTu");

            migrationBuilder.DropTable(
                name: "TN_QT_ChungTuChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_DA_ChuTruongDauTu");

            migrationBuilder.DropTable(
                name: "VDT_DA_ChuTruongDauTu_ChiPhi");

            migrationBuilder.DropTable(
                name: "VDT_DA_ChuTruongDauTu_DM_HangMuc");

            migrationBuilder.DropTable(
                name: "VDT_DA_ChuTruongDauTu_HangMuc");

            migrationBuilder.DropTable(
                name: "VDT_DA_ChuTruongDauTu_NguonVon");

            migrationBuilder.DropTable(
                name: "VDT_DA_DuAn");

            migrationBuilder.DropTable(
                name: "VDT_DA_DuAn_ChiPhi");

            migrationBuilder.DropTable(
                name: "VDT_DA_DuAn_HangMuc");

            migrationBuilder.DropTable(
                name: "VDT_DA_DuToan");

            migrationBuilder.DropTable(
                name: "VDT_DA_DuToan_ChiPhi");

            migrationBuilder.DropTable(
                name: "VDT_DA_DuToan_DM_HangMuc");

            migrationBuilder.DropTable(
                name: "VDT_DA_DuToan_HangMuc");

            migrationBuilder.DropTable(
                name: "VDT_DA_DuToan_Nguonvon");

            migrationBuilder.DropTable(
                name: "VDT_DA_GoiThau");

            migrationBuilder.DropTable(
                name: "VDT_DA_GoiThau_ChiPhi");

            migrationBuilder.DropTable(
                name: "VDT_DA_GoiThau_HangMuc");

            migrationBuilder.DropTable(
                name: "VDT_DA_GoiThau_NguonVon");

            migrationBuilder.DropTable(
                name: "VDT_DA_HopDong_DM_HangMuc");

            migrationBuilder.DropTable(
                name: "VDT_DA_HopDong_GoiThau_ChiPhi");

            migrationBuilder.DropTable(
                name: "VDT_DA_HopDong_GoiThau_HangMuc");

            migrationBuilder.DropTable(
                name: "VDT_DA_HopDong_GoiThau_NhaThau");

            migrationBuilder.DropTable(
                name: "VDT_DA_NguonVon");

            migrationBuilder.DropTable(
                name: "VDT_DA_QDDauTu");

            migrationBuilder.DropTable(
                name: "VDT_DA_QDDauTu_ChiPhi");

            migrationBuilder.DropTable(
                name: "VDT_DA_QDDauTu_DM_HangMuc");

            migrationBuilder.DropTable(
                name: "VDT_DA_QDDauTu_HangMuc");

            migrationBuilder.DropTable(
                name: "VDT_DA_QDDauTu_NguonVon");

            migrationBuilder.DropTable(
                name: "VDT_DA_TT_HopDong");

            migrationBuilder.DropTable(
                name: "VDT_DM_ChiPhi");

            migrationBuilder.DropTable(
                name: "VDT_DM_DonViThucHienDuAn");

            migrationBuilder.DropTable(
                name: "VDT_DM_DuAn_ChiPhi");

            migrationBuilder.DropTable(
                name: "VDT_DM_HinhThucQuanLy");

            migrationBuilder.DropTable(
                name: "VDT_DM_KieuThongTri");

            migrationBuilder.DropTable(
                name: "VDT_DM_LoaiCongTrinh");

            migrationBuilder.DropTable(
                name: "VDT_DM_LoaiDuAn");

            migrationBuilder.DropTable(
                name: "VDT_DM_LoaiHopDong");

            migrationBuilder.DropTable(
                name: "VDT_DM_LoaiThongTri");

            migrationBuilder.DropTable(
                name: "VDT_DM_NhaThau");

            migrationBuilder.DropTable(
                name: "VDT_DM_NhomDuAn");

            migrationBuilder.DropTable(
                name: "VDT_DM_NhomQuanLy");

            migrationBuilder.DropTable(
                name: "VDT_DM_PhanCapDuAn");

            migrationBuilder.DropTable(
                name: "VDT_KHV_KeHoach5Nam");

            migrationBuilder.DropTable(
                name: "VDT_KHV_KeHoach5Nam_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_KHV_KeHoach5Nam_DeXuat");

            migrationBuilder.DropTable(
                name: "VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_KHV_KeHoachVonUng_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_KHV_KeHoachVonUng_DX");

            migrationBuilder.DropTable(
                name: "VDT_KHV_KeHoachVonUng_DX_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_KHV_PhanBoVon_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_KHV_PhanBoVon_DonVi");

            migrationBuilder.DropTable(
                name: "VDT_KHV_PhanBoVon_DonVi_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet");

            migrationBuilder.DropTable(
                name: "VDT_KT_KhoiTao");

            migrationBuilder.DropTable(
                name: "VDT_KT_KhoiTao_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_KT_KhoiTao_DuLieu");

            migrationBuilder.DropTable(
                name: "VDT_KT_KhoiTao_DuLieu_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan");

            migrationBuilder.DropTable(
                name: "VDT_NC_NhuCauChi");

            migrationBuilder.DropTable(
                name: "VDT_NC_NhuCauChi_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_QDDT_KHLCNhaThau");

            migrationBuilder.DropTable(
                name: "VDT_QT_BCQuyetToanNienDo");

            migrationBuilder.DropTable(
                name: "VdtQtBcquyetToanNienDoChiTiets");

            migrationBuilder.DropTable(
                name: "VDT_QT_BCQuyetToanNienDo_ChiTiet_01");

            migrationBuilder.DropTable(
                name: "VDT_QT_BCQuyetToanNienDo_PhanTich");

            migrationBuilder.DropTable(
                name: "VDT_QT_DeNghiQuyetToan");

            migrationBuilder.DropTable(
                name: "VDT_QT_DeNghiQuyetToan_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_QT_DeNghiQuyetToan_Nguonvon");

            migrationBuilder.DropTable(
                name: "VDT_QT_DeNghiQuyetToanNienDo");

            migrationBuilder.DropTable(
                name: "VDT_QT_DeNghiQuyetToanNienDo_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_QT_QuyetToan");

            migrationBuilder.DropTable(
                name: "VDT_QT_QuyetToan_ChiPhi");

            migrationBuilder.DropTable(
                name: "VDT_QT_QuyetToan_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_QT_QuyetToan_Nguonvon");

            migrationBuilder.DropTable(
                name: "VDT_QT_QuyetToan_NguonVon_ChenhLech");

            migrationBuilder.DropTable(
                name: "VDT_QT_TongHopSoLieu");

            migrationBuilder.DropTable(
                name: "VDT_QT_XuLySoLieu");

            migrationBuilder.DropTable(
                name: "VDT_ThongTri");

            migrationBuilder.DropTable(
                name: "VDT_ThongTri_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_TongHop_NguonNSDauTu");

            migrationBuilder.DropTable(
                name: "VDT_TT_DeNghiThanhToan_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_TT_DeNghiThanhToan_KHV");

            migrationBuilder.DropTable(
                name: "VDT_TT_DeNghiThanhToanUng_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_TT_PheDuyetThanhToan");

            migrationBuilder.DropTable(
                name: "VDT_TT_PheDuyetThanhToan_ChiTiet");

            migrationBuilder.DropTable(
                name: "VDT_TT_ThanhToanQuaKhoBac_ChiTiet");

            migrationBuilder.DropTable(
                name: "HT_NguoiDung");

            migrationBuilder.DropTable(
                name: "HT_Nhom");

            migrationBuilder.DropTable(
                name: "HT_ChucNang");

            migrationBuilder.DropTable(
                name: "HT_Quyen");

            migrationBuilder.DropTable(
                name: "NH_KHTongThe_NhiemVuChi");

            migrationBuilder.DropTable(
                name: "NS_BK_ChungTu");

            migrationBuilder.DropTable(
                name: "NS_DT_ChungTu");

            migrationBuilder.DropTable(
                name: "NS_SKT_ChungTu");

            migrationBuilder.DropTable(
                name: "TL_DM_CanBo");

            migrationBuilder.DropTable(
                name: "VDT_KHV_KeHoachVonUng");

            migrationBuilder.DropTable(
                name: "VDT_KHV_PhanBoVon");

            migrationBuilder.DropTable(
                name: "VDT_KHV_PhanBoVon_DonVi_PheDuyet");

            migrationBuilder.DropTable(
                name: "VDT_TT_DeNghiThanhToan");

            migrationBuilder.DropTable(
                name: "VDT_TT_DeNghiThanhToanUng");

            migrationBuilder.DropTable(
                name: "VDT_TT_ThanhToanQuaKhoBac");

            migrationBuilder.DropTable(
                name: "HT_LoaiQuyen");

            migrationBuilder.DropTable(
                name: "NH_DM_NhiemVuChi");

            migrationBuilder.DropTable(
                name: "TL_DM_CapBac");

            migrationBuilder.DropTable(
                name: "TL_DM_ChucVu");

            migrationBuilder.DropTable(
                name: "TL_DM_DonVi");

            migrationBuilder.DropSequence(
                name: "VDT_DM_LoaiCongTrinh_SEQ");
        }
    }
}
