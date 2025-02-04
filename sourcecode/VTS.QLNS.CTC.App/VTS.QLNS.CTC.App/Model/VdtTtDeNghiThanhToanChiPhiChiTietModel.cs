using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtTtDeNghiThanhToanChiPhiChiTietModel : ModelBase
    {
        public Guid? Id { get; set; }
        public Guid? IIdNoiDungChi { get; set; }
        public string SNoiDungChi { get; set; }

        private string _sMaOrder;
        public string SMaOrder {
            get => _sMaOrder; 
            set {
                SetProperty(ref _sMaOrder, value);
                OnPropertyChanged(nameof(SMaChiPhi));
            } 
        }
        private Guid? _iIdParent;
        public Guid? IIdParent
        {
            get => _iIdParent;
            set => SetProperty(ref _iIdParent, value);
        }

        public string SMaChiPhi => StringUtils.ConvertMaOrder(SMaOrder);

        private double _fGiaTriPheDuyet;
        public double FGiaTriPheDuyet
        {
            get => _fGiaTriPheDuyet;
            set => SetProperty(ref _fGiaTriPheDuyet, value);
        }


        private double _fGiaTriDeNghi;
        public double FGiaTriDeNghi 
        { 
            get => _fGiaTriDeNghi; 
            set => SetProperty(ref _fGiaTriDeNghi, value); 
        }

        private string _sGhiChu;
        public string SGhiChu 
        { 
            get => _sGhiChu; 
            set => SetProperty(ref _sGhiChu, value); 
        }

        private bool _isEditHangMuc;
        public bool IsEditHangMuc
        {
            get => _isEditHangMuc;
            set => SetProperty(ref _isEditHangMuc, value);
        }
    }
}
