using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhBaoCaoGhiChuService
    {
        void Add(BhCauHinhBaoCao dmGhiChu);
        void UpdateRange(IEnumerable<BhCauHinhBaoCao> dmGhiChus);
        void Save(BhCauHinhBaoCao dmGhiChu);
        BhCauHinhBaoCao FindById(Guid id);
        IEnumerable<BhCauHinhBaoCao> FindByCondition(Expression<Func<BhCauHinhBaoCao, bool>> predicate);
        void AddOrUpdateRange(IEnumerable<BhCauHinhBaoCao> listEntities);
        void AddReportConfig(Dictionary<string, object> data, string idType, int iNamLamViec, string idMaDonVi = null);

    }
}
