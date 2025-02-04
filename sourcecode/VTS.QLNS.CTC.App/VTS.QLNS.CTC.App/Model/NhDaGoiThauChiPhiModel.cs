using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaGoiThauChiPhiModel : CurrencyDetailModelBase
    {
        public Guid? IIdParentId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdQdDauTuChiPhiId { get; set; }
        public Guid? IIdDuToanChiPhiId { get; set; }
        public Guid? IIdCacQuyetDinhChiPhiId { get; set; }

        private double? _ftienGoiThauEur;
        public double? FTienGoiThauEur
        {
            get => _ftienGoiThauEur;
            set
            {
                if (SetProperty(ref _ftienGoiThauEur, value))
                {
                    OnPropertyChanged(nameof(HasValue));
                }
            }
        }
        private double? _fTienGoiThauNgoaiTeKhac;
        public double? FTienGoiThauNgoaiTeKhac
        {
            get => _fTienGoiThauNgoaiTeKhac;
            set
            {
                if (SetProperty(ref _fTienGoiThauNgoaiTeKhac, value))
                {
                    OnPropertyChanged(nameof(HasValue));
                }
            }
        }
        private double? _fTienGoiThauUsd;
        public double? FTienGoiThauUsd
        {
            get => _fTienGoiThauUsd;
            set
            {
                if (SetProperty(ref _fTienGoiThauUsd, value))
                {
                    OnPropertyChanged(nameof(HasValue));
                }
            }
        }
        private double? _fTienGoiThauVnd;
        public double? FTienGoiThauVnd
        {
            get => _fTienGoiThauVnd;
            set
            {
                if (SetProperty(ref _fTienGoiThauVnd, value))
                {
                    OnPropertyChanged(nameof(HasValue));
                }
            }
        }
        public Guid? IIdGoiThauNguonVonId { get; set; }
        public string STenChiPhi { get; set; }
        public string STT { get; set; }
        public string SMaOrder { get; set; }

        public Guid? IIdChiPhiId { get; set; }

        //// Another properties
        public string SMaChiPhi { get; set; }

        private string _sTenNguonVon;
        public string STenNguonVon
        {
            get => _sTenNguonVon;
            set => SetProperty(ref _sTenNguonVon, value);
        }
        public ObservableCollection<NhDmChiPhiModel> ItemsLoaiNoiDungChi { get; set; }

        private ObservableCollection<NhDaGoiThauHangMucModel> _goiThauHangMucs = new ObservableCollection<NhDaGoiThauHangMucModel>();
        public ObservableCollection<NhDaGoiThauHangMucModel> GoiThauHangMucs
        {
            get => _goiThauHangMucs;
            set => SetProperty(ref _goiThauHangMucs, value);
        }
        public bool IsSaved { get; set; } = false;

        public virtual bool IsLoaiNoiDungChi => IIdParentId == null;

        public virtual bool IsNoiDungChi => IIdParentId != null;
    }
}
