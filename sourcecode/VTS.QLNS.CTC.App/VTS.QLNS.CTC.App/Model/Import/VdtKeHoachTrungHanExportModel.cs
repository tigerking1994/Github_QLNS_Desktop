using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class VdtKeHoachTrungHanExportModel : BindableBase
    {
        public Guid? IdKeHoach5NamChiTiet { get; set; }
        public Guid? IIDDuAn { get; set; }
        public int IIDNguonVonID { get; set; }
        public double? fGiaTriNamThuNam { get; set; }
        public double? fGiaTriNamThuTu { get; set; }
        public double? fGiaTriNamThuBa { get; set; }
        public double? fGiaTriNamThuHai { get; set; }
        public double? fGiaTriNamThuNhat { get; set; }
        public double? fGiaTriKeHoach { get; set; }
        public double? TongMucDauTu { get; set; }
        public string STT { get; set; }
        public string sTenDuAn { get; set; }
        public string sSoQuyetDinh { get; set; }
        public DateTime? dNgayQuyetDinh { get; set; }
        public string GhiChu { get; set; }
        public string sDiaDiem { get; set; }
        public string ThoiGianThucHien { get; set; }
        public string sNgaySoQuyetDinh { get; set; }
        public string sLoaiNganSach { get; set; }
        public string STrangThai { get; set; }
    }
}
