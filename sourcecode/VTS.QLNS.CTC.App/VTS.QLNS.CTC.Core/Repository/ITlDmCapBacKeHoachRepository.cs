using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmCapBacKeHoachRepository : IRepository<TlDmCapBacKeHoach>
    {
        TlDmCapBacKeHoach FindByMaCb(string maCb);
        TlDmCapBacKeHoach FindByMaCbAndHsl(string maCb, decimal? hsl);
        int CountByYear(int year);
        TlDmCapBacKeHoach FindByMaCbAndHslAndNhom(string maCb, decimal? hsl, string nhom);
    }
}
