using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQsKeHoachChiTietNq104Repository : IRepository<TlQsKeHoachChiTietNq104>
    {
        int DeleteByNam(int nam);
        TlQsKeHoachChiTietNq104 FindByCondition(string maDonVi, int thang, int nam);
    }
}
