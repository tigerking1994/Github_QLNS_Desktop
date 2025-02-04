using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(1, "NSDonVi", 3, 0)]
    public class DonViByPhanBoVonImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _idDonVi;
        [ColumnAttribute("Mã đơn vị", 0)]
        public string Id_DonVi
        {
            get => _idDonVi;
            set => SetProperty(ref _idDonVi, value);
        }

        private string _tenDonVi;
        [ColumnAttribute("Tên đơn vị", 1)]
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private string _idDonViParent;
        [ColumnAttribute("Mã đơn vị cha", 2)]
        public string Id_DonVi_Parent
        {
            get => _idDonViParent;
            set => SetProperty(ref _idDonViParent, value);
        }

        private string _sDiaChi;
        [ColumnAttribute("Địa chỉ", 3)]
        public string SDiaChi
        {
            get => _sDiaChi;
            set => SetProperty(ref _sDiaChi, value);
        }

        private string _bHangCha;
        [ColumnAttribute("Hàng cha", 4)]
        public string MoTa
        {
            get => _bHangCha;
            set => SetProperty(ref _bHangCha, value);
        }

        private string _iCapDonVi;
        [ColumnAttribute("Cấp đơn vị", 5)]
        public string ICapDonVi
        {
            get => _iCapDonVi;
            set => SetProperty(ref _iCapDonVi, value);
        }
    }
}
