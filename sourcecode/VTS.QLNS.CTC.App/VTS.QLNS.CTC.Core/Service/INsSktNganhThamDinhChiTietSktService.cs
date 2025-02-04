using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsSktNganhThamDinhChiTietSktService
    {
        IEnumerable<NsSktNganhThamDinhChiTietSkt> FindByCondition(Expression<Func<NsSktNganhThamDinhChiTietSkt, bool>> predicate);

        void AddOrUpdateRange(IEnumerable<NsSktNganhThamDinhChiTietSkt> listEntities);
        int AddRange(IEnumerable<NsSktNganhThamDinhChiTietSkt> listEntities);
        int UpdateRange(IEnumerable<NsSktNganhThamDinhChiTietSkt> listEntities);
        void BulkInsert(List<NsSktNganhThamDinhChiTietSkt> lstData);

        int Add(NsSktNganhThamDinhChiTietSkt entity);
        int Update(NsSktNganhThamDinhChiTietSkt entity);
        void DeleteByIdChungTuSkt(Guid voucherId);
        void DeleteByYearOfWork(int namLamViec);
        List<JsonNsSktNganhThamDinhChiTietSktQuery> GetNsSktNganhThamDinhChiTietByChungTuId(List<Guid> iIds);
    }
}