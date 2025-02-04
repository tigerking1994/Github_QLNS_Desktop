using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDaTtHopDongRepository : IRepository<VdtDaTtHopDong>
    {
        List<VdtDaTtHopDong> FindByListDuAnId(List<Guid> iIdDuAnIds);
        IEnumerable<HopDongQuery> FindAllHopDongByNamLamViec(int namLamViec);
        bool CheckExistHopDongByGoiThai(Guid iIdGoiThau);
        void DeactiveHopDong(Guid id);
    }
}
