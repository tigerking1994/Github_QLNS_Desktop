using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QttBHXHChiTietGiaiThichService : IQttBHXHChiTietGiaiThichService
    {
        private readonly IQttBHXHChiTietGiaiThichRepository _repository;
        public QttBHXHChiTietGiaiThichService(IQttBHXHChiTietGiaiThichRepository iQttBHXHChiTietGiaiThichRepository)
        {
            _repository = iQttBHXHChiTietGiaiThichRepository;
        }

        public void Add(BhQttBHXHChiTietGiaiThich chungTuChiTietGiaiThich)
        {
            _repository.Add(chungTuChiTietGiaiThich);
        }

        public int AddRange(IEnumerable<BhQttBHXHChiTietGiaiThich> chungTuChiTiets)
        {
            return _repository.AddRange(chungTuChiTiets);
        }

        public BhQttBHXHChiTietGiaiThichQuery ExportGiaiThichBangLoi(int namLamViec, int quy, int loaiQuy, string maDonVi, string loaiThu)
        {
            return _repository.ExportGiaiThichBangLoi(namLamViec, quy, loaiQuy, maDonVi, loaiThu);
        }

        public BhQttBHXHChiTietGiaiThichQuery ExportGiaiThichBangLoiTongHopDonVi(int namLamViec, int quy, int loaiQuy, string maDonVis, string maLoaiThu)
        {
            return _repository.ExportGiaiThichBangLoiTongHopDonVi(namLamViec, quy, loaiQuy, maDonVis, maLoaiThu);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichGiamDong(int namLamViec, string maDonVi)
        {
            return _repository.ExportGiaiThichGiamDong(namLamViec, maDonVi);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichGiamDongTongHopDonVi(int namLamViec, string maDonVis)
        {
            return _repository.ExportGiaiThichGiamDongTongHopDonVi(namLamViec, maDonVis);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichSoLieuThang(int namLamViec, int quy, int loaiQuy, string maDonVi, int dvt, bool isLuyKe)
        {
            return _repository.ExportGiaiThichSoLieuThang(namLamViec, quy, loaiQuy, maDonVi, dvt, isLuyKe);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichSoLieuThangQuy(int namLamViec, int quy, int loaiQuy, string maDonVi, int dvt, bool isLuyKe)
        {
            return _repository.ExportGiaiThichSoLieuThangQuy(namLamViec, quy, loaiQuy, maDonVi, dvt, isLuyKe);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTongHopSoSanh(int namLamViec, string maDonVi)
        {
            return _repository.ExportGiaiThichTongHopSoSanh(namLamViec, maDonVi);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTongHopSoSanhDonVi(int namLamViec, string maDonVis)
        {
            return _repository.ExportGiaiThichTongHopSoSanhDonVi(namLamViec, maDonVis);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTruyThu(int namLamViec, string maDonVi)
        {
            return _repository.ExportGiaiThichTruyThu(namLamViec, maDonVi);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTruyThuTongHopDonVi(int namLamViec, string maDonVis)
        {
            return _repository.ExportGiaiThichTruyThuTongHopDonVi(namLamViec, maDonVis);
        }

        public BhQttBHXHChiTietGiaiThich FindByCondition(BhQttBHXHChiTietGiaiThichCriteria condition)
        {
            return _repository.FindByCondition(condition);
        }

        public BhQttBHXHChiTietGiaiThich FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThich> FindByQttId(Guid iDQTT)
        {
            return _repository.FindByQttId(iDQTT);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThich> FindByVouCherId(Guid voucherID)
        {
            return _repository.FindByVouCherId(voucherID);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThich> GetChiTietGiaiThichTongHopSoSanh(BhQttBHXHChiTietGiaiThichCriteria condition)
        {
            return _repository.GetChiTietGiaiThichTongHopSoSanh(condition);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThich> GetChiTietGiaiThichTongHopSoSanhTonTai(BhQttBHXHChiTietGiaiThichCriteria condition)
        {
            return _repository.GetChiTietGiaiThichTongHopSoSanhTonTai(condition);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> GetChiTietGiaiThichTruyThu(BhQttBHXHChiTietGiaiThichCriteria condition)
        {
            return _repository.GetChiTietGiaiThichTruyThu(condition);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichBangLoiQuery> GetGiaiThichBangLoi(BhQttBHXHChiTietGiaiThichCriteria condition)
        {
            return _repository.GetGiaiThichBangLoi(condition);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> GetGiaiThichTruyThu(int namLamViec, string maDonVi, int quy, int loaiQuy)
        {
            return _repository.GetGiaiThichTruyThu(namLamViec, maDonVi, quy, loaiQuy);
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> GetGiaiThichTruyThuDonVi(int namLamViec, string maDonVi, int quy, int loaiQuy)
        {
            return _repository.GetGiaiThichTruyThuDonVi(namLamViec, maDonVi, quy, loaiQuy);
        }

        public bool HasMonthlyExplains(int iNamLamViec, int iQuy, int iLoai, bool isLuyKe, string sMaDonVi)
        {
            return _repository.HasMonthlyExplains(iNamLamViec, iQuy, iLoai, isLuyKe, sMaDonVi);
        }

        public int RemoveRange(IEnumerable<BhQttBHXHChiTietGiaiThich> chiTiets)
        {
            return _repository.RemoveRange(chiTiets);
        }

        public void Update(BhQttBHXHChiTietGiaiThich chungTuChiTietGiaiThich)
        {
            _repository.Update(chungTuChiTietGiaiThich);
        }
    }
}
