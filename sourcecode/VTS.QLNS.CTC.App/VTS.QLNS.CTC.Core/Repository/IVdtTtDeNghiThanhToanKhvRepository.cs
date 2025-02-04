using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtTtDeNghiThanhToanKhvRepository : IRepository<VdtTtDeNghiThanhToanKhv>
    {
        List<VdtTtDeNghiThanhToanKhv> FindByDeNghiThanhToanId(Guid deNghiThanhToanId);
        List<VdtTtKeHoachVonQuery> GetKhvDeNghiTamUng(Guid iIdDuAnId, int iIdNguonVonId, DateTime dNgayDeNghi, int iNamKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet, Guid ID_DuAn_HangMuc);
        List<VdtTtKeHoachVonQuery> GetKhvDeNghiThanhToan(Guid iIdDuAnId, int iIdNguonVonId, DateTime dNgayDeNghi, int iNamKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet, Guid ID_DuAn_HangMuc);
        List<MlnsByKeHoachVonQuery> GetMucLucNganSachByKeHoachVon(int iNamLamViec, List<TongHopNguonNSDauTuQuery> lstCondition);
        List<VdtTtThongTinCanCu> GetThongTinCanCuByIdDeNghiThanhToan(Guid? iID_DeNghiThanhToanID);
    }
}
