using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhMSTNKeHoachDatHangDanhMuc : EntityBase
    {
        public Guid? IID_KeHoachDatHang { get; set; }
        public string STenDanhMuc { get; set; }
        public string SDonViTinh { get; set; }
        public int? ISoLuong { get; set; }   
        public double? FDonGia_VND { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public Guid? IID_NhaThauID { get; set; }
        public Guid? IID_ParentID { get; set; }
        public string SGhiChu { get; set; }
        public string SMaOrder { get; set; }
    }
}
