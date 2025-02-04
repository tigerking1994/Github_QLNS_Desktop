using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhCpBsChungTuChiTietRepository : IRepository<BhCpBsChungTuChiTiet>
    {
        IEnumerable<BhCpBsChungTuChiTiet> FindChungTuChiTietByChungTuId(BhCpBsChungTuChiTietCriteria searchCondition);
        IEnumerable<BhCpBsChungTuChiTiet> FindVoucherDetailByCondition(BhCpBsChungTuChiTietCriteria searchCondition);
        bool ExistVoucherDetail(Guid chungTuId);
        BhCpBsChungTuChiTiet FindById(Guid id);
        IEnumerable<BhCpBsChungTuChiTietQuery> ExportKeHoachCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, string userName, int donViTinh, string sXauNoiMa);
        IEnumerable<BhCpBsChungTuChiTietQuery> ExportTongHopCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, string userName, int donViTinh, string sXauNoiMa);
        IEnumerable<BhCpBsChungTuChiTietQuery> ExportThongTriCapPhatBoSungKCBBHYT(string sIdCsYTe, int iQuy, int iNamLamViec, int donViTinh, string sXauNoiMa);
        IEnumerable<BhCpBsChungTuChiTietQuery> ExportTheoCoSoYTe(Guid voucherID, string maCSYT, int iNamLamViec);
        IEnumerable<string> GetMaCoSoYTeDetailByCondition(int iQuy, int iNamLamViec, string sXauNoiMa, bool isTongHop, AllocationFunction functionType, bool isShowAllCoSoYTe);
    }
}
