using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlPhuCapDieuChinhService
    {
        IEnumerable<TlPhuCapDieuChinhQuery> FindAllPhuCapDieuChinh();
        int Add(TlPhuCapDieuChinh tlPhuCapDieuChinh);
        int AddRange(IEnumerable<TlPhuCapDieuChinh> tlPhuCapDieuChinhs);
        int Update(TlPhuCapDieuChinh tlPhuCapDieuChinh);
        int Delete(Guid id);
        IEnumerable<TlPhuCapDieuChinh> FindAll();
    }
}
