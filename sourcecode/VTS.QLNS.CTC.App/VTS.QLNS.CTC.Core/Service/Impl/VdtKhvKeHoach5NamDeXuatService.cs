using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvKeHoach5NamDeXuatService : IVdtKhvKeHoach5NamDeXuatService
    {
        private readonly IVdtKhvKeHoach5NamDeXuatRepository _repository;
        private readonly IVdtKhvKeHoach5NamDeXuatChiTietRepository _chitietRepository;
        private readonly IVdtDaDuAnRepository _duAnRepository;

        public VdtKhvKeHoach5NamDeXuatService(IVdtKhvKeHoach5NamDeXuatRepository repository,
            IVdtKhvKeHoach5NamDeXuatChiTietRepository chitietRepository,
            IVdtDaDuAnRepository duAnRepository)
        {
            _repository = repository;
            _chitietRepository = chitietRepository;
            _duAnRepository = duAnRepository;
        }

        public VdtKhvKeHoach5NamDeXuat Add(VdtKhvKeHoach5NamDeXuat entity)
        {
            _repository.Add(entity);
            return entity;
        }

        public int Adjust(VdtKhvKeHoach5NamDeXuat entity, List<VdtKhvKeHoach5NamDeXuatChiTiet> details)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    // Thêm kế hoạch trung hạn điều chỉnh, clone từ kế hoạch trung hạn gốc
                    _repository.Add(entity);

                    // Update BActive = false của kế hoạch trung hạn gốc
                    VdtKhvKeHoach5NamDeXuat parentEntity = _repository.Find(entity.IIdParentId);
                    if (parentEntity != null)
                    {
                        parentEntity.BActive = false;
                    }
                    _repository.Update(parentEntity);

                    // Clone chi tiết
                    if (details != null && details.Count > 0)
                    {
                        details = details.Select(x =>
                        {
                            x.IdParentModified = x.Id;
                            x.Id = Guid.NewGuid();
                            x.IIdKeHoach5NamId = entity.Id;
                            x.SGhiChu = string.Empty;
                            x.BActive = true;
                            return x;
                        }).OrderBy(x => x.SMaOrder).ToList();

                        //var refDictionary = details.ToDictionary(x => x.IdParentModified, x => x.Id);
                        var refDictionary = details.GroupBy(p => p.IdParentModified, g => g.Id).ToDictionary(g => g.Key, g => g.First());

                        // Update dự án
                        List<VdtDaDuAn> lstDuAn = new List<VdtDaDuAn>();
                        foreach (var item in details)
                        {
                            // Cập nhật IdReference, IdParent
                            if (item.IdReference != null) item.IdReference = refDictionary[item.IdReference];
                            if (item.IdParent != null) item.IdParent = refDictionary[item.IdParent];
                        }

                        // Thêm chi tiết
                        _chitietRepository.AddRange(details);
                    }

                    transactionScope.Complete();
                    return DBContextSaveChangeState.SUCCESS;
                }
                catch (Exception)
                {
                    return DBContextSaveChangeState.ERROR;
                }
            }
        }

        public int Agregate(VdtKhvKeHoach5NamDeXuat entity, List<VdtKhvKeHoach5NamDeXuatChiTiet> details, List<Guid> lstVoucher)
        {
            using (var transactionScope = new TransactionScope(
               TransactionScopeOption.Required,
               new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    // Thêm kế hoạch tổng hợp
                    _repository.Add(entity);

                    foreach (var item in lstVoucher)
                    {
                        VdtKhvKeHoach5NamDeXuat itemChungTu = _repository.Find(item);
                        if (itemChungTu != null)
                        {
                            itemChungTu.IIdTongHopParent = entity.Id;
                            _repository.Update(itemChungTu);
                        }
                    }

                    // Add chi tiết
                    if (details != null && details.Count > 0)
                    {
                        details = details.Select(x =>
                        {
                            x.IdParentModified = x.Id;
                            x.Id = Guid.NewGuid();
                            x.IIdKeHoach5NamId = entity.Id;
                            return x;
                        }).OrderBy(x => x.SMaOrder).ToList();

                        var refDictionary = details.ToDictionary(x => x.IdParentModified, x => x.Id);

                        // Update dự án
                        List<VdtDaDuAn> lstDuAn = new List<VdtDaDuAn>();
                        foreach (var item in details)
                        {
                            // Cập nhật IdReference, IdParent
                            if (item.IdReference != null) item.IdReference = refDictionary[item.IdReference];
                            if (item.IdParent != null) item.IdParent = refDictionary[item.IdParent];
                        }

                        // Thêm chi tiết
                        _chitietRepository.AddRange(details);
                    }

                    transactionScope.Complete();
                    return DBContextSaveChangeState.SUCCESS;
                }
                catch (Exception)
                {
                    return DBContextSaveChangeState.ERROR;
                }
            }
        }

        public int Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public VdtKhvKeHoach5NamDeXuat FindAggregateVoucher(string sTongHop)
        {
            return _repository.FindAggregateVoucher(sTongHop);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuat> FindByCondition(Expression<Func<VdtKhvKeHoach5NamDeXuat, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public VdtKhvKeHoach5NamDeXuat FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuat> FindByIdDonViParent(string idDonVi, int type, int iGiaiDoanTu, int iGiaiDoanDen)
        {
            return _repository.FindByIdDonViParent(idDonVi, type, iGiaiDoanTu, iGiaiDoanDen);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatQuery> FindConditionIndex(int yearOfWork)
        {
            return _repository.FindConditionIndex(yearOfWork);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            VdtKhvKeHoach5NamDeXuat chungTu = _repository.Find(id);
            chungTu.BKhoa = isLock;
            _repository.Update(chungTu);
        }

        public int Update(VdtKhvKeHoach5NamDeXuat item)
        {
            return _repository.Update(item);
        }

        public int FindCurrentPeriod(int year)
        {
            int phanDu = year % 5;
            int phanNguyen = year / 5;
            if (phanDu == 0)
            {
                return (phanNguyen - 1) * 5 + 1;
            }
            else
            {
                return phanNguyen * 5 + 1;
            }
        }

        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id)
        {
            return _repository.IsExistSoQuyetDinh(soQuyetDinh, id);
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatQuery> FindConditionAll()
        {
            return _repository.FindConditionAll();
        }
    }
}
