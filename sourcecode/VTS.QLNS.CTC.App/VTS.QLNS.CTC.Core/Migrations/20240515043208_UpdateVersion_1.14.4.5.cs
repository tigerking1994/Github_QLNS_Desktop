using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11445 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tl_DS_CanBo_NghiHuu_KeHoach",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Is_Nam = table.Column<bool>(nullable: false),
                    Ma_Hieu_CanBo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    Ma_CB = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CV = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Nam = table.Column<int>(nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: false),
                    Parent = table.Column<string>(nullable: true),
                    Ten_CanBo = table.Column<string>(maxLength: 150, nullable: true),
                    MoTa = table.Column<string>(maxLength: 200, nullable: true),
                    Ten_DonVi = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tl_DS_CanBo_NghiHuu_KeHoach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TL_DS_CBNH_KeHoach",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    HeSoLuong = table.Column<decimal>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: true),
                    Is_Nam = table.Column<bool>(nullable: true),
                    Loai = table.Column<string>(maxLength: 200, nullable: true),
                    Ma_CanBo = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CB = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_CV = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_Hieu_CanBo = table.Column<string>(unicode: false, maxLength: 127, nullable: true),
                    Ma_PBan = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Ma_TangGiam = table.Column<string>(maxLength: 50, nullable: true),
                    Nam = table.Column<int>(nullable: true),
                    Nam_TN = table.Column<int>(nullable: true),
                    Ngay_NN = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_TN = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_XN = table.Column<DateTime>(type: "datetime", nullable: true),
                    PCCV = table.Column<bool>(nullable: true),
                    Parent = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Readonly = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    Ten_CanBo = table.Column<string>(maxLength: 150, nullable: true),
                    Ten_DonVi = table.Column<string>(maxLength: 100, nullable: true),
                    Thang = table.Column<int>(nullable: true),
                    Thang_TNN = table.Column<int>(nullable: true),
                    UserCreator = table.Column<string>(maxLength: 255, nullable: true),
                    UserModifier = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_DS_CBNH_KeHoach", x => x.Id);
                    //table.ForeignKey(
                    //    name: "FK_TL_DS_CBNH_KeHoach_TL_DM_CapBac_Ma_CB",
                    //    column: x => x.Ma_CB,
                    //    principalTable: "TL_DM_CapBac",
                    //    principalColumn: "Ma_Cb",
                    //    onDelete: ReferentialAction.Restrict);
                    //table.ForeignKey(
                    //    name: "FK_TL_DS_CBNH_KeHoach_TL_DM_ChucVu_Ma_CV",
                    //    column: x => x.Ma_CV,
                    //    principalTable: "TL_DM_ChucVu",
                    //    principalColumn: "Ma_Cv",
                    //    onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TL_DS_CBNH_KeHoach_Ma_CB",
                table: "TL_DS_CBNH_KeHoach",
                column: "Ma_CB");

            migrationBuilder.CreateIndex(
                name: "IX_TL_DS_CBNH_KeHoach_Ma_CV",
                table: "TL_DS_CBNH_KeHoach",
                column: "Ma_CV");


            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.5_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.5_social_insurance_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tl_DS_CanBo_NghiHuu_KeHoach");

            migrationBuilder.DropTable(
                name: "TL_DS_CBNH_KeHoach");
        }
    }
}
