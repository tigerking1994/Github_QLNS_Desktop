using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhKhChiTietModel : ModelBase
    {
        public Guid? IIdParentId { get; set; }
        public Guid? IIdParentAdjustId { get; set; }
        public Guid? IIdGocId { get; set; }
        public string SSoKeHoach { get; set; }
        public DateTime? DNgayKeHoach { get; set; }
        public string SMoTaChiTiet { get; set; }

        private double _fGiaTriNgoaiTeKhac;
        public double FGiaTriNgoaiTeKhac
        {
            get => _fGiaTriNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriNgoaiTeKhac, value);
        }

        private double _fGiaTriUsd;
        public double FGiaTriUsd
        {
            get => _fGiaTriUsd;
            set => SetProperty(ref _fGiaTriUsd, value);
        }

        private double _fGiaTriVnd;
        public double FGiaTriVnd
        {
            get => _fGiaTriVnd;
            set => SetProperty(ref _fGiaTriVnd, value);
        }

        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsActive { get; set; }
        public bool BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int ILanDieuChinh { get; set; }
        public string SSoKeHoachTongTheBQP { get; set; }
        public Guid? IIdKHTongTheId { get; set; }
        public Guid? IIdDonViThuHuongId { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public string SSoLanDc { get; set; }
        public int IRowIndex { get; set; }
        public int TotalFiles { get; set; }
        public string DieuChinhTu { get; set; }
    }
}