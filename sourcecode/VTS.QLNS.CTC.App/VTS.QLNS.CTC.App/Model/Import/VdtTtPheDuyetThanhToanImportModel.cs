using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ", 11, 0)]
    public class VdtTtPheDuyetThanhToanImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _sLNS;
        [ColumnAttribute("NS", 0)]
        public string SLNS
        {
            get => _sLNS;
            set => SetProperty(ref _sLNS, value);
        }

        private string _sLK;
        [ColumnAttribute("LK", 1)]
        public string SLK
        {
            get => _sLK;
            set => SetProperty(ref _sLK, value);
        }

        private string _sM;
        [ColumnAttribute("M", 2)]
        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sTM;
        [ColumnAttribute("TM", 3)]
        public string STM
        {
            get => _sTM;
            set => SetProperty(ref _sTM, value);
        }

        private string _sTTM;
        [ColumnAttribute("TTM", 4)]
        public string STTM
        {
            get => _sTTM;
            set => SetProperty(ref _sTTM, value);
        }

        private string _sNG;
        [ColumnAttribute("NG", 5)]
        public string SNG
        {
            get => _sNG;
            set => SetProperty(ref _sNG, value);
        }

        private Guid _iIdMuc;
        public Guid IIdMuc
        {
            get => _iIdMuc;
            set => SetProperty(ref _iIdMuc, value);
        }

        private Guid _iIdTieuMuc;
        public Guid IIdTieuMuc
        {
            get => _iIdTieuMuc;
            set => SetProperty(ref _iIdTieuMuc, value);
        }

        private Guid _iIdTietMuc;
        public Guid IIdTietMuc
        {
            get => _iIdTietMuc;
            set => SetProperty(ref _iIdTietMuc, value);
        }

        private Guid _iIdNganh;
        public Guid IIdNganh
        {
            get => _iIdNganh;
            set => SetProperty(ref _iIdNganh, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("TenDuAn", 6)]
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sMaDuAn;
        [ColumnAttribute("MaDuAn", 7)]
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sSoDeNghi;
        [ColumnAttribute("SoDeNghi", 8)]
        public string SSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private Guid _iIdDeNghiThanhToan;
        public Guid IIdDeNghiThanhToan
        {
            get => _iIdDeNghiThanhToan;
            set => SetProperty(ref _iIdDeNghiThanhToan, value);
        }

        private string _fGiaTriThanhToanTN;
        [ColumnAttribute("GiaTriThanhToanTN", 9, ValidateType.IsNumber)]
        public string FGiaTriThanhToanTN
        {
            get => _fGiaTriThanhToanTN;
            set => SetProperty(ref _fGiaTriThanhToanTN, value);
        }

        private string _fGiaTriThanhToanNN;
        [ColumnAttribute("GiaTriThanhToanNN", 10, ValidateType.IsNumber)]
        public string FGiaTriThanhToanNN
        {
            get => _fGiaTriThanhToanNN;
            set => SetProperty(ref _fGiaTriThanhToanNN, value);
        }

        private string _fGiaTriThuHoiNamTruocTN;
        [ColumnAttribute("GiaTriThuHoiNamTruocTN", 11, ValidateType.IsNumber)]
        public string FGiaTriThuHoiNamTruocTN
        {
            get => _fGiaTriThuHoiNamTruocTN;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocTN, value);
        }

        private string _fGiaTriThuHoiNamTruocNN;
        [ColumnAttribute("GiaTriThuHoiNamTruocNN", 12, ValidateType.IsNumber)]
        public string FGiaTriThuHoiNamTruocNN
        {
            get => _fGiaTriThuHoiNamTruocNN;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocNN, value);
        }

        private string _fGiaTriThuHoiNamNayTN;
        [ColumnAttribute("GiaTriThuHoiNamNayTN", 13, ValidateType.IsNumber)]
        public string FGiaTriThuHoiNamNayTN
        {
            get => _fGiaTriThuHoiNamNayTN;
            set => SetProperty(ref _fGiaTriThuHoiNamNayTN, value);
        }

        private string _fGiaTriThuHoiNamNayNN;
        [ColumnAttribute("GiaTriThuHoiNamNayNN", 14, ValidateType.IsNumber)]
        public string FGiaTriThuHoiNamNayNN
        {
            get => _fGiaTriThuHoiNamNayNN;
            set => SetProperty(ref _fGiaTriThuHoiNamNayNN, value);
        }
    }
}
