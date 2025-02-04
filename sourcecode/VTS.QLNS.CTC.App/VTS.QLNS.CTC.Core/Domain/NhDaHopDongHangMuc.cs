using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhDaHopDongHangMuc : EntityBase
    {
        public Guid? IIdHopDongChiPhiId { get; set; }
        public Guid? IIdCacQuyetDinhHangMucId { get; set; }
        public Guid? IIdGoiThauHangMucId { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdKeHoachDatHangDanhMucId { get; set; }
        public Guid? IIdHopDongNhaThauId { get; set; }
        public string SMaHangMuc { get; set; }
        public string STenHangMuc { get; set; }
        public string STenChiPhi { get; set; }
        public string SDonViTinh { get; set; }
        public int? ISoLuong { get; set; }
        public double? FDonGia { get; set; }
        public string SMaOrder { get; set; }
        public string SThanhToanBang { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriEur { get; set; }
        public double? FGiaTriVnd { get; set; }
        public string SGhiChu { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; }
    }
}
