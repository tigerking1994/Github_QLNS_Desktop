using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class KHLCNhaThauDetailModel : DetailModelBase
    {
        public override Guid Id { get; set; }
        public Guid? iID_DuAnID { get; set; }
        public Guid? IdGoiThau { get; set; }
        public string sTenDuAn { get; set; }
        public string sMaGoiThau { get; set; }

        private DateTime? _dNgayLap;
        public DateTime? dNgayLap
        {
            get => _dNgayLap;
            set => SetProperty(ref _dNgayLap, value);
        }

        private string _sTenGoiThau;
        public string sTenGoiThau
        {
            get => _sTenGoiThau;
            set => SetProperty(ref _sTenGoiThau, value);
        }

        private string _sHinhThucChonNhaThau;
        public string sHinhThucChonNhaThau
        {
            get => _sHinhThucChonNhaThau;
            set => SetProperty(ref _sHinhThucChonNhaThau, value);
        }

        private string _sPhuongThucDauThau;
        public string sPhuongThucDauThau
        {
            get => _sPhuongThucDauThau;
            set => SetProperty(ref _sPhuongThucDauThau, value);
        }

        private DateTime? _dBatDauChonNhaThau;
        public DateTime? dBatDauChonNhaThau
        {
            get => _dBatDauChonNhaThau;
            set => SetProperty(ref _dBatDauChonNhaThau, value);
        }

        private string _sHinhThucHopDong;
        public string sHinhThucHopDong
        {
            get => _sHinhThucHopDong;
            set => SetProperty(ref _sHinhThucHopDong, value);
        }

        private double? _iThoiGianThucHien;
        public double? iThoiGianThucHien
        {
            get => _iThoiGianThucHien;
            set => SetProperty(ref _iThoiGianThucHien, value);
        }

        private double? _fTienTrungThau;
        public double? fTienTrungThau
        {
            get => _fTienTrungThau;
            set => SetProperty(ref _fTienTrungThau, value);
        }

        public int iLoai { get; set; }
        public bool IsEdit => ( Id == Guid.Empty) ? false : true;

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }

        private List<ChiPhiHangMucModel> _chiPhiHangMucModels;
        public List<ChiPhiHangMucModel> ChiPhiHangMucModels
        {
            get => _chiPhiHangMucModels;
            set => SetProperty(ref _chiPhiHangMucModels, value);
        }
    }
}
