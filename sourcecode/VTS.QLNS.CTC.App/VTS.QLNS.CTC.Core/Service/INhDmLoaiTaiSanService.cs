using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmLoaiTaiSanService
    {
        IEnumerable<NhDmLoaiTaiSan> FindAll();
        IEnumerable<NhDmLoaiTaiSan> FindAll(Expression<Func<NhDmLoaiTaiSan, bool>> predicate);
        NhDmLoaiTaiSan FindById(Guid? id);
        void Update(NhDmLoaiTaiSan nhDmLoaiTaiSan);
        void Add(NhDmLoaiTaiSan nhDmLoaiTaiSan);
        void Delete(Guid id);
    }
}
