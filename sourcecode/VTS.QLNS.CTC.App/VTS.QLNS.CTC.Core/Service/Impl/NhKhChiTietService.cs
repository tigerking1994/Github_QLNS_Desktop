using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhKhChiTietService : INhKhChiTietService
    {
        private INhKhChiTietRepository _nhKhChiTietRepository;

        public NhKhChiTietService(INhKhChiTietRepository nhKhChiTietRepository)
        {
            _nhKhChiTietRepository = nhKhChiTietRepository;
        }

        public void Add(NhKhChiTiet nhKhChiTiet)
        {
            _nhKhChiTietRepository.Add(nhKhChiTiet);
        }

        public void Delete(Guid id)
        {
            _nhKhChiTietRepository.Delete(id);
        }

        public IEnumerable<NhKhChiTiet> FindAll()
        {
           return  _nhKhChiTietRepository.FindAll();
        }

        public IEnumerable<NhKhChiTietQuery> FindAllNhkHChiTietHasSoKeHoachTTBQP()
        {
            return _nhKhChiTietRepository.FindAllNhkHChiTietHasSoKeHoachTTBQP();

        }

        public NhKhChiTiet FindById(Guid id)
        {
            return _nhKhChiTietRepository.Find(id);
        }

        public void LockOrUnlock(Guid id, bool isKhoa)
        {
            NhKhChiTiet khChiTiet = _nhKhChiTietRepository.Find(id);
            khChiTiet.BIsKhoa = isKhoa;
            _nhKhChiTietRepository.Update(khChiTiet);
        }

        public void Update(NhKhChiTiet nhKhChiTiet)
        {
            _nhKhChiTietRepository.Update(nhKhChiTiet);
        }

        public int Adjust(NhKhChiTiet entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    // Thêm kế hoạch chi tiết điều chỉnh, clone từ kế hoạch chi tiết gốc
                    _nhKhChiTietRepository.Add(entity);

                    NhKhChiTiet parentEntity = _nhKhChiTietRepository.Find(entity.IIdParentAdjustId);
                    if (parentEntity != null)
                    {
                        parentEntity.BIsActive = false;
                    }
                    _nhKhChiTietRepository.Update(parentEntity);

                    transactionScope.Complete();
                    return DBContextSaveChangeState.SUCCESS;
                }
                catch (Exception)
                {
                    return DBContextSaveChangeState.ERROR;
                }
            }
        }

        public IEnumerable<NhKhChiTietQuery> FindByCondition(int namLamViec)
        {
            return _nhKhChiTietRepository.FindByCondition(namLamViec);
        }
    }
}
