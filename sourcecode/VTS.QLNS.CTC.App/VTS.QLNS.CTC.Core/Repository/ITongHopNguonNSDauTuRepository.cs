using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITongHopNguonNSDauTuRepository : IRepository<VdtTongHopNguonNsdauTu>
    {
        List<TongHopNguonNSDauTuQuery> GetNguonVonTongHopNguonDauTuKHVN(int iNamKeHoach);
        List<TongHopNguonNSDauTuQuery> GetNguonVonTongHopNguonDauTuByCondition(List<TongHopNguonNSDauTuQuery> lstCondition);
        void InsertTongHopNguonDauTu_Tang(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid iIDQuyetDinhOld);
        void DeleteTongHopNguonDauTu_Giam(string sLoai, Guid iIdQuyetDinh);
        void InsertTongHopNguonDauTu(Guid iIDChungTu, string sLoai, List<TongHopNguonNSDauTuQuery> lstData);
        void InsertTongHopNguonDauTuQuyetToan(Guid iIDChungTu, List<TongHopNguonNSDauTuQuery> lstData);
        IEnumerable<VdtBaoCaoKetQuaGiaiNganChiKPDTQuery> GetBcKetQuaGiaiNganChiPhiKinhPhiDT(string iIdDonViQuanLy, int iNamKeHoach, int iIdNguonVonId);
    }
}
