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
    public class NhDaGoiThauHangMucSerrvice : INhDaGoiThauHangMucSerrvice
    {
        private INhDaGoiThauHangMucRepository _nhDaGoiThauRepository;

        public NhDaGoiThauHangMucSerrvice(INhDaGoiThauHangMucRepository nhDaGoiThauRepository)
        {
            _nhDaGoiThauRepository = nhDaGoiThauRepository;
        }

        public int Add(NhDaGoiThauHangMuc entitis)
        {
            return _nhDaGoiThauRepository.Add(entitis);
        }

        public int AddRange(List<NhDaGoiThauHangMuc> entitis)
        {
            return _nhDaGoiThauRepository.AddRange(entitis);
        }

        public int DeleteHangMuc(Guid idChiPhi)
        {
            return _nhDaGoiThauRepository.Delete(idChiPhi);
        }

        public IEnumerable<NhDaGoiThauHangMucQuery> FindByChiPhiId(Guid idChiPhi)
        {
            return _nhDaGoiThauRepository.FindByChiPhiId(idChiPhi);
        }
        public IEnumerable<NhDaGoiThauHangMucQuery> FindByGoiThauId(Guid idGoiThau)
        {
            return _nhDaGoiThauRepository.FindByGoiThauId(idGoiThau);
        }

        public IEnumerable<NhDaGoiThauHangMuc> FindListHangMuc(Guid idChiPhi)
        {
            return _nhDaGoiThauRepository.FindAll().Where(x => x.IIdGoiThauChiPhiId == idChiPhi);
        }

        public IEnumerable<NhDaGoiThauHangMuc> FindDataHangMucByGoiThauID(Guid idHangMuc) 
        {
            return _nhDaGoiThauRepository.FindDataByGoiThauID(idHangMuc);
        }
        
        public NhDaGoiThauHangMuc FindHangMucById(Guid idHangMuc)
        {
            return _nhDaGoiThauRepository.FindHangMucById(idHangMuc);
        }

        public int Update(NhDaGoiThauHangMuc entity)
        {
            return _nhDaGoiThauRepository.Update(entity);
        }

        public int UpdateRange(List<NhDaGoiThauHangMuc> entitis)
        {
            return _nhDaGoiThauRepository.UpdateRange(entitis);
        }

        public IEnumerable<NhDaDetailHangMucQuery> GetGoiThauHangMucByKhlcntId(Guid iIdKhlcnt)
        {
            return _nhDaGoiThauRepository.GetGoiThauHangMucByKhlcntId(iIdKhlcnt);
        }
        public IEnumerable<NhDaDetailHangMucQuery> GetGoiThauHangMucByGoiThautId(Guid iIdKhlcnt)
        {
            return _nhDaGoiThauRepository.GetGoiThauHangMucByGoiThautId(iIdKhlcnt);
        }

        public IEnumerable<NhDaHangMucGoiThauQuery> FindByChiPhi(Guid idChiPhi)
        {
            return _nhDaGoiThauRepository.FindByChiPhi(idChiPhi);
        }

        public IEnumerable<NhDaGoiThauHangMuc> FindAll()
        {
            return _nhDaGoiThauRepository.FindAll();
        }

        public IEnumerable<NhDaGoiThauHangMuc> FindAll(Expression<Func<NhDaGoiThauHangMuc, bool>> predicate)
        {
            return _nhDaGoiThauRepository.FindAll(predicate);
        }

        public int Delete(NhDaGoiThauHangMuc entity)
        {
           return _nhDaGoiThauRepository.Delete(entity);
        }

        public IEnumerable<NhDaGoiThauChiPhiHangMucQuery> FindGoiThauChiPhiHangMucByGoiThauId(Guid idGoiThau)
        {
            return _nhDaGoiThauRepository.FindGoiThauChiPhiByGoiThauId(idGoiThau);
        }
    }
}
