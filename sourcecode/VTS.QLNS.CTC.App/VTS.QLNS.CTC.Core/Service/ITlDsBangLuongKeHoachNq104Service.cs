using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDsBangLuongKeHoachNq104Service
    {
        IEnumerable<TlDsBangLuongKeHoachNq104> FindAll();
        TlDsBangLuongKeHoachNq104 FindByCondition(string cACH0, string maDonVi, int nam);
        int Add(TlDsBangLuongKeHoachNq104 tlDsBangLuongKeHoach);
        int Delete(Guid id);
    }
}
