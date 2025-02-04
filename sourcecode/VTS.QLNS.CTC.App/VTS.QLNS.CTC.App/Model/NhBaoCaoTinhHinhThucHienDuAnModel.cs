using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhBaoCaoTinhHinhThucHienDuAnModel : BindableBase
    {
        public string Stt { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? IdParent { get; set; }
        public Guid? IdHopDong { get; set; }
        public string sSoDeNghi { get; set; }
        public DateTime? dNgayDeNghi { get; set; }
        public string NgayDeNghi { get; set; }
        public string sChuDauTu { get; set; }
        public string sLoaiNoiDung { get; set; }
        public string sCoQuanTT { get; set; }
        public string sLoaiDeNghiTT { get; set; }
        public double? fTongDeNghi_USD { get; set; }
        public double? fTongDeNghi_VND { get; set; }
        public double? fTongPheDuyet_BangSo_USD { get; set; }
        public double? fTongPheDuyet_BangSo_VND { get; set; }
        public double? fGiaTriDuocCap_USD { get; set; }
        public double? fGiaTriDuocCap_VND { get; set; }
        public double? fGiaTriTTTU_USD { get; set; }
        public double? fGiaTriTTTU_VND { get; set; }
        public string Mlns { get; set; }
        public string TenNhaThau { get; set; }
        public int? iLoaiNoiDungChi { get; set; }
        public int? iCoQuanThanhToan { get; set; }
        public int? iLoaiDeNghi { get; set; }
        public string SoHopDong { get; set; }
        public int? ThoiGianThucHien { get; set; }
        public double? GiaTriHopDong { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public double? SoThanhToan { get; set; }
        public double? SoTamUng { get; set; }
        public double? SoThuHoiTamUng { get; set; }
        public double? TongCongGiaiNgan { get; set; }
        public bool? IsHangCha { get; set; }
        private bool _hasChildren;
        public bool HasChildren
        {
            get => _hasChildren;
            set => SetProperty(ref _hasChildren, value);
        }
        public HashSet<Guid> AncestorIds { get; set; }
        private bool _isShowChildren;
        public bool IsShowChildren
        {
            get => _isShowChildren;
            set => SetProperty(ref _isShowChildren, value);
        }

        private bool _isExpandGroup;
        public bool IsExpandGroup
        {
            get => _isExpandGroup;
            set => SetProperty(ref _isExpandGroup, value);
        }

    }
}
