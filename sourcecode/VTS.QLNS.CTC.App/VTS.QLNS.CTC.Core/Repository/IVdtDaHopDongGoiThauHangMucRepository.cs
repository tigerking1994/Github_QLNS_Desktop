using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDaHopDongGoiThauHangMucRepository : IRepository<VdtDaHopDongGoiThauHangMuc>
    {
        IEnumerable<HopDongHangMucQuery> GetPhuLucHangMucByGoiThau(Guid iIdGoiThauId, Guid iIdHopDongId);
        IEnumerable<HopDongHangMucQuery> GetPhuLucChiPhiByGoiThau(Guid iIdGoiThauId, Guid iIdHopDongId);
        IEnumerable<HopDongHangMucQuery> GetGoiThauChiPhiByHopDong(Guid iIdHopDongId);
        IEnumerable<HopDongHangMucQuery> GetAllHangMucByGoiThau(Guid iIdGoiThauId);
        IEnumerable<HopDongHangMucQuery> GetAllHangMucByListGoiThauHopDongAdd(Guid iIdHopDongId, List<Guid> listGoiThauId);
        IEnumerable<HopDongHangMucQuery> GetAllHangMucByHopDong(Guid iIdHopDongId);
        IEnumerable<HopDongHangMucQuery> GetAllHangMucByListGoiThau(List<Guid> listGoiThauId);
        IEnumerable<HopDongHangMucQuery> GetAllHangMucByHopDongDieuChinh(Guid iIdHopDongId);
        IEnumerable<VdtDmLoaiHopDong> GetAllLoaiHopDong();
    }
}
