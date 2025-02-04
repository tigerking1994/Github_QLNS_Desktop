using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhHdnkPhuongAnNhapKhauModel : ModelBase
    {
        
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public Guid? IIdQddauTuId { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }

        private string _sSoQuyetDinh;
        [ValidateAttribute("Số quyết định", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private DateTime? _dNgayQuyetDinh;
        [ValidateAttribute("Ngày quyết định", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }
        //[ValidateAttribute("Tỉ giá", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdTiGiaId { get; set; }
        //[ValidateAttribute("Mã ngoại tệ khác", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SMaNgoaiTeKhac { get; set; }
        public Guid? IIdPhuongAnNhapKhauGocId { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsActive { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool BIsGoc { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool BIsXoa { get; set; }
        public int? ILoai { get; set; }
        public string SLoaiSoCu { get; set; }

        // Another properties
        public string STenDonVi { get; set; }
        public string STenChuongTrinh { get; set; }
        public string SDieuChinhTu { get; set; }
        [ValidateAttribute("Số kế hoạch tổng thể BQP", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid IIdKhTongTheId { get; set; }
        public string SSoSoCu { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public string STenDuAn { get; set; }
        public ObservableCollection<NhDaGoiThauModel> NhDaGoiThaus { get; set; }
    }
}
