using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IPbdtcBHXHChiTietRepository : IRepository<BhPbdtcBHXHChiTiet>
    {
        IEnumerable<BhPbdtcBHXHChiTiet> FindByCondition(Expression<Func<BhPbdtcBHXHChiTiet, bool>> predicate);
        IEnumerable<BhPbdtcBHXHChiTietQuery> FindChungTuChiTiet(Guid chungTuPhaBoId, string sLNS, string sIdDonVi, int iNamLamViec, string userName, int? loaiDotNhanPhanBo);
        IEnumerable<BhPbdtcBHXHChiTietQuery> FindChungTuChiTietDieuChinh(Guid chungTuPhanBoId, string sLNS, int iNamLamViec, string userName);
        IEnumerable<BhPbdtcBHXHChiTietQuery> ExportExcelPhanBoDuToanChi(Guid chungTuId, string sLNS, int iNamLamViec, string sMaDonVi);
        IEnumerable<BhPbdtcBHXHChiTietQuery> GetSoChuaPhanBo(Guid iD_Ndtctg, Guid iD_Mlns, Guid? idChungTuEdit);
        List<BhPbdtcBHXHChiBHXHReportQuery> GetListDataPhanBoLoaiChiBHXH(int yearOfWork, string selectedUnits, Guid? IdLoaiChi, string sMaLoaiChi, string lstIDChungTu, int donViTinh, bool IsTongHopDonViKhoi, int dotNhan, bool isMillionRound);
        List<BhPbdtcBHXHChiBHXHReportQuery> GetListDataPhanBoLoaiChiKPQLKCBKHAC(int yearOfWork, string selectedUnits, Guid? idLoaiChi, string sMaLoaiChi, string lstIDChungTu, int donViTinh, bool IsTongHopDonViKhoi, int dotNhan, bool isMillionRound);
        List<BhPbdtcBHXHChiTietQuery> FindGiaTriDieuChinhThuBHXHChangeRequest(string sIdDonVi, int iNamLamViec);
        List<ReportDuToanChiBHXHBHYTBHTNQuery> ExportBaoCaoGopChiKQPL(int yearOfWork, string selectedUnits, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound,string lstMaLoaiChi);
        List<ReportDuToanChiBHXHBHYTBHTNQuery> ExportBaoCaoTachChiKQPL(int yearOfWork, string selectedUnits, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound, string lstMaLoaiChi);
    }
}
