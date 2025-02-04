using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhKhTongTheModel : ModelBase
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
        //[Validate("Giai đoạn từ", Utility.Enum.DATA_TYPE.Int, 4, true)]
        public int? IGiaiDoanTu
        {
            get => _iGiaiDoanTu;
            set => SetProperty(ref _iGiaiDoanTu, value);
        }

        private int? _iGiaiDoanDen;
        //[Validate("Giai đoạn đến", Utility.Enum.DATA_TYPE.Int, 4, true)]
        public int? IGiaiDoanDen
        {
            get => _iGiaiDoanDen;
            set => SetProperty(ref _iGiaiDoanDen, value);
        }

        private int? _iGiaiDoanTu_TTCP;
        [Validate("Giai đoạn từ TTCP", Utility.Enum.DATA_TYPE.Int, 4, true)]
        public int? IGiaiDoanTu_TTCP
        {
            get => _iGiaiDoanTu_TTCP;
            set => SetProperty(ref _iGiaiDoanTu_TTCP, value);
        }

        private int? _iGiaiDoanDen_TTCP;
        [Validate("Giai đoạn đến TTCP", Utility.Enum.DATA_TYPE.Int, 4, true)]
        public int? IGiaiDoanDen_TTCP
        {
            get => _iGiaiDoanDen_TTCP;
            set => SetProperty(ref _iGiaiDoanDen_TTCP, value);
        }

        private int? _iGiaiDoanTu_BQP;
        [Validate("Giai đoạn từ BQP", Utility.Enum.DATA_TYPE.Int, 4, true)]
        public int? IGiaiDoanTu_BQP
        {
            get => _iGiaiDoanTu_BQP;
            set => SetProperty(ref _iGiaiDoanTu_BQP, value);
        }

        private int? _iGiaiDoanDen_BQP;
        [Validate("Giai đoạn đến BQP", Utility.Enum.DATA_TYPE.Int, 4, true)]
        public int? IGiaiDoanDen_BQP
        {
            get => _iGiaiDoanDen_BQP;
            set => SetProperty(ref _iGiaiDoanDen_BQP, value);
        }

        public int? INamKeHoach { get; set; }

        [Validate("Số kế hoạch TTCP", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoKeHoachTtcp { get; set; }

        [Validate("Ngày ban hành kế hoạch TTCP", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayKeHoachTtcp { get; set; }

        private string _sMoTaChiTietKhttcp;
        [Validate("Mô tả TTCP", Utility.Enum.DATA_TYPE.String)]
        public string SMoTaChiTietKhttcp
        {
            get => _sMoTaChiTietKhttcp;
            set => SetProperty(ref _sMoTaChiTietKhttcp, value);
        }

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

        [Validate("Số kế hoạch BQP", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoKeHoachBqp { get; set; }

        [Validate("Ngày ban hành kế hoạch BQP", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayKeHoachBqp { get; set; }

        private string _sMoTaChiTietKhbqp;
        [Validate("Mô tả BQP", Utility.Enum.DATA_TYPE.String)]
        public string SMoTaChiTietKhbqp
        {
            get => _sMoTaChiTietKhbqp;
            set => SetProperty(ref _sMoTaChiTietKhbqp, value);
        }

        private double _fTongGiaTriKhbqp;
        public double FTongGiaTriKhbqp
        {
            get => _fTongGiaTriKhbqp;
            set => SetProperty(ref _fTongGiaTriKhbqp, value);
        }

        private double _fTongGiaTriKhbqpVnd;
        public double FTongGiaTriKhbqpVnd
        {
            get => _fTongGiaTriKhbqpVnd;
            set => SetProperty(ref _fTongGiaTriKhbqpVnd, value);
        }

        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }

        public int ILanDieuChinh { get; set; }
        public string SNam => (ILoai == Loai_KHTT.GIAIDOAN) ? string.Format("{0} - {1}", IGiaiDoanTu, IGiaiDoanDen) : INamKeHoach.ToString();
        public string SGiaiDoanTTCP => IdParentTongThe != null ? string.Format("{0} ~ {1}", IGiaiDoanTu_TTCP, IGiaiDoanDen_TTCP) : string.Empty;
        public string SGiaiDoanBQP => IdParentTongThe != null ? string.Format("{0} ~ {1}", IGiaiDoanTu_BQP, IGiaiDoanDen_BQP) : string.Empty;
        public int TotalFiles { get; set; }
        public string DieuChinhTu { get; set; }
        public int? ILoai { get; set; }
        public string TenKeHoach { get; set; }

        private bool _isShowChildren;
        public bool IsShowChildren
        {
            get => _isShowChildren;
            set => SetProperty(ref _isShowChildren, value);
        }

        public HashSet<Guid> AncestorIds { get; internal set; }
        public bool HasChildren { get; internal set; }
        public Guid? IdParentTongThe { get; set; }
    }
}