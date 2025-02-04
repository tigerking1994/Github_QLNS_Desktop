using System;
using System.Linq;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Utility;
using System.Transactions;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvPhanBoVonService : IVdtKhvPhanBoVonService
    {
        private readonly IVdtKhvPhanBoVonRepository _vdtKhvPhanBoVonRepository;
        private readonly IVdtKhvPhanBoVonChiTietRepository _vdtKhvPhanBoVonChiTietRepository;
        private readonly IVdtTtDeNghiThanhToanKhvRepository _vdtTtDeNghiThanhToanKhvRepository;

        public VdtKhvPhanBoVonService(IVdtKhvPhanBoVonRepository vdtKhvPhanBoVonRepository,
            IVdtKhvPhanBoVonChiTietRepository vdtKhvPhanBoVonChiTietRepository,
            IVdtTtDeNghiThanhToanKhvRepository vdtTtDeNghiThanhToanKhvRepository)
        {
            _vdtKhvPhanBoVonRepository = vdtKhvPhanBoVonRepository;
            _vdtKhvPhanBoVonChiTietRepository = vdtKhvPhanBoVonChiTietRepository;
            _vdtTtDeNghiThanhToanKhvRepository = vdtTtDeNghiThanhToanKhvRepository;
        }

        public bool Insert(VdtKhvPhanBoVon data, string sUserLogin, ref string sMessError)
        {
            try
            {
                data.DDateCreate = DateTime.Now;
                data.SUserCreate = sUserLogin;
                data.IIdPhanBoGocId = data.Id;
                data.BActive = true;
                data.BIsGoc = true;
                data.BKhoa = false;
                return _vdtKhvPhanBoVonRepository.Add(data) != 0;
            }
            catch (Exception ex)
            {
                sMessError = ex.InnerException.Message;
            }
            return false;
        }

        public IEnumerable<PhanBoVonQuery> GetDataPhanBoVonInIndexView(int iLoaiKeHoachVon)
        {
            return _vdtKhvPhanBoVonRepository.GetDataPhanBoVonInIndexView(iLoaiKeHoachVon);
        }

        public bool UpdatePhanBoVon(VdtKhvPhanBoVon data, string sUserLogin, ref string sMessError, ref MidiumTermPlanCriteria dataDetail)
        {
            try
            {
                var dataUpdate = _vdtKhvPhanBoVonRepository.Find(data.Id);
                dataUpdate.DDateUpdate = DateTime.Now;
                dataUpdate.SUserUpdate = sUserLogin;
                dataUpdate.SSoQuyetDinh = data.SSoQuyetDinh;
                dataUpdate.DNgayQuyetDinh = data.DNgayQuyetDinh;
                return _vdtKhvPhanBoVonRepository.Update(dataUpdate) > 0;
            }
            catch (Exception ex)
            {
                sMessError = ex.InnerException.Message;
            }
            return false;
        }

        public bool DeletePhanBoVon(VdtKhvPhanBoVon data, string sUserLogin, ref string sMessError)
        {
            try
            {
                IEnumerable<VdtKhvPhanBoVonChiTiet> phanBoVonChiTiets = _vdtKhvPhanBoVonChiTietRepository.GetPhanBoVonChiTietByIidPhanBoVonID(data.Id);
                VdtKhvPhanBoVon dataDelete = _vdtKhvPhanBoVonRepository.Find(data.Id);
                if (phanBoVonChiTiets != null)
                {
                    _vdtKhvPhanBoVonChiTietRepository.RemovePhanBoVonChiTiet(phanBoVonChiTiets);
                }
                if (dataDelete.IIdParentId.HasValue)
                {
                    var parentData = _vdtKhvPhanBoVonRepository.Find(dataDelete.IIdParentId);
                    if (parentData != null && parentData.Id != Guid.Empty)
                    {
                        parentData.BActive = true;
                        parentData.DDateDelete = null;
                        parentData.SUserDelete = null;
                        _vdtKhvPhanBoVonRepository.Update(parentData);
                    }
                }
                _vdtKhvPhanBoVonRepository.RemovePhanBoVon(dataDelete);
                return true;
            }
            catch (Exception ex)
            {
                sMessError = ex.Message;
            }
            return false;
        }

        public bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVon objPhanBoVon, int iLoai)
        {
            return _vdtKhvPhanBoVonRepository.ExistPhanBoVonByNamKeHoachAndDonViQuanLy(objPhanBoVon, iLoai);
        }

        public List<RptAnnualBudgetAllocationQuery> GetDataRptAnnualBudgetAllocation(int iNamKeHoach, DateTime dDenNgay, int iIdnguonVon, string sUserLogin)
        {
            return _vdtKhvPhanBoVonRepository.GetDataRptAnnualBudgetAllocation(iNamKeHoach, dDenNgay, iIdnguonVon, sUserLogin);
        }

        public bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVon objPhanBoVon, int iLoai)
        {
            return _vdtKhvPhanBoVonRepository.ExistPhanBoVonBySoQuyetDinhAndDonVi(objPhanBoVon, iLoai);
        }

        public IEnumerable<VdtKhvPhanBoVon> FindAll(Expression<Func<VdtKhvPhanBoVon, bool>> predicate)
        {
            return _vdtKhvPhanBoVonRepository.FindAll(predicate);
        }

        public List<KeHoachVonQuery> GetKeHoachVonCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet)
        {
            return _vdtKhvPhanBoVonRepository.GetKeHoachVonCapPhatThanhToan(duAnId, nguonVonId, dNgayDeNghi, namKeHoach, iCoQuanThanhToan, iIdPheDuyet);
        }

        public List<KeHoachVonQuery> GetDeNghiTamUngCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet)
        {
            return _vdtKhvPhanBoVonRepository.GetDeNghiTamUngCapPhatThanhToan(duAnId, nguonVonId, dNgayDeNghi, namKeHoach, iCoQuanThanhToan, iIdPheDuyet);
        }

        public IEnumerable<VdtKhvPhanBoVon> FindByCondition(Expression<Func<VdtKhvPhanBoVon, bool>> predicate)
        {
            return _vdtKhvPhanBoVonRepository.FindAll(predicate);
        }

        public VdtKhvPhanBoVon FindById(Guid id)
        {
            return _vdtKhvPhanBoVonRepository.Find(id);
        }

        public int Delete(Guid id)
        {
            return _vdtKhvPhanBoVonRepository.Delete(id);
        }

        public int Update(VdtKhvPhanBoVon item)
        {
            return _vdtKhvPhanBoVonRepository.Update(item);
        }

        public IEnumerable<VdtKhvPhanBoVonChiTiet> GetPhanBoVonByIdPhanBoVon(Guid idPhanBoVon)
        {
            return _vdtKhvPhanBoVonChiTietRepository.GetPhanBoVonChiTietByIidPhanBoVonID(idPhanBoVon);
        }

        public void CreateVoucherImports(VdtKhvPhanBoVon itemNew, List<VdtKhvPhanBoVonChiTiet> itemDetailNew)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _vdtKhvPhanBoVonRepository.Add(itemNew);

                var dataUpdate = _vdtKhvPhanBoVonRepository.Find(itemNew.IIdParentId);

                if (dataUpdate != null)
                {
                    dataUpdate.BActive = false;
                    _vdtKhvPhanBoVonRepository.Update(dataUpdate);
                }
                itemDetailNew = itemDetailNew.Select(x => { x.Id = Guid.NewGuid(); return x; }).ToList();
                _vdtKhvPhanBoVonChiTietRepository.AddRange(itemDetailNew);

                scope.Complete();
            }
        }

        public int Adjust(VdtKhvPhanBoVon entity)
        {
            using (var transactionScope = new TransactionScope(
                 TransactionScopeOption.Required,
                 new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    _vdtKhvPhanBoVonRepository.Add(entity);

                    VdtKhvPhanBoVon objUpdate = _vdtKhvPhanBoVonRepository.Find(entity.IIdParentId);
                    if (objUpdate != null)
                    {
                        objUpdate.BActive = false;
                        _vdtKhvPhanBoVonRepository.Update(objUpdate);
                    }

                    MidiumTermPlanCriteria dataCreation = new MidiumTermPlanCriteria();
                    dataCreation.VocherIDL = entity.Id.ToString();
                    dataCreation.VocherIDF = entity.IIdParentId.ToString();

                    _vdtKhvPhanBoVonChiTietRepository.CreateSettlementVoucherApprovedDetail(dataCreation);

                    transactionScope.Complete();

                    return DBContextSaveChangeState.SUCCESS;
                }
                catch (Exception ex)
                {
                    return DBContextSaveChangeState.ERROR;
                }
            }
        }

        public IEnumerable<ChungTuThanhToanQuery> GetKeHoachVonByThanhToanUngIds(List<Guid> lstid)
        {
            return _vdtKhvPhanBoVonRepository.GetKeHoachVonByThanhToanUngIds(lstid);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            VdtKhvPhanBoVon chungTu = _vdtKhvPhanBoVonRepository.Find(id);
            chungTu.BKhoa = isLock;
            _vdtKhvPhanBoVonRepository.Update(chungTu);
        }

        public void AddRange(List<VdtTtDeNghiThanhToanKhv> lstDataKHV)
        {
            _vdtTtDeNghiThanhToanKhvRepository.AddRange(lstDataKHV);
        }

        public IEnumerable<BcDuToanTheoLoaiCongTrinhQuery> GetBcDuToanTheoLoaiCongTrinh(int iLoaiChungTu, int iNamKeHoach, double fDonViTinh, List<string> lstDonViId)
        {
            List<BcDuToanTheoLoaiCongTrinhQuery> results = new List<BcDuToanTheoLoaiCongTrinhQuery>();
            var data = _vdtKhvPhanBoVonRepository.GetBcDuToanTheoLoaiCongTrinh(iLoaiChungTu, iNamKeHoach, fDonViTinh, lstDonViId);
            if (data != null)
            {
                foreach(var item in data.Where(n => !n.IIDParent.HasValue).OrderBy(n => n.IThuTu))
                {
                    results.AddRange(ReciveGetBcDuToanTheoLoaiCongTrinh(item, data));
                }
            }
            return results;
        }

        #region Helper
        private List<BcDuToanTheoLoaiCongTrinhQuery> ReciveGetBcDuToanTheoLoaiCongTrinh(BcDuToanTheoLoaiCongTrinhQuery current, IEnumerable<BcDuToanTheoLoaiCongTrinhQuery> lstData)
        {
            List<BcDuToanTheoLoaiCongTrinhQuery> results = new List<BcDuToanTheoLoaiCongTrinhQuery>();
            List<BcDuToanTheoLoaiCongTrinhQuery> lstChild = new List<BcDuToanTheoLoaiCongTrinhQuery>();

            foreach (var child in lstData.Where(n => n.IIDParent == current.IIDLoaiCongTrinh).OrderBy(n => n.IThuTu))
            {
                lstChild = ReciveGetBcDuToanTheoLoaiCongTrinh(child, lstData);
                current.FCapPhatBangLenhChi += child.FCapPhatBangLenhChi;
                current.FCapPhatTaiKhoBac += child.FCapPhatTaiKhoBac;
                current.BIsHangCha = true;
            }
            if (current.FSum == 0) return results;
            results.Add(current);
            if (lstChild != null) results.AddRange(lstChild);
            return results;
        }
        #endregion
    }
}
