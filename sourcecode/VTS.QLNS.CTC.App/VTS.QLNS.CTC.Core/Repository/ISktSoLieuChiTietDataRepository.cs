using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ISktSoLieuChiTietDataRepository : IRepository<NsDtdauNamChungTuChiTietCanCu>
    {
        IEnumerable<DuToanDauNamCanCuQuery> FindCanCuSoNhuCau(string lstChungTu, string lstIdMucLuc, string idDonVi, int loaiCanCu, int namLamViec);
    }
}
