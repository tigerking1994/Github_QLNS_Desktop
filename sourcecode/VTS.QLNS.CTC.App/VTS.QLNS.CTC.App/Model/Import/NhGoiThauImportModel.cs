using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Thông tin gói thầu", 11, 0)]
    public class NhGoiThauImportModel : BindableBase
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

        private string _sTenGoiThau;
        [ColumnAttribute("Mã gói thầu", 0)]
        public string STenGoiThau
        {
            get => _sTenGoiThau;
            set => SetProperty(ref _sTenGoiThau, value);
        }

        private string _sHinhThucChonNhaThau;
        [ColumnAttribute("Hình thức chọn nhà thầu", 1)]
        public string SHinhThucChonNhaThau
        {
            get => _sHinhThucChonNhaThau;
            set => SetProperty(ref _sHinhThucChonNhaThau, value);
        }

        private string _sPhuongThucChonNhaThau;
        [ColumnAttribute("Phương thức chọn nhà thầu", 2)]
        public string SPhuongThucChonNhaThau
        {
            get => _sPhuongThucChonNhaThau;
            set => SetProperty(ref _sPhuongThucChonNhaThau, value);
        }

        private string _sLoaiHopDong;
        [ColumnAttribute("Loại hợp đồng", 3)]
        public string SLoaiHopDong
        {
            get => _sLoaiHopDong;
            set => SetProperty(ref _sLoaiHopDong, value);
        }

        private string  _iThoiGianThucHien;
        [ColumnAttribute("Thời gian thực hiện", 4)]
        public string IThoiGianThucHien
        {
            get => _iThoiGianThucHien;
            set => SetProperty(ref _iThoiGianThucHien, value);
        }

    }
}
