using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class KeHoachVonQuery
    {
        public Guid Id { get; set; }
        public string sSoQuyetDinh { get; set; }
        public DateTime? dNgayQuyetDinh { get; set; }
        public int iNamKeHoach { get; set; }
        public Guid? iID_DonViQuanLyID { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public int iID_NguonVonID { get; set; }
        public int PhanLoai { get; set; }
        //public double? LenhChi { get; set; }
        public double? FLuyKeThanhToan { get; set; }
        public double? FTongGiaTri { get; set; }
        public string TenLoai { get; set; }
        public string sMaNguonCha { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; }
        [NotMapped]
        public string NgayQuyetDinhDisplay
        {
            get => dNgayQuyetDinh.HasValue ? dNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty;
        }
        [NotMapped]
        public string SSoKeHoachDropDown
        {
            get
            {
                return string.Format("{0}-{1}", sSoQuyetDinh, TenLoai);
            }
        }
    }
}
