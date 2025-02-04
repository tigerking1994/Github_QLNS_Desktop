using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvPhanBoVonDonViPheDuyetService : IVdtKhvPhanBoVonDonViPheDuyetService
    {
        private readonly IVdtKhvPhanBoVonDonViPheDuyetRepository _vdtKhvPhanBoVonDonViPheDuyetRepository;
        private readonly IVdtKhvPhanBoVonDonViChiTietPheDuyetRepository _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository;

        public VdtKhvPhanBoVonDonViPheDuyetService(IVdtKhvPhanBoVonDonViPheDuyetRepository vdtKhvPhanBoVonDonViPheDuyetRepository,
            IVdtKhvPhanBoVonDonViChiTietPheDuyetRepository vdtKhvPhanBoVonDonViChiTietPheDuyetRepository)
        {
            _vdtKhvPhanBoVonDonViPheDuyetRepository = vdtKhvPhanBoVonDonViPheDuyetRepository;
            _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository = vdtKhvPhanBoVonDonViChiTietPheDuyetRepository;
        }

        public bool Insert(VdtKhvPhanBoVonDonViPheDuyet data, string sUserLogin, ref string sMessError)
        {
            try
            {
                data.DDateCreate = DateTime.Now;
                data.SUserCreate = sUserLogin;
                data.IIdPhanBoGocId = data.Id;
                data.BActive = true;
                data.BIsGoc = true;
                data.BKhoa = false;
                return _vdtKhvPhanBoVonDonViPheDuyetRepository.Add(data) != 0;
            }
            catch (Exception ex)
            {
                sMessError = ex.InnerException.Message;
            }
            return false;
        }

        public IEnumerable<PhanBoVonDonViPheDuyetQuery> GetDataPhanBoVonInIndexView(int iLoaiKeHoachVon)
        {
            return _vdtKhvPhanBoVonDonViPheDuyetRepository.GetDataPhanBoVonInIndexView(iLoaiKeHoachVon);
        }

        public bool UpdatePhanBoVon(VdtKhvPhanBoVonDonViPheDuyet data, string sUserLogin, ref string sMessError, ref MidiumTermPlanCriteria dataDetail)
        {
            try
            {
                var dataUpdate = _vdtKhvPhanBoVonDonViPheDuyetRepository.Find(data.Id);
                dataUpdate.DDateUpdate = DateTime.Now;
                dataUpdate.SUserUpdate = sUserLogin;
                dataUpdate.SSoQuyetDinh = data.SSoQuyetDinh;
                dataUpdate.DNgayQuyetDinh = data.DNgayQuyetDinh;
                return _vdtKhvPhanBoVonDonViPheDuyetRepository.Update(dataUpdate) > 0;
            }
            catch (Exception ex)
            {
                sMessError = ex.InnerException.Message;
            }
            return false;
        }

        public bool DeletePhanBoVon(VdtKhvPhanBoVonDonViPheDuyet data, string sUserLogin, ref string sMessError)
        {
            try
            {
                IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> phanBoVonChiTiets = _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetPhanBoVonChiTietByIidPhanBoVonID(data.Id);
                VdtKhvPhanBoVonDonViPheDuyet dataDelete = _vdtKhvPhanBoVonDonViPheDuyetRepository.Find(data.Id);
                if (phanBoVonChiTiets != null)
                {
                    _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.RemovePhanBoVonChiTiet(phanBoVonChiTiets);
                }
                if (dataDelete.IIdParentId.HasValue)
                {
                    var parentData = _vdtKhvPhanBoVonDonViPheDuyetRepository.Find(dataDelete.IIdParentId);
                    if (parentData != null && parentData.Id != Guid.Empty)
                    {
                        parentData.BActive = true;
                        parentData.DDateDelete = null;
                        parentData.SUserDelete = null;
                        _vdtKhvPhanBoVonDonViPheDuyetRepository.Update(parentData);
                    }
                }
                _vdtKhvPhanBoVonDonViPheDuyetRepository.RemovePhanBoVon(dataDelete);
                return true;
            }
            catch (Exception ex)
            {
                sMessError = ex.Message;
            }
            return false;
        }

        public bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVonDonViPheDuyet objPhanBoVon, int iLoai)
        {
            return _vdtKhvPhanBoVonDonViPheDuyetRepository.ExistPhanBoVonByNamKeHoachAndDonViQuanLy(objPhanBoVon, iLoai);
        }

        //public List<RptAnnualBudgetAllocationQuery> GetDataRptAnnualBudgetAllocation(int iNamKeHoach, DateTime dDenNgay, int iIdnguonVon, string sUserLogin)
        //{
        //    return _vdtKhvPhanBoVonDonViPheDuyetRepository.GetDataRptAnnualBudgetAllocation(iNamKeHoach, dDenNgay, iIdnguonVon, sUserLogin);
        //}

        public bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVonDonViPheDuyet objPhanBoVon, int iLoai)
        {
            return _vdtKhvPhanBoVonDonViPheDuyetRepository.ExistPhanBoVonBySoQuyetDinhAndDonVi(objPhanBoVon, iLoai);
        }

        public IEnumerable<VdtKhvPhanBoVonDonViPheDuyet> FindAll(Expression<Func<VdtKhvPhanBoVonDonViPheDuyet, bool>> predicate)
        {
            return _vdtKhvPhanBoVonDonViPheDuyetRepository.FindAll(predicate);
        }

        //public List<KeHoachVonQuery> GetKeHoachVonCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet)
        //{
        //    return _vdtKhvPhanBoVonDonViPheDuyetRepository.GetKeHoachVonCapPhatThanhToan(duAnId, nguonVonId, dNgayDeNghi, namKeHoach, iCoQuanThanhToan, iIdPheDuyet);
        //}

        //public List<KeHoachVonQuery> GetDeNghiTamUngCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet)
        //{
        //    return _vdtKhvPhanBoVonDonViPheDuyetRepository.GetDeNghiTamUngCapPhatThanhToan(duAnId, nguonVonId, dNgayDeNghi, namKeHoach, iCoQuanThanhToan, iIdPheDuyet);
        //}

        public IEnumerable<VdtKhvPhanBoVonDonViPheDuyet> FindByCondition(Expression<Func<VdtKhvPhanBoVonDonViPheDuyet, bool>> predicate)
        {
            return _vdtKhvPhanBoVonDonViPheDuyetRepository.FindAll(predicate);
        }

        public VdtKhvPhanBoVonDonViPheDuyet FindById(Guid id)
        {
            return _vdtKhvPhanBoVonDonViPheDuyetRepository.Find(id);
        }

        public int Delete(Guid id)
        {
            return _vdtKhvPhanBoVonDonViPheDuyetRepository.Delete(id);
        }

        public int Update(VdtKhvPhanBoVonDonViPheDuyet item)
        {
            return _vdtKhvPhanBoVonDonViPheDuyetRepository.Update(item);
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> GetPhanBoVonByIdPhanBoVon(Guid idPhanBoVon)
        {
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetPhanBoVonChiTietByIidPhanBoVonID(idPhanBoVon);
        }

        public void CreateVoucherImports(VdtKhvPhanBoVonDonViPheDuyet itemNew, List<VdtKhvPhanBoVonDonViChiTietPheDuyet> itemDetailNew)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _vdtKhvPhanBoVonDonViPheDuyetRepository.Add(itemNew);

                var dataUpdate = _vdtKhvPhanBoVonDonViPheDuyetRepository.Find(itemNew.IIdParentId);

                if (dataUpdate != null)
                {
                    dataUpdate.BActive = false;
                    _vdtKhvPhanBoVonDonViPheDuyetRepository.Update(dataUpdate);
                }
                itemDetailNew = itemDetailNew.Select(x => { x.Id = Guid.NewGuid(); return x; }).ToList();
                _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.AddRange(itemDetailNew);

                scope.Complete();
            }
        }

        public int Adjust(VdtKhvPhanBoVonDonViPheDuyet entity)
        {
            using (var transactionScope = new TransactionScope(
                 TransactionScopeOption.Required,
                 new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    _vdtKhvPhanBoVonDonViPheDuyetRepository.Add(entity);

                    VdtKhvPhanBoVonDonViPheDuyet objUpdate = _vdtKhvPhanBoVonDonViPheDuyetRepository.Find(entity.IIdParentId);
                    if (objUpdate != null)
                    {
                        objUpdate.BActive = false;
                        _vdtKhvPhanBoVonDonViPheDuyetRepository.Update(objUpdate);
                    }

                    MidiumTermPlanCriteria dataCreation = new MidiumTermPlanCriteria();
                    dataCreation.VocherIDL = entity.Id.ToString();
                    dataCreation.VocherIDF = entity.IIdParentId.ToString();

                    //_vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.CreateSettlementVoucherApprovedDetail(dataCreation);

                    transactionScope.Complete();

                    return DBContextSaveChangeState.SUCCESS;
                }
                catch (Exception ex)
                {
                    return DBContextSaveChangeState.ERROR;
                }
            }
        }

        //public IEnumerable<ChungTuThanhToanQuery> GetKeHoachVonByThanhToanUngIds(List<Guid> lstid)
        //{
        //    return _vdtKhvPhanBoVonDonViPheDuyetRepository.GetKeHoachVonByThanhToanUngIds(lstid);
        //}

        public IEnumerable<PhanBoVonDonViPheDuyetReportQuery> GetPhanBoVonDonViPheDuyetReport(string lstId, string lstLct, int yearPlan, int type, string lstDonVi, double donViTienTe)
        {
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetPhanBoVonDonViPheDuyetReport(lstId, lstLct, yearPlan, type, lstDonVi, donViTienTe);
        }

        public IEnumerable<long> GetVonBoTri5Nam(string lstId, int yearPlan)
        {
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetVonBoTri5Nam(lstId, yearPlan);
        }

        public IEnumerable<PhanBoVonDonViPheDuyetDieuChinhReportQuery> GetPhanBoVonDonViPheDuyetDieuChinhReport(string lstId, string lstLct, int yearPlan, int type, string lstDonVi, double donViTienTe)
        {
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetPhanBoVonDonViPheDuyetDieuChinhReport(lstId, lstLct, yearPlan, type, lstDonVi, donViTienTe);
        }

        public IEnumerable<VdtKhvVonNamDonViPheDuyetNguonVonQuery> GetPhanBoVonDonViPheDuyetNguonVon(int type, string lstId, string lstLct, string lstNguonVon, string lstDonVi, double donViTienTe)
        {
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetPhanBoVonDonViPheDuyetNguonVon(type, lstId, lstLct, lstNguonVon, lstDonVi, donViTienTe);
        }

        public IEnumerable<VdtKhvVonNamDonViPheDuyetNguonVonDieuChinhQuery> GetPhanBoVonDonViPheDuyetNguonVonDieuChinh(int type, string lstId, string lstLct, string lstNguonVon, string lstDonVi, double donViTienTe)
        {
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetPhanBoVonDonViPheDuyetNguonVonDieuChinh(type, lstId, lstLct, lstNguonVon, lstDonVi, donViTienTe);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            VdtKhvPhanBoVonDonViPheDuyet chungTu = _vdtKhvPhanBoVonDonViPheDuyetRepository.Find(id);
            chungTu.BKhoa = isLock;
            _vdtKhvPhanBoVonDonViPheDuyetRepository.Update(chungTu);
        }
    }
}
