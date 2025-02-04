using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvKeHoach5NamChiTietService : IVdtKhvKeHoach5NamChiTietService
    {
        private readonly IVdtKhvKeHoach5NamChiTietRepository _vdtKhvKeHoach5NamChiTietRepository;
        private readonly IVdtKhvKeHoach5NamRepository _vdtKhvKeHoach5NamRepository;

        public VdtKhvKeHoach5NamChiTietService(IVdtKhvKeHoach5NamChiTietRepository vdtKhvKeHoach5NamChiTietRepository, IVdtKhvKeHoach5NamRepository vdtKhvKeHoach5NamRepository)
        {
            _vdtKhvKeHoach5NamChiTietRepository = vdtKhvKeHoach5NamChiTietRepository;
            _vdtKhvKeHoach5NamRepository = vdtKhvKeHoach5NamRepository;
        }

        public int AddRange(IEnumerable<VdtKhvKeHoach5NamChiTiet> entities)
        {
            return _vdtKhvKeHoach5NamChiTietRepository.AddRange(entities);
        }

        public void CreateSettlementVoucherDetail(MidiumTermPlanCriteria creation)
        {
            _vdtKhvKeHoach5NamChiTietRepository.CreateSettlementVoucherDetail(creation);
        }

        public void Delete(Guid id)
        {
            VdtKhvKeHoach5NamChiTiet entity = _vdtKhvKeHoach5NamChiTietRepository.Find(id);
            if (entity != null)
            {
                _vdtKhvKeHoach5NamChiTietRepository.Delete(entity);
            }
        }

        public VdtKhvKeHoach5NamChiTiet FindById(Guid id)
        {
            return _vdtKhvKeHoach5NamChiTietRepository.Find(id);
        }

        public IEnumerable<VdtKhvKeHoach5NamChiTiet> FindByIdKeHoach5Nam(Guid id)
        {
            return _vdtKhvKeHoach5NamChiTietRepository.FindByIdKeHoach5Nam(id);
        }

        public int Update(VdtKhvKeHoach5NamChiTiet entity)
        {
            return _vdtKhvKeHoach5NamChiTietRepository.Update(entity);
        }

        public IEnumerable<VdtKhvKeHoach5NamReportQuery> FindByReportKeHoachTrungHan(string id, string lct, int idNguonVon, int type, double donViTinh, string lstDonViThucHienDuAn)
        {
            return _vdtKhvKeHoach5NamChiTietRepository.FindByReportKeHoachTrungHan(id, lct, idNguonVon, type, donViTinh, lstDonViThucHienDuAn);
        }

        public IEnumerable<VdtKhvKeHoach5NamChiTietQuery> FindByKeHoach5NamChiTiet(string id)
        {
            return _vdtKhvKeHoach5NamChiTietRepository.FindByKeHoach5NamChiTiet(id);
        }

        public IEnumerable<VdtKhvKeHoach5NamChiTietQuery> FindChiTietDuAnChuyenTiep(Guid id)
        {
            return _vdtKhvKeHoach5NamChiTietRepository.FindChiTietDuAnChuyenTiep(id);
        }

        public IEnumerable<VdtKhvKeHoach5NamChuyenTiepReportQuery> FindByReportKeHoachTrungHanChuyenTiep(string id, string lstBudget, string lstUnit, int type, double donViTinh)
        {
            return _vdtKhvKeHoach5NamChiTietRepository.FindByReportKeHoachTrungHanChuyenTiep(id, lstBudget, lstUnit, type, donViTinh);
        }

        public IEnumerable<VdtKhvKeHoach5NamExportQuery> GetDataExportKeHoachTrungHan(string id)
        {
            return _vdtKhvKeHoach5NamChiTietRepository.GetDataExportKeHoachTrungHan(id);
        }
    }
}
