using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(1, "Thu nộp BHXH Import", 4, 0)]
    public class FeeCollectionBhxhImportModel : BindableBase
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

        public List<FeeCollectionBhxhImportModel> LstPhuCap { get; set; }

        private string _sTenCanBo;
        [ColumnAttribute("Tên cán bộ", 0)]
        public string STenCanBo
        {
            get => _sTenCanBo;
            set => SetProperty(ref _sTenCanBo, value);
        }

        private string _sMaCanBo;
        [ColumnAttribute("Mã cán bộ", 1)]
        public string SMaCanBo
        {
            get => _sMaCanBo;
            set => SetProperty(ref _sMaCanBo, value);
        }

        private string _iThang;
        [ColumnAttribute("Tháng", 2, ValidateType.IsNumber, true)]
        public string IThang
        {
            get => _iThang;
            set => SetProperty(ref _iThang, value);
        }

        private string _iNam;
        [ColumnAttribute("Năm", 3, ValidateType.IsNumber, true)]
        public string INam
        {
            get => _iNam;
            set => SetProperty(ref _iNam, value);
        }

        private string _iIdMaDonVi;
        [ColumnAttribute("Mã đơn vị", 4)]
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set => SetProperty(ref _iIdMaDonVi, value);
        }

        private string _sMaCachTinhLuong;
        [ColumnAttribute("Mã cách tính lương", 5)]
        public string SMaCachTinhLuong
        {
            get => _sMaCachTinhLuong;
            set => SetProperty(ref _sMaCachTinhLuong, value);
        }

        private string _sMaCapBac;
        [ColumnAttribute("Mã cấp bậc", 6)]
        public string SMaCapBac
        {
            get => _sMaCapBac;
            set => SetProperty(ref _sMaCapBac, value);
        }

        private string _sMaPhuCap;
        [ColumnAttribute("Mã phụ cấp", 7)]
        public string SMaPhuCap
        {
            get => _sMaPhuCap;
            set => SetProperty(ref _sMaPhuCap, value);
        }

        private string _giaTri;
        [ColumnAttribute("Giá trị", 8, ValidateType.IsNumber)]
        public string GiaTri
        {
            get => _giaTri;
            set => SetProperty(ref _giaTri, value);
        }

        private string _sMaHieuCanBo;
        [ColumnAttribute("Mã hiệu cán bộ", 9)]
        public string SMaHieuCanBo
        {
            get => _sMaHieuCanBo;
            set => SetProperty(ref _sMaHieuCanBo, value);
        }

        public Guid? IIdParentId { get; set; }
    }
}
