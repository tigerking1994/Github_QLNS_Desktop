using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtDaTtHopDongService
    {
        IEnumerable<VdtDmLoaiHopDong> GetAllLoaiHopDong();
        void AddRange(IEnumerable<VdtDaTtHopDong> entity);
        List<VdtDaTtHopDong> FindByListDuAnId(List<Guid> iIdDuAnIds);
        IEnumerable<HopDongQuery> FindAllHopDongByNamLamViec(int namLamViec);
        int Add(VdtDaTtHopDong entity);
        int Update(VdtDaTtHopDong entity);
        int Delete(Guid id);
        VdtDaTtHopDong Find(params object[] keyValues);
        IEnumerable<VdtDaTtHopDong> FindAll(Expression<Func<VdtDaTtHopDong, bool>> predicate);
        bool CheckExistHopDongByGoiThai(Guid iIdGoiThau);
        IEnumerable<HopDongHangMucQuery> GetPhuLucHangMucByGoiThau(Guid iIdGoiThauId, Guid iIdHopDongId);
        IEnumerable<HopDongHangMucQuery> GetPhuLucChiPhiByGoiThau(Guid iIdGoiThauId, Guid iIdHopDongId);
        IEnumerable<HopDongHangMucQuery> GetGoiThauChiPhiByHopDong(Guid iIdHopDongId);
        IEnumerable<HopDongHangMucQuery> GetAllHangMucByGoiThau(Guid iIdGoiThauId);
        IEnumerable<HopDongHangMucQuery> GetAllHangMucByListGoiThauHopDongAdd(Guid iIdHopDongId, List<Guid> listGoiThauId);
        IEnumerable<HopDongHangMucQuery> GetAllHangMucByHopDong(Guid iIdHopDongId);
        IEnumerable<HopDongHangMucQuery> GetAllHangMucByHopDongDieuChinh(Guid iIdHopDongId);
        IEnumerable<HopDongHangMucQuery> GetAllHangMucByListGoiThau(List<Guid> listGoiThauId);
        void DeleteHopDongDetail(Guid iIdHopDongId);
        void InsertHopDongGoiThauNhaThau(List<VdtDaHopDongGoiThauNhaThau> lstData);
        void InsertHopDongGoiThauHangMuc(List<VdtDaHopDongGoiThauHangMuc> lstData);
        void DeleteHopDongGoiThauNhaThau(List<Guid> listGoiThauNhaThauId);
        void InsertHopDongGoiThauChiPhi(List<VdtDaHopDongGoiThauChiPhi> lstData);
        void InsertHopDongDMHangMuc(List<VdtDaHopDongDmHangMuc> lstData);
        IEnumerable<VdtDaHopDongGoiThauNhaThau> ListGoiThauNhaThauByGoiThauId(Guid goiThauId);
        void LockOrUnlock(Guid id, bool isLock);
        void DeleteHopDongDanhMucHangMuc(List<Guid> guids);
        void DeleteHopDong(Guid id);
        void DeactiveHopDong(Guid id);
        double CalculateTotalValueGoiThau(Guid goithauId, Guid hopDongId);
        double CalculateTotalUsedValueOfChiPhi(Guid chiphiId, Guid hopDongId);
        void SaveHopDong(VdtDaTtHopDong vdtDaTtHopDong);
        void SaveHopDongDC(VdtDaTtHopDong vdtDaTtHopDongDC, VdtDaTtHopDong vdtDaTtHopDongGoc);
    }
}
