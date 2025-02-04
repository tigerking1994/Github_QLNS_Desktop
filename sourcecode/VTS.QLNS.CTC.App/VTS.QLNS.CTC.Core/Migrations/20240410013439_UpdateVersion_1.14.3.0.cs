using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11430 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TL_QS_ChungTuChiTiet_NQ104",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BinhNhat = table.Column<double>(nullable: true),
                    BinhNhi = table.Column<double>(nullable: true),
                    CCQP = table.Column<double>(nullable: true),
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
                    table.PrimaryKey("PK_TL_QS_ChungTuChiTiet_NQ104", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_QS_ChungTu_NQ104",
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
                    table.PrimaryKey("PK_TL_QS_ChungTu_NQ104", x => x.ID);
                    //table.ForeignKey(
                    //    name: "FK_TL_QS_ChungTu_NQ104_TL_DM_DonVi_NQ104_Ma_DonVi",
                    //    column: x => x.Ma_DonVi,
                    //    principalTable: "TL_DM_DonVi_NQ104",
                    //    principalColumn: "Ma_DonVi",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TL_QS_ChungTu_NQ104_Ma_DonVi",
                table: "TL_QS_ChungTu_NQ104",
                column: "Ma_DonVi");

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.0_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.0_social_insurance_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_QS_ChungTuChiTiet_NQ104");

            migrationBuilder.DropTable(
                name: "TL_QS_ChungTu_NQ104");
        }
    }
}
