using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Thông tin chứng từ", 4, 0)]
    public class RealRevenueExpenditureDetailImportModel : BindableBase
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

        [ColumnAttribute(ValidateType.IsLoaiHinh)]
        public string ConcatenateCode
        {
            get => _lns;
        }

        private string _idMaLoaiHinh;
        [ColumnAttribute("IdMaLoaiHinh", 0)]
        public string IdMaLoaiHinh
        {
            get => _idMaLoaiHinh;
            set => SetProperty(ref _idMaLoaiHinh, value);
        }

        private string _idMaLoaiHinhCha;
        [ColumnAttribute("IdMaLoaiHinhCha", 1)]
        public string IdMaLoaiHinhCha
        {
            get => _idMaLoaiHinhCha;
            set => SetProperty(ref _idMaLoaiHinhCha, value);
        }

        private string _lns;
        [ColumnAttribute("LNS", 2)]
        public string LNS
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _description;
        [ColumnAttribute("Mô tả", 3)]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _tongSoThu;
        [ColumnAttribute("Tổng số thu", 4, ValidateType.IsNumber)]
        public string TongSoThu
        {
            get => _tongSoThu;
            set => SetProperty(ref _tongSoThu, value);
        }

        private string _tongSoChiPhi;
        [ColumnAttribute("Tổng số chi phí", 5, ValidateType.IsNumber)]
        public string TongSoChiPhi
        {
            get => _tongSoChiPhi;
            set => SetProperty(ref _tongSoChiPhi, value);
        }

        private string _tongSoQTNS;
        [ColumnAttribute("Tổng số QTNS", 6, ValidateType.IsNumber)]
        public string TongSoQTNS
        {
            get => _tongSoQTNS;
            set => SetProperty(ref _tongSoQTNS, value);
        }

        private string _khauHaoTSTD;
        [ColumnAttribute("Khấu hao TSCĐ", 7, ValidateType.IsNumber)]
        public string KhauHaoTSTD
        {
            get => _khauHaoTSTD;
            set => SetProperty(ref _khauHaoTSTD, value);
        }

        private string _tienLuong;
        [ColumnAttribute("Tiền lương", 8, ValidateType.IsNumber)]
        public string TienLuong
        {
            get => _tienLuong;
            set => SetProperty(ref _tienLuong, value);
        }

        private string _qtnsKhac;
        [ColumnAttribute("QTNS Khác", 9, ValidateType.IsNumber)]
        public string QtnsKhac
        {
            get => _qtnsKhac;
            set => SetProperty(ref _qtnsKhac, value);
        }

        private string _chiPhiKhac;
        [ColumnAttribute("Chi phí khác", 10, ValidateType.IsNumber)]
        public string ChiPhiKhac
        {
            get => _chiPhiKhac;
            set => SetProperty(ref _chiPhiKhac, value);
        }

        private string _tongNopNSNN;
        [ColumnAttribute("Tổng nộp NSNN", 11, ValidateType.IsNumber)]
        public string TongNopNSNN
        {
            get => _tongNopNSNN;
            set => SetProperty(ref _tongNopNSNN, value);
        }

        private string _thueGTGT;
        [ColumnAttribute("Thuế GTGT", 12, ValidateType.IsNumber)]
        public string ThueGTGT
        {
            get => _thueGTGT;
            set => SetProperty(ref _thueGTGT, value);
        }

        private string _thueTNDN;
        [ColumnAttribute("Thuế TNDN", 13, ValidateType.IsNumber)]
        public string ThueTNDN
        {
            get => _thueTNDN;
            set => SetProperty(ref _thueTNDN, value);
        }

        private string _thueTndnNopQuaBQP;
        [ColumnAttribute("Thuế Tndn Nộp qua BQP", 14, ValidateType.IsNumber)]
        public string ThueTndnNopQuaBQP
        {
            get => _thueTndnNopQuaBQP;
            set => SetProperty(ref _thueTndnNopQuaBQP, value);
        }

        private string _phiLePhi;
        [ColumnAttribute("Phí/Lệ phí", 15, ValidateType.IsNumber)]
        public string PhiLePhi
        {
            get => _phiLePhi;
            set => SetProperty(ref _phiLePhi, value);
        }

        private string _nsnnKhac;
        [ColumnAttribute("NSNN khác", 16, ValidateType.IsNumber)]
        public string NsnnKhac
        {
            get => _nsnnKhac;
            set => SetProperty(ref _nsnnKhac, value);
        }

        private string _nsnnKhacNopQuaBQP;
        [ColumnAttribute("Nsnn khác nộp qua BQP", 17, ValidateType.IsNumber)]
        public string NsnnKhacNopQuaBQP
        {
            get => _nsnnKhacNopQuaBQP;
            set => SetProperty(ref _nsnnKhacNopQuaBQP, value);
        }

        private string _chenhLech;
        [ColumnAttribute("Chênh lệch", 18, ValidateType.IsNumber)]
        public string ChenhLech
        {
            get => _chenhLech;
            set => SetProperty(ref _chenhLech, value);
        }

        private string _nopNSQP;
        [ColumnAttribute("Nộp NSQP", 19, ValidateType.IsNumber)]
        public string NopNSQP
        {
            get => _nopNSQP;
            set => SetProperty(ref _nopNSQP, value);
        }

        private string _boSungKinhPhi;
        [ColumnAttribute("Bổ sung kinh phí", 20, ValidateType.IsNumber)]
        public string BoSungKinhPhi
        {
            get => _boSungKinhPhi;
            set => SetProperty(ref _boSungKinhPhi, value);
        }

        private string _ppTrichCacQuy;
        [ColumnAttribute("Trích các quỹ", 21, ValidateType.IsNumber)]
        public string PpTrichCacQuy
        {
            get => _ppTrichCacQuy;
            set => SetProperty(ref _ppTrichCacQuy, value);
        }

        private string _chuaPhanPhoi;
        [ColumnAttribute("Chưa phân phối", 22, ValidateType.IsNumber)]
        public string ChuaPhanPhoi
        {
            get => _chuaPhanPhoi;
            set => SetProperty(ref _chuaPhanPhoi, value);
        }

        private string _ghiChu;
        [ColumnAttribute("Ghi chú", 23)]
        public string GhiChu
        {
            get => _ghiChu;
            set => SetProperty(ref _ghiChu, value);
        }
    }
}
