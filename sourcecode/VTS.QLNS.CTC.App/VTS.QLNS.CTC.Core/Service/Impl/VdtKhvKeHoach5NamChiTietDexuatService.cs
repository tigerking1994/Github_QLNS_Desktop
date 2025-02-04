using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvKeHoach5NamChiTietDeXuatService : IVdtKhvKeHoach5NamDeXuatChiTietService
    {
        private readonly IVdtKhvKeHoach5NamDeXuatChiTietRepository _vdtKhvKeHoach5NamChiTietDexuatRepository;

        public VdtKhvKeHoach5NamChiTietDeXuatService(IVdtKhvKeHoach5NamDeXuatChiTietRepository vdtKhvKeHoach5NamChiTietDexuatRepository)
        {
            _vdtKhvKeHoach5NamChiTietDexuatRepository = vdtKhvKeHoach5NamChiTietDexuatRepository;
        }

        public int AddRange(IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> entities)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.AddRange(entities);
        }

        public void Delete(Guid id)
        {
            VdtKhvKeHoach5NamDeXuatChiTiet entity = _vdtKhvKeHoach5NamChiTietDexuatRepository.Find(id);
            if (entity != null)
            {
                _vdtKhvKeHoach5NamChiTietDexuatRepository.Delete(entity);
            }
        }

        //public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindAll()
        //{
        //   return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindAll();
        //}

        public VdtKhvKeHoach5NamDeXuatChiTiet FindById(Guid id)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.Find(id);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByLevel(int level, Guid id, Guid? idParent)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindByLevel(level, id, idParent);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindConditionIndex(string voucherId)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindConditionIndex(voucherId);
        }

        public int FindNextSoChungTuIndex(Guid id)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindNextSoChungTuIndex(id);
        }

        public int Update(VdtKhvKeHoach5NamDeXuatChiTiet entity)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.Update(entity);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatExportQuery> GetDataExportKeHoachTrungHanDeXuat(Guid iID)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.GetDataExportKeHoachTrungHanDeXuat(iID);
        }

        //public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindBySMaOrder(string sMaOrder)
        //{
        //    return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindBySMaOrder(sMaOrder);
        //}

        //public int FindByMaxStt(int level, Guid id)
        //{
        //    return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindByMaxStt(level, id);
        //}

        public IEnumerable<DuAnHangMucQuery> FindListDuAnHangMuc(string lstId, string lstDuAnId)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindListDuAnHangMuc(lstId, lstDuAnId);
        }

        public IEnumerable<DuAnNguonVonQuery> FindListNguonVon(string lstId, string listIdDuAn)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindListNguonVon(lstId, listIdDuAn);
        }

        public IEnumerable<DuAnQuery> FindListDuAn(string lstId)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindListDuAn(lstId);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatReportQuery> FindByReportKeHoachTrungHanDeXuat(string id, string lct, string lstNguonVon, int type, double donViTinh, int iNamLamViec)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindByReportKeHoachTrungHanDeXuat(id, lct, lstNguonVon, type, donViTinh, iNamLamViec);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByIdKeHoach5Nam(Guid id)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindByIdKeHoach5Nam(id);
        }

        //public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindConditionModifiedIndex(string voucherId)
        //{
        //    return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindConditionModifiedIndex(voucherId);
        //}

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindListVoucherDetailsModified(Guid idKhth)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindListVoucherDetailsModified(idKhth);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindListKH5NamDeXuatDieuChinhChiTiet(Guid idKhth)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindListKH5NamDeXuatDieuChinhChiTiet(idKhth);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> FindSuggestionReport(int type, string lstId, string lstDonVi, double donviTinh, string lstNgVon)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindSuggestionReport(type, lstId, lstDonVi, donviTinh, lstNgVon);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByListId(List<Guid> lstId)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindByListId(lstId);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindChiTietDuAnChuyenTiep(Guid id)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindChiTietDuAnChuyenTiep(id);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatReportQuery> FindByReportKeHoachTrungHanDeXuatChuyenTiep(string lstId, string lstBudget, string lstLoaiCongTrinh, string lstUnit, int type, double donViTinh)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindByReportKeHoachTrungHanDeXuatChuyenTiep(lstId, lstBudget, lstLoaiCongTrinh, lstUnit, type, donViTinh);
        }

        public IEnumerable<DuAnTrungHanDeXuatQuery> FindAllDuAnChuyenTiep(string idDonVi)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindAllDuAnChuyenTiep(idDonVi);
        }

        public IEnumerable<DuAnTrungHanDeXuatQuery> FindAllDuAnChuyenTiepDieuChinh(string iIdDonVi)
        {
            return _vdtKhvKeHoach5NamChiTietDexuatRepository.FindAllDuAnChuyenTiepDieuChinh(iIdDonVi);
        }
    }
}
