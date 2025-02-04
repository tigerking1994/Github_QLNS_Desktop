using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhQtQuyetToanNienDoQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdGocId { get; set; }
        public string SSoDeNghi { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public int? INamKeHoach { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? IIdNguonVonId { get; set; }
        public int? ILoaiThanhToan { get; set; }
        public int? ILoaiQuyetToan { get; set; }
        public int? ICoQuanThanhToan { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsActive { get; set; }
        public bool BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool BIsXoa { get; set; }

        // Another properties
        public string STenDonVi { get; set; }
        public string STenNguonVon { get; set; }
        public string SLoaiThanhToan { get; set; }
        public string SLoaiQuyetToan { get; set; }
        public string SCoQuanThanhToan { get; set; }
        public virtual Guid? iID_TongHopID { get; set; }
        public virtual string sTongHopChildID { get; set; }
        public virtual bool HasChildren { get; set; }
        public virtual bool IsShowChildren { get; set; } = false;
    }
}
