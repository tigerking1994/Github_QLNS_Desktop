using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using System.Collections;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhKhcCheDoBhXhChiTietService
    {
        void Add(BhKhcCheDoBhXhChiTiet entity);
        void Update(BhKhcCheDoBhXhChiTiet entity);
        void Delete(Guid id);
        void LockOrUnlock(Guid id, bool status);
        IEnumerable<BhKhcCheDoBhXhChiTiet> FindByIdChiTiet(Guid id);
        BhKhcCheDoBhXhChiTiet FindById(Guid id);
        IEnumerable<BhKhcCheDoBhXhChiTiet> FindByConditionForChildUnit(KhcCheDoBhXhChiTietCriteria searchModel);
        IEnumerable<BhKhcCheDoBhXhChiTiet> FindByConditionForDonVi(KhcCheDoBhXhChiTietCriteria searchModel);
        bool ExistBHXHChiTiet(Guid bhxhId);
        int AddRange(IEnumerable<BhKhcCheDoBhXhChiTiet> khcBhxhChiTiets);
        int RemoveRange(IEnumerable<BhKhcCheDoBhXhChiTiet> khcBhxhChiTiets);
        IEnumerable<BhKhcCheDoBhXhChiTiet> FindByCondition(Expression<Func<BhKhcCheDoBhXhChiTiet, bool>> predicate);
        IEnumerable<BhKhcCheDoBhXhChiTiet> FindAll(Expression<Func<BhKhcCheDoBhXhChiTiet, bool>> predicate);
        void AddAggregate(KhcCheDoBhXhChiTietCriteria creation);
        IEnumerable<ReportKhcTongHopBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop);
        List<BhKhcCheDoBhXhChiTiet> GetDataDetailVoucher(KhcCheDoBhXhChiTietCriteria searchCondition);
        List<BhKhcCheDoBhXhChiTiet> GetDataSummaryDetailVoucher(KhcCheDoBhXhChiTietCriteria searchCondition);
        IEnumerable<BhKhcCheDoBhXhChiTietQuery> GetPlanData(int iNam, string sSoChungTu, string SLNS);
    }
}
