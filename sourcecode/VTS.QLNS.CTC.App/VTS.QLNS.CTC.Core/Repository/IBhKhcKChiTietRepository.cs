using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhKhcKChiTietRepository : IRepository<BhKhcKChiTiet>
    {
        void AddAggregate(KhcKChiTietCriteria creation);
        bool ExistKhcKcbChiTiet(Guid bhxhId);
        IEnumerable<BhKhcKChiTiet> FindByConditionForChildUnit(KhcKChiTietCriteria searchCondition);
        IEnumerable<BhKhcKChiTiet> FindByIdChiTiet(Guid id);
        IEnumerable<ReportKhcKQuery> FindChungTuTongHopForDonVi(string listTenDonVi, int iNamlamViec, Guid IDLoaichi, int donViTinh, string lstLNS);
        IEnumerable<ReportKhcKQuery> FindChungTuHSSVNLDForDonVi(string listTenDonVi, int iNamlamViec, int iLoaiTongHop, string lstLNS);
        IEnumerable<BhKhcKChiTiet> GetReportKeHoach(KhcKChiTietCriteria searchModel);
    }
}
