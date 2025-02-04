using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class HopDongChiPhiQueryModel : ModelBase
    {
        private bool _isSelected;
        public new bool IsSelected
        {
            get => _isSelected;
            set
            {
                SetProperty(ref _isSelected, value);
                OnPropertyChanged(nameof(IsEditHangMuc));
            }
        }

        private bool _isHangCha;
        public override bool IsHangCha
        {
            get => _isHangCha;
            set
            {
                SetProperty(ref _isHangCha, value);
                OnPropertyChanged(nameof(IsEditHangMuc));
            }
        }

        public Guid IdHopDongGoiThauNhaThau { get; set; }
        public string TenChiPhi { get; set; }
        public Guid? IdGoiThauChiPhi { get; set; }
        public Guid? IdChiPhi { get; set; }
        public double? GiaTriPheDuyet { get; set; }
        public string MaOrDer { get; set; }
        public Guid? IdGoiThau { get; set; }

        private double _fGiaTriChiPhi;
        public double FGiaTriChiPhi
        {
            get => _fGiaTriChiPhi;
            set
            {
                SetProperty(ref _fGiaTriChiPhi, value);
                OnPropertyChanged(nameof(FGiaTriConLai));
                OnPropertyChanged(nameof(IsDisabledSelection));
            }
        }

        private double _fGiaTriChiPhiTrongGoiTHauKhac;
        public double FGiatriChiPhiTrongGoiThauKhac
        {
            get => _fGiaTriChiPhiTrongGoiTHauKhac;
            set
            {
                SetProperty(ref _fGiaTriChiPhiTrongGoiTHauKhac, value);
                OnPropertyChanged(nameof(FGiaTriConLai));
                OnPropertyChanged(nameof(IsDisabledSelection));
            }
        }

        public double FTienCoTheSD { get; set; }
        public double FGiaTriConLai => FTienCoTheSD - FGiaTriChiPhi - FGiatriChiPhiTrongGoiThauKhac;
        public bool IsDisabledSelection => FGiatriChiPhiTrongGoiThauKhac >= FTienCoTheSD;
        public Guid? IdChiPhiParent { get; set; }
        public bool IsEditHangMuc => !IsHangCha && IsSelected;
        public double? FGiaTriTruocDC { get; set; }

        private bool _isDisableEditGiatriChiPhi;

        public bool IsDisableGiatriChiPhi
        {
            get => _isDisableEditGiatriChiPhi;
            set => SetProperty(ref _isDisableEditGiatriChiPhi, value);
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }

        public ObservableCollection<HopDongChiPhiHangMucQueryModel> ListHangMuc { get; set; }
        public List<VdtDaHopDongDmHangMuc> ListVdtDaHopDongDmHangMuc { get; set; }

        public HopDongChiPhiQueryModel clone()
        {
            HopDongChiPhiQueryModel rs = new HopDongChiPhiQueryModel();
            rs.IdHopDongGoiThauNhaThau = IdHopDongGoiThauNhaThau;
            rs.IsHangCha = IsHangCha;
            rs.TenChiPhi = TenChiPhi;
            rs.IdGoiThauChiPhi = IdGoiThauChiPhi;
            rs.IdChiPhi = IdChiPhi;
            rs.GiaTriPheDuyet = GiaTriPheDuyet;
            rs.MaOrDer = MaOrDer;
            rs.IdGoiThau = IdGoiThau;
            rs.FGiaTriChiPhi = 0;
            rs.FTienCoTheSD = FTienCoTheSD;
            rs.IdChiPhiParent = IdChiPhiParent;
            rs.ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>(ListHangMuc.Select(hm => hm.Clone()));
            return rs;
        }
    }
}
