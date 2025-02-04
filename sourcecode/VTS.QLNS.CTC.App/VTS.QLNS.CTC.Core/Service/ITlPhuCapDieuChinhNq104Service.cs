using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlPhuCapDieuChinhNq104Service
    {
        IEnumerable<TlPhuCapDieuChinhNq104Query> FindAllPhuCapDieuChinh();
        int Add(TlPhuCapDieuChinhNq104 tlPhuCapDieuChinh);
        int AddRange(IEnumerable<TlPhuCapDieuChinhNq104> tlPhuCapDieuChinhs);
        int Update(TlPhuCapDieuChinhNq104 tlPhuCapDieuChinh);
        int Delete(Guid id);
        IEnumerable<TlPhuCapDieuChinhNq104> FindAll();
    }
}
