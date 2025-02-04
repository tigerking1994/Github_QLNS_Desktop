using System;
using VTS.QLNS.CTC.App.ViewModel;

namespace VTS.QLNS.CTC.App.Model
{
    public class InitializationProcessModel : ViewModelBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _namKhoiTao;
        public int NamKhoiTao
        {
            get => _namKhoiTao;
            set => SetProperty(ref _namKhoiTao, value);
        }

        private Guid? _iIdDonViId;
        public Guid? IIdDonViId
        {
            get => _iIdDonViId;
            set => SetProperty(ref _iIdDonViId, value);
        }

        private string _iIdMaDonVi;
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set => SetProperty(ref _iIdMaDonVi, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private DateTime? _ngayKhoiTao;
        public DateTime? NgayKhoiTao
        {
            get => _ngayKhoiTao;
            set => SetProperty(ref _ngayKhoiTao, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
    }
}
