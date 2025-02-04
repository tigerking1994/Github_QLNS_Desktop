using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQsKeHoachChiTietNq104Service : ITlQsKeHoachChiTietNq104Service
    {
        private readonly ITlQsKeHoachChiTietNq104Repository _tlQsKeHoachChiTietRepository;

        public TlQsKeHoachChiTietNq104Service(ITlQsKeHoachChiTietNq104Repository tlQsKeHoachChiTietRepository)
        {
            _tlQsKeHoachChiTietRepository = tlQsKeHoachChiTietRepository;
        }

        public int AddRange(IEnumerable<TlQsKeHoachChiTietNq104> lstQsKeHoachChiTiet)
        {
            return _tlQsKeHoachChiTietRepository.AddRange(lstQsKeHoachChiTiet);
        }

        public int UpdateRange(IEnumerable<TlQsKeHoachChiTietNq104> lstQsKeHoachChiTiet)
        {
            return _tlQsKeHoachChiTietRepository.UpdateRange(lstQsKeHoachChiTiet);
        }


        public int Delete(Guid id)
        {
            return _tlQsKeHoachChiTietRepository.Delete(id);
        }

        public int DeleteByNam(int nam)
        {
            return _tlQsKeHoachChiTietRepository.DeleteByNam(nam);
        }

        public TlQsKeHoachChiTietNq104 FindByCondition(string maDonVi, int thang, int nam)
        {
            return _tlQsKeHoachChiTietRepository.FindByCondition(maDonVi, thang, nam);
        }
    }
}
