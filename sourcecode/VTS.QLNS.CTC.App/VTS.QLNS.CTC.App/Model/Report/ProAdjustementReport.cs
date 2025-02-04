using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class ProAdjustementReport : BindableBase
    {
        public Guid? DonViQuanLyId { get; set; }
        public Guid? IIDDuAnId { get; set; }
        public string STenDuAn { get; set; }
        public int? IIDNguonVonId { get; set; }
        public Guid? IIDLoaiNguonVonId { get; set; }
        public Guid? IIDLoaiCongTrinhId { get; set; }
        public Guid? IIDCapPheDuyetId { get; set; }
        public Guid? IIDMucId { get; set; }
        public Guid? IIDTieuMucId { get; set; }
        public Guid? IIDTietMucId { get; set; }
        public Guid? IIDNganhId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public double? ChiTieuDauNam { get; set; }

        private double? _giam;
        public double? Giam
        {
            get
            {
                if(_giam != null)
                {
                    _giam =  Math.Abs(_giam.Value);
                }

                return _giam;
            }
            set
            {
                SetProperty(ref _giam, value);
                OnPropertyChanged(nameof(KeHoachDieuChinh));
            }
        }

        private double? _tang;
        public double? Tang
        {
            get
            {
                if (_tang != null)
                {
                    _tang = Math.Abs(_tang.Value);
                }

                return _tang;
            }
            set
            {
                SetProperty(ref _tang, value);
                OnPropertyChanged(nameof(KeHoachDieuChinh));
            }
        }

        public Guid? OrderDonVi { get; set; }

        private double? _keHoachDieuChinh;
        public double? KeHoachDieuChinh
        {
            get
            {
                _keHoachDieuChinh = (ChiTieuDauNam != null ? ChiTieuDauNam.Value : 0) 
                                    - (Giam != null ? Giam.Value : 0)
                                    + (Tang != null ? Tang.Value : 0);
                return _keHoachDieuChinh;
            }
            set => SetProperty(ref _keHoachDieuChinh, value);
        }
        public int? CT { get; set; }
        public int? CPD { get; set; }
        public string MaThuTu { get; set; }
        public string MaPhanCapDonVi { get; set; }
        public string TenDuAnCT { get; set; }

        private bool _isHangCha;
        public bool IsHangCha
        {
            get => _isHangCha;
            set => SetProperty(ref _isHangCha, value);
        }

        public string STenMuc { get; set; }
        public string IdDonVi { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
    }
}
