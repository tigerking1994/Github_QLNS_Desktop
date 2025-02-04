using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11433 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BH_QTC_ChungTuChiTiet_GiaiThich",
                columns: table => new
                {
                    iID_QT_CTCT_GiaiThich = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_QTC_QChungTu = table.Column<Guid>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iQuy = table.Column<int>(nullable: false),
                    sLNS = table.Column<string>(maxLength: 50, nullable: true),
                    sMaLoaiChi = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sMoTa_KienNghi = table.Column<string>(nullable: true),
                    sMoTa_TinhHinh = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_ChungTuChiTiet_GiaiThich", x => x.iID_QT_CTCT_GiaiThich)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.3_social_insurance_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_QTC_ChungTuChiTiet_GiaiThich");
        }
    }
}
