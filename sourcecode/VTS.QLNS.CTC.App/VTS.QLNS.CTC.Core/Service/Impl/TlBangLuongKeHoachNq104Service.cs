using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlBangLuongKeHoachNq104Service : ITlBangLuongKeHoachNq104Service
    {
        private readonly ITlBangLuongKeHoachNq104Repository _tlBangLuongKeHoachRepository;

        public TlBangLuongKeHoachNq104Service(ITlBangLuongKeHoachNq104Repository tlBangLuongKeHoachRepository)
        {
            _tlBangLuongKeHoachRepository = tlBangLuongKeHoachRepository;
        }

        public int AddRange(IEnumerable<TlBangLuongKeHoachNq104> tlBangLuongKeHoachs)
        {
            return _tlBangLuongKeHoachRepository.AddRange(tlBangLuongKeHoachs);
        }

        public void BulkInsert(IEnumerable<TlBangLuongKeHoachNq104> tlBangLuongKeHoachs)
        {
            _tlBangLuongKeHoachRepository.BulkInsert(tlBangLuongKeHoachs);
        }

        public int DeleteByParentId(Guid id)
        {
            return _tlBangLuongKeHoachRepository.DeleteByParentId(id);
        }

        public IEnumerable<TlBangLuongKeHoachNq104ExportQuery> ExportQuyLuongCanCu(int iNamLamViec, List<string> lstMaPhuCap, string lstIdChungTu)
        {
            return _tlBangLuongKeHoachRepository.ExportQuyLuongCanCu(iNamLamViec, lstMaPhuCap, lstIdChungTu);
        }

        public IEnumerable<TlBangLuongKeHoachNq104> FindAll()
        {
            return _tlBangLuongKeHoachRepository.FindAll();
        }

        public IEnumerable<TlDmCanBoKeHoachNq104> FindCbLuong(int thang, int nam, string maDonVi)
        {
            return _tlBangLuongKeHoachRepository.FindCanBo(thang, nam, maDonVi);
        }

        public IEnumerable<TlDmThueThuNhapCaNhanNq104> FindThue()
        {
            return _tlBangLuongKeHoachRepository.FindThue();
        }

        public DataTable GetDataBangLuong(string maDonVi, int nam)
        {
            return _tlBangLuongKeHoachRepository.GetDataBangLuong(maDonVi, nam);
        }
    }
}
