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
    public interface IBhKhcCheDoBhXhChiTietRepository : IRepository<BhKhcCheDoBhXhChiTiet>
    {
        IEnumerable<BhKhcCheDoBhXhChiTiet> FindByIdChiTiet(Guid id);
        IEnumerable<BhKhcCheDoBhXhChiTiet> FindByConditionForChildUnit(KhcCheDoBhXhChiTietCriteria searchCondition);
        IEnumerable<BhKhcCheDoBhXhChiTiet> FindByConditionForDonVi(KhcCheDoBhXhChiTietCriteria searchCondition);
        bool ExistBHXHChiTiet(Guid bhxhId);
        void AddAggregate(KhcCheDoBhXhChiTietCriteria creation);
        IEnumerable<ReportKhcTongHopBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop);
        List<BhKhcCheDoBhXhChiTiet> GetDataDetailVoucher(KhcCheDoBhXhChiTietCriteria searchCondition);
        List<BhKhcCheDoBhXhChiTiet> GetDataSummaryDetailVoucher(KhcCheDoBhXhChiTietCriteria searchCondition);
        IEnumerable<BhKhcCheDoBhXhChiTietQuery> GetPlanData(int iNam, string sSoChungTu,string sLNS);
    }
}
