using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public  class NsDcChungTuQuery
    {
        [Column("iID_DCChungTu")]
        public Guid Id { get; set; }
        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("iSoChungTuIndex")]
        public int? ISoChungTuIndex { get; set; }
        [Column("dNgayChungTu")]
        public DateTime DNgayChungTu { get; set; }
        [Column("sMota")]
        public string SMoTa { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("sDSLNS")]
        public string SDslns { get; set; }
        [Column("iLoaiChungTu")]
        public int ILoaiChungTu { get; set; }
        [Column("iLoaiDuKien")]
        public int ILoaiDuKien { get; set; }
        [Column("iNamNganSach")]
        public int? INamNganSach { get; set; }
        [Column("iID_MaNguonNganSach")]
        public int? IIdMaNguonNganSach { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("fDieuChinh")]
        public double FDieuChinh { get; set; }
        [Column("bKhoa")]
        public bool BKhoa { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("sTongHop")]
        public string STongHop { get; set; }
        [Column("bDaTongHop")]
        public bool? BDaTongHop { get; set; }
    }
}
