using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcQBHXHChiTietGiaiThichQuery
    {
        [Column("iID_QTC_ChungTuChiTiet_GiaiThich")]
        public Guid Id { get; set; }
        [Column("iID_QTC_ChungTu")]
        public Guid IID_QTC_QChungTu { get; set; }
        public int INamLamViec { get; set; }
        public int IQuy { get; set; }
        public string SMoTa { get; set; }
        public string SMoTa_KienNghi { get; set; }
        public string SMoTa_TinhHinh { get; set; }
        public string IID_MaDonVi { get; set; }
        public string SMaLoaiChi { get; set; }
        public string SLNS { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
    }
}
