using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmCanBoKeHoachRepository : IRepository<TlDmCanBoKeHoach>
    {
        IEnumerable<TlDmCanBoKeHoach> FindByYear(int year);
        IEnumerable<TlDmCanBoKeHoach> FindLoadIndex();
        IEnumerable<TlDmCanBoKeHoach> FindAllCanBo();
        int DeleteByYear(int year);
        TlDmCanBoKeHoach FindByMaCanBo(string maCanBo);
    }
}
