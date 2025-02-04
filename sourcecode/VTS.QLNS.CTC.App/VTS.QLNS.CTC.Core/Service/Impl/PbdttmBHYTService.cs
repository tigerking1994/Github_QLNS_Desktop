using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class PbdttmBHYTService : IPbdttmBHYTService
    {
        private readonly IPbdttmBHYTRepository _iPbdttmBHYTRepository;
        public PbdttmBHYTService(IPbdttmBHYTRepository iPbdttmBHYTRepository)
        {
            _iPbdttmBHYTRepository = iPbdttmBHYTRepository;
        }

        public IEnumerable<BhPbdttmBHYT> FindByCondition(Expression<Func<BhPbdttmBHYT, bool>> predicate)
        {
            return _iPbdttmBHYTRepository.FindByCondition(predicate);
        }
        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            return _iPbdttmBHYTRepository.GetSoChungTuIndexByCondition(iNamLamViec);
        }
        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _iPbdttmBHYTRepository.LockOrUnLock(id, lockStatus);
        }

        public int Add(BhPbdttmBHYT item)
        {
            return _iPbdttmBHYTRepository.Add(item);
        }

        public int AddRange(List<BhPbdttmBHYT> items)
        {
            return _iPbdttmBHYTRepository.AddRange(items);
        }

        public int Update(BhPbdttmBHYT item)
        {
            return _iPbdttmBHYTRepository.Update(item);
        }

        public int Delete(BhPbdttmBHYT item)
        {
            return _iPbdttmBHYTRepository.Delete(item);
        }
        public int RemoveRange(List<BhPbdttmBHYT> items)
        {
            return _iPbdttmBHYTRepository.RemoveRange(items);
        }

        public BhPbdttmBHYT FindById(Guid Id)
        {
            return _iPbdttmBHYTRepository.Find(Id);
        }
        public IEnumerable<BhPbdttmBHYT> FindDotNhanByChungTuPhanBo(Guid idPhanBo)
        {
            return _iPbdttmBHYTRepository.FindDotNhanByChungTuPhanBo(idPhanBo);
        }

        public IEnumerable<BhPbdttmBHYT> FindBySoQuyetDinh(string soQuyetDinh, int nam)
        {
            return _iPbdttmBHYTRepository.FindBySoQuyetDinh(soQuyetDinh, nam);
        }

        public IEnumerable<BhPbdttmBHYT> FindByLuyKeDot(DateTime? ngayQuyetDinh, int nam)
        {
            return _iPbdttmBHYTRepository.FindByLuyKeDot(ngayQuyetDinh, nam);
        }

        public IEnumerable<BhPbdttmBHYT> FindBySoChungTu(string soChungTu, int nam)
        {
            return _iPbdttmBHYTRepository.FindBySoChungTu(soChungTu, nam);
        }

        public IEnumerable<BhPbdttmBHYTQuery> FindBySoQuyetDinhLuyKe(string soQuyetDinh, string ngayQuyetDinh, int nam)
        {
            return _iPbdttmBHYTRepository.FindBySoQuyetDinhLuyKe(soQuyetDinh, ngayQuyetDinh, nam);
        }

        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTTM(int year, bool isInTheoChungTu)
        {
            return _iPbdttmBHYTRepository.GetSoQuyetDinhDTTM(year, isInTheoChungTu);
        }

        public IEnumerable<BhPbdttmBHYT> FindByIdNhanDuToan(string id)
        {
            return _iPbdttmBHYTRepository.FindByIdNhanDuToan(id);
        }
    }
}
