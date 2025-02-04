using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaHopDongChiPhiService
    {
        void Add(NhDaHopDongChiPhi nhDaHopDongChiPhi);
        void Delete(NhDaHopDongChiPhi nhDaHopDongChiPhi);
        IEnumerable<NhDaHopDongChiPhi> FindByIdHopDong(Guid idHopDong);
        IEnumerable<NhDaHopDongChiPhi> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh);
        IEnumerable<NhDaHopDongChiPhi> FindByHopDongIdGoiThauChiPhiId(Guid? idHopDong, Guid? idGoiThauChiPhi);
        void AddRange(List<NhDaHopDongChiPhi> entities);
        void DeleteChiphiHopDongTrongNuoc(Guid? IIdHopDongGoiThauNhaThauId);
        void UpdateRange(List<NhDaHopDongChiPhi> entities);
        void SaveListHangMuc(NhDaHopDongHangMuc entities);
        public IEnumerable<NhDaHopDongChiPhi> FindByHopDongGoiThauNhaThauID(Guid? idHopDongGoiThauNhaThau);
        IEnumerable<NhDaHopDongChiPhi> FindAll(Expression<Func<NhDaHopDongChiPhi, bool>> predicate);
        IEnumerable<NhDaGoiThauChiPhiHangMucQuery> FindHopDongChiPhihangMucById(Guid idHopDongID);
        void DeleteChiphiHangMucHopDongByIdHopDong(Guid idHopDongID);
    }
}
