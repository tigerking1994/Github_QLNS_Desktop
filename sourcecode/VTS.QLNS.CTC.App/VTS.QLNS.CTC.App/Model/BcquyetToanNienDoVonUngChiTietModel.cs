using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class BcquyetToanNienDoVonUngChiTietModel : BindableBase
    {
        public bool BIsChuyenTiep { get; set; }
        public string iStt { get; set; }
        public bool IsHangCha { get; set; }
        public int ICoQuanThanhToan { get; set; }
        public Guid IIDDuAnID { get; set; }
        public string SMaDuAn { get; set; }
        public string SDiaDiem { get; set; }
        public string STenDuAn { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public string SMaLoaiCongTrinh { get; set; }
        // col 1
        public double FUngTruocChuaThuHoiNamTruoc { get; set; }
        // col 2
        public double FLuyKeThanhToanNamTruoc { get; set; }
        // col 3
        public double FKeHoachVonDuocKeoDai { get; set; }
        // col 4
        public double FVonKeoDaiDaThanhToanNamNay { get; set; }
        // col 5
        public double FThuHoiVonNamNay { get; set; }
        // col 6
        private double _fGiaTriThuHoiTheoGiaiNganThucTe;
        public double FGiaTriThuHoiTheoGiaiNganThucTe
        {
            get => _fGiaTriThuHoiTheoGiaiNganThucTe;
            set => SetProperty(ref _fGiaTriThuHoiTheoGiaiNganThucTe, value);
        }
        // col 7
        public double FKHVUNamNay { get; set; }
        // col 8
        public double FVonDaThanhToanNamNay { get; set; }
        // col 9
        public double FKHVUChuaThuHoiChuyenNamSau { get; set; }
        // col 10
        public double FTongSoVonDaThanhToanThuHoi { get; set; }
    }
}
