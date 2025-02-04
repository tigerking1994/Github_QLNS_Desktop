using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NHTHTongHopQuery
    {
        public Guid Id { get; set; }
        public Guid iID_ChungTu { get; set; }
        public Guid? iID_DuAnID { get; set; }
        public Guid? iID_HopDongID { get; set; }
        public string sMaNguon { get; set; }
        public string sMaNguonCha { get; set; }
        public string sMaDich { get; set; }
        public double? fGiaTriUsd { get; set; }
        public double? fGiaTriVnd { get; set; }
        public Guid? iID_TiGia { get; set; }
        public Guid? iID_MucLucNganSach { get; set; }
        public int iStatus { get; set; }
        public bool bIsLog { get; set; }
        public int? ICoQuanThanhToan { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public Guid? IIDKHTTNhiemVuChiID { get; set; }
    }
}
