using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQttmBHYTService
    {
        BhQttmBHYT FindById(Guid id);
        int Delete(BhQttmBHYT item);
        IEnumerable<BhQttmBHYT> FindAggregateVoucher(string sct, int namLamViec);
        int Update(BhQttmBHYT item);
        bool IsExistAggregateVoucher(int namLamViec);
        IEnumerable<BhQttQuarterQuery> GetQuarterYearByYear(int namLamViec);
        IEnumerable<BhQttmBHYTQuery> FindByCondition(int namLamViec);
        void LockOrUnlock(Guid id, bool isLock);
        List<string> FindLNSExist(BhQttmBHYTChiTietCriteria condition, Guid voucherId, List<string> listLNSSelected);
        List<string> FindVoucherLNSExist(BhQttmBHYTChiTietCriteria condition, Guid voucherId, int loaiChungTu);
        void Add(BhQttmBHYT chungTu);
        int GetVoucherIndex(int year);
        List<int> GetVoucherYears(int year);
        IEnumerable<BhQttmBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        IEnumerable<BhQttmBHYT> FindByCondition(int namLamViec, int quyNam, int loaiChungTu);
        IEnumerable<BhQttmBHYTQuery> FindChungTuDonVi(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam);
        IEnumerable<BhQttmBHYTQuery> FindAllChungTuDonVi(int namLamViec, int quyNam);
        IEnumerable<BhQttmBHYTQuery> FindChungTuDonViTongHop(int namLamViec, int loaiTongHop, string userName, int quyNam);
        List<string> FindCurrentUnits(int namLamViec, int quynam, int loaiQuyNam);
    }
}
