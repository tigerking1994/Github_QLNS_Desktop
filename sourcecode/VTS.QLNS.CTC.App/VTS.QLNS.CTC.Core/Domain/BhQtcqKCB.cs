using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhQtcqKCB : EntityBase
    {
        [Column("ID_QTC_Quy_KCB")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IIdDonVi { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get;set; }
        public DateTime DNgayQuyetDinh { get; set; }
        public int IQuyChungTu { get; set; }
        public int INamChungTu { get;set; }
        public string SMoTa { get;set; }
        public DateTime? DNgaySua { get;set; }
        public DateTime? DNgayTao { get;set; }
        public string SNguoiSua { get;set; }
        public string SNguoiTao { get;set; }
        public string STongHop { get;set; }
        public Guid? IIdTongHopID { get; set; }
        public int ILoaiTongHop { get;set; }
        public bool BIsKhoa { get;set; }
        public Double? FTongTienDuToanNamTruocChuyenSang { get;set; }
        public Double? FTongTienDuToanGiaoNamNay { get;set; }
        public Double? FTongTienTongDuToanDuocGiao { get;set; }
        public Double? FTongTienThucChi { get;set; }
        public Double? FTongTienQuyetToanDaDuyet { get;set; }
        public Double? FTongTienDeNghiQuyetToanQuyNay { get;set; }
        public Double? FTongTienXacNhanQuyetToanQuyNay { get; set; }
        public bool BDaTongHop { get; set; }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
        public string SDSSoChungTuTongHop { get; set; }
        public string SDSLNS { get;set; }
    }
}
