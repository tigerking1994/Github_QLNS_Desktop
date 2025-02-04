using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class SumExplainSubtractModel : BindableBase
    {
        private double _tongLuongThang;
        public double TongLuongThang
        {
            get => _tongLuongThang;
            set => SetProperty(ref _tongLuongThang, value);
        }

        private double _tongNgayNghi;
        public double TongNgayNghi
        {
            get => _tongNgayNghi;
            set => SetProperty(ref _tongNgayNghi, value);
        }

        private double _tongSoNguoi;
        public double TongSoNguoi
        {
            get => _tongSoNguoi;
            set => SetProperty(ref _tongSoNguoi, value);
        }

        private double _tongLuongCapBac;
        public double TongLuongCapBac
        {
            get => _tongLuongCapBac;
            set => SetProperty(ref _tongLuongCapBac, value);
        }

        private double _tongLuongThamNien;
        public double TongLuongThamNien
        {
            get => _tongLuongThamNien;
            set => SetProperty(ref _tongLuongThamNien, value);
        }

        private double _tongLuongPhuCapCongVu;
        public double TongLuongPhuCapCongVu
        {
            get => _tongLuongPhuCapCongVu;
            set => SetProperty(ref _tongLuongPhuCapCongVu, value);
        }

        private double _tongLuongPhuCapKhacBh;
        public double TongLuongPhuCapKhacBh
        {
            get => _tongLuongPhuCapKhacBh;
            set => SetProperty(ref _tongLuongPhuCapKhacBh, value);
        }

        private double _tongLuongPhuCapKhac;
        public double TongLuongPhuCapKhac
        {
            get => _tongLuongPhuCapKhac;
            set => SetProperty(ref _tongLuongPhuCapKhac, value);
        }

        private double _tongBaoHiem;
        public double TongBaoHiem
        {
            get => _tongBaoHiem;
            set => SetProperty(ref _tongBaoHiem, value);
        }
    }
}
