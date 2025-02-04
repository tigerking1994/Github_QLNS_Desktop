using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhKhcKcbChiTietModel : ModelBase
    {
        private Guid _iID_BH_KHC_KCB_ChiTiet;
        public Guid IID_BH_KHC_KCB_ChiTiet { get => _iID_BH_KHC_KCB_ChiTiet; set => SetProperty(ref _iID_BH_KHC_KCB_ChiTiet, value); }
        private Guid _iID_KHC_KCB;
        public Guid IID_KHC_KCB
        {
            get => _iID_KHC_KCB;
            set => SetProperty(ref _iID_KHC_KCB, value);
        }
        private Guid? _iID_MucLucNganSach;

        public Guid? IID_MucLucNganSach
        {
            get => _iID_MucLucNganSach;
            set => SetProperty(ref _iID_MucLucNganSach, value);
        }
        private string _sNoiDung;

        public string SNoiDung
        {
            get => _sNoiDung; set => SetProperty(ref _sNoiDung, value);
        }

        private double? _fTienDaThucHienNamTruoc;

        public double? FTienDaThucHienNamTruoc
        {
            get => _fTienDaThucHienNamTruoc; set => SetProperty(ref _fTienDaThucHienNamTruoc, value);
        }

        private double? _fTienUocThucHienNamTruoc;

        public double? FTienUocThucHienNamTruoc
        {
            get => _fTienUocThucHienNamTruoc; set => SetProperty(ref _fTienUocThucHienNamTruoc, value);
        }
        private double? _fTienKeHoachThucHienNamNay;

        public double? FTienKeHoachThucHienNamNay
        {
            get => _fTienKeHoachThucHienNamNay; set => SetProperty(ref _fTienKeHoachThucHienNamNay, value);
        }
        private string _sGhiChu;

        public string SGhiChu
        {
            get => _sGhiChu; set => SetProperty(ref _sGhiChu, value);
        }

        private DateTime? _dNgaySua;

        public DateTime? DNgaySua
        {
            get => _dNgaySua; set => SetProperty(ref _dNgaySua, value);
        }

        private DateTime? _dNgayTao;

        public DateTime? DNgayTao
        {
            get => _dNgayTao; set => SetProperty(ref _dNgayTao, value);
        }

        private string _sNguoiSua;

        public string SNguoiSua
        {
            get => _sNguoiSua; set => SetProperty(ref _sNguoiSua, value);
        }

        private string _sNguoiTao;

        public string SNguoiTao
        {
            get => _sNguoiTao; set => SetProperty(ref _sNguoiTao, value);
        }

        public bool IsAuToFillTuChi { get; set; }
        public string SXauNoiMa { get; set; }

        public int INamLamViec { get; set; }
        public string IIDMaDonVi { get; set; }
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

        public bool IsDataNotNull => FTienDaThucHienNamTruoc > 0 || FTienUocThucHienNamTruoc > 0 || FTienKeHoachThucHienNamNay > 0;

        private bool _isRemainRow;
        public bool IsRemainRow
        {
            get => _isRemainRow;
            set => SetProperty(ref _isRemainRow, value);
        }

        public int IRemainRow { get; set; }

    }
}
