using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQsKeHoachChiTietRepository : IRepository<TlQsKeHoachChiTiet>
    {
        int DeleteByNam(int nam);
        TlQsKeHoachChiTiet FindByCondition(string maDonVi, int thang, int nam);
    }
}
