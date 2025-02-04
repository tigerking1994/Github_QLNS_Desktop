using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDtTmBHYTTNChiTietModel : ModelBase
    {
        public override Guid Id { get; set; }
        private Guid? _iID_DTTM_BHYT_ThanNhan;
        public Guid? IID_DTTM_BHYT_ThanNhan { get => _iID_DTTM_BHYT_ThanNhan; set => SetProperty(ref _iID_DTTM_BHYT_ThanNhan, value); }
        private Guid _iID_MLNS;
        public Guid IID_MLNS { get => _iID_MLNS; set => SetProperty(ref _iID_MLNS, value); }
        public string SNoiDung { get; set; }
        private string _sLNS;
        public string SLNS { get => _sLNS; set => SetProperty(ref _sLNS, value); }
        private double? _fDuToan;
        public double? FDuToan { get => _fDuToan; set => SetProperty(ref _fDuToan, value); }
        private DateTime? _dNgaySua;
        public DateTime? DNgaySua { get => _dNgaySua; set => SetProperty(ref _dNgaySua, value); }
        private DateTime? _dNgayTao;
        public DateTime? DNgayTao { get => _dNgayTao; set => SetProperty(ref _dNgayTao, value); }
        private string _sNguoiSua;
        public string SNguoiSua { get => _sNguoiSua; set => SetProperty(ref _sNguoiSua, value); }
        private string _sNguoiTao;
        public string SNguoiTao { get => _sNguoiTao; set => SetProperty(ref _sNguoiTao, value); }
        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu; set => SetProperty(ref _sGhiChu, value);
        }
        private string _sXauNoiMa;
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);

        }
        public bool IsAuToFillTuChi { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public string SMoTa { get; set; }
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public Guid IdParent { get; set; }
        public string STenDonVi { get; set; }
        public bool BHangCha { get; set; }

        public string STTM { get; set; }
    }
}
