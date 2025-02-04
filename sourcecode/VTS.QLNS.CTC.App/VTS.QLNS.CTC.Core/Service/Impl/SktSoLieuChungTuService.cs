using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SktSoLieuChungTuService : ISktSoLieuChungTuService
    {
        private readonly ISktSoLieuChungTuRepository _chungTuRepository;

        public SktSoLieuChungTuService(ISktSoLieuChungTuRepository chungTuRepository,
            ISktChungTuChiTietRepository chungTuChiTietRepository)
        {
            _chungTuRepository = chungTuRepository;
        }

        public NsDtdauNamChungTu Add(NsDtdauNamChungTu entity)
        {
            _chungTuRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            NsDtdauNamChungTu entity = _chungTuRepository.Find(id);
            if (entity != null)
            {
                return _chungTuRepository.Delete(entity);
            }
            return 0;
        }

        public NsDtdauNamChungTu Find(params object[] keyValues)
        {
            return _chungTuRepository.Find(keyValues);
        }

        public int Update(NsDtdauNamChungTu entity)
        {
            return _chungTuRepository.Update(entity);
        }

        public IEnumerable<NsDtdauNamChungTu> FindByCondition(Expression<Func<NsDtdauNamChungTu, bool>> predicate)
        {
            return _chungTuRepository.FindAll(predicate);
        }

        public void UpdateTotalChungTu(string idDonVi, string loaiDonVi, int loaiChungTu, int namLamViec, int namNganSach, int loaiNNS, int nguonNganSach)
        {
            _chungTuRepository.UpdateTotalChungTu(idDonVi, loaiDonVi, loaiChungTu, namLamViec, namNganSach, loaiNNS, nguonNganSach);
        }

        public int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach, int loaiChungTu)
        {
            return _chungTuRepository.GetSoChungTuIndexByCondition(namLamViec, nguonNganSach, namNganSach, loaiChungTu);
        }

        public void UpdateTotalChungTu(string idDonVi, string loaiDonVi, int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loaiNNS, string chungTuId)
        {
            _chungTuRepository.UpdateTotalChungTu(idDonVi, loaiDonVi, loaiChungTu, namLamViec, namNganSach, nguonNganSach, loaiNNS, chungTuId);
        }
        public void UpdateChildChungTu(int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, string chungTuId)
        {
            _chungTuRepository.UpdateChildChungTu(loaiChungTu, namLamViec, namNganSach, nguonNganSach, chungTuId);
        }
        public List<NsDtdauNamChungTu> GetDataExportJson(List<Guid> lstId)
        {
            return _chungTuRepository.GetDataExportJson(lstId);
        }

        public void BulkInsertNsDtdauNamChungTu(List<NsDtdauNamChungTu> lstData)
        {
            _chungTuRepository.BulkInsert(lstData);
        }

        public void DeleteCtdnctByDeleteMlns(Guid iID_CTDTDauNam, string sMLNS, int iNamLamViec)
        {
            _chungTuRepository.DeleteCtdnctByDeleteMlns(iID_CTDTDauNam, sMLNS, iNamLamViec);
        }
    }
}
