using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlCanBoPhuCapKeHoachNq104Service : ITlCanBoPhuCapKeHoachNq104Service
    {
        private readonly ITlCanBoPhuCapKeHoachNq104Repository _tlCanBoPhuCapKeHoachRepository;

        public TlCanBoPhuCapKeHoachNq104Service(ITlCanBoPhuCapKeHoachNq104Repository tlCanBoPhuCapKeHoachRepository)
        {
            _tlCanBoPhuCapKeHoachRepository = tlCanBoPhuCapKeHoachRepository;
        }

        public int AddRange(IEnumerable<TlCanBoPhuCapKeHoachNq104> tlCanBoPhuCapKeHoaches)
        {
            return _tlCanBoPhuCapKeHoachRepository.AddRange(tlCanBoPhuCapKeHoaches);
        }

        public void BulkInsert(IEnumerable<TlCanBoPhuCapKeHoachNq104> tlCanBoPhuCapKeHoaches)
        {
            _tlCanBoPhuCapKeHoachRepository.BulkInsert(tlCanBoPhuCapKeHoaches);
        }

        public void BulkUpdate(IEnumerable<TlCanBoPhuCapKeHoachNq104> tlCanBoPhuCapKeHoaches)
        {
            _tlCanBoPhuCapKeHoachRepository.BulkUpdate(tlCanBoPhuCapKeHoaches);
        }

        public int DeleteByMaCanBo(string maCanBo)
        {
            return _tlCanBoPhuCapKeHoachRepository.DeleteByMaCanBo(maCanBo);
        }

        public int DeleteManyMaCanBo(string lstMaCanBo)
        {
            return _tlCanBoPhuCapKeHoachRepository.DeleteManyMaCanBo(lstMaCanBo);
        }

        public int DeleteByYear(int year)
        {
            return _tlCanBoPhuCapKeHoachRepository.DeleteByYear(year);
        }

        public IEnumerable<TlCanBoPhuCapKeHoachNq104> FindByCondition(Expression<Func<TlCanBoPhuCapKeHoachNq104, bool>> predicate)
        {
            return _tlCanBoPhuCapKeHoachRepository.FindByCondition(predicate);
        }
        public IEnumerable<TlCanBoPhuCapKeHoachNq104> FindByMaCanBo(string maCanBo)
        {
            return _tlCanBoPhuCapKeHoachRepository.FindByMaCanBo(maCanBo);
        }

        public IEnumerable<TlPhuCapNq104Query> FindCanBoPhuCap(string maCanBo)
        {
            return _tlCanBoPhuCapKeHoachRepository.FindCanBoPhuCap(maCanBo);
        }
    }
}
