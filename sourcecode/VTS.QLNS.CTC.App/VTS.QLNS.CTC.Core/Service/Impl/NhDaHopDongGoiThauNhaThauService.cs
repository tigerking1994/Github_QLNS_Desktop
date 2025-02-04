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
    public class NhDaHopDongGoiThauNhaThauService : INhDaHopDongGoiThauNhaThauService
    {
        private INhDaHopDongGoiThauNhaThauRepository _repository;

        public NhDaHopDongGoiThauNhaThauService(INhDaHopDongGoiThauNhaThauRepository repository)
        {
            _repository = repository;
        }

        public void AddRange(List<NhDaHopDongGoiThauNhaThau> entities)
        {
            _repository.AddRange(entities);
        }

        public void Delete(NhDaHopDongGoiThauNhaThau entity)
        {
            _repository.Delete(entity);
        }

        public IEnumerable<NhDaHopDongGoiThauNhaThauQuery> FindByIdHopDong(Guid? idHopDong)
        {
            return _repository.FindByIdHopDong(idHopDong);
        }

        public void UpDate(NhDaHopDongGoiThauNhaThau entity)
        {
            _repository.Update(entity);
        }

        public void UpDateRange(List<NhDaHopDongGoiThauNhaThau> entities)
        {
            _repository.UpdateRange(entities);
        }
    }
}
