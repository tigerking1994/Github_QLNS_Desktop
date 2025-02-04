using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaGoiThauService : INhDaGoiThauService
    {
        private readonly INhDaGoiThauRepository _nhDaGoiThauRepository;
        private readonly INhDaGoiThauChiPhiRepository _nhDaGoiThauChiPhiRepository;
        private readonly INhDaGoiThauHangMucRepository _nhDaGoiThauHangMucRepository;
        private readonly INhDagoiThauNguonVonRepository _nhDagoiThauNguonVonRepository;

        public NhDaGoiThauService(INhDaGoiThauRepository nhDaGoiThauRepository,
            INhDaGoiThauChiPhiRepository nhDaGoiThauChiPhiRepository,
            INhDaGoiThauHangMucRepository nhDaGoiThauHangMucRepository,
            INhDagoiThauNguonVonRepository nhDagoiThauNguonVonRepository)
        {
            _nhDaGoiThauRepository = nhDaGoiThauRepository;
            _nhDaGoiThauChiPhiRepository = nhDaGoiThauChiPhiRepository;
            _nhDaGoiThauHangMucRepository = nhDaGoiThauHangMucRepository;
            _nhDagoiThauNguonVonRepository = nhDagoiThauNguonVonRepository;
        }

        public void DeleteByIidKhlcNhaThau(Guid iIdKhlcNhaThauId)
        {
            _nhDaGoiThauRepository.DeleteByIidKhlcNhaThau(iIdKhlcNhaThauId);
        }

        public int Add(NhDaGoiThau entity)
        {
            return _nhDaGoiThauRepository.Add(entity);
        }

        public int Update(IEnumerable<NhDaGoiThau> datas)
        {
            return _nhDaGoiThauRepository.UpdateRange(datas);
        }

        public int AddRange(IEnumerable<NhDaGoiThau> entities)
        {
            return _nhDaGoiThauRepository.AddRange(entities);
        }

        public int Delete(Guid id)
        {
            var listNguonvon = _nhDagoiThauNguonVonRepository.FindAll().Where(x => x.IIdGoiThauId == id);
            var listChiPhi = _nhDaGoiThauChiPhiRepository.FindAll(x => x.IIdGoiThauId == id);
            foreach (var item in listChiPhi)
            {
                var listHangMuc = _nhDaGoiThauHangMucRepository.FindAll().Where(x => x.IIdGoiThauChiPhiId == item.Id);
                foreach (var hangMuc in listHangMuc)
                {
                    _nhDaGoiThauHangMucRepository.Delete(hangMuc);
                }
                _nhDaGoiThauChiPhiRepository.Delete(item);
            }
            foreach (var item in listNguonvon)
            {
                _nhDagoiThauNguonVonRepository.Delete(item);
            }
            return _nhDaGoiThauRepository.Delete(id);
        }

        public NhDaGoiThau FindById(params object[] keyValues)
        {
            return _nhDaGoiThauRepository.Find(keyValues);
        }

        public IEnumerable<NhDaGoiThauQuery> GetAll()
        {
            return _nhDaGoiThauRepository.GetAll();
        }

        public IEnumerable<NhDaGoiThauTrongNuocQuery> GetAllGoiThauTrongNuoc(int ILoai, int IThuocMenu)
        {
            return _nhDaGoiThauRepository.GetAllGoiThauTrongNuoc(ILoai, IThuocMenu);
        }
        public IEnumerable<NhDaGoiThauTrongNuocQuery> GetAllGoiThauTrongNuocByILoai(int ILoai, int IThuocMenu)
        {
            return _nhDaGoiThauRepository.GetAllGoiThauTrongNuocByILoai(ILoai, IThuocMenu);
        }

        public IEnumerable<NhDaThongTinNhaThauHopDongQuery> GetThongTinHopDongByIdGoiThau(Guid idGoiThau)
        {
            return _nhDaGoiThauRepository.GetThongTinHopDongByIdGoiThau(idGoiThau);
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            NhDaGoiThau entity = _nhDaGoiThauRepository.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _nhDaGoiThauRepository.Update(entity);
            }
        }

        public int Update(NhDaGoiThau entity)
        {
            return _nhDaGoiThauRepository.Update(entity);
        }

        public int UpdateRange(IEnumerable<NhDaGoiThau> entities)
        {
            return _nhDaGoiThauRepository.UpdateRange(entities);
        }

        public void UpdateRange(List<NhDaGoiThau> entities, bool bIsActive = true)
        {
            if (bIsActive)
            {
                List<VdtDaGoiThau> lstData = new List<VdtDaGoiThau>();
                _nhDaGoiThauRepository.UpdateRange(entities);
                var lstId = lstData.Select(n => n.Id).ToList();
                if (lstId != null)
                    _nhDaGoiThauRepository.DeleteGoiThauDetail(lstId);
            }
            else
            {
                var lstData = entities.Select(n => { n.BActive = false; return n; });
                _nhDaGoiThauRepository.UpdateRange(lstData);
            }
        }

        public IEnumerable<NhDaGoiThau> FindAll()
        {
            return _nhDaGoiThauRepository.FindAll();
        }

        public IEnumerable<NhDaGoiThau> FindByIidKhlcNhaThau(Guid iIdKhlcNhaThau)
        {
            return _nhDaGoiThauRepository.FindByIidKhlcNhaThau(iIdKhlcNhaThau);
        }
          public IEnumerable<NhDaGoiThau> FindByIidKhlcNhaThauID(Guid iIdKhlcNhaThau)
        {
            return _nhDaGoiThauRepository.FindByIidKhlcNhaThauID(iIdKhlcNhaThau);
        }

        public IEnumerable<NhDaGoiThauDetailQuery> FindGoiThauDetail()
        {
            return _nhDaGoiThauRepository.FindGoiThauDetail();
        }

        public void DeleteRange(IEnumerable<NhDaGoiThau> items)
        {
            List<Guid> lstId = items.Select(n => n.Id).ToList();
            if (lstId != null)
                _nhDaGoiThauRepository.DeleteListGoiThau(lstId);
        }

        public IEnumerable<NhDaGoiThau> FindAll(Expression<Func<NhDaGoiThau, bool>> predicate)
        {
            return _nhDaGoiThauRepository.FindAll(predicate);
        }
    }
}
