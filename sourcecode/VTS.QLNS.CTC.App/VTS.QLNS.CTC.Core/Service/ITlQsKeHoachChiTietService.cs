using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQsKeHoachChiTietService
    {
        int AddRange(IEnumerable<TlQsKeHoachChiTiet> lstQsKeHoachChiTiet);
        int UpdateRange(IEnumerable<TlQsKeHoachChiTiet> lstQsKeHoachChiTiet);
        int DeleteByNam(int nam);
        TlQsKeHoachChiTiet FindByCondition(string maDonVi, int thang, int nam);
        int Delete(Guid id);
    }
}
