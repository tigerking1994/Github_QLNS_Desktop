using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhQtThongTriQuyetToanChiTiet : EntityBase
    {
        public virtual Guid? iID_ThongTriQuyetToanID { get; set; }
        public virtual Guid? iID_DuAnID { get; set; }
        public virtual Guid? iID_HopDongID { get; set; }
        public virtual Guid? iID_ThanhToan_ChiTietID { get; set; }
        public virtual double? fDeNghiQuyetToanNam_VND { get; set; }
        public virtual double? fDeNghiQuyetToanNam_USD { get; set; }
        public virtual double? fThuaNopTraNSNN_VND { get; set; }
        public virtual double? fThuaNopTraNSNN_USD { get; set; }
        [MaxLength(50)]
        public virtual string sMaThuTu { get; set; }
        [MaxLength(500)]
        public virtual string sTenNoiDungChi { get; set; }
    }
}
