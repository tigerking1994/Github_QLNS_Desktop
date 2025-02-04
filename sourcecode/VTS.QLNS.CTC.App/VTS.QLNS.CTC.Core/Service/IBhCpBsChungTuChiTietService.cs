using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhCpBsChungTuChiTietService
    {
        IEnumerable<BhCpBsChungTuChiTiet> FindChungTuChiTietByChungTuId(BhCpBsChungTuChiTietCriteria searchModel);
        int RemoveRange(IEnumerable<BhCpBsChungTuChiTiet> chungTuChiTiets);
        IEnumerable<BhCpBsChungTuChiTiet> FindByCondition(Expression<Func<BhCpBsChungTuChiTiet, bool>> predicate);
        IEnumerable<BhCpBsChungTuChiTiet> FindVoucherDetailByCondition(BhCpBsChungTuChiTietCriteria searchModel);
        bool ExistVoucherDetail(Guid chungTuId);
        int AddRange(IEnumerable<BhCpBsChungTuChiTiet> chungTuChiTiets);
        BhCpBsChungTuChiTiet FindById(Guid id);
        int Update(BhCpBsChungTuChiTiet item);
        IEnumerable<BhCpBsChungTuChiTiet> FindAllVouchers();
        IEnumerable<BhCpBsChungTuChiTietQuery> ExportKeHoachCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, string userName, int donViTinh,string sXauNoiMa);
        IEnumerable<BhCpBsChungTuChiTietQuery> ExportTongHopCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, string userName, int donViTinh, string sXauNoiMa);
        IEnumerable<BhCpBsChungTuChiTietQuery> ExportThongTriCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, int donViTinh, string sXauNoiMa);
        IEnumerable<BhCpBsChungTuChiTietQuery> ExportTheoCoSoYTe(Guid voucherID, string maCSYT, int iNamLamViec);
        IEnumerable<string> GetMaCoSoYTeDetailByCondition(int iQuy, int iNamLamViec, string sXauNoiMa, bool isTongHop, AllocationFunction functionType, bool isShowAllCoSoYTe = false);

    }
}
