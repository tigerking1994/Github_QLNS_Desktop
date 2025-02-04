using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCapBacKeHoachNq104Service
    {
        int Add(TlDmCapBacKeHoachNq104 entity);
        IEnumerable<TlDmCapBacKeHoachNq104> FindAll();
        int AddRang(List<TlDmCapBacKeHoachNq104> entities);
        int UpDate(TlDmCapBacKeHoachNq104 entity);
        int Delete(Guid id);
        TlDmCapBacKeHoachNq104 FindByMaCb(string maCb);
        TlDmCapBacKeHoachNq104 FindByMaCbAndHsl(string maCb, decimal? hsl);
        int CountByYear(int year);
        TlDmCapBacKeHoachNq104 FindByCondition(Expression<Func<TlDmCapBacKeHoachNq104, bool>> predicate);
        TlDmCapBacKeHoachNq104 FindByMaCbAndHslAndNhom(string maCb, decimal? hsl, string nhom);
    }
}
