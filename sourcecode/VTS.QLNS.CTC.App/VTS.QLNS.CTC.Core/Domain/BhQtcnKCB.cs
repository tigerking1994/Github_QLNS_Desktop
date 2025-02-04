using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhQtcnKCB : EntityBase
    {
        [Column("ID_QTC_Nam_KCB_QuanYDonVi")]
        [Key]
        public override Guid Id { get; set; }
        public Guid? IIdDonVi { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public bool BThucChiTheo4Quy { get; set; }
        public int? INamLamViec { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string STongHop { get; set; }
        public Guid? IITongHopID { get; set; }
        public int ILoaiTongHop { get; set; }
        public bool BIsKhoa { get; set; }
        public Double? FTongTienDuToanNamTruocChuyenSang { get; set; }
        public Double? FTongTienDuToanGiaoNamNay { get; set; }
        public Double? FTongTienTongDuToanDuocGiao { get; set; }
        public Double? FTongTienThucChi { get; set; }
        public Double? FTongTienThua { get; set; }
        public Double? FTongTienThieu { get; set; }
        public Double? FTiLeThucHienTrenDuToan { get; set; }
        public string SDSSoChungTuTongHop { get; set; }
        public bool BDaTongHop { get; set; }
        public string SDSLNS { get; set; }
    }
}
