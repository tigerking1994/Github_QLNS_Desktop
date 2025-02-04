using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ", 10, 0)]
    public class VdtTtDeNghiThanhToanNSQPImportModel : BindableBase
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

        private string _bThanhToanTheoHD;
        [ColumnAttribute("Thanh toán theo hợp đồng", 20)]
        public string BThanhToanTheoHD
        {
            get => _bThanhToanTheoHD;
            set => SetProperty(ref _bThanhToanTheoHD, value);
        }

        private string _sSoHopDongChiPhi;
        [ColumnAttribute("Số hợp đồng", 7)]
        public string SSoHopDongChiPhi
        {
            get => _sSoHopDongChiPhi;
            set => SetProperty(ref _sSoHopDongChiPhi, value);
        }

        private string _sSoBangKlht;
        //[ColumnAttribute("Số", 9)]
        public string SSoBangKlht
        {
            get => _sSoBangKlht;
            set => SetProperty(ref _sSoBangKlht, value);
        }

        private string _dNgayBangKlht;
        //[ColumnAttribute("Ngày", 10)]
        public string DNgayBangKlht
        {
            get => _dNgayBangKlht;
            set => SetProperty(ref _dNgayBangKlht, value);
        }

        private string _fLuyKeGiaTriNghiemThuKlht;
        //[ColumnAttribute("Lũy kế giá trị KLHT", 11)]
        public string FLuyKeGiaTriNghiemThuKlht
        {
            get => _fLuyKeGiaTriNghiemThuKlht;
            set => SetProperty(ref _fLuyKeGiaTriNghiemThuKlht, value);
        }


        private Guid? _iIdHopDong;
        public Guid? IIdHopDong
        {
            get => _iIdHopDong;
            set => SetProperty(ref _iIdHopDong, value);
        }

        /*private string _sMaNguonVon;
        [ColumnAttribute("Nguồn vốn", 8)]
        public string SMaNguonVon
        {
            get => string.IsNullOrEmpty(_sMaNguonVon) ? null : _sMaNguonVon.Trim();
            set => SetProperty(ref _sMaNguonVon, value);
        }*/

        private string _sLoaiKHV;
        [ColumnAttribute("Loại khv", 10)]
        public string SLoaiKHV
        {
            get => _sLoaiKHV;
            set => SetProperty(ref _sLoaiKHV, value);
        }

        private string _sKHV;
        [ColumnAttribute("kế hoạch vốn", 11)]
        public string SKHV
        {
            get => string.IsNullOrEmpty(_sKHV) ? null : _sKHV.Trim();
            set => SetProperty(ref _sKHV, value);
        }

        /*private string _sNamKeHoach;
        [ColumnAttribute("Năm kế hoạch", 12)]
        public string SNamKeHoach
        {
            get => _sNamKeHoach;
            set => SetProperty(ref _sNamKeHoach, value);
        }*/

        private string _sNoiDung;
        [ColumnAttribute("Nội dung", 13)]
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private string _bHoanTraUngTruoc;
        //[ColumnAttribute("Thanh toán để hoàn trả ứng trước", 15)]
        public string BHoanTraUngTruoc
        {
            get => _bHoanTraUngTruoc;
            set => SetProperty(ref _bHoanTraUngTruoc, value);
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

        private string _sThuHoiUTTN;
        [ColumnAttribute("Thu hồi tạm ứng TN", 18)]
        public string SThuHoiUTTN
        {
            get => _sThuHoiUTTN;
            set => SetProperty(ref _sThuHoiUTTN, value);
        }

        private string _sThuHoiUTNN;
        [ColumnAttribute("Thu hồi tạm ứng NN", 19)]
        public string SThuHoiUTNN
        {
            get => _sThuHoiUTNN;
            set => SetProperty(ref _sThuHoiUTNN, value);
        }

        private string _sThueGTGT;
        //[ColumnAttribute("Thuế giá trị gia tăng", 22)]
        public string SThueGTGT
        {
            get => _sThueGTGT;
            set => SetProperty(ref _sThueGTGT, value);
        }

        private string _sChuyenTienBH;
        //[ColumnAttribute("Chuyển tiền bảo hành", 23)]
        public string SChuyenTienBH
        {
            get => _sChuyenTienBH;
            set => SetProperty(ref _sChuyenTienBH, value);
        }

        private string _sTenDVTH;
        //[ColumnAttribute("Tên đơn vị thụ hưởng", 24)]
        public string STenDVTH
        {
            get => _sTenDVTH;
            set => SetProperty(ref _sTenDVTH, value);
        }

        private string _sMaNH;
        //[ColumnAttribute("Mã ngân hàng", 25)]
        public string SMaNH
        {
            get => _sMaNH;
            set => SetProperty(ref _sMaNH, value);
        }
        private string _sSTK;
        //[ColumnAttribute("STK", 26)]
        public string SSTK
        {
            get => _sSTK;
            set => SetProperty(ref _sSTK, value);
        }
        public Guid? KHVId { get; set; }
    }
}
