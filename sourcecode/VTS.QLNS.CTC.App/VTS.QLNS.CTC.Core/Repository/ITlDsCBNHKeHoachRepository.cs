using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDsCBNHKeHoachRepository : IRepository<TlDsCBNHKeHoach>
    {
        IEnumerable<TlDsCBNHKeHoach> FindByYear(int year);
        IEnumerable<TlDsCBNHKeHoach> FindLoadIndex();
        IEnumerable<TlDsCBNHKeHoach> FindAllCanBo();

        IEnumerable<TlDsCBNHKeHoach> FindAllCanBoNghiHuu();
        int DeleteByYear(int year);
        TlDsCBNHKeHoach FindByMaCanBo(string maCanBo);
    }
}
