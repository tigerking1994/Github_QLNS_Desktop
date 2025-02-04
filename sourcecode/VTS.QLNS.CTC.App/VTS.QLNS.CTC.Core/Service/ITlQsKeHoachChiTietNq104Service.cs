using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQsKeHoachChiTietNq104Service
    {
        int AddRange(IEnumerable<TlQsKeHoachChiTietNq104> lstQsKeHoachChiTiet);
        int UpdateRange(IEnumerable<TlQsKeHoachChiTietNq104> lstQsKeHoachChiTiet);
        int DeleteByNam(int nam);
        TlQsKeHoachChiTietNq104 FindByCondition(string maDonVi, int thang, int nam);
        int Delete(Guid id);
    }
}
