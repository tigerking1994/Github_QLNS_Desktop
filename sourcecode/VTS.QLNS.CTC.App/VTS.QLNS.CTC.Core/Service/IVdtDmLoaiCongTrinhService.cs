using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtDmLoaiCongTrinhService
    {
        public IEnumerable<VdtDmLoaiCongTrinh> FindAll();
        VdtDmLoaiCongTrinh FindById(Guid id);
        IEnumerable<VdtDmLoaiCongTrinh> FindByListId(List<Guid?> lstId);
    }
}
