using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDuToanFilterModel : BindableBase
    {
        public NhDuToanFilterModel()
        {
            this.SoQuyetDinh = string.Empty;
            this.DuAn = string.Empty;
            this.NgayQuyetDinhFrom = null;
        }

        private string _soQuyetDinh;
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set => SetProperty(ref _soQuyetDinh, value);
        }

        private string _duAn;
        public string DuAn
        {
            get => _duAn;
            set => SetProperty(ref _duAn, value);
        }

        private DateTime? _ngayQuyetDinhFrom;
        public DateTime? NgayQuyetDinhFrom
        {
            get => _ngayQuyetDinhFrom;
            set => SetProperty(ref _ngayQuyetDinhFrom, value);
        }

        private DateTime? _ngayQuyetDinhTo;
        public DateTime? NgayQuyetDinhTo
        {
            get => _ngayQuyetDinhTo;
            set => SetProperty(ref _ngayQuyetDinhTo, value);
        }
    }
}
