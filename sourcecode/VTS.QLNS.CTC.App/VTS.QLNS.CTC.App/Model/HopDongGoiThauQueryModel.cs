using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class HopDongGoiThauQueryModel : ModelBase
    {
        private bool _isSelected;
        public new bool IsSelected
        {
            get => _isSelected;
            set
            {
                SetProperty(ref _isSelected, value);
                OnPropertyChanged(nameof(IsEditChiPhi));
            }
        }

        public Guid Id { get; set; }
        public Guid? HopDongId { get; set; }
        public Guid GoiThauId { get; set; }
        public Guid? NhaThauId { get; set; }
        public Guid IdHopDongGoiThauNhaThau { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string SMaGoiThau { get; set; }
        public string STenGoiThau { get; set; }
        public double FTienTrungThau { get; set; }
        public double FGiaTriConLai { get; set; }
        public double? FGiaTriTrungThauTruocDC { get; set; }

        private double _fGiaTriGoiThau = 0;
        public double FGiaTriGoiThau
        {
            get => _fGiaTriGoiThau;
            set
            {
                SetProperty(ref _fGiaTriGoiThau, value);
                OnPropertyChanged(nameof(FGiaTriConLaiSauChinhSua));
            }
        }

        private double _fGiaTriChiPhiTrongGoiTHauKhac;
        public double FGiatriSuDungTrongGoiThauKhac
        {
            get => _fGiaTriChiPhiTrongGoiTHauKhac;
            set
            {
                SetProperty(ref _fGiaTriChiPhiTrongGoiTHauKhac, value);
                OnPropertyChanged(nameof(FGiaTriConLaiSauChinhSua));
            }
        }

        public double FGiaTriConLaiSauChinhSua => FGiaTriConLai - FGiaTriGoiThau - FGiatriSuDungTrongGoiThauKhac;

        public double _fGiaTriTrungThau;
        public double FGiaTriTrungThau 
        {
            get => _fGiaTriTrungThau;
            set => SetProperty(ref _fGiaTriTrungThau, value);
        }
        public double FGiaTriDaSD { get; set; }
        public bool IsEditChiPhi => IsSelected;

        private double _fGiaTriHopDong;
        public double FGiaTriHopDong
        {
            get => _fGiaTriHopDong;
            set => SetProperty(ref _fGiaTriHopDong, value);
        }

        private string _sThoiGianThucHien;
        public string SThoiGianThucHien
        {
            get => _sThoiGianThucHien;
            set => SetProperty(ref _sThoiGianThucHien, value);
        }

        public double? FGiaTriHopDongTruocDieuChinh { get; set; }

        public ObservableCollection<HopDongChiPhiQueryModel> ListChiPhi { get; set; }

        public HopDongGoiThauQueryModel Clone()
        {
            var goithau = new HopDongGoiThauQueryModel();
            goithau.IdHopDongGoiThauNhaThau = Guid.NewGuid();
            goithau.GoiThauId = GoiThauId;
            goithau.IIdDuAnId = IIdDuAnId;
            goithau.STenGoiThau = STenGoiThau;
            goithau.FTienTrungThau = FTienTrungThau;
            goithau.FGiaTriGoiThau = 0;
            goithau.FGiaTriConLai = FGiaTriConLai;
            goithau.ListChiPhi = new ObservableCollection<HopDongChiPhiQueryModel>(ListChiPhi.Select(cp => cp.clone()));
            return goithau;
        }
    }
}
