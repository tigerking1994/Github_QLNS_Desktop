using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDmCauHinhThamSoService
    {
        void Add(BhDmCauHinhThamSo entity);
        void Update(BhDmCauHinhThamSo entity);
        void Delete(BhDmCauHinhThamSo entity);
        void AddRange(IEnumerable<BhDmCauHinhThamSo> entities);
        void UpdateRange(IEnumerable<BhDmCauHinhThamSo> entities);
        IEnumerable<BhDmCauHinhThamSo> FindAll();
        BhDmCauHinhThamSo FindById(Guid id);
        IEnumerable<BhDmCauHinhThamSo> FindByCondition(Expression<Func<BhDmCauHinhThamSo, bool>> predicate);
    }
}
