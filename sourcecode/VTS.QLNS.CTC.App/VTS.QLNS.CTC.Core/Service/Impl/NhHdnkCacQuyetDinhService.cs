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
    public class NhHdnkCacQuyetDinhService : INhHdnkCacQuyetDinhService
    {
        private readonly INhHdnkCacQuyetDinhRepository _repository;

        public NhHdnkCacQuyetDinhService(
            INhHdnkCacQuyetDinhRepository nhHdnkCacQuyetDinhRepository)
        {
            _repository = nhHdnkCacQuyetDinhRepository;

        }

        public IEnumerable<NhQuyetDinhDamPhamQuery> FindByCondition(int iLoai)
        {
            return _repository.FindByCondition(iLoai);
        }

        public IEnumerable<NhHdnkCacQuyetDinh> FindByIdNhiemVuChiILoaiQuyetDinh(Guid idNhiemVuChi, int iLoaiQuyetDinh)
        {
            return _repository.FindByIdNhiemVuChiILoaiQuyetDinh(idNhiemVuChi, iLoaiQuyetDinh);
        }

        public IEnumerable<NhHdnkCacQuyetDinh> FindByIdNhiemVuChi(Guid idNhiemVuChi)
        {
            return _repository.FindByIdNhiemVuChi(idNhiemVuChi);
        }

        public NhHdnkCacQuyetDinh FindById(Guid? id)
        {
            return _repository.Find(id);
        }

        public void LockOrUnlock(Guid id, bool BIsKhoa)
        {
            NhHdnkCacQuyetDinh entity = _repository.Find(id);
            if (entity != null) entity.BIsKhoa = BIsKhoa;
            _repository.Update(entity);
        }

        public void Update(NhHdnkCacQuyetDinh entity)
        {
            if (entity != null) _repository.Update(entity);
        }
        public void DeleteQuyetDinh(Guid id, Guid? idParentId)
        {
            _repository.DeleteQuyetDinh(id, idParentId);
        }

        public void Add(NhHdnkCacQuyetDinh entity)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                if (entity != null) _repository.Add(entity);
                transactionScope.Complete();
            }
        }

        public int Adjust(NhHdnkCacQuyetDinh entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    // Thêm quyết định điều chỉnh, clone từ quyết định gốc
                    if(entity!=null)
                    {
                        entity.BIsGoc = false;
                    }
                    _repository.Add(entity);
                    entity.BIsGoc = false;
                    _repository.Update(entity);
                    NhHdnkCacQuyetDinh parentEntity = _repository.Find(entity.IIdParentAdjustId);
                    if (parentEntity != null)
                    {
                        parentEntity.BIsActive = false;
                    }
                    _repository.Update(parentEntity);

                    transactionScope.Complete();
                    return DBContextSaveChangeState.SUCCESS;
                }
                catch (Exception)
                {
                    return DBContextSaveChangeState.ERROR;
                }
            }
        }

        public IEnumerable<NhCacQuyetDinhNhiemVuChiQuery> FindByNhiemVuChi(Guid idNhiemVuChi, int iLoaiQuyetDinh, Guid idKhTongThe)
        {
            return _repository.FindByNhiemVuChi(idNhiemVuChi, iLoaiQuyetDinh, idKhTongThe);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }
    }
}
