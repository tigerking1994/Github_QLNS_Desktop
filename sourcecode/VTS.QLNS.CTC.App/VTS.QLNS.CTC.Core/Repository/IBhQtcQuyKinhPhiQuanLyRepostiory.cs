using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhQtcQuyKinhPhiQuanLyRepostiory : IRepository<BhQtcQuyKinhPhiQuanLy>
    {
        IEnumerable<BhQtcQuyKinhPhiQuanLyQuery> FindIndex(int iNamChungTu);
        IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu);
        int GetSoChungTuIndexByCondition(int namLamViec);
    }
}
