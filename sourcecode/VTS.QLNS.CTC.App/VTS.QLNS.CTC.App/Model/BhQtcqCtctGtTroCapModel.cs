using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcqCtctGtTroCapModel : BindableBase
    {
        public Guid Id { get; set; }
        public Guid IID_QTC_Quy_ChungTu { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public int INamLamViec { get; set; }
        public int IQuy { get; set; }
        private string _sMaHieuCanBo;
        public string SMaHieuCanBo
        {
            get => _sMaHieuCanBo;
            set => SetProperty(ref _sMaHieuCanBo, value);
        }
        private string _sTenCanBo;
        public string STenCanBo
        {
            get => _sTenCanBo;
            set => SetProperty(ref _sTenCanBo, value);
        }
        private string _sMaCapBac;
        public string SMaCapBac
        {
            get => _sMaCapBac;
            set => SetProperty(ref _sMaCapBac, value);
        }
        private string _iD_MaPhanHo;
        public string ID_MaPhanHo
        {
            get => _iD_MaPhanHo;
            set => SetProperty(ref _iD_MaPhanHo, value);
        }
        private int? _iSoNgayHuong;
        public int? ISoNgayHuong
        {
            get => _iSoNgayHuong;
            set => SetProperty(ref _iSoNgayHuong, value);
        }
        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }
        private double? _fSoTien;
        public double? FSoTien
        {
            get => _fSoTien;
            set => SetProperty(ref _fSoTien, value);
        }
        public string SXauNoiMa { get; set; }
        private string _sTenPhanHo;

        public string STenPhanHo
        {
            get => _sTenPhanHo;
            set => SetProperty(ref _sTenPhanHo, value);
        }
        public string ID_MaDonVi { get; set; }
        public bool HasData => !string.IsNullOrWhiteSpace(SMaHieuCanBo) || !string.IsNullOrWhiteSpace(STenPhanHo) || !string.IsNullOrWhiteSpace(SSoQuyetDinh);
        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;
            set => SetProperty(ref _isModified, value);
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        private bool _isAdded;
        public bool IsAdded
        {
            get => _isAdded;
            set => SetProperty(ref _isAdded, value);
        }

        private bool _isFilter = true;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }
        private string _sTenCapBac;
        public string STenCapBac
        {
            get => _sTenCapBac;
            set => SetProperty(ref _sTenCapBac, value);
        }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        private string _sSoSoBHXH;
        public string SSoSoBHXH
        {
            get => _sSoSoBHXH;
            set => SetProperty(ref _sSoSoBHXH, value);
        }
        private DateTime? _dTuNgay;
        public DateTime? DTuNgay
        {
            get => _dTuNgay;
            set => SetProperty(ref _dTuNgay, value);
        }
        private DateTime? _dDenNgay;
        public DateTime? DDenNgay
        {
            get => _dDenNgay;
            set => SetProperty(ref _dDenNgay, value);
        }
        private double? _fTienLuongThangDongBHXH;
        public double? FTienLuongThangDongBHXH
        {
            get => _fTienLuongThangDongBHXH;
            set => SetProperty(ref _fTienLuongThangDongBHXH, value);
        }
        public string SMaCanBo { get; set; }
        private bool _checked;
        public bool Checked
        {
            get => _checked;
            set => SetProperty(ref _checked, value);
        }

        private int? _iSoNgayTruyLinh;
        public int? ISoNgayTruyLinh
        {
            get => _iSoNgayTruyLinh;
            set => SetProperty(ref _iSoNgayTruyLinh, value);

        }
        private double? _fTienTruyLinh;
        public Double? FTienTruyLinh
        {
            get => _fTienTruyLinh;
            set => SetProperty(ref _fTienTruyLinh, value);
        }
        public bool BHangCha { get; set; }
        public string TuNgay { get;set; }
        public string DenNgay { get; set; }
        public string NgayQuyetDinh { get; set; }
    }
}
