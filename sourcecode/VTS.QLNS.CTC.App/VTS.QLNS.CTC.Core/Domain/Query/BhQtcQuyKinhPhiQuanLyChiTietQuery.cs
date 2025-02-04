using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcQuyKinhPhiQuanLyChiTietQuery
    {
        [Column("ID_QTC_Quy_KinhPhiQuanLy_ChiTiet")]
        public Guid Id { get; set; }
        public Guid? IID_QTC_Quy_KinhPhiQuanLy { get; set; }
        public Guid? IID_MucLucNganSach { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string SNoiDung { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public double? FTienDuToanDuocGiao { get; set; }
        public double? FTienThucChi { get; set; }
        public double? FTienQuyetToanDaDuyet { get; set; }
        public double? FTienDeNghiQuyetToanQuyNay { get; set; }
        public double? FTienXacNhanQuyetToanQuyNay { get; set; }
        public string SGhiChu { get; set; }
        [NotMapped]
        public string Stt { get; set; }
        [NotMapped]
        public string SMoTa { get; set; }
        [NotMapped]
        public string SXauNoiMa { get; set; }
        [NotMapped]
        public int INamLamViec { get; set; }

        [NotMapped]
        public string Nganh { get; set; }

        public string STenDonVi { get; set; }
        [NotMapped]
        public Guid? IdParent { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public bool BHangCha { get; set; }
  
        public string STTM { get; set; }
        [NotMapped]
        public string SChiTietToi { get; set; }
        [NotMapped]
        public string SLNS { get; set; }
        [NotMapped]
        public string SL { get; set; }
        [NotMapped]
        public string SK { get; set; }

        [NotMapped]
        public string SNG { get; set; }
        [NotMapped]
        public string STNG { get; set; }
        [NotMapped]
        public string STNG1 { get; set; }
        [NotMapped]
        public string STNG2 { get; set; }
        [NotMapped]
        public string STNG3 { get; set; }
        [NotMapped]
        public string BHangChaDuToan { get; set; }
        [NotMapped]
        public Guid IID_MLNS { get; set; }
        [NotMapped]
        public Guid IID_MLNS_Cha { get; set; }
        [NotMapped]
        public bool IsHasData => FTienDuToanDuocGiao.GetValueOrDefault(0) != 0 || FTienThucChi.GetValueOrDefault(0) != 0 || FTienQuyetToanDaDuyet.GetValueOrDefault(0) != 0 || FTienXacNhanQuyetToanQuyNay.GetValueOrDefault(0) != 0;

        public string SDuToanChiTietToi { get;set; }
        [Column("IID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
    }
}
