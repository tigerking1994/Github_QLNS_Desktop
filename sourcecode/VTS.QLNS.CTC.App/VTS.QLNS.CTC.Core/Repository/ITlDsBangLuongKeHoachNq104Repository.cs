using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDsBangLuongKeHoachNq104Repository : IRepository<TlDsBangLuongKeHoachNq104>
    {
        TlDsBangLuongKeHoachNq104 FindByCondition(string cACH0, string maDonVi, int nam);
    }
}
