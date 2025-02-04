using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.ViewModel;

namespace VTS.QLNS.CTC.App.Model
{
    public class ApproveSettlementDoneDialogModel : ViewModelBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _soQuyetDinh;
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set => SetProperty(ref _soQuyetDinh, value);
        }

        //private DateTime? _ngayQuyetDinh;
        //public DateTime? NgayQuyetDinh
        //{
        //    get => _ngayQuyetDinh;
        //    set => SetProperty(ref _ngayQuyetDinh, value);
        //}

        private string _coQuanPheDuyet;
        public string CoQuanPheDuyet
        {
            get => _coQuanPheDuyet;
            set => SetProperty(ref _coQuanPheDuyet, value);
        }

        private string _nguoiKy;
        public string NguoiKy
        {
            get => _nguoiKy;
            set => SetProperty(ref _nguoiKy, value);
        }

        private double? _chiPhiThietHai;
        public double? ChiPhiThietHai
        {
            get => _chiPhiThietHai;
            set => SetProperty(ref _chiPhiThietHai, value);
        }

        private double? _chiPhiKhongTaoTaiSan;
        public double? ChiPhiKhongTaoTaiSan
        {
            get => _chiPhiKhongTaoTaiSan;
            set => SetProperty(ref _chiPhiKhongTaoTaiSan, value);
        }

        private double? _daiHanThuocQuanLy;
        public double? DaiHanThuocQuanLy
        {
            get => _daiHanThuocQuanLy;
            set => SetProperty(ref _daiHanThuocQuanLy, value);
        }

        private double? _daiHanDonViKhacQuanLy;
        public double? DaiHanDonViKhacQuanLy
        {
            get => _daiHanDonViKhacQuanLy;
            set => SetProperty(ref _daiHanDonViKhacQuanLy, value);
        }

        private double? _nganHanThuocQuanLy;
        public double? NganHanThuocQuanLy
        {
            get => _nganHanThuocQuanLy;
            set => SetProperty(ref _nganHanThuocQuanLy, value);
        }

        private double? _nganHanDonViKhacQuanLy;
        public double? NganHanDonViKhacQuanLy
        {
            get => _nganHanDonViKhacQuanLy;
            set => SetProperty(ref _nganHanDonViKhacQuanLy, value);
        }
    }
}
