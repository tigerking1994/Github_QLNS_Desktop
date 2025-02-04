using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaGoiThauService
    {
        void DeleteByIidKhlcNhaThau(Guid iIdKhlcNhaThauId);
        int Add(NhDaGoiThau entity);
        int Update(IEnumerable<NhDaGoiThau> datas);
        int AddRange(IEnumerable<NhDaGoiThau> entities);
        int Delete(Guid id);
        void DeleteRange(IEnumerable<NhDaGoiThau> items);
        NhDaGoiThau FindById(params object[] keyValues);
        int Update(NhDaGoiThau entity);
        int UpdateRange(IEnumerable<NhDaGoiThau> entities);
        void UpdateRange(List<NhDaGoiThau> entities, bool bIsActive = true);
        IEnumerable<NhDaGoiThau> FindAll();
        IEnumerable<NhDaGoiThau> FindAll(Expression<Func<NhDaGoiThau, bool>> predicate);
        IEnumerable<NhDaGoiThau> FindByIidKhlcNhaThau(Guid iIdKhlcNhaThau);
        IEnumerable<NhDaGoiThau> FindByIidKhlcNhaThauID(Guid iIdKhlcNhaThau);
        void LockOrUnlock(Guid id, bool status);
        IEnumerable<NhDaGoiThauTrongNuocQuery> GetAllGoiThauTrongNuoc(int ILoai, int IThuocMenu);
        IEnumerable<NhDaGoiThauTrongNuocQuery> GetAllGoiThauTrongNuocByILoai(int ILoai, int IThuocMenu);
        IEnumerable<NhDaThongTinNhaThauHopDongQuery> GetThongTinHopDongByIdGoiThau(Guid idGoiThau);
        IEnumerable<NhDaGoiThauQuery> GetAll();
        IEnumerable<NhDaGoiThauDetailQuery> FindGoiThauDetail();
    }
}
