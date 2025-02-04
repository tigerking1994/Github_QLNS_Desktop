using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11299 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BH_KHTM_BHYT",
                columns: table => new
                {
                    iID_KHTM_BHYT = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fThuBHYTNLDDong = table.Column<double>(nullable: true),
                    fThuBHYTNSDDong = table.Column<double>(nullable: true),
                    fTong = table.Column<double>(nullable: true),
                    fTongBHYT = table.Column<double>(nullable: true),
                    fTongKeHoach = table.Column<double>(nullable: true),
                    iID_TongHop = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: true),
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
                    table.PrimaryKey("PK_BH_KHTM_BHYT", x => x.iID_KHTM_BHYT)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.9.9.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_KHTM_BHYT");
        }
    }
}
