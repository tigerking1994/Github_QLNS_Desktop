using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaHopDongHangMucModel : CurrencyDetailModelBase
    {
        public Guid IIdHopDongChiPhiId { get; set; }
        public Guid? IIdGoiThauHangMucId { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SMaHangMuc { get; set; }
        public string STenHangMuc { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaOrder { get; set; }
        public string SThanhToanBang { get; set; }
        public double? FGiaTriNgoaiTeKhacDuyet { get; set; }
        public double? FGiaTriUSDDuyet { get; set; }
        public double? FGiaTriEURDuyet { get; set; }
        public double? FGiaTriVNDDuyet { get; set; }
        public Guid IIdHangMucId { get; set; }
        public Guid? IIdHangMucParentId { get; set; }
        public Guid? IIdKeHoachDatHangDanhMucId { get; set; }
        public Guid? IIdHopDongNhaThauId { get; set; }
        public int? STT { get; set; }
        public string SGhiChu { get; set; }

        [NotMapped]
        private bool _isNotCheckedAll;
        [NotMapped]
        public bool IsNotCheckedAll
        {
            get => _isNotCheckedAll;
            set => SetProperty(ref _isNotCheckedAll, value);
        }
        [NotMapped]
        public string Order { get; set; }

        private string _sDonViTinh;
        public string SDonViTinh
        {
            get => _sDonViTinh;
            set => SetProperty(ref _sDonViTinh, value);
        }

        private int? _iSoLuong;
        public int? ISoLuong
        {
            get => _iSoLuong;
            set => SetProperty(ref _iSoLuong, value);
        }

        private double? _fDonGia;
        public double? FDonGia
        {
            get => _fDonGia;
            set => SetProperty(ref _fDonGia, value);
        }
        public List<ComboboxItem> _itemsThanhToanBang;
        public List<ComboboxItem> ItemsThanhToanBang
        {
            get => _itemsThanhToanBang;
            set => SetProperty(ref _itemsThanhToanBang, value);
        }
        public ComboboxItem _SelectThanhToanBang;
        public ComboboxItem SelectThanhToanBang
        {
            get => _SelectThanhToanBang;
            set => SetProperty(ref _SelectThanhToanBang, value);
        }
        private bool _isChirenl;
        public bool IsChirenl
        {
            get => _isChirenl;
            set => SetProperty(ref _isChirenl, value);
        }

    }
}
