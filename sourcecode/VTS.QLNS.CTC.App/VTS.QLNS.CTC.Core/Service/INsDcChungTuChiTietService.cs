using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsDcChungTuChiTietService
    {
        void AddAggregateVoucherDetail(EstimationVoucherDetailCriteria creation);
        int AddRange(IEnumerable<NsDcChungTuChiTiet> entities);
        int UpdateRange(IEnumerable<NsDcChungTuChiTiet> entities);
        void DeleteByIdChungTu(Guid id);
        IEnumerable<NsDcChungTuChiTiet> FindByChungTuID(Guid id);
        void DeleteByIds(IEnumerable<string> ids);
        IEnumerable<NsDcChungTuChiTietQuery> FindByCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDcChungTuChiTietQuery> FindAllNSDCChungTuByCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDcChungTuChiTiet> FindByCondition(Expression<Func<NsDcChungTuChiTiet, bool>> predicate);
        IEnumerable<NsDcChungTuChiTietQuery> FindDuToanByCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDcChungTuChiTietQuery> FindByConditionTongSo(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDcChungTuChiTietQuery> FindChungTuChiTietForDcDuToanTongHopReport(EstimationVoucherDetailCriteria searchCondition);
        NsDcChungTuChiTiet FindByChungTuIdAndMlnsId(Guid voucherId, Guid mlnsId);
        IEnumerable<DataDieuChinhQuery> FindDataDieuChinh(EstimationVoucherDetailCriteria searchCondition);
        NsDcChungTuChiTiet FindById(Guid id);
        int Update(NsDcChungTuChiTiet entity);
        void BulkInsert(List<NsDcChungTuChiTiet> lstData);
        IEnumerable<NsDcChungTuChiTietQuery> FindByVoucherID(Guid voucherID);
        IEnumerable<NsDcChungTuChiTietQuery> FindByUnits(string maDonVi, int namLamViec, string iidChungTuNhan);

    }
}
