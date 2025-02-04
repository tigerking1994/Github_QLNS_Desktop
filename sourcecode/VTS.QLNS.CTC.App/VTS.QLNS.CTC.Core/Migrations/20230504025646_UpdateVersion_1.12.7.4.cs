using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11274 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "bCapNhat",
                table: "TL_CanBo_PhuCap",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bCapNhat",
                table: "TL_CanBo_PhuCap");
        }
    }
}
