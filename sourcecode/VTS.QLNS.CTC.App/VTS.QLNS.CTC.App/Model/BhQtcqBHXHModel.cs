using FlexCel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcqBHXHModel : BindableBase
    {
        public Guid Id { get; set; }
        public Guid? IIdDonVi { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime DNgayQuyetDinh { get; set; }
        public int IQuyChungTu { get; set; }
        public int INamChungTu { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string STongHop { get; set; }
        public Guid? IID_TongHopID { get; set; }
        public int ILoaiTongHop { get; set; }
        public bool BIsKhoa { get; set; }

        public Double? _fTongTienDuToanDuyet;
        public Double? FTongTienDuToanDuyet { get => _fTongTienDuToanDuyet; set => SetProperty(ref _fTongTienDuToanDuyet, value); }
        // Lỹ kế đến cuối quý này
        public int? _iTongSoLuyKeCuoiQuyNay;
        public int? ITongSoLuyKeCuoiQuyNay { get => _iTongSoLuyKeCuoiQuyNay; set => SetProperty(ref _iTongSoLuyKeCuoiQuyNay, value); }

        public Double? _fTongTienLuyKeCuoiQuyNay;
        public Double? FTongTienLuyKeCuoiQuyNay { get => _fTongTienLuyKeCuoiQuyNay; set => SetProperty(ref _fTongTienLuyKeCuoiQuyNay, value); }
        //  Sỹ Quan
        public int? _iTongSoSQDeNghi;
        public int? ITongSoSQDeNghi { get => _iTongSoSQDeNghi; set => SetProperty(ref _iTongSoSQDeNghi, value); }

        public Double? _fTongTienSQDeNghi;
        public Double? FTongTienSQDeNghi { get => _fTongTienSQDeNghi; set => SetProperty(ref _fTongTienSQDeNghi, value); }
        // QNCN
        public int? _iTongSoQNCNDeNghi;
        public int? ITongSoQNCNDeNghi { get => _iTongSoQNCNDeNghi; set => SetProperty(ref _iTongSoQNCNDeNghi, value); }

        public Double? _fTongTienQNCNDeNghi;
        public Double? FTongTienQNCNDeNghi { get => _fTongTienQNCNDeNghi; set => SetProperty(ref _fTongTienQNCNDeNghi, value); }

        // CC, cN, VCQP
        public int? _iTongSoCNVCQPDeNghi;
        public int? ITongSoCNVCQPDeNghi { get => _iTongSoCNVCQPDeNghi; set => SetProperty(ref _iTongSoCNVCQPDeNghi, value); }

        public Double? _fTongTienCNVCQPDeNghi;
        public Double? FTongTienCNVCQPDeNghi { get => _fTongTienCNVCQPDeNghi; set => SetProperty(ref _fTongTienCNVCQPDeNghi, value); }

        // HDLD
        public int? _iTongSoHDLDDeNghi;
        public int? ITongSoHDLDDeNghi { get => _iTongSoHDLDDeNghi; set => SetProperty(ref _iTongSoHDLDDeNghi, value); }

        public Double? _fTongTienHDLDDeNghi;
        public Double? FTongTienHDLDDeNghi { get => _fTongTienHDLDDeNghi; set => SetProperty(ref _fTongTienHDLDDeNghi, value); }

        // HSQBS
        public int? _iTongSoHSQBSDeNghi;
        public int? ITongSoHSQBSDeNghi { get => _iTongSoHSQBSDeNghi; set => SetProperty(ref _iTongSoHSQBSDeNghi, value); }

        public Double? _fTongTienHSQBSDeNghi;
        public Double? FTongTienHSQBSDeNghi { get => _fTongTienHSQBSDeNghi; set => SetProperty(ref _fTongTienHSQBSDeNghi, value); }

        // Tổng số
        public int? _iTongSoDeNghi;
        public int? ITongSoDeNghi { get => _iTongSoDeNghi; set => SetProperty(ref _iTongSoDeNghi, value); }

        public Double? _fTongTienDeNghi;
        public Double? FTongTienDeNghi { get => _fTongTienDeNghi; set => SetProperty(ref _fTongTienDeNghi, value); }

        public Double? _fTongTienPheDuyet;
        public Double? FTongTienPheDuyet { get => _fTongTienPheDuyet; set => SetProperty(ref _fTongTienPheDuyet, value); }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
        private bool _bDaTongHop;
        public bool BDaTongHop
        {
            get => _bDaTongHop;
            set => SetProperty(ref _bDaTongHop, value);
        }
        public string SDSLNS { get; set; }
        public string BDaTongHopString => BDaTongHop?  "Đã tổng hợp" : "";

        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }
        public string SoChungTuParent { get; set; }

        private bool _isExpand;
        public bool IsExpand
        {
            get => _isExpand;
            set => SetProperty(ref _isExpand, value);
        }
        public bool IsChildSummary { get; set; }
        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set => SetProperty(ref _isCollapse, value);
        }
        public string SDSSoChungTuTongHop { get; set; }
        public string STenDonVi { get;set; }

        public string SQuy => IQuyChungTu switch
        {
            1 => "Quý I",
            2 => "Quý II",
            3 => "Quý III",
            4 => "Quý IV",
            _ => string.Empty
        };
        private bool _isFilter;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }
    }
}
