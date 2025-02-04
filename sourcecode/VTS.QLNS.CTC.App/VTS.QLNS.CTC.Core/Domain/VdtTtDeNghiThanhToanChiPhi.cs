using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class VdtTtDeNghiThanhToanChiPhi : EntityBase
    {
        public override Guid Id { get; set; }
        public Guid? IIdDeNghiThanhToanId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public string SNguoiLap { get; set; }
        public Guid? IIdLoaiNguonVonId { get; set; }
        public int? INamKeHoach { get; set; }
        public string SGhiChu { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdPhanBoVonChiPhiId { get; set; }
        public bool? BKhoa { get; set; }
        public string SGhiChuPheDuyet { get; set; }
        public string SLyDoTuChoi { get; set; }
        public Guid? IIdParent { get; set; }
        public bool? BTongHop { get; set; }
    }
}
