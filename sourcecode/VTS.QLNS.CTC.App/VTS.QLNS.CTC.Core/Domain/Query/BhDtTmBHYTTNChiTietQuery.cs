using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDtTmBHYTTNChiTietQuery
    {
        [Column("iID_DTTM_BHYT_ThanNhan_ChiTiet")]
        public Guid Id { get; set; }
        [Column("iID_DTTM_BHYT_ThanNhan")]
        public Guid IID_DTTM_BHYT_ThanNhan { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("iID_MLNS")]
        public Guid IID_MLNS { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("fDuToan")]
        public double? FDuToan { get; set; }
        [Column("sLNS")]
        public string SLNS { get; set; }
        [Column("sNoiDung")]
        public string SNoiDung { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
    }
}
