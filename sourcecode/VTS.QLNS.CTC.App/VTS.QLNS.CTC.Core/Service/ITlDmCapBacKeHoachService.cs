using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCapBacKeHoachService
    {
        int Add(TlDmCapBacKeHoach entity);
        IEnumerable<TlDmCapBacKeHoach> FindAll();
        int AddRang(List<TlDmCapBacKeHoach> entities);
        int UpDate(TlDmCapBacKeHoach entity);
        int Delete(Guid id);
        TlDmCapBacKeHoach FindByMaCb(string maCb);
        TlDmCapBacKeHoach FindByMaCbAndHsl(string maCb, decimal? hsl);
        int CountByYear(int year);
        TlDmCapBacKeHoach FindByCondition(Expression<Func<TlDmCapBacKeHoach, bool>> predicate);
        TlDmCapBacKeHoach FindByMaCbAndHslAndNhom(string maCb, decimal? hsl, string nhom);
    }
}
