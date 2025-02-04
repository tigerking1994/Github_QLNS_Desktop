using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDcDuToanThuChiTietService
    {
        IEnumerable<BhDcDuToanThuChiTiet> FindByConditionForChildUnit(BhDcDuToanThuChiTietCriteria searchModel);
        IEnumerable<BhDcDuToanThuChiTiet> FindByIdChiTiet(Guid id);
        void Delete(Guid id);
        IEnumerable<BhDcDuToanThuChiTiet> FindByCondition(Expression<Func<BhDcDuToanThuChiTiet, bool>> predicate);
        void AddAggregate(BhDcDuToanThuChiTietCriteria creation);
        bool ExistKhcKcbChiTiet(Guid bhxhId);
        int AddRange(IEnumerable<BhDcDuToanThuChiTiet> item);
        BhDcDuToanThuChiTiet FindById(Guid id);
        void Update(BhDcDuToanThuChiTiet entity);
        IEnumerable<BhDcDuToanThuChiTiet> FindAllChungTuDuToan();
        IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByCondition(BhDcDuToanThuChiTietCriteria searchModel);
        int RemoveRange(IEnumerable<BhDcDuToanThuChiTiet> chungTuChiTiets);
        IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByUnit(string maDonVi, int dvt, int namLamViec, bool isAggregate);
        IEnumerable<BhDcDuToanThuChiTietQuery> GetAggregateAdjustData(int iNam, string sMaDonVi);
        IEnumerable<BhDcDuToanThuChiTietQuery> GetUnitAggregateAdjustData(int iNam, string sMaDonVi);
        IEnumerable<RptDcDuToanThuChiTietQuery> ExportDieuChinhDtTheoDoiTuong(string maDonVi, int dvt, int namLamViec, bool isAggregate);
        IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByAgencySummaryDetail(string maDonVi, int dvt, int namLamViec);
        IEnumerable<BhDcDuToanThuChiTietQuery> GetSettlementData(int namLamViec, string maDonVi, int thangQuy, int loaiThangQuy);
    }
}
