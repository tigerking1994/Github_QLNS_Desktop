using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11261 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sNgaySua",
                table: "NS_QT_ChungTuChiTiet_GiaiThich",
                newName: "sNguoiSua");

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.6.1.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sNguoiSua",
                table: "NS_QT_ChungTuChiTiet_GiaiThich",
                newName: "sNgaySua");
        }
    }
}
