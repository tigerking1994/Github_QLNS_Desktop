using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using System.Windows.Documents;
using System;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Thông tin chi phí hạng mục", 8, 0)]
    public class PddaHangMucImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        //private string _muc;
        //[ColumnAttribute("Mức", 0, isRequired: true)]
        //public string Muc
        //{
        //    get => _muc;
        //    set => SetProperty(ref _muc, value);
        //}

        private string _stt;
        [ColumnAttribute("STT", 0)]
        public string STT
        {
            get => _stt;
            set
            {
                SetProperty(ref _stt, value);
            }
        }

        private string _loai;
        [ColumnAttribute("Loại", 1)]
        public string Loai
        {
            get => _loai;
            set
            {
                SetProperty(ref _loai, value);
            }
        }

        private string _sMaChiPhi;
        [ColumnAttribute("MaChiPhi", 2)]
        public string SMaChiPhi
        {
            get => _sMaChiPhi;
            set
            {
                SetProperty(ref _sMaChiPhi, value);
            }
        }

        private string _sTenCPHM;
        [ColumnAttribute("Tên hạng mục/Chi phí", 3)]
        public string STenCPHM
        {
            get => _sTenCPHM;
            set
            {
                SetProperty(ref _sTenCPHM, value);
            }
        }

        private string _fGiaTriPD;
        [ColumnAttribute("Giá trị phê duyệt", 4)]
        public string FGiaTriPD
        {
            get => _fGiaTriPD;
            set
            {
                SetProperty(ref _fGiaTriPD, value);
            }
        }

        
        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }

        private Guid? _iIdChiPhi;
        public Guid? IIdChiPhi
        {
            get => _iIdChiPhi;
            set => SetProperty(ref _iIdChiPhi, value);
        }

        public Guid? Id { set; get; } = Guid.NewGuid();


        private Guid? _idParent;
        public Guid? IdParent
        {
            get => _idParent;
            set => SetProperty(ref _idParent, value);
        }


        private string _sError;
        public string SError
        {
            get => _sError;
            set => SetProperty(ref _sError, value);
        }
    }
}
