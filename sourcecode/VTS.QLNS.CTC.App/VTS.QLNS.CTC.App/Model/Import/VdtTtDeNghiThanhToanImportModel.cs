using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ", 10, 0)]
    public class VdtTtDeNghiThanhToanImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _iStt;
        [ColumnAttribute("STT", 0, ValidateType.IsNumber)]
        public string iStt
        {
            get => _iStt;
            set => SetProperty(ref _iStt, value);
        }

        private string _sSoDeNghi;
        [ColumnAttribute("Số đề nghị", 1)]
        public string SSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private string _sNgayDeNghi;
        [ColumnAttribute("Ngày đề nghị", 2)]
        public string SNgayDeNghi
        {
            get => _sNgayDeNghi;
            set => SetProperty(ref _sNgayDeNghi, value);
        }

        private string _sLoaiDeNghi;
        [ColumnAttribute("Loại đề nghị", 3)]
        public string SLoaiDeNghi
        {
            get => string.IsNullOrEmpty(_sLoaiDeNghi) ? null : _sLoaiDeNghi.Trim();
            set => SetProperty(ref _sLoaiDeNghi, value);
        }

        private int _iLoaiDeNghi;
        public int ILoaiDeNghi
        {
            get => _iLoaiDeNghi;
            set => SetProperty(ref _iLoaiDeNghi, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Tên dự án", 5)]
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án", 6)]
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private Guid _iIdDuAnId;
        public Guid IIdDuAnId
        {
            get => _iIdDuAnId;
            set => SetProperty(ref _iIdDuAnId, value);
        }

        private string _sSoHopDong;
        [ColumnAttribute("Số hợp đồng", 7)]
        public string SSoHopDong
        {
            get => _sSoHopDong;
            set => SetProperty(ref _sSoHopDong, value);
        }

        private Guid? _iIdHopDong;
        public Guid? IIdHopDong
        {
            get => _iIdHopDong;
            set => SetProperty(ref _iIdHopDong, value);
        }

        private string _sMaNguonVon;
        [ColumnAttribute("Nguồn vốn", 8)]
        public string SMaNguonVon
        {
            get => string.IsNullOrEmpty(_sMaNguonVon) ? null : _sMaNguonVon.Trim();
            set => SetProperty(ref _sMaNguonVon, value);
        }

        private string _sMaKhv;
        [ColumnAttribute("Mã loại kế hoạch vốn", 10)]
        public string SMaKhv
        {
            get => _sMaKhv;
            set => SetProperty(ref _sMaKhv, value);
        }

        private string _sSoKhv;
        [ColumnAttribute("Mã loại kế hoạch vốn", 11)]
        public string SSoKhv
        {
            get => string.IsNullOrEmpty(_sSoKhv) ? null : _sSoKhv.Trim();
            set => SetProperty(ref _sSoKhv, value);
        }

        private string _sNamKeHoach;
        [ColumnAttribute("Năm kế hoạch", 12)]
        public string SNamKeHoach
        {
            get => _sNamKeHoach;
            set => SetProperty(ref _sNamKeHoach, value);
        }

        private string _sNoiDung;
        [ColumnAttribute("Nội dung", 13)]
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private string _sThanhToanTN;
        [ColumnAttribute("Thanh toán TN", 14)]
        public string SThanhToanTN
        {
            get => _sThanhToanTN;
            set => SetProperty(ref _sThanhToanTN, value);
        }

        private string _sThanhToanNN;
        [ColumnAttribute("Thanh toán NN", 15)]
        public string SThanhToanNN
        {
            get => _sThanhToanNN;
            set => SetProperty(ref _sThanhToanNN, value);
        }

        private string _sThuHoiTN;
        [ColumnAttribute("Thu hồi TN", 16)]
        public string SThuHoiTN
        {
            get => _sThuHoiTN;
            set => SetProperty(ref _sThuHoiTN, value);
        }

        private string _sThuHoiNN;
        [ColumnAttribute("Thu hồi NN", 17)]
        public string SThuHoiNN
        {
            get => _sThuHoiNN;
            set => SetProperty(ref _sThuHoiNN, value);
        }
    }
}
