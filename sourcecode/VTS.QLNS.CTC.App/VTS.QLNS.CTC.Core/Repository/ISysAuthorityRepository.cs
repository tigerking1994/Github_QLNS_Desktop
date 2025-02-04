using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ISysAuthorityRepository : IRepository<HtQuyen>
    {
        public IEnumerable<HtQuyen> FindAllWithFunction();
        HtQuyen FindOneWithFunction(string authName);
        IEnumerable<HtLoaiQuyen> FindAllAuthorTypes();
        void UpdateAuthorities(IEnumerable<HtQuyen> entities);

    }
}
