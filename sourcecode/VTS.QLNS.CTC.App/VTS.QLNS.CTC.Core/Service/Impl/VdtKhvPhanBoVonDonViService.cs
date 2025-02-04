using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvPhanBoVonDonViService : IVdtKhvPhanBoVonDonViService
    {
        private readonly IVdtKhvPhanBoVonDonViRepository _vdtPhanBoVonRepository;
        private readonly IVdtKhvPhanBoVonDonViChiTietRepository _vdtPhanBoVonChiTietRepository;

        public VdtKhvPhanBoVonDonViService(IVdtKhvPhanBoVonDonViRepository vdtPhanBoVonRepository,
            IVdtKhvPhanBoVonDonViChiTietRepository vdtPhanBoVonChiTietRepository)
        {
            _vdtPhanBoVonRepository = vdtPhanBoVonRepository;
            _vdtPhanBoVonChiTietRepository = vdtPhanBoVonChiTietRepository;
        }

        public IEnumerable<VdtKhvPhanBoVonDonViQuery> GetDataPhanBoVonDonViIndexView()
        {
            return _vdtPhanBoVonRepository.GetDataPhanBoVonDonViIndexView();
        }

        public bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVonDonVi objPhanBoVon)
        {
            return _vdtPhanBoVonRepository.ExistPhanBoVonBySoQuyetDinhAndDonVi(objPhanBoVon);
        }

        public bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVonDonVi objPhanBoVon)
        {
            return _vdtPhanBoVonRepository.ExistPhanBoVonByNamKeHoachAndDonViQuanLy(objPhanBoVon);
        }

        public bool Insert(VdtKhvPhanBoVonDonVi data, string sUserLogin, ref string sMessError)
        {
            try
            {
                data.DDateCreate = DateTime.Now;
                data.SUserCreate = sUserLogin;
                data.BActive = true;
                data.BIsGoc = true;
                return _vdtPhanBoVonRepository.Add(data) != 0;
            }
            catch (Exception ex)
            {
                sMessError = ex.InnerException.Message;
            }
            return false;
        }

        public bool UpdatePhanBoVon(VdtKhvPhanBoVonDonVi data, string sUserLogin, ref string sMessError)
        {
            try
            {
                var dataUpdate = _vdtPhanBoVonRepository.Find(data.Id);
                dataUpdate.DDateUpdate = DateTime.Now;
                dataUpdate.SUserUpdate = sUserLogin;
                dataUpdate.SSoQuyetDinh = data.SSoQuyetDinh;
                dataUpdate.DNgayQuyetDinh = data.DNgayQuyetDinh;
                dataUpdate.SNguoiLap = data.SNguoiLap;
                dataUpdate.STruongPhong = data.STruongPhong;
                return _vdtPhanBoVonRepository.Update(dataUpdate) > 0;
            }
            catch (Exception ex)
            {
                sMessError = ex.InnerException.Message;
            }
            return false;
        }

        public void DeletePhanBoVonDonVi(VdtKhvPhanBoVonDonVi data)
        {
            VdtKhvPhanBoVonDonVi dataDelete = _vdtPhanBoVonRepository.Find(data.Id);

            var lstChildData = _vdtPhanBoVonChiTietRepository.GetPhanBoVonDonViChiTietByIidPhanBoVonID(data.Id);
            if (lstChildData != null && lstChildData.Any())
            {
                _vdtPhanBoVonChiTietRepository.RemovePhanBoVonChiTiet(lstChildData);
            }
            if (dataDelete != null && dataDelete.IIdParentId.HasValue)
            {
                var parentData = _vdtPhanBoVonRepository.Find(dataDelete.IIdParentId);
                if (parentData != null && parentData.Id != Guid.Empty)
                {
                    parentData.BActive = true;
                    _vdtPhanBoVonRepository.Update(parentData);
                }
            }

            _vdtPhanBoVonRepository.Delete(dataDelete);

            if (data != null && data.IIdParentId.HasValue)
            {
                VdtKhvPhanBoVonDonVi dataUpdate = _vdtPhanBoVonRepository.Find(data.IIdParentId.Value);

                if (dataUpdate != null)
                {
                    dataUpdate.BActive = true;
                    _vdtPhanBoVonRepository.Update(dataUpdate);
                }
            }
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTietQuery> GetAllDuAnInPhanBoVon(int iNamKeHoach, DateTime dNgayLap, string iIdMaDonViQuanLyId, int iNguonVonId, int? filterHasQDDT)
        {
            return _vdtPhanBoVonChiTietRepository.GetAllDuAnInPhanBoVon(iNamKeHoach, dNgayLap, iIdMaDonViQuanLyId, iNguonVonId, filterHasQDDT);
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTietQuery> GetPhanBoVonChiTietByParentId(Guid iIdParent)
        {
            return _vdtPhanBoVonChiTietRepository.GetPhanBoVonChiTietByParentId(iIdParent);
        }

        public IEnumerable<ExportVonNamDonViQuery> GetKeHoachVonNamDonViExport(List<Guid> lstPhanboVonId)
        {
            List<YearPlanManagerExportCriteria> lstData = lstPhanboVonId.Select(n => new YearPlanManagerExportCriteria() { Id = n }).ToList();
            return _vdtPhanBoVonChiTietRepository.GetKeHoachVonNamDonViExport(lstData);
        }

        public IEnumerable<VdtKhvVonNamDonViReportQuery> GetReportKeHoachVonNamDonVi(int type, string theLoaiCongTrinh, string lstId, string lstLct, double donViTinh)
        {
            return _vdtPhanBoVonChiTietRepository.GetReportKeHoachVonNamDonVi(type, theLoaiCongTrinh, lstId, lstLct, donViTinh);
        }

        public bool CreatePhanBoVonChiTiet(Guid iIdParentId, List<VdtKhvPhanBoVonDonViChiTiet> lstData)
        {
            var objParent = _vdtPhanBoVonRepository.Find(iIdParentId);
            if (objParent != null && objParent.Id != Guid.Empty)
            {
                objParent.FThanhToan = lstData.Sum(n => n.FThanhToanDC ?? 0);
                objParent.FThuHoiVonUngTruoc = lstData.Sum(n => n.FThuHoiVonUngTruocDC ?? 0);
                _vdtPhanBoVonRepository.Update(objParent);
            }
            var lstChildData = _vdtPhanBoVonChiTietRepository.GetPhanBoVonDonViChiTietByIidPhanBoVonID(iIdParentId);
            List<VdtKhvPhanBoVonDonViChiTiet> lstAdd = new List<VdtKhvPhanBoVonDonViChiTiet>();
            List<VdtKhvPhanBoVonDonViChiTiet> lstEdit = new List<VdtKhvPhanBoVonDonViChiTiet>();
            List<VdtKhvPhanBoVonDonViChiTiet> lstDelete = new List<VdtKhvPhanBoVonDonViChiTiet>();

            foreach (var item in lstData)
            {
                if (lstChildData.Any(n => n.IIdDuAnId == item.IIdDuAnId))
                {
                    var itemEdit = lstChildData.FirstOrDefault(n => n.IIdDuAnId == item.IIdDuAnId && n.IIdLoaiCongTrinhId == item.IIdLoaiCongTrinhId);
                    itemEdit.FKeHoachVonDuocDuyetNamNay = item.FKeHoachVonDuocDuyetNamNay;
                    itemEdit.FLuyKeVonNamTruoc = item.FLuyKeVonNamTruoc;
                    itemEdit.FThanhToan = item.FThanhToan;
                    itemEdit.FThuHoiVonUngTruoc = item.FThuHoiVonUngTruoc;
                    itemEdit.FThanhToanDC = item.FThanhToanDC;
                    itemEdit.FThuHoiVonUngTruocDC = item.FThuHoiVonUngTruocDC;
                    itemEdit.FUocThucHien = item.FUocThucHien;
                    itemEdit.FUocThucHienDC = item.FUocThucHienDC;
                    itemEdit.FVonKeoDaiCacNamTruoc = item.FVonKeoDaiCacNamTruoc;
                    lstEdit.Add(itemEdit);
                }
                else
                {
                    lstAdd.Add(item);
                }
            }

            if (lstEdit != null && lstEdit.Any())
            {
                _vdtPhanBoVonChiTietRepository.UpdateRange(lstEdit);
            }

            var lstDuAnExist = lstData.Select(n => n.IIdDuAnId);
            if (lstDuAnExist != null)
            {
                _vdtPhanBoVonChiTietRepository.RemovePhanBoVonChiTiet(lstChildData.Where(n => !lstDuAnExist.Contains(n.IIdDuAnId)));
            }

            return _vdtPhanBoVonChiTietRepository.AddRange(lstAdd.Select(n => { n.IIdPhanBoVonDonVi = iIdParentId; return n; })) != 0;
        }

        public VdtKhvPhanBoVonDonVi FindById(Guid id)
        {
            return _vdtPhanBoVonRepository.Find(id);
        }

        public int Update(VdtKhvPhanBoVonDonVi item)
        {
            return _vdtPhanBoVonRepository.Update(item);
        }

        public VdtKhvPhanBoVonDonVi Add(VdtKhvPhanBoVonDonVi entity)
        {
            _vdtPhanBoVonRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            return _vdtPhanBoVonRepository.Delete(id);
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTietDieuChinhQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid iIdParent)
        {
            return _vdtPhanBoVonChiTietRepository.GetPhanBoVonChiTietDieuChinhByParentId(iIdParent);
        }

        public VdtKhvPhanBoVonDonViChiTiet GetPhanBoVonChiTietById(Guid id)
        {
            return _vdtPhanBoVonChiTietRepository.Find(id);
        }
        public IEnumerable<VdtKhvPhanBoVonDonVi> FindByCondition(Expression<Func<VdtKhvPhanBoVonDonVi, bool>> predicate)
        {
            return _vdtPhanBoVonRepository.FindAll(predicate);
        }
        public IEnumerable<PhanBoVonDonViDieuChinhReportQuery> GetPhanBoVonDieuChinhReport(string lstId, string lstLct, int yearPlan, int type, double donViTienTe)
        {
            return _vdtPhanBoVonChiTietRepository.GetPhanBoVonDieuChinhReport(lstId, lstLct, yearPlan, type, donViTienTe);
        }

        public IEnumerable<PhanBoVonDonViGocReportQuery> GetPhanBoVonDonViGocReport(string lstId, string lstLct, int yearPlan, int type, double donViTienTe)
        {
            return _vdtPhanBoVonChiTietRepository.GetPhanBoVonDonViGocReport(lstId, lstLct, yearPlan, type, donViTienTe);
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTiet> GetPhanBoVonChiTietByIdDuAn(Guid idDuAn)
        {
            return _vdtPhanBoVonChiTietRepository.GetPhanBoVonChiTietByIdDuAn(idDuAn);
        }

        public IEnumerable<VdtKhvPhanBoVonDonVi> GetPhanBoVonByListId(List<Guid> lstId, int yearPlan)
        {
            return _vdtPhanBoVonRepository.GetPhanBoVonByListId(lstId, yearPlan);
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTiet> GetPhanBoVonDonViByIdPhanBoVon(Guid idPhanBoVon)
        {
            return _vdtPhanBoVonChiTietRepository.GetPhanBoVonDonViByIdPhanBoVon(idPhanBoVon);
        }

        public void CreateVoucherImports(VdtKhvPhanBoVonDonVi itemNew, List<VdtKhvPhanBoVonDonViChiTiet> itemDetailNew)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _vdtPhanBoVonRepository.Add(itemNew);

                var dataUpdate = _vdtPhanBoVonRepository.Find(itemNew.IIdParentId);

                if (dataUpdate != null)
                {
                    dataUpdate.BActive = false;
                    _vdtPhanBoVonRepository.Update(dataUpdate);
                }
                itemDetailNew = itemDetailNew.Select(x => { x.Id = Guid.NewGuid(); return x; }).ToList();
                _vdtPhanBoVonChiTietRepository.AddRange(itemDetailNew);

                scope.Complete();
            }
        }

        public int Adjust(VdtKhvPhanBoVonDonVi entity, List<VdtKhvPhanBoVonDonViChiTiet> detail)
        {
            using (var transactionScope = new TransactionScope(
                 TransactionScopeOption.Required,
                 new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    _vdtPhanBoVonRepository.Add(entity);

                    VdtKhvPhanBoVonDonVi objUpdate = _vdtPhanBoVonRepository.Find(entity.IIdParentId);
                    if (objUpdate != null)
                    {
                        objUpdate.BActive = false;
                        _vdtPhanBoVonRepository.Update(objUpdate);
                    }

                    detail.Select(x => { x.IIdParentId = x.Id; x.IIdPhanBoVonDonVi = entity.Id; x.Id = Guid.NewGuid(); return x; }).ToList();

                    _vdtPhanBoVonChiTietRepository.AddRange(detail);

                    transactionScope.Complete();

                    return DBContextSaveChangeState.SUCCESS;
                }
                catch (Exception ex)
                {
                    return DBContextSaveChangeState.ERROR;
                }
            }
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            VdtKhvPhanBoVonDonVi chungTu = _vdtPhanBoVonRepository.Find(id);
            chungTu.BKhoa = isLock;
            _vdtPhanBoVonRepository.Update(chungTu);
        }

        public VdtKhvPhanBoVonDonVi FindAggregateVoucher(string sTongHop)
        {
            return _vdtPhanBoVonRepository.FindAggregateVoucher(sTongHop);
        }

        public int Agregate(VdtKhvPhanBoVonDonVi entity, List<VdtKhvPhanBoVonDonViChiTiet> details)
        {
            using (var transactionScope = new TransactionScope(
               TransactionScopeOption.Required,
               new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    // Thêm kế hoạch tổng hợp
                    _vdtPhanBoVonRepository.Add(entity);

                    // Add chi tiết
                    if (details != null && details.Count > 0)
                    {
                        // Thêm chi tiết
                        _vdtPhanBoVonChiTietRepository.AddRange(details);
                    }

                    transactionScope.Complete();
                    return DBContextSaveChangeState.SUCCESS;
                }
                catch (Exception ex)
                {
                    return DBContextSaveChangeState.ERROR;
                }
            }
        }

        public IEnumerable<VdtKhvVonNamDeXuatDieuChinhNguonVonQuery> GetPhanBoVonDieuChinhNguonVon(int type, string lstId, string lstLct, string lstNguonVon, double donViTienTe)
        {
            return _vdtPhanBoVonChiTietRepository.GetPhanBoVonDieuChinhNguonVon(type, lstId, lstLct, lstNguonVon, donViTienTe);
        }

        public IEnumerable<VdtKhvVonNamDeXuatGocNguonVonQuery> GetPhanBoVonDonViGocNguonVon(int type, string lstId, string lstLct, string lstNguonVon, double donViTienTe)
        {
            return _vdtPhanBoVonChiTietRepository.GetPhanBoVonDonViGocNguonVon(type, lstId, lstLct, lstNguonVon, donViTienTe);
        }

        public IEnumerable<PhanBoVonDonViQuery> GetPhanBoVonDonViDieuChinh(string idPhanBoVonDv)
        {
            return _vdtPhanBoVonChiTietRepository.GetPhanBoVonDonViDieuChinh(idPhanBoVonDv);
        }

        public IEnumerable<KeHoachVonDauTuTrungHan5NamQuery> GetVonBoTri5Nam(string lstId, int yearPlan)
        {
            return _vdtPhanBoVonChiTietRepository.GetVonBoTri5Nam(lstId, yearPlan);
        }
    }
}
