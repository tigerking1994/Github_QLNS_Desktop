using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISktSoLieuChiTietCanCuDataService
    {
        NsDtdauNamChungTuChiTietCanCu Find(params object[] keyValues);
        int Update(NsDtdauNamChungTuChiTietCanCu entity);
        int AddRange(IEnumerable<NsDtdauNamChungTuChiTietCanCu> entities);
        int Delete(Guid id);
        IEnumerable<NsDtdauNamChungTuChiTietCanCu> FindByCondition(Expression<Func<NsDtdauNamChungTuChiTietCanCu, bool>> predicate);
        IEnumerable<DuToanDauNamCanCuQuery> FindCanCuSoNhuCau(string lstChungTu, string lstIdMucLuc, string idDonVi, int loaiCanCu, int namLamViec);
        int RemoveRange(IEnumerable<NsDtdauNamChungTuChiTietCanCu> sktChungTuChiTiets);
        void BulkInsertNsDtdauNamChungTuChiTietCanCu(List<NsDtdauNamChungTuChiTietCanCu> lstData);
    }
}
