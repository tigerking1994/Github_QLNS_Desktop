using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhKhcKinhphiQuanlyChiTietService
    {
        IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindByConditionForChildUnit(KhcQuanlyKinhphiChiTietCriteria searchModel);
        bool ExistKhcKinhphiQuanlyChiTiet(Guid bhxhId);
        int AddRange(IEnumerable<BhKhcKinhphiQuanlyChiTiet> khcKinhphiQuanlyChiTiets);
        BhKhcKinhphiQuanlyChiTiet FindById(Guid id);
        void Update(BhKhcKinhphiQuanlyChiTiet entity);
        IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindByIdChiTiet(Guid id);
        void Delete(Guid id);
        IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindByCondition(Expression<Func<BhKhcKinhphiQuanlyChiTiet, bool>> predicate);
        int RemoveRange(IEnumerable<BhKhcKinhphiQuanlyChiTiet> bhKhcKinhphiQuanlyChiTiets);
        void AddAggregate(KhcQuanlyKinhphiChiTietCriteria creation);
        IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindAll(Expression<Func<BhKhcKinhphiQuanlyChiTiet, bool>> predicate);
        IEnumerable<ReportKhcQuanLyKinhPhiTongHopBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop);

        List<BhKhcKinhphiQuanlyChiTiet> GetDataDetailVoucher(KhcQuanlyKinhphiChiTietCriteria searchCondition);
    }
}
