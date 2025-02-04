using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDaGoiThauRepository : IRepository<VdtDaGoiThau>
    {
        IEnumerable<VdtDaGoiThauQuery> FindByCondition(int namLamViec);
        void DeleteGoiThauChiTiet(Guid id);
        IEnumerable<VdtDmNhaThau> GetAllNhaThau();
        IEnumerable<VdtDaGoiThauDetailQuery> FindListDetail(Guid goiThauId);
        IEnumerable<VdtDaGoiThauDetailQuery> FindListDieuChinhDetail(Guid goiThauId, Guid goiThauGocId, DateTime dngayLap);
        IEnumerable<VdtDmChiPhi> GetListChiPhiByDuAn(Guid idDuAn, DateTime ngayLap);
        IEnumerable<NsNguonNganSach> GetListNguonVonByDuAn(Guid idDuAn, DateTime ngayLap);
        IEnumerable<VdtDaDuAnHangMuc> GetListHangMucByDuAn(Guid idDuAn, DateTime ngayLap);
        double? GetTongMucDTChiPhi(Guid idChiPhi, Guid idDuAn, DateTime dNgayLap);
        double? GetTongMucDTNguonVon(int idNguonVon, Guid idDuAn, DateTime dNgayLap);
        double? GetTongMucDTHangMuc(Guid idHangMuc, Guid idDuAn, DateTime dNgayLap);
        void DeleteGoiThauChiPhi(Guid idGoiThau);
        void DeleteGoiThauNguonVon(Guid idGoiThau);
        void DeleteGoiThauHangMuc(Guid idGoiThau);
        IEnumerable<VdtDaDuAn> FindDuAnByDonViGoiThau(string donviUserId, int namLamViec);
        IEnumerable<GoiThauChiPhiQuery> FindListGoiThauChiPhi(Guid goiThauId);
        IEnumerable<GoiThauNguonVonQuery> FindListGoiThauNguonVon(Guid goiThauId);
        IEnumerable<GoiThauHangMucQuery> FindListGoiThauHangMuc(Guid goiThauId, Guid chiPhiDuAnId, bool isDuToan);
        VdtDaGoiThau FindGoiThauDieuChinhByGoiThauGocId(Guid goiThauGocId);
        IEnumerable<NhaThauHopDongQuery> FindListNhaThauHopDongByGoiThau(Guid goiThauId);
        IEnumerable<VdtDaGoiThauQuery> FindByKhlcNhaThauId(Guid iIdKhlcNhaThauId);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauNguonVonByChungTu(Guid iIdChungTu, string sLoaiChungTu);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByChungTu(Guid iIdChungTu, string sLoaiChungTu, bool bIsAdd);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByChungTuCtdt_KhlcntEdit(Guid iIdKhlcnt);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauHangMucByChungTu(Guid iIdChungTu, string sLoaiChungTu);
        void DeleteGoiThauDetail(List<Guid> iIdGoiThaus);
        void DeleteListGoiThau(List<Guid> iIdGoiThaus);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauNguonVonByKhlcNhaThauId(Guid iIdKhlcNhaThau);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByKhlcNhaThauId(Guid iIdKhlcNhaThau);
        IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauHangMucByKhlcNhaThauId(Guid iIdKhlcNhaThau);
        IEnumerable<VdtKhlcntChiPhiNguonVonCanCuQuery> GetAllNguonVonByLoaiCanCuInKhlcntScreen(List<Guid> iIdCanCuIds, string sLoaiCanCu);
        IEnumerable<VdtKhlcntChiPhiNguonVonCanCuQuery> GetAllChiPhiByLoaiCanCuInKhlcntScreen(List<Guid> iIdCanCuIds, string sLoaiCanCu);
        void ReActiveGoiThauByKhlcntId(Guid iIdKhlcnt);
        IEnumerable<HopDongGoiThauQuery> FindGoiThauByDuAn(Guid duanId);
        IEnumerable<HopDongGoiThauQuery> FindGoiThauByHopDong(Guid hopdongId);
        IEnumerable<HopDongGoiThauQuery> DCFindGoiThauByHopDong(Guid hopDongGocId, Guid? hopdongDCId);
        string GetTypeOfGoiThau(Guid goithauId);
    }
}
