using FlexCel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcnKCBModel : BindableBase
    {
        public  Guid Id { get; set; }
        public Guid? IIdDonVi { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public bool BThucChiTheo4Quy { get; set; }
        public int INamLamViec { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string STongHop { get; set; }
        public Guid? IITongHopID { get; set; }
        public int ILoaiTongHop { get; set; }
        public bool BIsKhoa { get; set; }

        private Double? _fTongTienDuToanNamTruocChuyenSang;
        public Double? FTongTienDuToanNamTruocChuyenSang
        {
            get => _fTongTienDuToanNamTruocChuyenSang;
            set => SetProperty(ref _fTongTienDuToanNamTruocChuyenSang, value);
        }

        private Double? _fTongTienDuToanGiaoNamNay;
        public Double? FTongTienDuToanGiaoNamNay
        {
            get => _fTongTienDuToanGiaoNamNay;
            set => SetProperty(ref _fTongTienDuToanGiaoNamNay, value);
        }

        private Double? _fTongTienTongDuToanDuocGiao;
        public Double? FTongTienTongDuToanDuocGiao
        {
            get => _fTongTienTongDuToanDuocGiao;
            set => SetProperty(ref _fTongTienTongDuToanDuocGiao, value);
        }

        private Double? _fTongTienThucChi;
        public Double? FTongTienThucChi
        {
            get => _fTongTienThucChi;
            set => SetProperty(ref _fTongTienThucChi, value);
        }

        private Double? _fTongTienThua;
        public Double? FTongTienThua
        {
            get => _fTongTienThua;
            set => SetProperty(ref _fTongTienThua, value);
        }

        private Double? _fTongTienThieu;
        public Double? FTongTienThieu
        {
            get => _fTongTienThieu;
            set => SetProperty(ref _fTongTienThieu, value);
        }

        private Double? _fTiLeThucHienTrenDuToan;
        public Double? FTiLeThucHienTrenDuToan
        {
            get => _fTiLeThucHienTrenDuToan;
            set => SetProperty(ref _fTiLeThucHienTrenDuToan, value);
        }

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
        private bool _isFilter;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }

    }
}
