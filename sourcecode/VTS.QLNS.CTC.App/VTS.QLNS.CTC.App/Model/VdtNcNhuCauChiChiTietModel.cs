using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtNcNhuCauChiChiTietModel : DetailModelBase
    {
        public int iStt { get; set; }
        public Guid? iID_DuAnId { get; set; }
        public string sTenDuAn { get; set; }
        public string sLoaiThanhToan { get; set; }
        public Guid? iID_LoaiCongTrinhId { get; set; }
        public double fKeHoachVonNam { get; set; }
        public double fThanhToanKLHTQuyTruoc { get; set; }
        public double fThanhToanTamUngQuyTruoc { get; set; }
        public double fTongQuyTruoc { get; set; }
        public double fThanhToanKLHTQuyNay { get; set; }
        public double fThanhToanTamUngQuyNay { get; set; }
        public double fThuHoiUng { get; set; }
        public double fTongQuyNay { get; set; }
        public double fSoConGiaiNganNam { get; set; }

        private string _sGhiChu;
        public string sGhiChu 
        { 
            get => _sGhiChu; 
            set => SetProperty(ref _sGhiChu, value);
        }

        private double _fGiaTriDeNghi;
        public double fGiaTriDeNghi 
        { 
            get => _fGiaTriDeNghi; 
            set => SetProperty(ref _fGiaTriDeNghi, value);
        }

        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        public string sMaDuAn { get; set; }
    }
}
