using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsBaoCaoGhiChuService
    {
        void Add(NsCauHinhBaoCao dmGhiChu);
        void UpdateRange(IEnumerable<NsCauHinhBaoCao> dmGhiChus);
        void Save(NsCauHinhBaoCao dmGhiChu);
        NsCauHinhBaoCao FindById(Guid id);
        IEnumerable<NsCauHinhBaoCao> FindByCondition(Expression<Func<NsCauHinhBaoCao, bool>> predicate);
        void AddOrUpdateRange(IEnumerable<NsCauHinhBaoCao> listEntities);
        void RemoveRange(IEnumerable<NsCauHinhBaoCao> listEntities);

    }
}
