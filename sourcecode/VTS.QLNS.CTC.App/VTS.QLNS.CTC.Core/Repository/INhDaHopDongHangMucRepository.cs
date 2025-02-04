using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaHopDongHangMucRepository : IRepository<NhDaHopDongHangMuc>
    {
        IEnumerable<NhDaHopDongHangMuc> FindByHopDongChiPhi(Guid idHopDongChiPhi);
        void DeleteHopDongHangMucTrongNuoc(Guid? IIdHopDongChiPhiId);
        void AddOrUpdate(Guid id, IEnumerable<NhDaHopDongHangMuc> hopDongHangMucs);
        void AddOrUpdateListHangMuc(Guid id, IEnumerable<NhDaHopDongHangMuc> hopDongHangMucs);
        IEnumerable<NhDaHopDongHangMuc> FindByIdHopDong(Guid IdHopDong);
        IEnumerable<NhDaGoiThauHopDongHangMucQuery> FindByIdGoiThau(Guid IdGoiThau);
    }
}
