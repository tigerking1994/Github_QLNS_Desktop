using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhQtThongTriQuyetToanQuery
    {
        public virtual Guid Id { get; set; }
        public virtual string sSoThongTri { get; set; }
        public virtual DateTime? dNgayLap { get; set; }
        public virtual Guid? iID_KHTT_NhiemVuChiID { get; set; }
        public virtual Guid? iID_DonViID { get; set; }
        public virtual string iID_MaDonVi { get; set; }
        public virtual int? iNamThongTri { get; set; }
        public virtual int? iLoaiThongTri { get; set; }
        public virtual int? iLoaiNoiDungChi { get; set; }
        public virtual double? fThongTri_VND { get; set; }
        public virtual double? fThongTri_USD { get; set; }

        public virtual string sTenNhiemVuChi { get; set; }
        public virtual string sTenDonVi { get; set; }
        public virtual string sLoaiThongTri { get; set; }
        public virtual string sLoaiNoiDungChi { get; set; }
    }
}
