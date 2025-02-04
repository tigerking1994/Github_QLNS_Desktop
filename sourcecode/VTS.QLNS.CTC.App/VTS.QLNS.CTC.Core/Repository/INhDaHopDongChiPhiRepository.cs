using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaHopDongChiPhiRepository : IRepository<NhDaHopDongChiPhi>
    {
        public IEnumerable<NhDaHopDongChiPhi> FindByIdHopDong(Guid idHopDong);
        public IEnumerable<NhDaHopDongChiPhi> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh);
        public IEnumerable<NhDaHopDongChiPhi> FindByHopDongIdGoiThauChiPhiId(Guid? idHopDong, Guid? idGoiThauChiPhi);
        void DeleteChiphiHopDongTrongNuoc(Guid? iId_HopDongGoiThauNhaThauId);
        public IEnumerable<NhDaHopDongChiPhi> FindByHopDongGoiThauNhaThauID(Guid? idHopDongGoiThauNhaThau);
        void DeleteByHdNguonVonId(Guid id);
        void SaveListHangMuc(NhDaHopDongHangMuc listhangmuc);
        void DeleteChiphiHangMucHopDongByIdHopDong(Guid? IIdHopDongId);
        IEnumerable<NhDaGoiThauChiPhiHangMucQuery> FindHopDongChiPhihangMucById(Guid idHopDongID);
    }
}
