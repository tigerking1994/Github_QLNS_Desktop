using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaKhlcNhaThauQuery
    {
        public Guid Id { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public Guid? IIdDonViQuanLy { get; set; }
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public string SMaDonViQuanLy { get; set; }
        public string STenDonVi { get; set; }
        public Guid? IIdDuAnID { get; set; }
        public string STenDuAn { get; set; }
        public string STenChuongTrinh { get; set; }
        public int? ILanDieuChinh { get; set; }
        public int? ILoaiKHLCNT { get; set; }
        public Guid? IIdParentID { get; set; }
        public string SSoQuyetDinhParent { get; set; }
        public bool BIsKhoa { get; set; }
        public bool BIsActive { get; set; }
        private string SNguoiTao { get; set; }
        public int ITepDinhKem { get; set; }
        public Guid? IIdTiGiaID { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public Guid? IIdQDDauTuID { get; set; }
        public Guid? IIdDuToanID { get; set; }
        public int? ILoai { get; set; }
        public int? IThuocMenu { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public double? FTiGiaNhap { get; set; }
        public Guid? iIDNhiemVuChiID { get; set; }
        public Guid? IIdKHTongTheID { get; set; }
    }
}
