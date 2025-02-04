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
    public interface IBhKhcKinhphiQuanlyChiTietRepostiory : IRepository<BhKhcKinhphiQuanlyChiTiet>
    {
        void AddAggregate(KhcQuanlyKinhphiChiTietCriteria creation);
        bool ExistKhcKinhphiQuanlyChiTiet(Guid bhxhId);
        IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindByConditionForChildUnit(KhcQuanlyKinhphiChiTietCriteria searchCondition);
        IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindByIdChiTiet(Guid id);
        IEnumerable<ReportKhcQuanLyKinhPhiTongHopBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop);
        List<BhKhcKinhphiQuanlyChiTiet> GetDataDetailVoucher(KhcQuanlyKinhphiChiTietCriteria searchCondition);
    }
}
