using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaGoiThauHangMucSerrvice
    {
        int AddRange(List<NhDaGoiThauHangMuc> entitis);
        int Add(NhDaGoiThauHangMuc entitis);
        int Update(NhDaGoiThauHangMuc entity);
        int UpdateRange(List<NhDaGoiThauHangMuc> entitis);
        int DeleteHangMuc(Guid idChiPhi);
        int Delete(NhDaGoiThauHangMuc entity);
        IEnumerable<NhDaGoiThauHangMuc> FindListHangMuc(Guid idChiPhi);
        IEnumerable<NhDaGoiThauHangMuc> FindDataHangMucByGoiThauID(Guid idChiPhi);
        NhDaGoiThauHangMuc FindHangMucById(Guid idHangMuc);
        IEnumerable<NhDaDetailHangMucQuery> GetGoiThauHangMucByKhlcntId(Guid iIdKhlcnt);
        IEnumerable<NhDaDetailHangMucQuery> GetGoiThauHangMucByGoiThautId(Guid iIdKhlcnt);
        IEnumerable<NhDaGoiThauHangMucQuery> FindByChiPhiId(Guid idChiPhi);
        IEnumerable<NhDaGoiThauHangMucQuery> FindByGoiThauId(Guid idGoiThau);
        IEnumerable<NhDaHangMucGoiThauQuery> FindByChiPhi(Guid idChiPhi);
        IEnumerable<NhDaGoiThauHangMuc> FindAll();
        IEnumerable<NhDaGoiThauHangMuc> FindAll(Expression<Func<NhDaGoiThauHangMuc, bool>> predicate);
        IEnumerable<NhDaGoiThauChiPhiHangMucQuery> FindGoiThauChiPhiHangMucByGoiThauId(Guid idGoiThau);

    }
}
