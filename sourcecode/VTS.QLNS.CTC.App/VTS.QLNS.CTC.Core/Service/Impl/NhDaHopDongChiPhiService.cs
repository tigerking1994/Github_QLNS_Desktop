using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaHopDongChiPhiService : INhDaHopDongChiPhiService
    {
        private readonly INhDaHopDongChiPhiRepository _nhDaHopDongChiPhiRepository;

        public NhDaHopDongChiPhiService(INhDaHopDongChiPhiRepository nhDaHopDongChiPhiRepository)
        {
            _nhDaHopDongChiPhiRepository = nhDaHopDongChiPhiRepository;
        }
        public void Add(NhDaHopDongChiPhi nhHopDongChiPhi)
        {
            _nhDaHopDongChiPhiRepository.Add(nhHopDongChiPhi);
        }
        public void Delete(NhDaHopDongChiPhi nhDaHopDongChiPhi)
        {
            _nhDaHopDongChiPhiRepository.Delete(nhDaHopDongChiPhi.Id);
        }
        public IEnumerable<NhDaHopDongChiPhi> FindByIdHopDong(Guid idHopDong)
        {
            return _nhDaHopDongChiPhiRepository.FindByIdHopDong(idHopDong);
        }
        public IEnumerable<NhDaHopDongChiPhi> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh)
        {
            return _nhDaHopDongChiPhiRepository.FindByHopDongIdQuyetDinhId(idHopDong, idQuyetDinh);
        }
        public IEnumerable<NhDaHopDongChiPhi> FindByHopDongIdGoiThauChiPhiId(Guid? idHopDong, Guid? idGoiThauChiPhi)
        {
            return _nhDaHopDongChiPhiRepository.FindByHopDongIdGoiThauChiPhiId(idHopDong, idGoiThauChiPhi);
        }

        public void AddRange(List<NhDaHopDongChiPhi> entities)
        {
            _nhDaHopDongChiPhiRepository.AddRange(entities);
        }

        public void DeleteChiphiHopDongTrongNuoc(Guid? IIdHopDongGoiThauNhaThauId)
        {
            _nhDaHopDongChiPhiRepository.DeleteChiphiHopDongTrongNuoc(IIdHopDongGoiThauNhaThauId);
        }

        public void UpdateRange(List<NhDaHopDongChiPhi> entities)
        {
            _nhDaHopDongChiPhiRepository.UpdateRange(entities);
        }
        public void SaveListHangMuc(NhDaHopDongHangMuc entities)
        {
            _nhDaHopDongChiPhiRepository.SaveListHangMuc(entities);
        }

        public IEnumerable<NhDaHopDongChiPhi> FindByHopDongGoiThauNhaThauID(Guid? idHopDongGoiThauNhaThau)
        {
            return _nhDaHopDongChiPhiRepository.FindByHopDongGoiThauNhaThauID(idHopDongGoiThauNhaThau);
        }

        public IEnumerable<NhDaHopDongChiPhi> FindAll(Expression<Func<NhDaHopDongChiPhi, bool>> predicate)
        {
            return _nhDaHopDongChiPhiRepository.FindAll(predicate);
        }
        public IEnumerable<NhDaGoiThauChiPhiHangMucQuery> FindHopDongChiPhihangMucById(Guid idHopDongID)
        {
            return _nhDaHopDongChiPhiRepository.FindHopDongChiPhihangMucById(idHopDongID);
        }
        public void DeleteChiphiHangMucHopDongByIdHopDong(Guid idHopDongID)
        {
            _nhDaHopDongChiPhiRepository.DeleteChiphiHangMucHopDongByIdHopDong(idHopDongID);
        }
    }
}
