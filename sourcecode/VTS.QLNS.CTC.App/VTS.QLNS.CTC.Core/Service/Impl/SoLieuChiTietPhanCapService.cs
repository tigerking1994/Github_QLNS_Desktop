using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SoLieuChiTietPhanCapService : ISoLieuChiTietPhanCapService
    {
        private readonly ISoLieuChiTietPhanCapRepository _soLieuChiTietPhanCapRepository;

        public SoLieuChiTietPhanCapService(ISoLieuChiTietPhanCapRepository soLieuChiTietPhanCapRepository)
        {
            _soLieuChiTietPhanCapRepository = soLieuChiTietPhanCapRepository;
        }

        public int AddRange(IEnumerable<NsDtdauNamPhanCap> entities)
        {
            return _soLieuChiTietPhanCapRepository.AddRange(entities);
        }

        public int Add(NsDtdauNamPhanCap entity)
        {
            return _soLieuChiTietPhanCapRepository.Add(entity);
        }

        public IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCap(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet)
        {
            return _soLieuChiTietPhanCapRepository.GetSoLieuChiTietPhanCap(namLamViec, xauNoiMa, listXauNoiMa, idChiTiet);
        }

        public IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDTDN(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet, Guid iID_CTDTDauNam, string XauNoiMaGoc)
        {
            return _soLieuChiTietPhanCapRepository.GetSoLieuChiTietPhanCapDTDN(namLamViec, xauNoiMa, listXauNoiMa, idChiTiet, iID_CTDTDauNam, XauNoiMaGoc);
        }

        public NsDtdauNamPhanCap Find(params object[] keyValues)
        {
            return _soLieuChiTietPhanCapRepository.Find(keyValues);
        }

        public int Update(NsDtdauNamPhanCap entity)
        {
            return _soLieuChiTietPhanCapRepository.Update(entity);
        }

        public int Delete(NsDtdauNamPhanCap entity)
        {
            return _soLieuChiTietPhanCapRepository.Delete(entity);
        }

        public IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDonVi0(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet)
        {
            return _soLieuChiTietPhanCapRepository.GetSoLieuChiTietPhanCapDonVi0(namLamViec, xauNoiMa, listXauNoiMa, idChiTiet);
        }

        public IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDonVi0_1(int namLamViec, Guid iID_CTDTDauNam)
        {
            return _soLieuChiTietPhanCapRepository.GetSoLieuChiTietPhanCapDonVi0_1(namLamViec, iID_CTDTDauNam);
        }


        public NsDtdauNamPhanCap FindByCondition(string idDonVi, string idChungTuChiTiet, int namLamViec)
        {
            return _soLieuChiTietPhanCapRepository.FindByCondition(idDonVi, idChungTuChiTiet, namLamViec);
        }

        public IEnumerable<NsDtdauNamPhanCap> FindDonViTongHop(string idDonVi, string mlnsId, int namLamViec)
        {
            return _soLieuChiTietPhanCapRepository.FindDonViTongHop(idDonVi, mlnsId, namLamViec);
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            _soLieuChiTietPhanCapRepository.DeleteByVoucherId(voucherId);
        }

        public IEnumerable<NsDtdauNamPhanCap> FindAll()
        {
            return _soLieuChiTietPhanCapRepository.FindAll();
        }

        public int RemoveRange(IEnumerable<NsDtdauNamPhanCap> entities)
        {
            return _soLieuChiTietPhanCapRepository.RemoveRange(entities);
        }

        public void BulkInsertNsDtdauNamPhanCap(List<NsDtdauNamPhanCap> lstData)
        {
            _soLieuChiTietPhanCapRepository.BulkInsert(lstData);
        }
    }
}
