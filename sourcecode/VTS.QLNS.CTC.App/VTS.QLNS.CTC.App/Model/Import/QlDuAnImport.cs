using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "DSDA", 1, 0)]
    public class QlDuAnImport : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }

        private string _stt;
        [ColumnAttribute("STT", 0)]
        public string STT
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Tên dự án", 1)]
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _iIdMaDonViThucHienDuAn;
        [ColumnAttribute("Mã đơn vị", 2, isRequired: true)]
        public string IIdMaDonViThucHienDuAn
        {
            get => _iIdMaDonViThucHienDuAn;
            set => SetProperty(ref _iIdMaDonViThucHienDuAn, value);
        }

        private string _sDiaDiem;
        [ColumnAttribute("Địa điểm thực hiện", 3)]
        public string SDiaDiem
        {
            get => _sDiaDiem;
            set => SetProperty(ref _sDiaDiem, value);
        }

        private string _sMucTieu;
        [ColumnAttribute("Mục tiêu đầu tiên", 4)]
        public string SMucTieu
        {
            get => _sMucTieu;
            set => SetProperty(ref _sMucTieu, value);
        }
 

        private string _fHanMucDauTu;
        [ColumnAttribute("Hạn mức đầu tư", 5)]
        public string FHanMucDauTu
        {
            get => _fHanMucDauTu;
            set => SetProperty(ref _fHanMucDauTu, value);
        }
    }
}
