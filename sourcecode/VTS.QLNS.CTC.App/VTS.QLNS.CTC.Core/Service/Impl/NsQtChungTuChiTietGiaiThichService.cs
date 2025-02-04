using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsQtChungTuChiTietGiaiThichService : INsQtChungTuChiTietGiaiThichService
    {
        private INsQtChungTuChiTietGiaiThichRepository _chungTuChiTietGiaiThichRepository;
        public NsQtChungTuChiTietGiaiThichService(INsQtChungTuChiTietGiaiThichRepository chungTuChiTietGiaiThichRepository)
        {
            _chungTuChiTietGiaiThichRepository = chungTuChiTietGiaiThichRepository;
        }

        public void Add(NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich)
        {
            _chungTuChiTietGiaiThichRepository.Add(chungTuChiTietGiaiThich);
        }

        public NsQtChungTuChiTietGiaiThich FindByCondition(SettlementVoucherDetailExplainCriteria condition)
        {
            return _chungTuChiTietGiaiThichRepository.FindByCondition(condition);
        }

        public NsQtChungTuChiTietGiaiThich FindByCondition(Expression<Func<NsQtChungTuChiTietGiaiThich, bool>> predicate)
        {
            return _chungTuChiTietGiaiThichRepository.FindByCondition(predicate);
        }

        public IEnumerable<NsQtChungTuChiTietGiaiThich> FindListCondition(SettlementVoucherDetailExplainCriteria condition)
        {
            return _chungTuChiTietGiaiThichRepository.FindListCondition(condition);
        }


        public NsQtChungTuChiTietGiaiThich FindById(Guid id)
        {
            return _chungTuChiTietGiaiThichRepository.Find(id);
        }

        public int Delete(Guid id)
        {
            return _chungTuChiTietGiaiThichRepository.Delete(id);
        }

        public void Update(NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich)
        {
            _chungTuChiTietGiaiThichRepository.Update(chungTuChiTietGiaiThich);
        }
        public IEnumerable<NsQtChungTuChiTietGiaiThich> GetDataChungTuGiaiTichBangSo(string sLoai, string iID_MaDonVi, int iThang, int iQuy, int iNamLamViec, int iNamNganSach, int iID_NguonNganSach, int isTongHop, string explainId)
        {
            return _chungTuChiTietGiaiThichRepository.GetDataChungTuGiaiTichBangSo(sLoai, iID_MaDonVi, iThang, iQuy, iNamLamViec, iNamNganSach, iID_NguonNganSach, isTongHop, explainId);
        }

        public IEnumerable<NsQtChungTuChiTietGiaiThich> GetDataChungTuGiaiTichBangLoi(string sLoai, string iID_MaDonVi, int iThang, int iQuy, int iNamLamViec, int iNamNganSach, int iID_NguonNganSach, int isTongHop, string explainId)
        {
            return _chungTuChiTietGiaiThichRepository.GetDataChungTuGiaiTichBangLoi(sLoai, iID_MaDonVi, iThang, iQuy, iNamLamViec, iNamNganSach, iID_NguonNganSach, isTongHop, explainId);
        }
    }
}
