using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaGoiThauNguonVonSerrvice : INhDaGoiThauNguonVonService
    {
        private INhDagoiThauNguonVonRepository _nhDagoiThauNguonVonRepository;

        public NhDaGoiThauNguonVonSerrvice(INhDagoiThauNguonVonRepository nhDagoiThauNguonVonRepository)
        {
            _nhDagoiThauNguonVonRepository = nhDagoiThauNguonVonRepository;
        }

        public int Add(NhDaGoiThauNguonVon entitis)
        {
            return _nhDagoiThauNguonVonRepository.Add(entitis);
        }

        public int AddRange(List<NhDaGoiThauNguonVon> entitis)
        {
            return _nhDagoiThauNguonVonRepository.AddRange(entitis);
        }

        public int DeleteNguonVon(Guid idNguonVon)
        {
            return _nhDagoiThauNguonVonRepository.Delete(idNguonVon);
        }

        public IEnumerable<NhDaGoiThauNguonVon> FindAll()
        {
            return _nhDagoiThauNguonVonRepository.FindAll();
        }

        public IEnumerable<NhDaGoiThauNguonVon> FindByListNguonVon(Guid idGoiThau)
        {
            return _nhDagoiThauNguonVonRepository.FindAll().Where(x => x.IIdGoiThauId == idGoiThau);
        }

        public IEnumerable<NhDaGoiThauNguonVon> FindByListNguonVonKhlcntId(Guid iIdKhlcnt)
        {
            return _nhDagoiThauNguonVonRepository.FindByListNguonVonKhlcntId(iIdKhlcnt);
        }
        public IEnumerable<NhDaGoiThauNguonVon> FindByListNguonVonGoiThauId(Guid iIdKhlcnt)
        {
            return _nhDagoiThauNguonVonRepository.FindByListNguonVonGoiThauId(iIdKhlcnt);
        }
        
        public IEnumerable<NhDaGoiThauNguonVon> GetListNguonVonByIdGoiThau(Guid idGoiThau)
        {
            return _nhDagoiThauNguonVonRepository.GetListNguonVonByIdGoiThau(idGoiThau);
        }

        public IEnumerable<NhDaGoiThauThongTinNguonVonQuery> FindByIdGoiThau(Guid idGoiThau)
        {
            return _nhDagoiThauNguonVonRepository.FindByIdGoiThau(idGoiThau);
        }

        public int Update(NhDaGoiThauNguonVon entity)
        {
            return _nhDagoiThauNguonVonRepository.Update(entity);
        }

        public int UpdateRange(List<NhDaGoiThauNguonVon> entitis)
        {
            return _nhDagoiThauNguonVonRepository.UpdateRange(entitis);
        }

        public IEnumerable<NhDaDetailNguonVonQuery> GetGoiThauNguonVonByKhlcntId(Guid iIdKhlcnt)
        {
            return _nhDagoiThauNguonVonRepository.GetGoiThauNguonVonByKhlcntId(iIdKhlcnt);
        }
        public IEnumerable<NhDaDetailNguonVonQuery> GetGoiThauNguonVonByGoiThauId(Guid iIdKhlcnt)
        {
            return _nhDagoiThauNguonVonRepository.GetGoiThauNguonVonByGoiThauId(iIdKhlcnt);
        }

        public NhDaGoiThauThongTinNguonVonQuery FindById(Guid id, Guid idGoiThau)
        {
            return _nhDagoiThauNguonVonRepository.FindByIdGoiThau(idGoiThau).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<NhDaCacQuyetDinhNguonVonGoiThauQuery> FindCacQuyetDinhNguonVonByIdGoiThau(Guid idGoiThau)
        {
            return _nhDagoiThauNguonVonRepository.FindCacQuyetDinhNguonVonByIdGoiThau(idGoiThau);
        }
    }
}
