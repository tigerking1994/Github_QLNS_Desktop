using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcQuyKPKQuery
    {
        [Column("ID_QTC_Quy_KPK")]
        public Guid Id { get; set; }
        public Guid? IID_DonVi { get; set; }
        public string IID_MaDonVi { get; set; }
        public Guid IID_LoaiChi { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
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
        public double? FTongTien_DuToanNamTruocChuyenSang { get; set; }
        public double? FTongTien_DuToanGiaoNamNay { get; set; }
        public double? FTongTien_TongDuToanDuocGiao { get; set; }
        public double? FTongTienThucChi { get; set; }
        public double? FTongTienQuyetToanDaDuyet { get; set; }
        public double? FTongTienDeNghiQuyetToanQuyNay { get; set; }
        public double? FTongTienXacNhanQuyetToanQuyNay { get; set; }
        public string STenDonVi { get; set; }
        public string STenDanhMucLoaiChi { get; set; }
        public string SDSLNS { get; set; }
    }
}
