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

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvKeHoachVonUngService : IVdtKhvKeHoachVonUngService
    {
        private readonly IVdtKhvKeHoachVonUngRepository _keHoachVonUngRepository;
        private readonly IVdtKhvKeHoachVonUngChiTietRepository _keHoachVonUngChiTietRepository;

        public VdtKhvKeHoachVonUngService(IVdtKhvKeHoachVonUngRepository keHoachVonUngRepository,
            IVdtKhvKeHoachVonUngChiTietRepository keHoachVonUngChiTietRepository)
        {
            _keHoachVonUngRepository = keHoachVonUngRepository;
            _keHoachVonUngChiTietRepository = keHoachVonUngChiTietRepository;
        }

        public IEnumerable<VdtKhvKeHoachVonUngQuery> GetKeHoachVonUngIndex()
        {
            return _keHoachVonUngRepository.GetKeHoachVonUngIndex();
        }

        public bool Insert(VdtKhvKeHoachVonUng dataInsert, string sUserLogin)
        {
            dataInsert.DDateCreate = DateTime.Now;
            dataInsert.SUserCreate = sUserLogin;
            return _keHoachVonUngRepository.Add(dataInsert) != 0;
        }
        public int Adjust(VdtKhvKeHoachVonUng entity, List<VdtKhvKeHoachVonUngChiTiet> listData)
        {
            using (var transactionScope = new TransactionScope(
                 TransactionScopeOption.Required,
                 new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    _keHoachVonUngRepository.Add(entity);
                    VdtKhvKeHoachVonUng objUpdate = _keHoachVonUngRepository.Find(entity.IIdParentId);
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
        public bool Update(VdtKhvKeHoachVonUng dataUpdate, string sUserLogin)
        {
            var data = _keHoachVonUngRepository.Find(dataUpdate.Id);
            if (data == null || data.Id == Guid.Empty) return false;
            data.SSoQuyetDinh = dataUpdate.SSoQuyetDinh;
            data.DDateUpdate = DateTime.Now;
            data.SUserUpdate = sUserLogin;
            return _keHoachVonUngRepository.Update(data) != 0;
        }

        public bool DeleteKeHoachVonUng(VdtKhvKeHoachVonUng data)
        {
            var dataDelete = _keHoachVonUngRepository.Find(data.Id);
            if (dataDelete == null || dataDelete.Id == Guid.Empty) return false;
            _keHoachVonUngChiTietRepository.DeleteKeHoachVonUngChiTietByParentId(data.Id);

            var parentId = dataDelete.IIdParentId;
            if (parentId != null)
            {
                var dataParent = _keHoachVonUngRepository.Find(parentId);
                dataParent.BActive = true;
                _keHoachVonUngRepository.Update(dataParent);
            }
            return _keHoachVonUngRepository.Delete(dataDelete) != 0;
        }

        public IEnumerable<VdtKhvKeHoachVonUng> FindAll(Expression<Func<VdtKhvKeHoachVonUng, bool>> predicate)
        {
            return _keHoachVonUngRepository.FindAll(predicate);
        }

        public IEnumerable<ExportVonUngDonViQuery> GetKeHoachVonUngDonViExport(List<Guid> lstPhanboVonId)
        {
            List<YearPlanManagerExportCriteria> lstData = lstPhanboVonId.Select(n => new YearPlanManagerExportCriteria() { Id = n }).ToList();
            return _keHoachVonUngRepository.GetKeHoachVonUngDonViExport(lstData);
        }

        public bool CheckTrungSoQuyetDinh(string sSoQuyetDinh, Guid id)
        {
            return _keHoachVonUngRepository.CheckTrungSoQuyetDinh(sSoQuyetDinh, id);
        }

        public VdtKhvKeHoachVonUng FindById(Guid id)
        {
            return _keHoachVonUngRepository.Find(id);
        }
    }
}
