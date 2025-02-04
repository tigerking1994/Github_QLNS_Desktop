using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtTtDeNghiThanhToanService
    {
        IEnumerable<VdtTtDeNghiThanhToanQuery> GetDataDeNghiThanhToanIndex(int namLamViec, string userName);
        IEnumerable<VdtTtDeNghiThanhToanChiPhiQuery> GetChiPhiInDenghiThanhToanScreen(DateTime dNgayDeNghi, Guid iIdDuAnId);
        bool DeleteDeNghiThanhToan(VdtTtDeNghiThanhToan data, string sUserLogin);
        bool Insert(VdtTtDeNghiThanhToan dataInsert, string sUserLogin);
        bool InsertRange(List<VdtTtDeNghiThanhToan> datas, string sUserLogin);
        bool Update(VdtTtDeNghiThanhToan dataUpdate, string sUserLogin);
        IEnumerable<DuAnDeNghiThanhToanQuery> GetDuAnByDeNghiThanhToan(string iIdDonViQuanLy, int iNguonVonId, Guid iIdVon, string type, DateTime dNgayLap, int iNamKeHoach);
        IEnumerable<DuAnDeNghiThanhToanQuery> GetDetailDuAnByDeNghiThanhToan(string iIdDonViQuanLy, int iNguonVonId, Guid iIdLoaiNguonVon, DateTime dNgayLap, int iNamKeHoach);
        VdtTtDeNghiThanhToan Find(params object[] keyValues);
        int Update(VdtTtDeNghiThanhToan entity);
        void LoadGiaTriThanhToan(int iCoQuanThanhToan, DateTime ngayDeNghi, bool bThanhToanTheoHopDong, string iIdChungTu, int nguonVonId, int namKeHoach, Guid? thanhToanId, int? loaiCoQuanTaiChinh, ref double thanhToanTN, ref double thanhToanNN, ref double tamUngTN, ref double tamUngNN, ref double luyKeTUUngTruocTN, ref double luyKeTUUngTruocNN, ref double sumTN, ref double sumNN, int? loaiKeHoachVon);
        void LoadGiaTriThanhToanBaoCao(int iCoQuanThanhToan, DateTime ngayDeNghi, bool bThanhToanTheoHopDong, string iIdChungTu, int nguonVonId, int namKeHoach, Guid? thanhToanId, int? loaiCoQuanTaiChinh, ref double thanhToanTN, ref double thanhToanNN, ref double tamUngTN, ref double tamUngNN, ref double luyKeTUUngTruocTN, ref double luyKeTUUngTruocNN, ref double sumTN, ref double sumNN, ref double sumThuHoiTN, ref double sumThuHoiNN, int? loaiKeHoachVon);
        DeNghiThanhToanValueQuery LoadGiaTriPheDuyetThanhToanByParentId(Guid iIdPheDuyetThanhToanId);
        public bool CheckExistSoQuyetDinh(Guid id, string soQuyetDinh);
        List<CapPhatThanhToanReportQuery> GetDataReport(string id, int namLamViec, int donViTinh);
        VdtTtDeNghiThanhToan FindByHopDongId(Guid hopdongId);
        int LockOrUnLock(Guid id, bool lockStatus);
        void UpdateThongTriThanhToan(Guid iIdThongTriId, List<Guid> lstThanhToanId);
        void TongHopDeNghiThanhToan(VdtTtDeNghiThanhToan vdtTtDeNghiThanhToan, List<Guid> childrenIds);
        List<VdtTtDeNghiThanhToan> FindDeNghiTongHop();

        VdtTtDeNghiThanhToan getLastRowBySoDeNghi(int? namLamViec);
        List<VdtTtDeNghiThanhToanQuery> getListDeNghiThanhToanByThongtriId(Guid? thongTriId, int namLamViec, string userName);

        IEnumerable<VdtTtDeNghiThanhToan> FindByCondition(Expression<Func<VdtTtDeNghiThanhToan, bool>> predicate);
    }
}
