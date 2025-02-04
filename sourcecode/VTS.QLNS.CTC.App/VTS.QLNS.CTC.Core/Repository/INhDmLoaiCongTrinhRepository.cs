using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDmLoaiCongTrinhRepository : IRepository<NhDmLoaiCongTrinh>
    {
        IEnumerable<NhDmLoaiCongTrinh> FindAll(AuthenticationInfo authenticationInfo);
        int UpdateNhDmLoaiCongTrinhRepository(IEnumerable<NhDmLoaiCongTrinh> entities);
        List<NhDmLoaiCongTrinh> GetAll();
    }
}
