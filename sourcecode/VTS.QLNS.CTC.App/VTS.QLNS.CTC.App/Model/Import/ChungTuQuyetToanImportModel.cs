using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Mau 01", 12, 1)]
    public class ChungTuQuyetToanImportModel : BindableBase
    {
        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
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

        private string _soBaoCao;
        [ColumnAttribute("Số báo cáo", 0)]
        public string SoBaoCao
        {
            get => _soBaoCao;
            set => SetProperty(ref _soBaoCao, value);
        }

        private string _ngayQuyetDinh;
        [ColumnAttribute("Ngày quyết định", 1, ValidateType.IsDateTime)]
        public string NgayQuyetDinh
        {
            get => _ngayQuyetDinh;
            set => SetProperty(ref _ngayQuyetDinh, value);
        }

        private string _tenDuAn;
        [ColumnAttribute("Tên dự án", 2)]
        public string TenDuAn
        {
            get => _tenDuAn;
            set => SetProperty(ref _tenDuAn, value);
        }

        private string _maDuAn;
        [ColumnAttribute("Mã dự án", 3)]
        public string MaDuAn
        {
            get => _maDuAn;
            set => SetProperty(ref _maDuAn, value);
        }

        public Guid IdDuAn { get; set; }

        //private string _ngayKhoiCong;
        //[ColumnAttribute("Ngày khởi công", 4, ValidateType.IsDateTime)]
        //public string NgayKhoiCong
        //{
        //    get => _ngayKhoiCong;
        //    set => SetProperty(ref _ngayKhoiCong, value);
        //}

        //private string _ngayHoanThanh;
        //[ColumnAttribute("Ngày hoàn thành", 5, ValidateType.IsDateTime)]
        //public string NgayHoanThanh
        //{
        //    get => _ngayHoanThanh;
        //    set => SetProperty(ref _ngayHoanThanh, value);


        public DateTime? NgayQuyetDinhValue
        {
            get
            {
                DateTime value;
                bool check = DateTime.TryParse(NgayQuyetDinh + " 00:00:00", out value);
                if (check)
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }

        //public DateTime? NgayKhoiCongValue
        //{
        //    get
        //    {
        //        DateTime value;
        //        bool check = DateTime.TryParse(NgayKhoiCong + " 00:00:00", out value);
        //        if (check)
        //        {
        //            return value;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public DateTime? NgayHoanThanhValue
        //{
        //    get
        //    {
        //        DateTime value;
        //        bool check = DateTime.TryParse(NgayHoanThanh + " 00:00:00", out value);
        //        if (check)
        //        {
        //            return value;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}



        public List<PheDuyetQuyetToanNguonVonImportModel> ListNguonVon { get; set; }

        public double ChiPhiThietHai { get; set; }
        public double ChiPhiKhongTaoNenTaiSan { get; set; }
        public double DaiHanCDT { get; set; }
        public double DaiHanDonViKhac { get; set; }
        public double NganHanCDT { get; set; }
        public double NganHanDonViKhac { get; set; }

        public List<PheDuyetQuyetToanChiPhiImportModel> ListChiPhi { get; set; }
    }
}
