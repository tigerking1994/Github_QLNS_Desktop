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
    public interface IBhKhcKcbChiTietRepository : IRepository<BhKhcKcbChiTiet>
    {
        void AddAggregate(KhcKcbChiTietCriteria creation);
        bool ExistKhcKcbChiTiet(Guid bhxhId);
        IEnumerable<BhKhcKcbChiTiet> FindByConditionForChildUnit(KhcKcbChiTietCriteria searchCondition);
        IEnumerable<BhKhcKcbChiTiet> FindByIdChiTiet(Guid id);
        IEnumerable<ReportKhcKcbBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop);
        List<BhKhcKcbChiTiet> GetDataDetailVoucher(KhcKcbChiTietCriteria searchCondition);
        IEnumerable<BhKhcKcbChiTietQuery> FindGiaTriKeHoachThuBHXH(string sMaDonVi, int iNamLamViec, double fTyLeThu);

    }
}
