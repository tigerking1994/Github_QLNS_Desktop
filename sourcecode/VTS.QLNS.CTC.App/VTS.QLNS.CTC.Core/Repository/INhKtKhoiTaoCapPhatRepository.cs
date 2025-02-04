using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhKtKhoiTaoCapPhatRepository : IRepository<NhKtKhoiTaoCapPhat>
    {
        IEnumerable<NhKtKhoiTaoCapPhatQuery> GetAll(int iNamLamViec);
        void DeleteKhoiTaoTheoQuyetDinh(Guid id, int Type);
    }
}
