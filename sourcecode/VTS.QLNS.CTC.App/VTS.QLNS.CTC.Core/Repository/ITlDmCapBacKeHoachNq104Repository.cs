using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmCapBacKeHoachNq104Repository : IRepository<TlDmCapBacKeHoachNq104>
    {
        TlDmCapBacKeHoachNq104 FindByMaCb(string maCb);
        TlDmCapBacKeHoachNq104 FindByMaCbAndHsl(string maCb, decimal? hsl);
        int CountByYear(int year);
        TlDmCapBacKeHoachNq104 FindByMaCbAndHslAndNhom(string maCb, decimal? hsl, string nhom);
    }
}
