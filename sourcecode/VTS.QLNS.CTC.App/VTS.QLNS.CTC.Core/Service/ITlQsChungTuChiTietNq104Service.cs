using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQsChungTuChiTietNq104Service
    {
        IEnumerable<TlQsChungTuChiTietNq104> FindAll();
        IEnumerable<TlQsChungTuChiTietNq104> FindAll(Expression<Func<TlQsChungTuChiTietNq104, bool>> predicate);
        TlQsChungTuChiTietNq104 FirstOrDefault(Expression<Func<TlQsChungTuChiTietNq104, bool>> predicate);
        int Add(IEnumerable<TlQsChungTuChiTietNq104> entites);
        int DeleteByChungTuId(Guid ParentId);
        int UpDate(TlQsChungTuChiTietNq104 entity);
        int DeleteId(Guid id);
        int UpDateRange(IEnumerable<TlQsChungTuChiTietNq104> entites);
    }
}
