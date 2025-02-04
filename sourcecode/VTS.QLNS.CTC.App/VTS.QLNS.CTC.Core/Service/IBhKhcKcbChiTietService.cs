using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhKhcKcbChiTietService
    {
        IEnumerable<BhKhcKcbChiTiet> FindByConditionForChildUnit(KhcKcbChiTietCriteria searchModel);
        bool ExistKhcKcbChiTiet(Guid bhxhId);
        int AddRange(IEnumerable<BhKhcKcbChiTiet> khcKinhphiQuanlyChiTiets);
        BhKhcKcbChiTiet FindById(Guid id);
        void Update(BhKhcKcbChiTiet entity);
        IEnumerable<BhKhcKcbChiTiet> FindByIdChiTiet(Guid id);
        void Delete(Guid id);
        IEnumerable<BhKhcKcbChiTiet> FindByCondition(Expression<Func<BhKhcKcbChiTiet, bool>> predicate);
        int RemoveRange(IEnumerable<BhKhcKcbChiTiet> bhKhcKinhphiQuanlyChiTiets);
        void AddAggregate(KhcKcbChiTietCriteria creation);
        IEnumerable<ReportKhcKcbBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop);
        IEnumerable<BhKhcKcbChiTiet> FindAll(Expression<Func<BhKhcKcbChiTiet, bool>> predicate);
        List<BhKhcKcbChiTiet> GetDataDetailVoucher(KhcKcbChiTietCriteria searchCondition);
        IEnumerable<BhKhcKcbChiTietQuery> FindGiaTriKeHoachThuBHXH(string sMaDonVi, int iNamLamViec, double fTyLeThu);

    }
}
