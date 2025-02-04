using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model.Import;

namespace VTS.QLNS.CTC.App.Model
{
    [SheetAttribute(0, "Import_QuyTienLuongNam", 5, 0)]
    public class TlBangLuongKeHoachImportModel : BindableBase
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

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }

        private bool _bHangcha;
        public bool BHangCha
        {
            get => _bHangcha;
            set => SetProperty(ref _bHangcha, value);
        }

        public string _iLevel;
        [ColumnAttribute("Level", 0)]
        public string ILevel
        {
            get => _iLevel;
            set => SetProperty(ref _iLevel, value);
        }
        public string _sXauNoiMa;
        [ColumnAttribute("XauNoiMa", 1)]
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        public string _sNoiDung;
        [ColumnAttribute("Nội dung", 2)]
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }
        public string _sQSBQ;
        [ColumnAttribute("QSBS năm", 4)]
        public string QSBQ
        {
            get => _sQSBQ;
            set => SetProperty(ref _sQSBQ, value);
        }
        public string _sLHT_TT;
        [ColumnAttribute("Lương chính", 5)]
        public string LHT_TT
        {
            get => _sLHT_TT;
            set => SetProperty(ref _sLHT_TT, value);
        }
        public string _sPCCV_TT;
        [ColumnAttribute("PC chức vụ", 6)]
        public string PCCV_TT
        {
            get => _sPCCV_TT;
            set => SetProperty(ref _sPCCV_TT, value);
        }
        public string _sPCTN_TT;
        [ColumnAttribute("PCTN nghề", 7)]
        public string PCTN_TT
        {
            get => _sPCTN_TT;
            set => SetProperty(ref _sPCTN_TT, value);
        }
        public string _sPCTNVK_TT;
        [ColumnAttribute("PCTNVK", 8)]
        public string PCTNVK_TT
        {
            get => _sPCTNVK_TT;
            set => SetProperty(ref _sPCTNVK_TT, value);
        }
        public string _sHSBL_TT;
        [ColumnAttribute("HSBL", 9)]
        public string HSBL_TT
        {
            get => _sHSBL_TT;
            set => SetProperty(ref _sHSBL_TT, value);
        }
    }
}
