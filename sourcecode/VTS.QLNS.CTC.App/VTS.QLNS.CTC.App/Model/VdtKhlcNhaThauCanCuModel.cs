using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhlcNhaThauCanCuModel : BindableBase
    {
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _iLoaiCanCu;
        public int ILoaiCanCu
        {
            get => _iLoaiCanCu;
            set => SetProperty(ref _iLoaiCanCu, value);
        }

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }

        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private int _iTepDinhKem;
        public int ITepDinhKem
        {
            get => _iTepDinhKem;
            set => SetProperty(ref _iTepDinhKem, value);
        }

        private double _fTongGiaTriPheDuyet;
        public double FTongGiaTriPheDuyet
        {
            get => _fTongGiaTriPheDuyet;
            set => SetProperty(ref _fTongGiaTriPheDuyet, value);
        }
    }
}
