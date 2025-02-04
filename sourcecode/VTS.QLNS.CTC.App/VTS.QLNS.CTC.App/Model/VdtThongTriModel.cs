using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtThongTriModel : BindableBase
    {
        private bool _bIsChecked;
        public bool BIsChecked
        {
            get => _bIsChecked;
            set => SetProperty(ref _bIsChecked, value);
        }

        public Guid? Id { get; set; }
        public int iRowIndex { get; set; }

        private string _sMaThongTri;
        public string sMaThongTri
        {
            get => _sMaThongTri;
            set => SetProperty(ref _sMaThongTri, value);
        }

        private DateTime? _dNgayThongTri;
        public DateTime? dNgayThongTri
        {
            get => _dNgayThongTri;
            set => SetProperty(ref _dNgayThongTri, value);
        }

        private int _iNamThongTri;
        public int iNamThongTri
        {
            get => _iNamThongTri;
            set => SetProperty(ref _iNamThongTri, value);
        }

        private string _sNguoiLap;
        public string sNguoiLap
        {
            get => _sNguoiLap;
            set => SetProperty(ref _sNguoiLap, value);
        }

        private string _sTruongPhong;
        public string sTruongPhong
        {
            get => _sTruongPhong;
            set => SetProperty(ref _sTruongPhong, value);
        }

        private string _sThuTruongDonVi;
        public string sThuTruongDonVi
        {
            get => _sThuTruongDonVi;
            set => SetProperty(ref _sThuTruongDonVi, value);
        }

        public string sMaNguonVon { get; set; }

        private Guid? _iID_DonViID;
        public Guid? iID_DonViID
        {
            get => _iID_DonViID;
            set => SetProperty(ref _iID_DonViID, value);
        }

        private string _iID_MaDonViID;
        public string iID_MaDonViID
        {
            get => _iID_MaDonViID;
            set => SetProperty(ref _iID_MaDonViID, value);
        }

        public DateTime? dNgayLapGanNhat { get; set; }
        public string sTenDonVi { get; set; }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sUserUpdate { get; set; }
        public DateTime? dDateUpdate { get; set; }
        public string sUserDelete { get; set; }
        public DateTime? dDateDelete { get; set; }
        public string sMaLoaiCongTrinh { get; set; }
        public string sTenLoaiCongTrinh { get; set; }
        public Guid? iID_LoaiThongTriID { get; set; }
        public Guid? iID_NhomQuanLyID { get; set; }
        public bool? bIsCanBoDuyet { get; set; }
        public string sIsCanBoDuyet { get; set; }
        public bool? bIsDuyet { get; set; }
        public string sIsDuyet { get; set; }
        public bool? bThanhToan { get; set; }
        public string sMoTa { get; set; }
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private int _iLoaiThongTri;
        public int ILoaiThongTri
        {
            get => _iLoaiThongTri;
            set => SetProperty(ref _iLoaiThongTri, value);
        }

        public string SLoaiThongTri
        {
            get
            {
                string sLoaiThongTri = string.Empty;
                switch (ILoaiThongTri)
                {
                    case (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN:
                        sLoaiThongTri = LoaiThongTriEnum.Name.CAP_THANH_TOAN;
                        break;
                    case (int)LoaiThongTriEnum.Type.CAP_TAM_UNG:
                        sLoaiThongTri = LoaiThongTriEnum.Name.CAP_TAM_UNG;
                        break;
                    case (int)LoaiThongTriEnum.Type.CAP_KINH_PHI:
                        sLoaiThongTri = LoaiThongTriEnum.Name.CAP_KINH_PHI;
                        break;
                    case (int)LoaiThongTriEnum.Type.CAP_HOP_THUC:
                        sLoaiThongTri = LoaiThongTriEnum.Name.CAP_HOP_THUC;
                        break;
                }
                return sLoaiThongTri;
            }
        }

        private int _iNamNganSach;
        public int INamNganSach
        {
            get => _iNamNganSach;
            set => SetProperty(ref _iNamNganSach, value);
        }

        public Guid? IIdBcQuyetToanNienDo { get; set; }

        // other properties
        public IEnumerable<VdtTtDeNghiThanhToanModel> ItemsChungTuThanhToan { get; set; }
    }
}
