using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDmLoaiCongTrinhRepository : IRepository<VdtDmLoaiCongTrinh>
    {
        VdtDmLoaiCongTrinh FindById(Guid id);
        IEnumerable<VdtDmLoaiCongTrinh> FindByListId(List<Guid?> lstId);
    }
}
