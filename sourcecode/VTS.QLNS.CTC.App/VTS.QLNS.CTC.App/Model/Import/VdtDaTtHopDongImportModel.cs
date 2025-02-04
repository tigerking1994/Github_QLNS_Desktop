using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Thông tin hợp đồng", 2, 0)]
    public class VdtDaTtHopDongImportModel : BindableBase
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

        private string _iStt;
        [ColumnAttribute("STT", 0)]
        public string IStt
        {
            get => _iStt;
            set => SetProperty(ref _iStt, value);
        }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án", 1)]
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sSoHopDong;
        [ColumnAttribute("Số hợp đồng", 2)]
        public string SSoHopDong
        {
            get => _sSoHopDong;
            set => SetProperty(ref _sSoHopDong, value);
        }

        private string _sTenHopDong;
        [ColumnAttribute("Tên hợp đồng", 3)]
        public string STenHopDong
        {
            get => _sTenHopDong;
            set => SetProperty(ref _sTenHopDong, value);
        }

        private string _dNgayHopDong;
        [ColumnAttribute("Ngày lập", 4)]
        public string DNgayHopDong
        {
            get => _dNgayHopDong;
            set => SetProperty(ref _dNgayHopDong, value);
        }

        private string _sMaLoaiHopDong;
        [ColumnAttribute("Mã loại hợp đồng", 5)]
        public string SMaLoaiHopDong
        {
            get => _sMaLoaiHopDong; 
            set => SetProperty(ref _sMaLoaiHopDong, value);
        }

        private string _sMaNhaThauThucHien;
        [ColumnAttribute("Mã nhà thầu đại diện", 6)]
        public string SMaNhaThauThucHien
        {
            get => _sMaNhaThauThucHien;
            set => SetProperty(ref _sMaNhaThauThucHien, value);
        }

        private string _iThoiGianThucHien;
        [ColumnAttribute("Thời gian thực hiện", 7, Utility.Enum.ValidateType.IsNumber)]
        public string IThoiGianThucHien
        {
            get => _iThoiGianThucHien;
            set => SetProperty(ref _iThoiGianThucHien, value);
        }

        private string _fTienHopDong;
        [ColumnAttribute("Giá trị hợp đồng", 8, Utility.Enum.ValidateType.IsNumber)]
        public string FTienHopDong
        {
            get => _fTienHopDong;
            set => SetProperty(ref _fTienHopDong, value);
        }
    }
}
