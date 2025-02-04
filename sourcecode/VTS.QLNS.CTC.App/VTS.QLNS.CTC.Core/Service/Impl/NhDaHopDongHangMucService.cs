using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaHopDongHangMucService : INhDaHopDongHangMucService
    {
        private readonly INhDaHopDongHangMucRepository _nhDaHopDongHangMucRepository;

        public NhDaHopDongHangMucService(INhDaHopDongHangMucRepository nhDaHopDongHangMucRepository)
        {
            _nhDaHopDongHangMucRepository = nhDaHopDongHangMucRepository;
        }

        public void Add(NhDaHopDongHangMuc hangMuc)
        {
            _nhDaHopDongHangMucRepository.Add(hangMuc);
        }

        public void AddRange(List<NhDaHopDongHangMuc> listHangMuc)
        {
            _nhDaHopDongHangMucRepository.AddRange(listHangMuc);
        }
        public void RemoveRange(List<NhDaHopDongHangMuc> listHangMuc)
        {
            _nhDaHopDongHangMucRepository.RemoveRange(listHangMuc);

        }

        public void Delete(Guid id)
        {
            _nhDaHopDongHangMucRepository.Delete(id);
        }

        public void DeleteByIdHopDongChiPhi(Guid idHopDongChiPhi)
        {
            var lstHangMuc = FindByHopDongChiPhi(idHopDongChiPhi).ToList();
            if (lstHangMuc != null && lstHangMuc.Count > 0)
            {
                _nhDaHopDongHangMucRepository.RemoveRange(lstHangMuc);
            }
        }

        public IEnumerable<NhDaHopDongHangMuc> FindByHopDongChiPhi(Guid IdHopDongChiPhi)
        {
            return _nhDaHopDongHangMucRepository.FindByHopDongChiPhi(IdHopDongChiPhi);
        }

        public NhDaHopDongHangMuc FindById(Guid Id)
        {
            return _nhDaHopDongHangMucRepository.Find(Id);
        }

        public IEnumerable<NhDaHopDongHangMuc> FindByIdHopDong(Guid IdHopDong)
        {
            return _nhDaHopDongHangMucRepository.FindByIdHopDong(IdHopDong);
        }

        public IEnumerable<NhDaGoiThauHopDongHangMucQuery> FindByIdGoiThau(Guid IdGoiThau)
        {
            return _nhDaHopDongHangMucRepository.FindByIdGoiThau(IdGoiThau);
        }

        public void Update(NhDaHopDongHangMuc hangMuc)
        {
            _nhDaHopDongHangMucRepository.Update(hangMuc);
        }

        public void DeleteHopDongHangMucTrongNuoc(Guid? IIdHopDongChiPhiId)
        {
            _nhDaHopDongHangMucRepository.DeleteHopDongHangMucTrongNuoc(IIdHopDongChiPhiId);
        }

        public void UpdateRange(List<NhDaHopDongHangMuc> entities)
        {
            _nhDaHopDongHangMucRepository.UpdateRange(entities);
        }

        public IEnumerable<NhDaHopDongHangMuc> FindAll(Expression<Func<NhDaHopDongHangMuc, bool>> predicate)
        {
            return _nhDaHopDongHangMucRepository.FindAll(predicate);
        }

        public void DeleteByIdHopDong(Guid idHopDong)
        {
            var lstHangMuc = FindByIdHopDong(idHopDong).ToList();
            if (lstHangMuc != null && lstHangMuc.Count > 0)
            {
                _nhDaHopDongHangMucRepository.RemoveRange(lstHangMuc);
            }
        }
    }
}
