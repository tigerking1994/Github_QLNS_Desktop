using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmCanBoKeHoachNq104Repository : IRepository<TlDmCanBoKeHoachNq104>
    {
        IEnumerable<TlDmCanBoKeHoachNq104> FindByYear(int year);
        IEnumerable<TlDmCanBoKeHoachNq104> FindLoadIndex();
        IEnumerable<TlDmCanBoKeHoachNq104> FindAllCanBo();
        int DeleteByYear(int year);
        TlDmCanBoKeHoachNq104 FindByMaCanBo(string maCanBo);
    }
}
