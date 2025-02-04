using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDsBangLuongKeHoachNq104Service : ITlDsBangLuongKeHoachNq104Service
    {
        private readonly ITlDsBangLuongKeHoachNq104Repository _tlDsBangLuongKeHoachRepository;

        public TlDsBangLuongKeHoachNq104Service(ITlDsBangLuongKeHoachNq104Repository tlDsBangLuongKeHoachRepository)
        {
            _tlDsBangLuongKeHoachRepository = tlDsBangLuongKeHoachRepository;
        }

        public int Add(TlDsBangLuongKeHoachNq104 tlDsBangLuongKeHoach)
        {
            return _tlDsBangLuongKeHoachRepository.Add(tlDsBangLuongKeHoach);
        }

        public int Delete(Guid id)
        {
            return _tlDsBangLuongKeHoachRepository.Delete(id);
        }

        public IEnumerable<TlDsBangLuongKeHoachNq104> FindAll()
        {
            return _tlDsBangLuongKeHoachRepository.FindAll();
        }

        public TlDsBangLuongKeHoachNq104 FindByCondition(string cACH0, string maDonVi, int nam)
        {
            return _tlDsBangLuongKeHoachRepository.FindByCondition(cACH0, maDonVi, nam);
        }
    }
}
