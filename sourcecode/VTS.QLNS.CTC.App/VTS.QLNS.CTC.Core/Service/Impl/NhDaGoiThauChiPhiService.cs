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
    public class NhDaGoiThauChiPhiService : INhDaGoiThauChiPhiService
    {
        private INhDaGoiThauChiPhiRepository _nhDaGoiThauChiPhiRepository;

        public NhDaGoiThauChiPhiService(INhDaGoiThauChiPhiRepository nhDaGoiThauChiPhiRepository)
        {
            _nhDaGoiThauChiPhiRepository = nhDaGoiThauChiPhiRepository;
        }

        public IEnumerable<NhDaGoiThauChiPhi> FindAll()
        {
            return _nhDaGoiThauChiPhiRepository.FindAll();
        }

        public int Add(NhDaGoiThauChiPhi entity)
        {
            return _nhDaGoiThauChiPhiRepository.Add(entity);
        }

        public int AddRange(List<NhDaGoiThauChiPhi> entitis)
        {
            return _nhDaGoiThauChiPhiRepository.AddRange(entitis);
        }

        public int Delete(Guid idGoiThau)
        {
            return _nhDaGoiThauChiPhiRepository.Delete(idGoiThau);
        }

        public IEnumerable<NhDaGoiThauChiPhi> FindListChiPhi(Guid idGoiThau)
        {
            return _nhDaGoiThauChiPhiRepository.FindAll().Where(x => x.IIdGoiThauId == idGoiThau);
        }
        public IEnumerable<NhDaGoiThauChiPhi> FindListChiPhiByNguonVon(Guid idNguonVon)
        {
            return _nhDaGoiThauChiPhiRepository.FindAll().Where(x => x.IIdGoiThauNguonVonId == idNguonVon);
        }

        public IEnumerable<NhDaGoiThauChiPhi> FindListChiPhiByKhlcnt(Guid iIdKhlcnt)
        {
            return _nhDaGoiThauChiPhiRepository.FindListChiPhiByKhlcnt(iIdKhlcnt);
        }
        public IEnumerable<NhDaGoiThauChiPhi> FindListChiPhiByGT(Guid iIdKhlcnt)
        {
            return _nhDaGoiThauChiPhiRepository.FindListChiPhiByGT(iIdKhlcnt);
        }

        
        public int Update(NhDaGoiThauChiPhi entity)
        {
            return _nhDaGoiThauChiPhiRepository.Update(entity);
        }

        public int UpdateRange(List<NhDaGoiThauChiPhi> entitis)
        {
            return _nhDaGoiThauChiPhiRepository.UpdateRange(entitis);
        }

        public IEnumerable<NhDaDetailChiPhiQuery> GetGoiThauChiPhiByKhlcntId(Guid iIdKhlcnt)
        {
            return _nhDaGoiThauChiPhiRepository.GetGoiThauChiPhiByKhlcntId(iIdKhlcnt);
        }
         public IEnumerable<NhDaDetailChiPhiQuery> GetGoiThauChiPhiByGoiThauId(Guid iIdKhlcnt)
        {
            return _nhDaGoiThauChiPhiRepository.GetGoiThauChiPhiByGoiThauId(iIdKhlcnt);
        }
        
        public IEnumerable<NhDaGoiThauChiPhiQuery> FindByGoiThauId(Guid idGoiThau)
        {
            return _nhDaGoiThauChiPhiRepository.FindByGoiThauId(idGoiThau);
        }

        public IEnumerable<NhDaCacQuyetDinhChiPhiGoiThauQuery> FindByCacQuyetDinhChiPhiByGoiThauId(Guid idGoiThau)
        {
            return _nhDaGoiThauChiPhiRepository.FindByCacQuyetDinhChiPhiByGoiThauId(idGoiThau);
        }

        public IEnumerable<NhDaGoiThauChiPhi> FindAll(Expression<Func<NhDaGoiThauChiPhi, bool>> predicate)
        {
            return _nhDaGoiThauChiPhiRepository.FindAll(predicate);
        }
    }
}
