using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class LoaiChiPhiNguonVonModel : DetailModelBase
    {
        private int _loai;
        public int Loai
        {
            get => _loai;
            set => SetProperty(ref _loai, value);
        }

        private string _tenLoai;
        public string TenLoai
        {
            get => _tenLoai;
            set => SetProperty(ref _tenLoai, value);
        }
    }
}
