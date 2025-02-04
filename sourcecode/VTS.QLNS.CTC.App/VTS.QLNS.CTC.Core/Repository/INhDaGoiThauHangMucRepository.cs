using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaGoiThauHangMucRepository : IRepository<NhDaGoiThauHangMuc>
    {
        IEnumerable<NhDaDetailHangMucQuery> GetGoiThauHangMucByKhlcntId(Guid iIdKhlcnt);
        IEnumerable<NhDaDetailHangMucQuery> GetGoiThauHangMucByGoiThautId(Guid iIdKhlcnt);
        IEnumerable<NhDaGoiThauHangMucQuery> FindByChiPhiId(Guid idChiPhi);
        IEnumerable<NhDaGoiThauHangMucQuery> FindByGoiThauId(Guid idGoiThau);
        IEnumerable<NhDaGoiThauHangMuc> FindDataByGoiThauID(Guid idGoiThauID);
        NhDaGoiThauHangMuc FindHangMucById(Guid idHangMuc);
        IEnumerable<NhDaHangMucGoiThauQuery> FindByChiPhi(Guid idChiPhi);
        void AddOrUpdate(Guid chiPhiId, IEnumerable<NhDaGoiThauHangMuc> items);
        IEnumerable<NhDaGoiThauChiPhiHangMucQuery> FindGoiThauChiPhiByGoiThauId(Guid idGoiThauID);

    }
}
