using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcNamKinhPhiQuanLyChiTietQuery
    {
        [Column("ID_QTC_Nam_KinhPhiQuanLy_ChiTiet")]
        public Guid Id { get; set; }
        public Guid? IID_QTC_Nam_KinhPhiQuanLy { get; set; }
        public Guid? IID_MucLucNganSach { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string SNoiDung { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public double? FTien_DuToanNamTruocChuyenSang { get; set; }
        public double? FDuToanNamTruocChuyenSang { get; set; }
        public double? FTien_DuToanGiaoNamNay { get; set; }
        public double? FTien_TongDuToanDuocGiao { get; set; }
        public double? FTien_ThucChi { get; set; }
        public double? FTienThua { get; set; }
        public double? FTienThieu { get; set; }
        public double? FTiLeThucHienTrenDuToan { get; set; }
        public string SL { get; set; }
        public string SXauNoiMa { get; set; }
        public int? INamLamViec { get; set; }
        public string IIdMaDonVi { get; set; }
        [NotMapped]
        public string Stt { get; set; }
        [NotMapped]
        public string SMoTa { get; set; }

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
        public bool IsHadData => FTien_DuToanGiaoNamNay.GetValueOrDefault(0) != 0;
        public string SDuToanChiTietToi { get; set; }
    }
}
