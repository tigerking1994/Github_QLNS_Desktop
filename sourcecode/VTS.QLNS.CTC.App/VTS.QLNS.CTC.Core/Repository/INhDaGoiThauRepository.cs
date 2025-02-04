using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaGoiThauRepository : IRepository<NhDaGoiThau>
    {
        IEnumerable<NhDaGoiThau> FindByIidKhlcNhaThau(Guid iIdKhlcNhaThau);
        IEnumerable<NhDaGoiThau> FindByIidKhlcNhaThauID(Guid iIdKhlcNhaThau);
        void DeleteByIidKhlcNhaThau(Guid iIdKhlcNhaThau);
        IEnumerable<NhDaGoiThauQuery> GetAll();
        IEnumerable<NhDaGoiThauTrongNuocQuery> GetAllGoiThauTrongNuoc(int ILoai, int IThuocMenu);
        IEnumerable<NhDaGoiThauTrongNuocQuery> GetAllGoiThauTrongNuocByILoai(int ILoai, int IThuocMenu);
        IEnumerable<NhDaThongTinNhaThauHopDongQuery> GetThongTinHopDongByIdGoiThau(Guid idGoiThau);
        IEnumerable<NhDaGoiThauDetailQuery> FindGoiThauDetail();
        void DeleteGoiThauDetail(List<Guid> iIdGoiThaus);
        void DeleteListGoiThau(List<Guid> iIdGoiThaus);
    }
}
