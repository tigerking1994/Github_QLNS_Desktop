using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDsBangLuongKeHoachService
    {
        IEnumerable<TlDsBangLuongKeHoach> FindAll();
        TlDsBangLuongKeHoach FindByCondition(string cACH0, string maDonVi, int nam);
        int Add(TlDsBangLuongKeHoach tlDsBangLuongKeHoach);
        int Delete(Guid id);
    }
}
