using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhDaQuyetDinhKhac : EntityBase
    {
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public bool? BIsActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public bool BIsXoa { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string STenQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public Guid? IIdParentId { get; set; }
        public int? IThuocMenu { get; set; }

        public List<NhDaQuyetDinhKhacChiPhi> LstChiTiet = new List<NhDaQuyetDinhKhacChiPhi>();

    }
}
