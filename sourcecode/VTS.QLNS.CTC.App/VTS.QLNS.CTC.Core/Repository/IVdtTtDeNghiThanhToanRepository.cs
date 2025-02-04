using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtTtDeNghiThanhToanRepository : IRepository<VdtTtDeNghiThanhToan>
    {
        IEnumerable<VdtTtDeNghiThanhToanQuery> GetDataDeNghiThanhToanIndex(int namLamViec, string userName);
        IEnumerable<DuAnDeNghiThanhToanQuery> GetDuAnByDeNghiThanhToan(string iIdDonViQuanLy, int iNguonVonId, Guid iIdVon, string type, DateTime dNgayLap, int iNamKeHoach);
        IEnumerable<DuAnDeNghiThanhToanQuery> GetDetailDuAnByDeNghiThanhToan(string iIdDonViQuanLy, int iNguonVonId, Guid iIdLoaiNguonVon, DateTime dNgayLap, int iNamKeHoach);
        IEnumerable<VdtTtDeNghiThanhToanChiPhiQuery> GetChiPhiInDenghiThanhToanScreen(DateTime dNgayDeNghi, Guid iIdDuAnId);
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

        VdtTtDeNghiThanhToan FindLastRowBySoDeNghi(int? namLamViec);
        List<VdtTtDeNghiThanhToanQuery> getListDeNghiThanhToanByThongtriId(Guid? id, int namLamViec, string userName);
    }
}
