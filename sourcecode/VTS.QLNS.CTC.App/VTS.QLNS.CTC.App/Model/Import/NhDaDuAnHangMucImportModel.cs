using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;


namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Sheet Import", 8, 0)]
    internal class NhDaDuAnHangMucImportModel : BindableBase
    {
        public Guid? IIdDuAnId { get; set; }

        private string _sMaHangMuc;
        public string SMaHangMuc
        {
            get => _sMaHangMuc;
            set => SetProperty(ref _sMaHangMuc, value);
        }

        private string _stenHangMuc;
        [ColumnAttribute("Tên hạng mục", 1)]
        public string STenHangMuc
        {
            get => _stenHangMuc;
            set => SetProperty(ref _stenHangMuc, value);
        }

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set
            {
                SetProperty(ref _iIdParentId, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }

        private Guid? _iIdLoaiCongTrinhId;
        public Guid? IIdLoaiCongTrinhId
        {
            get => _iIdLoaiCongTrinhId;
            set => SetProperty(ref _iIdLoaiCongTrinhId, value);
        }

        // Another properties
        public bool IsHangCha => IIdParentId.IsNullOrEmpty();

        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }

        private string _sTenLoaiCongTrinh;
        public string STenLoaiCongTrinh
        {
            get => _sTenLoaiCongTrinh;
            set => SetProperty(ref _sTenLoaiCongTrinh, value);
        }

        private string _sMaLoaiCongTrinh;
        [ColumnAttribute("Mã loại công trình", 2)]

        public string SMaLoaiCongTrinh
        {
            get => _sMaLoaiCongTrinh;
            set => SetProperty(ref _sMaLoaiCongTrinh, value);
        }

        private string _stt;
        [ColumnAttribute("STT", 0)]
        public string STT
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }
    }
}
