using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDcDuToanThuChiTietRepository : IRepository<BhDcDuToanThuChiTiet>
    {
        IEnumerable<BhDcDuToanThuChiTiet> FindByConditionForChildUnit(BhDcDuToanThuChiTietCriteria searchCondition);
        IEnumerable<BhDcDuToanThuChiTiet> FindByIdChiTiet(Guid id);
        void AddAggregate(BhDcDuToanThuChiTietCriteria creation);
        bool ExistKhcKcbChiTiet(Guid bhxhId);
        IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByCondition(BhDcDuToanThuChiTietCriteria searchCondition);
        IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByUnit(string maDonVi, int dvt, int namLamViec, bool isAggregate);
        IEnumerable<BhDcDuToanThuChiTietQuery> GetAggregateAdjustData(int iNam, string sMaDonVi);
        IEnumerable<BhDcDuToanThuChiTietQuery> GetUnitAggregateAdjustData(int iNam, string sMaDonVi);
        IEnumerable<RptDcDuToanThuChiTietQuery> ExportDieuChinhDtTheoDoiTuong(string maDonVi, int dvt, int namLamViec, bool isAggregate);
        IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByAgencySummaryDetail(string maDonVi, int dvt, int namLamViec);
        IEnumerable<BhDcDuToanThuChiTietQuery> GetSettlementData(int namLamViec, string maDonVi, int thangQuy, int loaiThangQuy);
    }
}
