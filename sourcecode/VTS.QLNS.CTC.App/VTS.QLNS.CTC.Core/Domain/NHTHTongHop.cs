using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NHTHTongHop : EntityBase
    {
        public bool? BIsLog { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public Guid? IIDTiGia { get; set; }
        public Guid? IIdChungTu { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdDonVi { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public int? INamKeHoach { get; set; }
        public int? IQuyKeHoach { get; set; }
        public int IStatus { get; set; }
        public Guid? IIDMucLucNganSach { get; set; }
        public string SMaNguon { get; set; }
        public string SMaDich { get; set; }
        public string SMaNguonCha { get; set; }
        public string SMaTienTrinh { get; set; }
        public int? ICoQuanThanhToan { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public Guid? IIDKHTTNhiemVuChiID { get; set; }
    }
}
