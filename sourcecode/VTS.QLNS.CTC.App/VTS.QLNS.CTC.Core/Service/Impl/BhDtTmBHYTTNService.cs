using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDtTmBHYTTNService : IBhDtTmBHYTTNService
    {
        private readonly IBhDtTmBHYTTNRepository _bhDtTmBHYTTNRepository;
        public BhDtTmBHYTTNService(IBhDtTmBHYTTNRepository bhDtTmBHYTTNRepository)
        {
            _bhDtTmBHYTTNRepository = bhDtTmBHYTTNRepository;
        }

        public int Add(BhDtTmBHYTTN entity)
        {
            return _bhDtTmBHYTTNRepository.Add(entity);
        }

        public int Delete(BhDtTmBHYTTN entity)
        {
            return _bhDtTmBHYTTNRepository.Delete(entity);
        }

        public IEnumerable<BhDtTmBHYTTNQuery> FindByCondition(int namLamViec)
        {
            return _bhDtTmBHYTTNRepository.FindByCondition(namLamViec);
        }

        public BhDtTmBHYTTN FindById(Guid id)
        {
            return _bhDtTmBHYTTNRepository.Find(id);
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            return _bhDtTmBHYTTNRepository.GetSoChungTuIndexByCondition(yearOfWork);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTuBHXH = _bhDtTmBHYTTNRepository.Find(id);
            chungTuBHXH.BIsKhoa = isLock;
            _bhDtTmBHYTTNRepository.Update(chungTuBHXH);
        }

        public int Update(BhDtTmBHYTTN item)
        {
            return _bhDtTmBHYTTNRepository.Update(item);
        }
        public IEnumerable<BhDtTmBHYTTNQuery> GetDanhSachDotNhanPhanBo(int yearOfWork, DateTime date, int iLoaiDuToan)
        {
            return _bhDtTmBHYTTNRepository.GetDanhSachDotNhanPhanBo(yearOfWork, date, iLoaiDuToan);
        }
    }
}
