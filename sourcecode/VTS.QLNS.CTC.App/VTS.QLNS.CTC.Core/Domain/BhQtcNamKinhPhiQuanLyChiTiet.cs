using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class BhQtcNamKinhPhiQuanLyChiTiet : EntityBase
    {
        [Column("ID_QTC_Nam_KinhPhiQuanLy_ChiTiet")]
        [Key]
        public override Guid Id { get => base.Id; set => base.Id = value; }
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
        public double? FTien_DuToanGiaoNamNay { get; set; }
        public double? FTien_TongDuToanDuocGiao { get; set; }
        public double? FTien_ThucChi { get; set; }
        public double? FTienThua { get; set; }
        public double? FTienThieu { get; set; }
        public double? FTiLeThucHienTrenDuToan { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SXauNoiMa { get; set; }
        public int? INamLamViec { get; set; }
        [NotMapped]
        public string SL { get; set; }
    }
}
