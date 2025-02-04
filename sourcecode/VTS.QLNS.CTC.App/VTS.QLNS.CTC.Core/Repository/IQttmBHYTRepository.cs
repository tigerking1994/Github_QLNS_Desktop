using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IQttmBHYTRepository : IRepository<BhQttmBHYT>
    {
        IEnumerable<BhQttmBHYT> FindAggregateVoucher(string sct, int namLamViec);
        bool IsExistAggregateVoucher(int namLamViec);
        IEnumerable<BhQttQuarterQuery> GetQuarterYearByYear(int namLamViec);
        IEnumerable<BhQttmBHYTQuery> FindByCondition(int namLamViec);
        int GetVoucherIndex(int year);
        List<int> GetVoucherYears(int year);
        IEnumerable<BhQttmBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        IEnumerable<BhQttmBHYT> FindByCondition(int namLamViec, int quyNam, int loaiChungTu);
        IEnumerable<BhQttmBHYTQuery> FindChungTuDonVi(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam);
        IEnumerable<BhQttmBHYTQuery> FindChungTuDonViTongHop(int namLamViec, int loaiTongHop, string userName, int quyNam);
        IEnumerable<BhQttmBHYTQuery> FindAllChungTuDonVi(int namLamViec, int quyNam);
        List<string> FindCurrentUnits(int namLamViec, int quynam, int loaiQuyNam);
    }
}
