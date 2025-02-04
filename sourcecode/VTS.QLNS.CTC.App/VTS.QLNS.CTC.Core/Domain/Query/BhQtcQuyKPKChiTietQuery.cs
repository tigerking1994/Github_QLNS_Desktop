using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcQuyKPKChiTietQuery
    {
        [Column("ID_QTC_Quy_KPK_ChiTiet")]
        [Key]
        public Guid Id { get; set; }
        public Guid? IID_QTC_Quy_KPK { get; set; }
        public Guid? IID_MucLucNganSach { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string SNoiDung { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public double? FTien_DuToanNamTruocChuyenSang { get; set; }
        public double? FTien_DuToanGiaoNamNay { get; set; }
        public double? FTien_TongDuToanDuocGiao { get; set; }
        public double? FTienThucChi { get; set; }
        public double? FTienQuyetToanDaDuyet { get; set; }
        public double? FTienDeNghiQuyetToanQuyNay { get; set; }
        public double? FTienXacNhanQuyetToanQuyNay { get; set; }
        [NotMapped]
        public string Stt { get; set; }
        [NotMapped]
        public string SMoTa { get; set; }
        public string SXauNoiMa { get; set; }
        [Column("INamLamViec")]
        public int INamLamViec { get; set; }
        [NotMapped]
        public string Nganh { get; set; }
        public string STenDonVi { get; set; }
        public string IID_MaDonVi { get; set; }
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
    
        public string SLNS { get; set; }
    
        public string SL { get; set; }
  
        public string SK { get; set; }

 
        public string SNG { get; set; }

        public string STNG { get; set; }

        public string STNG1 { get; set; }
  
        public string STNG2 { get; set; }

        public string STNG3 { get; set; }
        [Column("iIMaDonVi")]
        public string IIdMaDonVi { get; set; }
        public string BHangChaDuToan { get; set; }
        [NotMapped]
        public Guid IID_MLNS { get; set; }
        [NotMapped]
        public Guid IID_MLNS_Cha { get; set; }
        [NotMapped]
        public bool IsHasData => FTien_TongDuToanDuocGiao.GetValueOrDefault() != 0
                    || FTienQuyetToanDaDuyet.GetValueOrDefault() != 0
                    || FTienThucChi.GetValueOrDefault() != 0
                    || FTien_DuToanNamTruocChuyenSang.GetValueOrDefault() != 0
                    || FTien_DuToanGiaoNamNay.GetValueOrDefault() != 0
                    || FTienDeNghiQuyetToanQuyNay.GetValueOrDefault() != 0
                    || FTien_TongDuToanDuocGiao.GetValueOrDefault() != 0;
        public string SDuToanChiTietToi { get;set; }
    }
}
