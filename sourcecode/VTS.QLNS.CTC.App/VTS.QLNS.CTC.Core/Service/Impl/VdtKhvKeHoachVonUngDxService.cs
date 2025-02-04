using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvKeHoachVonUngDxService : IVdtKhvKeHoachVonUngDxService
    {
        private readonly IVdtKhvKeHoachVonUngDxRepository _keHoachVonUngRepository;
        private readonly IVdtKhvKeHoachVonUngDxChiTietRepository _keHoachVonUngChiTietRepository;

        public VdtKhvKeHoachVonUngDxService(IVdtKhvKeHoachVonUngDxRepository keHoachVonUngRepository,
            IVdtKhvKeHoachVonUngDxChiTietRepository keHoachVonUngChiTietRepository)
        {
            _keHoachVonUngRepository = keHoachVonUngRepository;
            _keHoachVonUngChiTietRepository = keHoachVonUngChiTietRepository;
        }

        public IEnumerable<VdtKhvKeHoachVonUngDxQuery> GetKeHoachVonUngIndex()
        {
            return _keHoachVonUngRepository.GetKeHoachVonUngDxIndex();
        }

        public IEnumerable<VdtKhvKeHoachVonUngDx> GetKHVUDeXuatInKHVUDuocDuyet(string iIdMaDonVi, int iNamKeHoach, DateTime dNgayLap)
        {
            return _keHoachVonUngRepository.GetKHVUDeXuatInKHVUDuocDuyet(iIdMaDonVi, iNamKeHoach, dNgayLap);
        }

        public bool Insert(VdtKhvKeHoachVonUngDx dataInsert, string sUserLogin)
        {
            dataInsert.DDateCreate = DateTime.Now;
            dataInsert.SUserCreate = sUserLogin;
            dataInsert.Id = Guid.NewGuid();
            return _keHoachVonUngRepository.Add(dataInsert) != 0;
        }

        public bool Update(VdtKhvKeHoachVonUngDx dataUpdate, string sUserLogin)
        {
            var data = _keHoachVonUngRepository.Find(dataUpdate.Id);
            if (data == null || data.Id == Guid.Empty) return false;
            data.SSoDeNghi = dataUpdate.SSoDeNghi;
            data.DDateUpdate = DateTime.Now;
            data.SUserUpdate = sUserLogin;
            return _keHoachVonUngRepository.Update(data) != 0;
        }

        public bool LogItem(Guid iId, string sUserLogin)
        {
            var data = _keHoachVonUngRepository.Find(iId);
            if (data == null || data.Id == Guid.Empty) return false;
            data.BKhoa = !data.BKhoa;
            data.DDateUpdate = DateTime.Now;
            data.SUserUpdate = sUserLogin;
            return _keHoachVonUngRepository.Update(data) != 0;
        }

        public bool DeleteKeHoachVonUng(VdtKhvKeHoachVonUngDx data)
        {
            var dataDelete = _keHoachVonUngRepository.Find(data.Id);
            if (dataDelete == null || dataDelete.Id == Guid.Empty) return false;
            _keHoachVonUngChiTietRepository.DeleteKeHoachVonUngChiTietByParentId(data.Id);
            var parentId = dataDelete.IIdParentId;
            if(parentId != null)
            {
                var dataParent = _keHoachVonUngRepository.Find(parentId);
                dataParent.BActive = true;
                _keHoachVonUngRepository.Update(dataParent);
            }
            return _keHoachVonUngRepository.Delete(dataDelete) != 0;
        }

        public IEnumerable<VdtKhvKeHoachVonUngDx> FindAll(Expression<Func<VdtKhvKeHoachVonUngDx, bool>> predicate)
        {
            return _keHoachVonUngRepository.FindAll(predicate);
        }

        public IEnumerable<VdtKhvKeHoachVonUngDxChiTietQuery> GetDuAnInKeHoachVonUngDetail(string iIdDonVi, DateTime dNgayLap, string sTongHop)
        {
            return _keHoachVonUngChiTietRepository.GetDuAnInKeHoachVonUngDetail(iIdDonVi, dNgayLap, sTongHop);
        }

        public IEnumerable<VdtKhvKeHoachVonUngDxChiTietQuery> GetKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng)
        {
            return _keHoachVonUngChiTietRepository.GetKeHoachVonUngChiTietByParentId(iIdKeHoachVonUng);
        }

        public bool InsertDetail(Guid parentId, List<VdtKhvKeHoachVonUngDxChiTiet> lstChild)
        {
            _keHoachVonUngChiTietRepository.DeleteKeHoachVonUngChiTietByParentId(parentId);
            if (_keHoachVonUngChiTietRepository.AddRange(lstChild) == 0) return false;
            var parentData = _keHoachVonUngRepository.Find(parentId);
            if (parentData == null || parentData.Id == Guid.Empty) return false;
            parentData.FGiaTriUng = lstChild.Sum(n => n.FGiaTriDeNghi);
            return _keHoachVonUngRepository.Update(parentData) != 0;
        }

        public void InsertKhVonUngDeXuatTongHop(Guid iIdKeHoachTongHop, List<Guid> lstIdChild)
        {
            _keHoachVonUngChiTietRepository.InsertKhVonUngDeXuatTongHop(iIdKeHoachTongHop, lstIdChild);
        }

        public int Adjust(VdtKhvKeHoachVonUngDx entity, List<VdtKhvKeHoachVonUngDxChiTiet> listData)
        {
            using (var transactionScope = new TransactionScope(
                 TransactionScopeOption.Required,
                 new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    _keHoachVonUngRepository.Add(entity);

                    var objUpdate = _keHoachVonUngRepository.Find(entity.IIdParentId);
                    if (objUpdate != null)
                    {
                        objUpdate.BActive = false;
                        _keHoachVonUngRepository.Update(objUpdate);
                    }

                    // Clone chi tiết
                    if (listData != null && listData.Count > 0)
                    {
                        listData = listData.Select(x =>
                        {
                            x.Id = Guid.NewGuid();
                            x.IIdKeHoachUngId = entity.Id;
                            return x;
                        }).ToList();


                        // Thêm chi tiết
                        _keHoachVonUngChiTietRepository.AddRange(listData);
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

        public IEnumerable<VdtKhvKeHoachVonUngDxChiTiet> FindAllCT(Expression<Func<VdtKhvKeHoachVonUngDxChiTiet, bool>> predicate)
        {
            return _keHoachVonUngChiTietRepository.FindAll(predicate);
        }

        public bool CheckTrungSoDeNghi(string sSoDeNghi, Guid id)
        {
            return _keHoachVonUngRepository.CheckTrungSoDeNghi(sSoDeNghi, id);
        }
        
        public IEnumerable<ExportVonUngDonViQuery> GetKeHoachVonUngDonViExport(List<Guid> lstPhanboVonId)
        {
            List<YearPlanManagerExportCriteria> lstData = lstPhanboVonId.Select(n => new YearPlanManagerExportCriteria() { Id = n }).ToList();
            return _keHoachVonUngChiTietRepository.GetKeHoachVonUngDonViExport(lstData);
        }

        public bool CheckExistSoKeHoach(string sSoQuyetDinh, int iNamLamViec, Guid? iId)
        {
            return _keHoachVonUngChiTietRepository.CheckExistSoKeHoach(sSoQuyetDinh, iNamLamViec, iId);
        }
    }
}
