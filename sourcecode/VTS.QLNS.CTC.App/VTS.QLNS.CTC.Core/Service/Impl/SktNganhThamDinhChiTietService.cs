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
    public class SktNganhThamDinhChiTietService : ISktNganhThamDinhChiTietService
    {
        private readonly ISktNganhThamDinhChiTietRepository _thDChungTuChiTietRepository;

        public SktNganhThamDinhChiTietService(ISktNganhThamDinhChiTietRepository chungTuChiTietRepository)
        {
            _thDChungTuChiTietRepository = chungTuChiTietRepository;
        }

        public NsSktNganhThamDinhChiTiet Add(NsSktNganhThamDinhChiTiet entity)
        {
            _thDChungTuChiTietRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            NsSktNganhThamDinhChiTiet entity = _thDChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                return _thDChungTuChiTietRepository.Delete(entity);
            }
            return 0;
        }

        public NsSktNganhThamDinhChiTiet Find(params object[] keyValues)
        {
            return _thDChungTuChiTietRepository.Find(keyValues);
        }

        public int Update(NsSktNganhThamDinhChiTiet entity)
        {
            return _thDChungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<NsSktNganhThamDinhChiTiet> FindByCondition(Expression<Func<NsSktNganhThamDinhChiTiet, bool>> predicate)
        {
            return _thDChungTuChiTietRepository.FindAll(predicate);
        }

        public IEnumerable<ThDChungTuChiTietQuery> FindByCondition(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach)
        {
            return _thDChungTuChiTietRepository.FindByCondition(namLamViec, idChungTu, namNganSach, nguonNganSach);
        }

        public IEnumerable<ThDChungTuChiTietQuery> FindByConditionNSBD(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach)
        {
            return _thDChungTuChiTietRepository.FindByConditionNSBD(namLamViec, idChungTu, namNganSach, nguonNganSach);
        }

        public int AddRange(IEnumerable<NsSktNganhThamDinhChiTiet> entities)
        {
            return _thDChungTuChiTietRepository.AddRange(entities);
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            _thDChungTuChiTietRepository.DeleteByVoucherId(voucherId);
        }

        public IEnumerable<ThDChungTuChiTietQuery> FindByConditionReport(int namLamViec, string idChungTu, string nganh, int namNganSach, int nguonNganSach)
        {
            return _thDChungTuChiTietRepository.FindByConditionReport(namLamViec, idChungTu, nganh, namNganSach, nguonNganSach);
        }

        public IEnumerable<ThDChungTuReportNSBDQuery> GetDataReportNSBD(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach)
        {
            return _thDChungTuChiTietRepository.GetDataReportNSBD(namLamViec, idChungTu, namNganSach, nguonNganSach);
        }
    }
}
