using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ThanhToanQuaKhoBacChiTietModel : DetailModelBase
    {
        public Guid iID_DuAnID { get; set; }
        public string sXauNoiMa { get; set; }
        public string sTenDuAn { get; set; }
        public Guid? iID_MucID { get; set; }
        public Guid? iID_TieuMucID { get; set; }
        public Guid? iID_TietMucID { get; set; }
        public Guid? iID_NganhID { get; set; }
        public Guid? iID_HopDongID { get; set; }
        public Guid? iID_NhaThauID { get; set; }

        private double? _fGiaTriThanhToan;
        public double? fGiaTriThanhToan
        {
            get => _fGiaTriThanhToan;
            set => SetProperty(ref _fGiaTriThanhToan, value);
        }

        private double? _fGiaTriTamUng;
        public double? fGiaTriTamUng 
        { 
            get => _fGiaTriTamUng; 
            set => SetProperty(ref _fGiaTriTamUng, value); 
        }

        public Guid? iID_DonViTienTeID { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public Guid? iID_TienTeID { get; set; }
        public double? fTiGia { get; set; }
        public double fChiTieuNganSachNam { get; set; }
    }
}
