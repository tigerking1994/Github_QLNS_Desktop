using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class BhKhcK : EntityBase
    {
        [Column("iID_BH_KHC_K")]
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
        public Guid IIDLoaiChi { get; set; }
        public double? FTongTienDaThucHienNamTruoc { get; set; }
        public double? FTongTienUocThucHienNamTruoc { get; set; }
        public double? FTongTienKeHoachThucHienNamNay { get; set; }
        public string STongHop { get; set; }
        public Guid? IID_TongHopID { get; set; }
        public bool BDaTongHop { get; set; }
        public int ILoaiTongHop { get; set; }
        public bool BIsKhoa { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string SMaLoaiChi { get; set; }
    }
}
