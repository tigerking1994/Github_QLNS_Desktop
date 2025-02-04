using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaGoiThauNguonVonModel : CurrencyDetailModelBase
    {
        public Guid? IIdGoiThauId { get; set; }
  
        private int? _iIdNguonVonId;
        [ValidateAttribute("Nguồn vốn", Utility.Enum.DATA_TYPE.Int, true)]
        public int? IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        private double? _fTienGoiThauUsd;
        public double? FTienGoiThauUsd 
        {
            get => _fTienGoiThauUsd;
            set => SetProperty(ref _fTienGoiThauUsd, value);
        }

        private double? _fTienGoiThauVnd;
        public double? FTienGoiThauVnd
        {
            get => _fTienGoiThauVnd;
            set => SetProperty(ref _fTienGoiThauVnd, value);
        }

        private double? _fTienGoiThauEur;
        public double? FTienGoiThauEur
        {
            get => _fTienGoiThauEur;
            set => SetProperty(ref _fTienGoiThauEur, value);
        }

        private double? _fTienGoiThauNgoaiTeKhac;
        public double? FTienGoiThauNgoaiTeKhac
        {
            get => _fTienGoiThauNgoaiTeKhac;
            set => SetProperty(ref _fTienGoiThauNgoaiTeKhac, value);
        }

        private string _sTenNguonVon;
        public string STenNguonVon
        {
            get => _sTenNguonVon;
            set => SetProperty(ref _sTenNguonVon, value);
        }
        public bool IsSaved { get; set; } = false;

        public Guid? IIdQddauTuNguonVonId { get; set; }
        public Guid? IIdDuToanNguonVonId { get; set; }
        public Guid? IIdCacQuyetDinhNguonVonId { get; set; }
        public Guid? IIdDuAnNguonVonId { get; set; }
        public Guid? IIdChuTruongDauTuNguonVonId { get; set; }
        public string SMaOrder { get; set; }

        // Another properties
        private ObservableCollection<NhDaGoiThauChiPhiModel> _goiThauChiPhis = new ObservableCollection<NhDaGoiThauChiPhiModel>();
        public ObservableCollection<NhDaGoiThauChiPhiModel> GoiThauChiPhis
        {
            get => _goiThauChiPhis;
            set => SetProperty(ref _goiThauChiPhis, value);
        }

        private ObservableCollection<NhDaGoiThauHangMucModel> _goiThauHangMucs = new ObservableCollection<NhDaGoiThauHangMucModel>();
        public ObservableCollection<NhDaGoiThauHangMucModel> GoiThauHangMucs
        {
            get => _goiThauHangMucs;
            set => SetProperty(ref _goiThauHangMucs, value);
        }
    }
}
