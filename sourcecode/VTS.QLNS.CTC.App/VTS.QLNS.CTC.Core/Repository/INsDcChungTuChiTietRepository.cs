using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsDcChungTuChiTietRepository : IRepository<NsDcChungTuChiTiet>
    {
        void AddAggregateVoucherDetail(EstimationVoucherDetailCriteria creation);
        void DeleteByIdChungTu(Guid id);
        void DeleteByIds(IEnumerable<string> ids);
        IEnumerable<NsDcChungTuChiTietQuery> FindByCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDcChungTuChiTietQuery> FindAllNSDCChungTuByCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDcChungTuChiTietQuery> FindDuToanByCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDcChungTuChiTietQuery> FindByConditionTongSo(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDcChungTuChiTietQuery> FindChungTuChiTietForDcDuToanTongHopReport(EstimationVoucherDetailCriteria searchCondition);
        NsDcChungTuChiTiet FindByChungTuIdAndMlnsId(Guid voucherId, Guid mlnsId);
        IEnumerable<DataDieuChinhQuery> FindDataDieuChinh(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<NsDcChungTuChiTietQuery> FindByVoucherID(Guid voucherID);
        IEnumerable<NsDcChungTuChiTietQuery> FindByUnits(string maDonVi, int namLamViec, string iidChungTuNhan);
    }
}
