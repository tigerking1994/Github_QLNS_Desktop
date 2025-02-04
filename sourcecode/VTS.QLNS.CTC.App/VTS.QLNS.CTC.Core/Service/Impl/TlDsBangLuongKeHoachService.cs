using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDsBangLuongKeHoachService : ITlDsBangLuongKeHoachService
    {
        private readonly ITlDsBangLuongKeHoachRepository _tlDsBangLuongKeHoachRepository;

        public TlDsBangLuongKeHoachService(ITlDsBangLuongKeHoachRepository tlDsBangLuongKeHoachRepository)
        {
            _tlDsBangLuongKeHoachRepository = tlDsBangLuongKeHoachRepository;
        }

        public int Add(TlDsBangLuongKeHoach tlDsBangLuongKeHoach)
        {
            return _tlDsBangLuongKeHoachRepository.Add(tlDsBangLuongKeHoach);
        }

        public int Delete(Guid id)
        {
            return _tlDsBangLuongKeHoachRepository.Delete(id);
        }

        public IEnumerable<TlDsBangLuongKeHoach> FindAll()
        {
            return _tlDsBangLuongKeHoachRepository.FindAll();
        }

        public TlDsBangLuongKeHoach FindByCondition(string cACH0, string maDonVi, int nam)
        {
            return _tlDsBangLuongKeHoachRepository.FindByCondition(cACH0, maDonVi, nam);
        }
    }
}
