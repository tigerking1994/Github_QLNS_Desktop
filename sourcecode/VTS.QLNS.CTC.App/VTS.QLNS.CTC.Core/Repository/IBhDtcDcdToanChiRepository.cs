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
    public interface IBhDtcDcdToanChiRepository : IRepository<BhDtcDcdToanChi>
    {
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, Guid iDLoaiChi);
        IEnumerable<BhDtcDcdToanChiQuery> FindIndex(int yearOfWork);
        int GetSoChungTuIndexByCondition(int namLamViec);
        IEnumerable<BhDtcDcdToanChi> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork);
        List<BhDtcDcdToanChi> FindAggregateAdjustVoucher(int yearOfWork, Guid? iIDLoaiDanhMucChi,string sMaLaoChi);
    }
}
