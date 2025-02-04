using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmNhaThauNguoiNhanService
    {
        void Add(NhDmNhaThauNguoiNhan nhDmNhaThauNguoiNhan);
        void Update(NhDmNhaThauNguoiNhan nhDmNhaThauNguoiNhan);
        void Delete(Guid id);
        NhDmNhaThauNguoiNhan FindById(Guid id);
        IEnumerable<NhDmNhaThauNguoiNhan> FindByCondition(Expression<Func<NhDmNhaThauNguoiNhan, bool>> predicate);
        IEnumerable<NhDmNhaThauNguoiNhan> FindAll();
    }
}
