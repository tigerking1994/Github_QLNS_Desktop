using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtDaGoiThauService
    {
        IEnumerable<VdtDaGoiThau> FindAll(Expression<Func<VdtDaGoiThau, bool>> predicate);
        IEnumerable<VdtDaGoiThauQuery> FindByCondition(int namLamViec);
        void DeleteGoiThauChiTiet(Guid id);
        IEnumerable<VdtDmNhaThau> GetAllNhaThau();
        VdtDaGoiThau FindById(Guid id);
        VdtDaGoiThau FindGoiThauDieuChinhByGoiThauGocId(Guid goiThauGocId);
        VdtDaGoiThau Add(VdtDaGoiThau entity);
        void AddRange(IEnumerable<VdtDaGoiThau> entities);
        VdtDaGoiThau UpdateData(VdtDaGoiThau item);
        void UpdateRange(IEnumerable<VdtDaGoiThau> items, bool bIsActive = true);
        int Update(VdtDaGoiThau entity);
        IEnumerable<VdtDaGoiThauDetailQuery> FindListDetail(Guid goiThauId);
        IEnumerable<VdtDaGoiThauDetailQuery> FindListDieuChinhDetail(Guid goiThauId, Guid goiThauGocId, DateTime dngayLap);
        IEnumerable<VdtDmChiPhi> GetListChiPhiByDuAn(Guid idDuAn, DateTime ngayLap);
        IEnumerable<NsNguonNganSach> GetListNguonVonByDuAn(Guid idDuAn, DateTime ngayLap);
        IEnumerable<VdtDaDuAnHangMuc> GetListHangMucByDuAn(Guid idDuAn, DateTime ngayLap);
        double? GetTongMucDTChiPhi(Guid idChiPhi, Guid idDuAn, DateTime dNgayLap);
        double? GetTongMucDTNguonVon(int idNguonVon, Guid idDuAn, DateTime dNgayLap);
        double? GetTongMucDTHangMuc(Guid idHangMuc, Guid idDuAn, DateTime dNgayLap);
        int AddRangeGoiThauChiPhi(IEnumerable<VdtDaGoiThauChiPhi> entities);
        int AddRangeGoiThauNguonVon(IEnumerable<VdtDaGoiThauNguonVon> entities);
        int AddRangeGoiThauHangMuc(IEnumerable<VdtDaGoiThauHangMuc> entities);
        int UpdateGoiThauChiPhi(VdtDaGoiThauChiPhi entity);
        int UpdateGoiThauNguonVon(VdtDaGoiThauNguonVon entity);
        int UpdateGoiThauHangMuc(VdtDaGoiThauHangMuc entity);
        int DeleteGoiThauChiPhi(Guid id);
        int DeleteGoiThauNguonVon(Guid id);
        int DeleteGoiThauHangMuc(Guid id);
        VdtDaGoiThauChiPhi FindGoiThauChiPhi(params object[] keyValues);
        VdtDaGoiThauNguonVon FindGoiThauNguonVon(params object[] keyValues);
        VdtDaGoiThauHangMuc FindGoiThauHangMuc(params object[] keyValues);
        IEnumerable<VdtDaDuAn> FindDuAnByDonViGoiThau(string donviUserId, int namLamViec);
        IEnumerable<GoiThauChiPhiQuery> FindListGoiThauChiPhi(Guid goiThauId);
        IEnumerable<GoiThauNguonVonQuery> FindListGoiThauNguonVon(Guid goiThauId);
        IEnumerable<GoiThauHangMucQuery> FindListGoiThauHangMuc(Guid goiThauId, Guid chiPhiDuAnId, bool isDuToan);
        void DeleteGoiThauNguonVonByGoiThauId(Guid goiThauId);
        void DeleteGoiThauChiPhiByGoiThauId(Guid goiThauId);
        IEnumerable<NhaThauHopDongQuery> FindListNhaThauHopDongByGoiThau(Guid goiThauId);
        void LockOrUnlock(Guid id, bool isLock);
        IEnumerable<VdtDaGoiThauQuery> FindByKhlcNhaThauId(Guid iIdKhlcNhaThauId);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauNguonVonByChungTu(Guid iIdChungTu, string sLoaiChungTu);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByChungTu(Guid iIdChungTu, string sLoaiChungTu, bool bIsAdd);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByChungTuCtdt_KhlcntEdit(Guid iIdKhlcnt);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauHangMucByChungTu(Guid iIdChungTu, string sLoaiChungTu);
        void DeleteRange(IEnumerable<VdtDaGoiThau> items);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauNguonVonByKhlcNhaThauId(Guid iIdKhlcNhaThau);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByKhlcNhaThauId(Guid iIdKhlcNhaThau);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauHangMucByKhlcNhaThauId(Guid iIdKhlcNhaThau);
        IEnumerable<VdtKhlcntChiPhiNguonVonCanCuQuery> GetAllNguonVonByLoaiCanCuInKhlcntScreen(List<Guid> iIdCanCuIds, string sLoaiCanCu);
        IEnumerable<VdtKhlcntChiPhiNguonVonCanCuQuery> GetAllChiPhiByLoaiCanCuInKhlcntScreen(List<Guid> iIdCanCuIds, string sLoaiCanCu);
        void ReActiveGoiThauByKhlcntId(Guid iIdKhlcnt);
        IEnumerable<HopDongGoiThauQuery> FindGoiThauByDuAn(Guid duanId);
        IEnumerable<HopDongGoiThauQuery> FindGoiThauByHopDong(Guid hopdongId);
        IEnumerable<HopDongGoiThauQuery> DCFindGoiThauByHopDong(Guid hopDongGocId, Guid? hopdongDCId = null);
        string GetTypeOfGoiThau(Guid goithauId);
    }
}
