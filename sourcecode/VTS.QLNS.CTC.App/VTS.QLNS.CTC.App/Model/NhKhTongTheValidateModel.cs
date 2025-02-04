using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhKhTongTheValidateModel : ModelBase
    {
        public Guid? IIdParentAdjustId { get; set; }
        public Guid? IIdGocId { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int IRowIndex { get; set; }

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set => SetProperty(ref _iIdParentId, value);
        }
        private int? _iGiaiDoanTu;
        public int? IGiaiDoanTu
        {
            get => _iGiaiDoanTu;
            set => SetProperty(ref _iGiaiDoanTu, value);
        }

        private int? _iGiaiDoanDen;
        public int? IGiaiDoanDen
        {
            get => _iGiaiDoanDen;
            set => SetProperty(ref _iGiaiDoanDen, value);
        }
        public int? INamKeHoach { get; set; }
        public string SSoKeHoachTtcp { get; set; }
        public DateTime? DNgayKeHoachTtcp { get; set; }
        public string SMoTaChiTietKhttcp { get; set; }

        private double _fTongGiaTriKhttcp;
        public double FTongGiaTriKhttcp
        {
            get => _fTongGiaTriKhttcp;
            set => SetProperty(ref _fTongGiaTriKhttcp, value);
        }

        private bool _bIsActive;
        public bool BIsActive
        {
            get => _bIsActive;
            set => SetProperty(ref _bIsActive, value);
        }

        public bool BIsGoc { get; set; }
        [ValidateAttribute("Số kế hoạch BQP", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoKeHoachBqp { get; set; }
        [ValidateAttribute("Ngày ban hành kế hoạch BQP", Utility.Enum.DATA_TYPE.Date, true)]

        public DateTime? DNgayKeHoachBqp { get; set; }
        [ValidateAttribute("Mô tả BQP", Utility.Enum.DATA_TYPE.String, 4000, false)]

        public string SMoTaChiTietKhbqp { get; set; }

        private double _fTongGiaTriKhbqp;
        public double FTongGiaTriKhbqp
        {
            get => _fTongGiaTriKhbqp;
            set => SetProperty(ref _fTongGiaTriKhbqp, value);
        }

        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }

        public int ILanDieuChinh { get; set; }
        public string SNam => (ILoai == Loai_KHTT.GIAIDOAN) ? string.Format("{0} - {1}", IGiaiDoanTu, IGiaiDoanDen) : INamKeHoach.ToString();
        public int TotalFiles { get; set; }
        public string DieuChinhTu { get; set; }
        public int? ILoai { get; set; }
        public string TenKeHoach { get; set; }
    }
}