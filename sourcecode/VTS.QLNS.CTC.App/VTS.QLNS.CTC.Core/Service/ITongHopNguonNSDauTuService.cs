using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITongHopNguonNSDauTuService
    {
        void InsertTongHopNguonDauTu_Tang(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid? iIDQuyetDinhOld = null);
        void InsertTongHopNguonDauTu_ThanhToan_Giam(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, List<TongHopNguonNSDauTuQuery> lstChungTu, List<TongHopNguonNSDauTuQuery> lstNguon, List<TongHopNguonNSDauTuQuery> lstChungTuAppend = null, double? fThueGiaTriGiaTangDuocDuyet = null, double? fChuyenTienBaoHanhDuocDuyet = null);
        void InsertTongHopNguonDauTu_KHVN_Giam(int iNamKeHoach, string sLoai, int iTypeExecute, Guid iIdQuyetDinh, List<TongHopNguonNSDauTuQuery> lstChungTu, double? fThueGiaTriGiaTangDuocDuyet = null, double? fChuyenTienBaoHanhDuocDuyet = null);
        IEnumerable<VdtTongHopNguonNsdauTu> FindByCondition(Expression<Func<VdtTongHopNguonNsdauTu, bool>> predicate);
        void InsertTongHopNguonDauTuQuyetToan(Guid iIDChungTu, List<TongHopNguonNSDauTuQuery> lstData);
        void DeleteTongHopNguonDauTu(string sLoai, Guid iIdQuyetDinh);
        IEnumerable<VdtBaoCaoKetQuaGiaiNganChiKPDTQuery> GetBcKetQuaGiaiNganChiPhiKinhPhiDT(string iIdDonViQuanLy, int iNamKeHoach, int iIdNguonVonId);
    }
}
