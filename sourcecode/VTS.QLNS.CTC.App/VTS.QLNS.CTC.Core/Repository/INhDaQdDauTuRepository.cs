using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaQdDauTuRepository : IRepository<NhDaQdDauTu>
    {
        IEnumerable<NhDaQdDauTuQuery> FindIndex(int yearOfWork, int iLoai);
        NhDaQdDauTu FindByDuAnId(Guid idDuAn);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
    }
}
