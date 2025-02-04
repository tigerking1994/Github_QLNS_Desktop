using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhQtcqBHXH : EntityBase
    {
        [Column("ID_QTC_Quy_CheDoBHXH")]
        [Key]
        public override Guid Id { get; set; }
        public Guid? IIdDonVi { get; set; }
        public string IIdMaDonVi { get;set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get;set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime DNgayQuyetDinh { get; set; }
        public int IQuyChungTu { get; set; }
        public int INamChungTu { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string STongHop { get; set; }
        public Guid? IID_TongHopID { get; set; }
        public int ILoaiTongHop { get; set; }
        public bool BIsKhoa { get; set; }
        public Double? FTongTienDuToanDuyet { get; set; }
        public int? ITongSoLuyKeCuoiQuyNay { get; set; }
        public Double? FTongTienLuyKeCuoiQuyNay { get; set; }
        public int? ITongSoSQDeNghi { get; set; }
        public Double? FTongTienSQDeNghi { get; set; }
        public int? ITongSoQNCNDeNghi { get; set; }
        public Double? FTongTienQNCNDeNghi { get; set; }
        public int? ITongSoCNVCQPDeNghi { get; set; }
        public Double? FTongTienCNVCQPDeNghi { get; set; }
        public int? ITongSoHSQBSDeNghi { get; set; }
        public Double? FTongTienHSQBSDeNghi { get; set; }
        public int? ITongSoHDLDDeNghi { get; set; }
        public Double? FTongTienHDLDDeNghi { get; set; }
        public int? ITongSoDeNghi { get; set; }
        public Double? FTongTienDeNghi { get; set; }
        public Double? FTongTienPheDuyet { get; set; }
        public string SDSSoChungTuTongHop { get;set; }
        public string SDSLNS { get; set; }
        public bool? BDaTongHop { get; set; }
    }
}
