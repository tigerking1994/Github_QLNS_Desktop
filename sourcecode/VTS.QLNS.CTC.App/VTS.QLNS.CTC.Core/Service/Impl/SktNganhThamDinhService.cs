using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SktNganhThamDinhService : ISktNganhThamDinhService
    {
        private readonly ISktNganhThamDinhRepository _thDChungTuChiTietRepository;

        public SktNganhThamDinhService(ISktNganhThamDinhRepository chungTuChiTietRepository)
        {
            _thDChungTuChiTietRepository = chungTuChiTietRepository;
        }

        public NsSktNganhThamDinh Add(NsSktNganhThamDinh entity)
        {
            _thDChungTuChiTietRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            NsSktNganhThamDinh entity = _thDChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                return _thDChungTuChiTietRepository.Delete(entity);
            }
            return 0;
        }

        public NsSktNganhThamDinh Find(params object[] keyValues)
        {
            return _thDChungTuChiTietRepository.Find(keyValues);
        }

        public int Update(NsSktNganhThamDinh entity)
        {
            return _thDChungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<NsSktNganhThamDinh> FindByCondition(Expression<Func<NsSktNganhThamDinh, bool>> predicate)
        {
            return _thDChungTuChiTietRepository.FindAll(predicate);
        }

        public IEnumerable<ThDChungTuQuery> FindByNamLamViec(int namLamViec, int namNganSach, int nguonNganSach, string userName, int loai, int loaiNganSach)
        {
            return _thDChungTuChiTietRepository.FindByNamLamViec(namLamViec, namNganSach, nguonNganSach, userName, loai, loaiNganSach);
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            return _thDChungTuChiTietRepository.GetSoChungTuIndexByCondition(namLamViec);
        }

        public bool CheckExitsByChungTuId(Guid chungtuId)
        {
            return _thDChungTuChiTietRepository.CheckExitsByChungTuId(chungtuId);
        }

        public int UpdateStatusDisable(Guid id)
        {
            NsSktNganhThamDinh item = _thDChungTuChiTietRepository.Find(id);
            if (item != null)
            {
                return _thDChungTuChiTietRepository.Update(item);
            }
            return 0;
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _thDChungTuChiTietRepository.LockOrUnLock(id, lockStatus);
        }

        public void CreateVoucherAggregate(string voucherId, string maDonVi, string tenDonVi, int namLamViec, string userCreate, int namNganSach, int nguonNganSach)
        {
            _thDChungTuChiTietRepository.CreateVoucherAggregate(voucherId, maDonVi, tenDonVi, namLamViec, userCreate, namNganSach, nguonNganSach);
        }
    }
}
