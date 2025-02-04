using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QttmBHYTService : IQttmBHYTService
    {
        private readonly IQttmBHYTRepository _repository;
        public QttmBHYTService(IQttmBHYTRepository iQttmBHYTRepository)
        {
            _repository = iQttmBHYTRepository;
        }

        public void Add(BhQttmBHYT chungTu)
        {
            _repository.Add(chungTu);
        }

        public int Delete(BhQttmBHYT item)
        {
            return _repository.Delete(item);
        }

        public IEnumerable<BhQttmBHYT> FindAggregateVoucher(string sct, int namLamViec)
        {
            return _repository.FindAggregateVoucher(sct, namLamViec);
        }

        public IEnumerable<BhQttmBHYTQuery> FindByCondition(int namLamViec)
        {
            return _repository.FindByCondition(namLamViec);
        }

        public BhQttmBHYT FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<BhQttmBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            return _repository.FindChungTuDaTongHopBySCT(sct, namLamViec);
        }

        public IEnumerable<BhQttmBHYT> FindByCondition(int namLamViec, int quyNam, int loaiChungTu)
        {
            return _repository.FindByCondition(namLamViec, quyNam, loaiChungTu);
        }

        public List<string> FindLNSExist(BhQttmBHYTChiTietCriteria condition, Guid voucherId, List<string> listLNSSelected)
        {
            var predicate = PredicateBuilder.True<BhQttmBHYT>();
            predicate = predicate.And(x => x.INamLamViec == condition.INamLamViec && x.IIDMaDonVi == condition.IIDMaDonVi
                        && x.IQuyNam == condition.IQuyNam && x.IQuyNamLoai == condition.IQuyNamLoai);
            if (voucherId != Guid.Empty)
                predicate = predicate.And(x => x.Id != voucherId);

            List<string> listLNSExist = new List<string>();
            List<BhQttmBHYT> chungTus = _repository.FindAll(predicate).ToList();
            chungTus.ForEach(x =>
            {
                listLNSExist.AddRange(x.SDsMlns.Split(','));
            });

            return listLNSSelected.Where(x => listLNSExist.Contains(x)).ToList();
        }

        public List<string> FindVoucherLNSExist(BhQttmBHYTChiTietCriteria condition, Guid voucherId, int loaiChungTu)
        {
            var predicate = PredicateBuilder.True<BhQttmBHYT>();
            predicate = predicate.And(x => x.INamLamViec == condition.INamLamViec && x.IIDMaDonVi == condition.IIDMaDonVi
                        && x.IQuyNam == condition.IQuyNam && x.IQuyNamLoai == condition.IQuyNamLoai && x.ILoaiTongHop == loaiChungTu);
            if (voucherId != Guid.Empty)
                predicate = predicate.And(x => x.Id != voucherId);
            List<string> listLNSExist = new List<string>();
            List<BhQttmBHYT> chungTus = _repository.FindAll(predicate).ToList();
            List<string> lstLNS = new List<string> { "9030001", "9030002", "9030003", "9030004", "9030005", "9030006" };
            if (chungTus != null && chungTus.Count > 0)
            {
                return lstLNS;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<BhQttQuarterQuery> GetQuarterYearByYear(int namLamViec)
        {
            return _repository.GetQuarterYearByYear(namLamViec);
        }

        public int GetVoucherIndex(int year)
        {
            return _repository.GetVoucherIndex(year);
        }

        public List<int> GetVoucherYears(int year)
        {
            return _repository.GetVoucherYears(year);
        }

        public bool IsExistAggregateVoucher(int namLamViec)
        {
            return _repository.IsExistAggregateVoucher(namLamViec);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var voucher = _repository.Find(id);
            voucher.BIsKhoa = isLock;
            _repository.Update(voucher);
        }

        public int Update(BhQttmBHYT item)
        {
            return _repository.Update(item);
        }

        public IEnumerable<BhQttmBHYTQuery> FindChungTuDonVi(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam)
        {
            return _repository.FindChungTuDonVi(namLamViec, loaiTongHop, bDaTongHop, quyNam);
        }

        public IEnumerable<BhQttmBHYTQuery> FindAllChungTuDonVi(int namLamViec, int quyNam)
        {
            return _repository.FindAllChungTuDonVi(namLamViec, quyNam);
        }

        public IEnumerable<BhQttmBHYTQuery> FindChungTuDonViTongHop(int namLamViec, int loaiTongHop, string userName, int quyNam)
        {
            return _repository.FindChungTuDonViTongHop(namLamViec, loaiTongHop, userName, quyNam);
        }

        public List<string> FindCurrentUnits(int namLamViec, int quynam, int loaiQuyNam)
        {
            return _repository.FindCurrentUnits(namLamViec, quynam, loaiQuyNam);
        }
    }
}
