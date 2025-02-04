using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class BhQtcQuyKinhPhiQuanLy : EntityBase
    {
        [Column("ID_QTC_Quy_KinhPhiQuanLy")]
        [Key]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        public Guid? IID_DonVi { get; set; }
        public string IID_MaDonVi { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? IQuyChungTu { get; set; }
        public int? INamChungTu { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string STongHop { get; set; }
        public Guid? IID_TongHopID { get; set; }
        public string SDSLNS { get; set; }
        public int ILoaiTongHop { get; set; }
        public bool BIsKhoa { get; set; }
        public double? FTongTienDuToanDuocGiao { get; set; }
        public double? FTongTienThucChi { get; set; }
        public double? FTongTienQuyetToanDaDuyet { get; set; }
        public double? FTongTienDeNghiQuyetToanQuyNay { get; set; }
        public double? FTongTienXacNhanQuyetToanQuyNay { get; set; }
    }
}
