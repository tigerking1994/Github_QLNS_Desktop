using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQsKeHoachChiTietService : ITlQsKeHoachChiTietService
    {
        private readonly ITlQsKeHoachChiTietRepository _tlQsKeHoachChiTietRepository;

        public TlQsKeHoachChiTietService(ITlQsKeHoachChiTietRepository tlQsKeHoachChiTietRepository)
        {
            _tlQsKeHoachChiTietRepository = tlQsKeHoachChiTietRepository;
        }

        public int AddRange(IEnumerable<TlQsKeHoachChiTiet> lstQsKeHoachChiTiet)
        {
            return _tlQsKeHoachChiTietRepository.AddRange(lstQsKeHoachChiTiet);
        }

        public int UpdateRange(IEnumerable<TlQsKeHoachChiTiet> lstQsKeHoachChiTiet)
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

        public TlQsKeHoachChiTiet FindByCondition(string maDonVi, int thang, int nam)
        {
            return _tlQsKeHoachChiTietRepository.FindByCondition(maDonVi, thang, nam);
        }
    }
}
