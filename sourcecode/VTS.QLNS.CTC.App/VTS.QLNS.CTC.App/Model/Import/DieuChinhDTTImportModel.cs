using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 8, 0)]
    public class DieuChinhDTTImportModel : BindableBase
    {
        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }
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

        private bool _bHangcha;
        public bool BHangCha
        {
            get => _bHangcha;
            set => SetProperty(ref _bHangcha, value);
        }
        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã", 0)]
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private string _sTenMLNS;
        [ColumnAttribute("Nội dung", 1)]
        public string STenMLNS
        {
            get => _sTenMLNS;
            set => SetProperty(ref _sTenMLNS, value);
        }
        private string _fThuBHXHNLD;
        [ColumnAttribute("BHXH NLĐ", 2, ValidateType.IsNumber)]
        public string FThuBHXHNLD
        {
            get => _fThuBHXHNLD;
            set
            {
                SetProperty(ref _fThuBHXHNLD, value);
            }
        }

        private string _fThuBHXHNSD;
        [ColumnAttribute("BHXH NSD", 3, ValidateType.IsNumber)]
        public string FThuBHXHNSD
        {
            get => _fThuBHXHNSD;
            set
            {
                SetProperty(ref _fThuBHXHNSD, value);
            }
        }

        private string _fThuBHYTNLD;
        [ColumnAttribute("BHYT NLĐ", 4, ValidateType.IsNumber)]
        public string FThuBHYTNLD
        {
            get => _fThuBHYTNLD;
            set
            {
                SetProperty(ref _fThuBHYTNLD, value);
            }
        }

        private string _fThuBHYTNSD;
        [ColumnAttribute("BHYT NSD", 5, ValidateType.IsNumber)]
        public string FThuBHYTNSD
        {
            get => _fThuBHYTNSD;
            set
            {
                SetProperty(ref _fThuBHYTNSD, value);
            }
        }

        private string _fThuBHTNNLD;
        [ColumnAttribute("BHTN NLĐ", 6, ValidateType.IsNumber)]
        public string FThuBHTNNLD
        {
            get => _fThuBHTNNLD;
            set
            {
                SetProperty(ref _fThuBHTNNLD, value);
            }
        }

        private string _fThuBHTNNSD;
        [ColumnAttribute("BHTN NSD", 7, ValidateType.IsNumber)]
        public string FThuBHTNNSD
        {
            get => _fThuBHTNNSD;
            set
            {
                SetProperty(ref _fThuBHTNNSD, value);
            }
        }

        private string _fThuBHXHNLDQTDauNam;
        [ColumnAttribute("BHXH NLĐ", 8, ValidateType.IsNumber)]
        public string FThuBHXHNLDQTDauNam
        {
            get => _fThuBHXHNLDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHXHNLDQTDauNam, value);
            }
        }

        private string _fThuBHXHNSDQTDauNam;
        [ColumnAttribute("BHXH NSD", 9, ValidateType.IsNumber)]
        public string FThuBHXHNSDQTDauNam
        {
            get => _fThuBHXHNSDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHXHNSDQTDauNam, value);
            }
        }

        private string _fThuBHYTNLDQTDauNam;
        [ColumnAttribute("BHYT NLĐ", 10, ValidateType.IsNumber)]
        public string FThuBHYTNLDQTDauNam
        {
            get => _fThuBHYTNLDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHYTNLDQTDauNam, value);
            }
        }

        private string _fThuBHYTNSDQTDauNam;
        [ColumnAttribute("BHYT NSD", 11, ValidateType.IsNumber)]
        public string FThuBHYTNSDQTDauNam
        {
            get => _fThuBHYTNSDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHYTNSDQTDauNam, value);
            }
        }

        private string _fThuBHTNNLDQTDauNam;
        [ColumnAttribute("BHTN NLĐ", 12, ValidateType.IsNumber)]
        public string FThuBHTNNLDQTDauNam
        {
            get => _fThuBHTNNLDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHTNNLDQTDauNam, value);
            }
        }

        private string _fThuBHTNNSDQTDauNam;
        [ColumnAttribute("BHTN NSD", 13, ValidateType.IsNumber)]
        public string FThuBHTNNSDQTDauNam
        {
            get => _fThuBHTNNSDQTDauNam;
            set
            {
                SetProperty(ref _fThuBHTNNSDQTDauNam, value);
            }
        }

        private string _fThuBHXHNLDQTCuoiNam;
        [ColumnAttribute("BHXH NLĐ", 14, ValidateType.IsNumber)]
        public string FThuBHXHNLDQTCuoiNam
        {
            get => _fThuBHXHNLDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHXHNLDQTCuoiNam, value);
            }
        }

        private string _fThuBHXHNSDQTCuoiNam;
        [ColumnAttribute("BHXH NSD", 15, ValidateType.IsNumber)]
        public string FThuBHXHNSDQTCuoiNam
        {
            get => _fThuBHXHNSDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHXHNSDQTCuoiNam, value);
            }
        }

        private string _fThuBHYTNLDQTCuoiNam;
        [ColumnAttribute("BHYT NLĐ", 16, ValidateType.IsNumber)]
        public string FThuBHYTNLDQTCuoiNam
        {
            get => _fThuBHYTNLDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHYTNLDQTCuoiNam, value);
            }
        }

        private string _fThuBHYTNSDQTCuoiNam;
        [ColumnAttribute("BHYT NSD", 17, ValidateType.IsNumber)]
        public string FThuBHYTNSDQTCuoiNam
        {
            get => _fThuBHYTNSDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHYTNSDQTCuoiNam, value);
            }
        }

        private string _fThuBHTNNLDQTCuoiNam;
        [ColumnAttribute("BHTN NLĐ", 18, ValidateType.IsNumber)]
        public string FThuBHTNNLDQTCuoiNam
        {
            get => _fThuBHTNNLDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHTNNLDQTCuoiNam, value);
            }
        }

        private string _fThuBHTNNSDQTCuoiNam;
        [ColumnAttribute("BHTN NSD", 19, ValidateType.IsNumber)]
        public string FThuBHTNNSDQTCuoiNam
        {
            get => _fThuBHTNNSDQTCuoiNam;
            set
            {
                SetProperty(ref _fThuBHTNNSDQTCuoiNam, value);
            }
        }

        private string _fThuBHXHNLDTang;
        [ColumnAttribute("BHXH NLĐ đóng (tăng)", 20, ValidateType.IsNumber)]
        public string FThuBHXHNLDTang
        {
            get => _fThuBHXHNLDTang;
            set => SetProperty(ref _fThuBHXHNLDTang, value);
        }

        private string _fThuBHXHNSDTang;
        [ColumnAttribute("BHXH NSD lao động đóng (tăng)", 21, ValidateType.IsNumber)]
        public string FThuBHXHNSDTang
        {
            get => _fThuBHXHNSDTang;
            set => SetProperty(ref _fThuBHXHNSDTang, value);
        }

        private string _fThuBHXHNLDGiam;
        [ColumnAttribute("BHXH NLĐ đóng (giảm)", 22, ValidateType.IsNumber)]
        public string FThuBHXHNLDGiam
        {
            get => _fThuBHXHNLDGiam;
            set => SetProperty(ref _fThuBHXHNLDGiam, value);
        }

        private string _fThuBHXHNSDGiam;
        [ColumnAttribute("BHXH NSD lao động (giảm)", 23, ValidateType.IsNumber)]
        public string FThuBHXHNSDGiam
        {
            get => _fThuBHXHNSDGiam;
            set => SetProperty(ref _fThuBHXHNSDGiam, value);
        }

        private string _fThuBHYTNLDTang;
        [ColumnAttribute("BHYT NLĐ đóng (tăng)", 24, ValidateType.IsNumber)]
        public string FThuBHYTNLDTang
        {
            get => _fThuBHYTNLDTang;
            set => SetProperty(ref _fThuBHYTNLDTang, value);
        }

        private string _fThuBHYTNSDTang;
        [ColumnAttribute("BHYT NSD lao động đóng (tăng)", 25, ValidateType.IsNumber)]
        public string FThuBHYTNSDTang
        {
            get => _fThuBHYTNSDTang;
            set => SetProperty(ref _fThuBHYTNSDTang, value);
        }

        private string _fThuBHYTNLDGiam;
        [ColumnAttribute("BHYT NLĐ đóng (giảm)", 26, ValidateType.IsNumber)]
        public string FThuBHYTNLDGiam
        {
            get => _fThuBHYTNLDGiam;
            set => SetProperty(ref _fThuBHYTNLDGiam, value);
        }

        private string _fThuBHYTNSDGiam;
        [ColumnAttribute("BHYT NSD lao động (giảm)", 27, ValidateType.IsNumber)]
        public string FThuBHYTNSDGiam
        {
            get => _fThuBHYTNSDGiam;
            set => SetProperty(ref _fThuBHYTNSDGiam, value);
        }

        private string _fThuBHTNNLDTang;
        [ColumnAttribute("BHTN NLĐ đóng (tăng)", 28, ValidateType.IsNumber)]
        public string FThuBHTNNLDTang
        {
            get => _fThuBHTNNLDTang;
            set => SetProperty(ref _fThuBHTNNLDTang, value);
        }

        private string _fThuBHTNNSDTang;
        [ColumnAttribute("BHTN NSD lao động đóng (tăng)", 29, ValidateType.IsNumber)]
        public string FThuBHTNNSDTang
        {
            get => _fThuBHTNNSDTang;
            set => SetProperty(ref _fThuBHTNNSDTang, value);
        }

        private string _fThuBHTNNLDGiam;
        [ColumnAttribute("BHTN NLĐ đóng (giảm)", 30, ValidateType.IsNumber)]
        public string FThuBHTNNLDGiam
        {
            get => _fThuBHTNNLDGiam;
            set => SetProperty(ref _fThuBHTNNLDGiam, value);
        }

        private string _fThuBHTNNSDGiam;
        [ColumnAttribute("BHTN NSD lao động (giảm)", 31, ValidateType.IsNumber)]
        public string FThuBHTNNSDGiam
        {
            get => _fThuBHTNNSDGiam;
            set => SetProperty(ref _fThuBHTNNSDGiam, value);
        }

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }

        public bool IsHasDttData => FThuBHXHNLD != "" || FThuBHXHNSD != "" || FThuBHYTNLD != "" || FThuBHYTNSD != "" || FThuBHTNNLD != "" || FThuBHTNNSD != "" || FThuBHXHNLDQTDauNam != ""
            || FThuBHXHNSDQTDauNam != "" || FThuBHYTNLDQTDauNam != "" || FThuBHYTNSDQTDauNam != "" || FThuBHTNNLDQTDauNam != "" || FThuBHTNNSDQTDauNam != "" || FThuBHXHNLDQTCuoiNam != ""
            || FThuBHXHNSDQTCuoiNam != "" || FThuBHYTNLDQTCuoiNam != "" || FThuBHYTNSDQTCuoiNam != "" || FThuBHTNNLDQTCuoiNam != "" || FThuBHTNNSDQTCuoiNam != ""
            || FThuBHXHNLDTang != "" || FThuBHXHNSDTang != "" || FThuBHXHNLDGiam != "" || FThuBHXHNSDGiam != "" || FThuBHYTNLDTang != "" || FThuBHYTNSDTang != ""
            || FThuBHYTNLDGiam != "" || FThuBHYTNSDGiam != "" || FThuBHTNNLDTang != "" || FThuBHTNNSDTang != "" || FThuBHTNNLDGiam != "" || FThuBHTNNSDGiam != "";
    }
}
