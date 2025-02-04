using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ChuTruongDTFilterModel: BindableBase
    {
        public ChuTruongDTFilterModel()
        {
            this.SoQuyetDinh = string.Empty;
            this.NoiDung = string.Empty;
            this.NgayQuyetDinhFrom = null;
            this.TMDTFrom = null;
            this.TMDTTo = null;
            this.TMDTValue = null;
        }
        private string _soQuyetDinh;
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set => SetProperty(ref _soQuyetDinh, value);
        }
        private string _noiDung;
        public string NoiDung
        {
            get => _noiDung;
            set => SetProperty(ref _noiDung, value);
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
        private double? _tMDTFrom;
        public double? TMDTFrom
        {
            get => _tMDTFrom;
            set => SetProperty(ref _tMDTFrom, value);
        }
        private double? _tMDTTo;
        public double? TMDTTo
        {
            get => _tMDTTo;
            set => SetProperty(ref _tMDTTo, value);
        }

        private double? _tMDTValue;

        public double? TMDTValue
        {
            get => _tMDTValue;
            set => SetProperty(ref _tMDTValue, value);
        }
    }
}
