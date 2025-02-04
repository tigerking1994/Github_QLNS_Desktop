using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDsBangLuongKeHoachRepository : IRepository<TlDsBangLuongKeHoach>
    {
        TlDsBangLuongKeHoach FindByCondition(string cACH0, string maDonVi, int nam);
    }
}
