using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class LbChungTuChiTietPhanCapService : ILbChungTuChiTietPhanCapService
    {
        private readonly ILbChungTuChiTietPhanCapRepository _lbChungTuChiTietPhanCapRepository;
        public LbChungTuChiTietPhanCapService(ILbChungTuChiTietPhanCapRepository lbChungTuChiTietPhanCapRepository)
        {
            _lbChungTuChiTietPhanCapRepository = lbChungTuChiTietPhanCapRepository;
        }

        public int Add(NsNganhChungTuChiTietPhanCap entity)
        {
            return _lbChungTuChiTietPhanCapRepository.Add(entity);
        }

        public int AddRange(IEnumerable<NsNganhChungTuChiTietPhanCap> entities)
        {
            return _lbChungTuChiTietPhanCapRepository.AddRange(entities);
        }

        public int Delete(NsNganhChungTuChiTietPhanCap entity)
        {
            return _lbChungTuChiTietPhanCapRepository.Delete(entity);
        }

        public void DeleteByNganhChiTiet(Guid idNganhChiTiet)
        {
            _lbChungTuChiTietPhanCapRepository.DeleteByNganhChiTiet(idNganhChiTiet);
        }

        public NsNganhChungTuChiTietPhanCap Find(params object[] keyValues)
        {
            return _lbChungTuChiTietPhanCapRepository.Find(keyValues);
        }

        public List<NsNganhChungTuChiTietPhanCap> FindByChiTietId(string chitietId, string idDonVi, int namLamViec)
        {
            return _lbChungTuChiTietPhanCapRepository.FindByChiTietId(chitietId, idDonVi, namLamViec);
        }

        public NsNganhChungTuChiTietPhanCap FindByCondition(string idDonVi, string idChungTuChiTiet, int namLamViec)
        {
            return _lbChungTuChiTietPhanCapRepository.FindByCondition(idDonVi, idChungTuChiTiet, namLamViec);
        }

        public IEnumerable<LbChiTietPhanCapQuery> GetSoLieuChiTietPhanCap(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet)
        {
            return _lbChungTuChiTietPhanCapRepository.GetSoLieuChiTietPhanCap(namLamViec, xauNoiMa, listXauNoiMa, idChiTiet);
        }

        public IEnumerable<LbChiTietPhanCapQuery> GetSoLieuChiTietPhanCapExport(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet)
        {
            return _lbChungTuChiTietPhanCapRepository.GetSoLieuChiTietPhanCapExport(namLamViec, xauNoiMa, listXauNoiMa, idChiTiet);
        }

        public int Update(NsNganhChungTuChiTietPhanCap entity)
        {
            return _lbChungTuChiTietPhanCapRepository.Update(entity);
        }
    }
}
