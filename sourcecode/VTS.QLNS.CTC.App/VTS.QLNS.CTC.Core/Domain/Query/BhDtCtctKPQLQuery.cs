using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDtCtctKPQLQuery
    {
        [Column("ID")]
        public Guid ID { get; set; }
        [Column("iID_ChungTuChiTiet")]
        public Guid? IIDChungTuChiTiet { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("iID_ChungTu")]
        public Guid? IIDChungTu { get; set; }
        [Column("sLNS")]
        public string SLNS { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("sTM")]
        public string STM { get; set; }
        [Column("sTTM")]
        public string STMM { get; set; }
        [Column("sNG")]
        public string SNG { get; set; }
        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }
        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }
        [Column("fSoTien")]
        public double? FSoTien { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("iID_MLNS")]
        public Guid? IID_MLNS { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? IID_MLNS_Cha { get; set; }
        public string SNoiDung { get; set; }
        public bool BHangCha { get; set; }
        public bool IsHangCha { get; set; }
        public bool IsRemainRow { get; set; }
        public int STT { get; set; }
        public string STenDonVi { get; set; }
    }
}
