using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmChucVuNq104Service
    {
        IEnumerable<TlDmChucVuNq104> FindAll();
        IEnumerable<TlDmChucVuNq104> FindAll(Expression<Func<TlDmChucVuNq104, bool>> predicate);
        TlDmChucVuNq104 FindByMaChucVu(string maChucVu);
        TlDmChucVuNq104 FindByHeSoChucVu(decimal? heSoCv);
    }
}
