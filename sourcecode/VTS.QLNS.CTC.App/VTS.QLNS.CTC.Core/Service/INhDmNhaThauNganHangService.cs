using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmNhaThauNganHangService
    {
        void Add(NhDmNhaThauNganHang nhDmNhaThauNganHang);
        void Update(NhDmNhaThauNganHang nhDmNhaThauNganHang);
        void Delete(Guid id);
        NhDmNhaThauNganHang FindById(Guid id);
        IEnumerable<NhDmNhaThauNganHang> FindByCondition(Expression<Func<NhDmNhaThauNganHang, bool>> predicate);
        IEnumerable<NhDmNhaThauNganHang> FindAll();
    }
}
