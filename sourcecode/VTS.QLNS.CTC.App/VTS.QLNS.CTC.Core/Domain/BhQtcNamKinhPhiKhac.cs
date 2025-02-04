using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class BhQtcNamKinhPhiKhac : EntityBase
    {
        [Column("ID_QTC_Nam_KPK")]
        [Key]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        public Guid? IID_DonVi { get; set; }
        public string IID_MaDonVi { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public bool? BThucChiTheo4Quy { get; set; }
        public int? INamLamViec { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string STongHop { get; set; }
        public Guid? IID_TongHopID { get; set; }
        public int ILoaiTongHop { get; set; }
        public bool BIsKhoa { get; set; }
        public double? FTongTien_DuToanNamTruocChuyenSang { get; set; }
        public double? FTongTien_DuToanGiaoNamNay { get; set; }
        public double? FTongTien_TongDuToanDuocGiao { get; set; }
        public double? FTongTien_ThucChi { get; set; }
        public double? FTongTienThua { get; set; }
        public double? FTongTienThieu { get; set; }
        public double? FTiLeThucHienTrenDuToan { get; set; }
        public Guid IID_LoaiChi { get; set; }
        public string SDSLNS { get; set; }
        public bool BDaTongHop { get; set; }
    }
}
