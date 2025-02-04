using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhKhcKinhphiQuanly : EntityBase
    {
        [Column("iID_BH_KHC_KinhPhiQuanLy")]
        [Key]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? INamLamViec { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string IID_MaDonVi { get; set; }
        public string SMoTa { get; set; }
        public double? FTongTienDaThucHienNamTruoc { get; set; }
        public double? FTongTienUocThucHienNamTruoc { get; set; }
        public double? FTongTienKeHoachThucHienNamNay { get; set; }
        public double? FTongTienCanBo { get; set; }
        public double? FTongTienQuanLuc { get; set; }
        public double? FTongTienTaiChinh { get; set; }
        public double? FTongTienQuanY { get; set; }
        public string STongHop { get; set; }
        public Guid? IID_TongHopID { get; set; }
        public int ILoaiTongHop { get; set; }
        public bool BIsKhoa { get; set; }
        public bool BDaTongHop { get;set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
    }
}
