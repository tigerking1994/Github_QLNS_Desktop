using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhKhcKChiTietService
    {
        IEnumerable<BhKhcKChiTiet> FindByConditionForChildUnit(KhcKChiTietCriteria searchModel);
        bool ExistKhcKcbChiTiet(Guid bhxhId);
        int AddRange(IEnumerable<BhKhcKChiTiet> khcKinhphiQuanlyChiTiets);
        BhKhcKChiTiet FindById(Guid id);
        void Update(BhKhcKChiTiet entity);
        IEnumerable<BhKhcKChiTiet> FindByIdChiTiet(Guid id);
        void Delete(Guid id);
        IEnumerable<BhKhcKChiTiet> FindByCondition(Expression<Func<BhKhcKChiTiet, bool>> predicate);
        int RemoveRange(IEnumerable<BhKhcKChiTiet> bhKhcKinhphiQuanlyChiTiets);
        void AddAggregate(KhcKChiTietCriteria creation);
        IEnumerable<ReportKhcKQuery> FindChungTuTongHopForDonVi(string listTenDonVi, int iNamlamViec, Guid IDLoaichi, int donViTinh, string lstLNS);
        IEnumerable<ReportKhcKQuery> FindChungTuHSSVNLDForDonVi(string listTenDonVi, int iNamlamViec, int iLoaiTongHop, string lstSLNS);
        IEnumerable<BhKhcKChiTiet> FindAll(Expression<Func<BhKhcKChiTiet, bool>> predicate);

        IEnumerable<BhKhcKChiTiet> GetReportKeHoach(KhcKChiTietCriteria searchModel);
    }
}
