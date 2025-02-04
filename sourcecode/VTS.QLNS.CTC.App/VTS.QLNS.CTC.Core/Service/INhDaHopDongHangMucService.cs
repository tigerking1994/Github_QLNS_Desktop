using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaHopDongHangMucService
    {
        void AddRange(List<NhDaHopDongHangMuc> listHangMuc);
        void Add(NhDaHopDongHangMuc hangMuc);
        void Delete(Guid id);
        void RemoveRange(List<NhDaHopDongHangMuc> listHangMuc);
        void DeleteByIdHopDongChiPhi(Guid idHopDongChiPhi);
        void Update(NhDaHopDongHangMuc hangMuc);
        NhDaHopDongHangMuc FindById(Guid Id);
        IEnumerable<NhDaHopDongHangMuc> FindByIdHopDong(Guid IdHopDong);
        IEnumerable<NhDaGoiThauHopDongHangMucQuery> FindByIdGoiThau(Guid IdGoiThau);
        IEnumerable<NhDaHopDongHangMuc> FindByHopDongChiPhi(Guid IdHopDongChiPhi);
        void DeleteHopDongHangMucTrongNuoc(Guid? IIdHopDongChiPhiId);
        void UpdateRange(List<NhDaHopDongHangMuc> entities);
        IEnumerable<NhDaHopDongHangMuc> FindAll(Expression<Func<NhDaHopDongHangMuc, bool>> predicate);
        void DeleteByIdHopDong(Guid idHopDong);
    }
}
