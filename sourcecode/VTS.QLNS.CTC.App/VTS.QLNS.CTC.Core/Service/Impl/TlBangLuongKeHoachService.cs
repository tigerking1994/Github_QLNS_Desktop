using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlBangLuongKeHoachService : ITlBangLuongKeHoachService
    {
        private readonly ITlBangLuongKeHoachRepository _tlBangLuongKeHoachRepository;

        public TlBangLuongKeHoachService(ITlBangLuongKeHoachRepository tlBangLuongKeHoachRepository)
        {
            _tlBangLuongKeHoachRepository = tlBangLuongKeHoachRepository;
        }

        public int AddRange(IEnumerable<TlBangLuongKeHoach> tlBangLuongKeHoachs)
        {
            return _tlBangLuongKeHoachRepository.AddRange(tlBangLuongKeHoachs);
        }

        public void BulkInsert(IEnumerable<TlBangLuongKeHoach> tlBangLuongKeHoachs)
        {
            _tlBangLuongKeHoachRepository.BulkInsert(tlBangLuongKeHoachs);
        }

        public int DeleteByParentId(Guid id)
        {
            return _tlBangLuongKeHoachRepository.DeleteByParentId(id);
        }

        public IEnumerable<TlBangLuongKeHoachExportQuery> ExportQuyLuongCanCu(int iNamLamViec, List<string> lstMaPhuCap, string lstIdChungTu)
        {
            return _tlBangLuongKeHoachRepository.ExportQuyLuongCanCu(iNamLamViec, lstMaPhuCap, lstIdChungTu);
        }

        public IEnumerable<TlBangLuongKeHoach> FindAll()
        {
            return _tlBangLuongKeHoachRepository.FindAll();
        }

        public IEnumerable<TlDmCanBoKeHoach> FindCbLuong(int thang, int nam, string maDonVi)
        {
            return _tlBangLuongKeHoachRepository.FindCanBo(thang, nam, maDonVi);
        }

        public IEnumerable<TlDmThueThuNhapCaNhan> FindThue()
        {
            return _tlBangLuongKeHoachRepository.FindThue();
        }

        public DataTable GetDataBangLuong(string maDonVi, int nam)
        {
            return _tlBangLuongKeHoachRepository.GetDataBangLuong(maDonVi, nam);
        }
    }
}
