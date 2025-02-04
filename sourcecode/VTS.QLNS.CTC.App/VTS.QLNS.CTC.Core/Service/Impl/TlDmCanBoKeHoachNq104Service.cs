using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCanBoKeHoachNq104Service : ITlDmCanBoKeHoachNq104Service
    {
        private readonly ITlDmCanBoKeHoachNq104Repository _TlDmCanBoKeHoachNq104Repository;

        public TlDmCanBoKeHoachNq104Service(ITlDmCanBoKeHoachNq104Repository TlDmCanBoKeHoachNq104Repository)
        {
            _TlDmCanBoKeHoachNq104Repository = TlDmCanBoKeHoachNq104Repository;
        }

        public int Add(TlDmCanBoKeHoachNq104 TlDmCanBoKeHoachNq104)
        {
            return _TlDmCanBoKeHoachNq104Repository.Add(TlDmCanBoKeHoachNq104);
        }

        public int AddRange(IEnumerable<TlDmCanBoKeHoachNq104> TlDmCanBoKeHoachNq104)
        {
            return _TlDmCanBoKeHoachNq104Repository.AddRange(TlDmCanBoKeHoachNq104);
        }

        public int Delete(Guid id)
        {
            return _TlDmCanBoKeHoachNq104Repository.Delete(id);
        }

        public int DeleteByYear(int year)
        {
            return _TlDmCanBoKeHoachNq104Repository.DeleteByYear(year);
        }

        public TlDmCanBoKeHoachNq104 Find(Guid id)
        {
            return _TlDmCanBoKeHoachNq104Repository.Find(id);
        }

        public IEnumerable<TlDmCanBoKeHoachNq104> FindAll()
        {
            return _TlDmCanBoKeHoachNq104Repository.FindAll();
        }

        public IEnumerable<TlDmCanBoKeHoachNq104> FindAllCanBo()
        {
            return _TlDmCanBoKeHoachNq104Repository.FindAllCanBo();
        }

        public IEnumerable<TlDmCanBoKeHoachNq104> FindByCondition(Expression<Func<TlDmCanBoKeHoachNq104, bool>> predicate)
        {
            return _TlDmCanBoKeHoachNq104Repository.FindAll(predicate);
        }

        public TlDmCanBoKeHoachNq104 FindByMaCanBo(string maCanbo)
        {
            return _TlDmCanBoKeHoachNq104Repository.FindByMaCanBo(maCanbo);
        }

        public IEnumerable<TlDmCanBoKeHoachNq104> FindByYear(int year)
        {
            return _TlDmCanBoKeHoachNq104Repository.FindByYear(year);
        }

        public IEnumerable<TlDmCanBoKeHoachNq104> FindLoadIndex()
        {
            return _TlDmCanBoKeHoachNq104Repository.FindLoadIndex();
        }

        public int Update(TlDmCanBoKeHoachNq104 tlDmCanBo)
        {
            return _TlDmCanBoKeHoachNq104Repository.Update(tlDmCanBo);
        }
    }
}
