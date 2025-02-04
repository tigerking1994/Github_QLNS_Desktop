using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtTtDeNghiThanhToanUngChiTietModel : DetailModelBase
    {
        public string sMaDuAn { get; set; }
        public string sTenDuAn { get; set; }
        public string sTenPhanCapDuAn { get; set; }
        public Guid? iID_DuAnID { get; set; }

        private Guid? _iId_HopDongId;
        public Guid? iId_HopDongId
        {
            get => _iId_HopDongId;
            set => SetProperty(ref _iId_HopDongId, value);
        }

        public Guid? iId_NhaThauId { get; set; }
        public double? fGiaTriThanhToan { get; set; }
        public double? fGiaTriTamUng { get; set; }
        public double? fGiaTriThuHoiUngNgoaiChiTieu { get; set; }
        public double? fGiaTriThuHoi { get; set; }
        public Guid? iId_DonViTienTeId { get; set; }
        public Guid? iId_TienTeId { get; set; }
        public Guid? iId_GoiThau { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public double? fTiGia { get; set; }
        public double? fLuyKeUng { get; set; }

        private double? _fLuyKeThanhToan;
        public double? fLuyKeThanhToan
        {
            get => _fLuyKeThanhToan;
            set => SetProperty(ref _fLuyKeThanhToan, value);
        }

        private double? _fLuyKeChiTieu;
        public double? fLuyKeChiTieu
        {
            get => _fLuyKeChiTieu;
            set => SetProperty(ref _fLuyKeChiTieu, value);
        }

        private string _sThongTinHopDong;
        public string sThongTinHopDong
        {
            get => _sThongTinHopDong;
            set => SetProperty(ref _sThongTinHopDong, value);
        }

        private string _sThongTinNhaThau;
        public string sThongTinNhaThau
        {
            get => _sThongTinNhaThau;
            set => SetProperty(ref _sThongTinNhaThau, value);
        }
        public string sGhiChu { get; set; }

        private ObservableCollection<ComboboxItem> _cbxHopDong;
        public ObservableCollection<ComboboxItem> CbxHopDong
        {
            get => _cbxHopDong;
            set => SetProperty(ref _cbxHopDong, value);
        }
    }
}
