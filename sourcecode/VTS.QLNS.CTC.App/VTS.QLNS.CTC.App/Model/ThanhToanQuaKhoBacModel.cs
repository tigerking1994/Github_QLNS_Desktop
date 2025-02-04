using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ThanhToanQuaKhoBacModel : BindableBase
    {
        public int iRowIndex { get; set; }
        public Guid Id { get; set; }

        private string _sNguoiLap;
        public string sNguoiLap
        {
            get => _sNguoiLap;
            set => SetProperty(ref _sNguoiLap, value);
        }

        private string _sSoThanhToan;
        public string sSoThanhToan
        {
            get => _sSoThanhToan;
            set => SetProperty(ref _sSoThanhToan, value);
        }

        private DateTime? _dNgayThanhToan;
        public DateTime? dNgayThanhToan
        {
            get => _dNgayThanhToan;
            set => SetProperty(ref _dNgayThanhToan, value);
        }

        public Guid? iID_DonViNhanThanhToanID { get; set; }
        public Guid? iID_NhomQuanLyID { get; set; }
        public Guid? iID_DonViQuanLyID { get; set; }
        public int iNamKeHoach { get; set; }
        public Guid? iID_LoaiNguonVonID { get; set; }
        public Guid? iID_LoaiNganSach { get; set; }
        public Guid iID_KhoanNganSach { get; set; }
        public double? fGiaTriThanhToan { get; set; }
        public double? fGiaTriTamUng { get; set; }
        public Guid? iID_DonViTienTeID { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public Guid? iID_TienTeID { get; set; }
        public double? fTiGia { get; set; }
        public string sGhiChu { get; set; }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sUserUpdate { get; set; }
        public DateTime? dDateUpdate { get; set; }
        public string sUserDelete { get; set; }
        public DateTime? dDateDelete { get; set; }
        public string iId_MaDonViNhanThanhToanID { get; set; }
        public string iId_MaDonViQuanLyID { get; set; }
        public int? iID_NguonVonID { get; set; }
        public string sTenNguonVon { get; set; }
        public string sLoaiNguonVon { get; set; }
        public string sTenDonVi { get; set; }
        public bool IsEdit { get; set; }
        public List<Guid> lstDuAnId { get; set; }
    }
}
