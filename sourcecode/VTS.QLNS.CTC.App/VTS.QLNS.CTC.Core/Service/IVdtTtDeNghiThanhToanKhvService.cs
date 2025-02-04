using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtTtDeNghiThanhToanKhvService
    {
        VdtTtDeNghiThanhToanKhv Add(VdtTtDeNghiThanhToanKhv entity);
        VdtTtDeNghiThanhToanKhv Find(params object[] keyValues);
        int Update(VdtTtDeNghiThanhToanKhv entity);
        int Delete(Guid id);
        void DeleteByDeNghiThanhToanId(Guid deNghiThanhToanId);
        public IEnumerable<VdtTtDeNghiThanhToanKhv> FindByCondition(Expression<Func<VdtTtDeNghiThanhToanKhv, bool>> predicate);
        List<VdtTtDeNghiThanhToanKhv> FindByDeNghiThanhToanId(Guid deNghiThanhToanId);
        List<VdtTtKeHoachVonQuery> GetKhvDeNghiTamUng(Guid iIdDuAnId, int iIdNguonVonId, DateTime dNgayDeNghi, int iNamKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet, Guid ID_DuAn_HangMuc);
        List<VdtTtKeHoachVonQuery> GetKhvDeNghiThanhToan(Guid iIdDuAnId, int iIdNguonVonId, DateTime dNgayDeNghi, int iNamKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet, Guid ID_DuAn_HangMuc);
        List<MlnsByKeHoachVonQuery> GetMucLucNganSachByKeHoachVon(int iNamLamViec, List<TongHopNguonNSDauTuQuery> lstCondition);
        List<VdtTtThongTinCanCu> GetThongTinCanCuByIdDeNghiThanhToan(Guid? iID_DeNghiThanhToanID);
        int AddRangeThongTinCanCu(IEnumerable<VdtTtThongTinCanCu> entities);
        int UpdateThongTinCanCu(VdtTtThongTinCanCu entity);
        int DeleteThongTinCanCu(Guid id);
        VdtTtThongTinCanCu FindThongTinCanCuById(params object[] keyValues);
    }
}
