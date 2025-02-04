using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmDonViService : IService<TlDmDonVi>, ITlDmDonViService
    {
        private ITlDmDonViRepository _repository;

        public TlDmDonViService(ITlDmDonViRepository tlDmDonViRepository)
        {
            _repository = tlDmDonViRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmDonVi> listEntities, AuthenticationInfo authenticationInfo)
        {
            foreach (var item in listEntities)
            {
                item.XauNoiMa = string.Format("{0} - {1}", item.ParentId ?? "", item.MaDonVi);
            }
            _repository.AddOrUpdateRange(listEntities);
        }

        public IEnumerable<TlDmDonVi> FindAll()
        {
            return _repository.FindAll().Where(x => (x.ITrangThai.HasValue  && (bool)x.ITrangThai) );
        }

        public override IEnumerable<TlDmDonVi> FindAll(AuthenticationInfo authenticationInfo)
        {
            IEnumerable<TlDmDonVi> results = _repository.FindAll().OrderBy(x => x.XauNoiMa).ToList();
            foreach (var donvi in results)
            {
                donvi.TenDonViCha = results.FirstOrDefault(dv => dv.MaDonVi != null && dv.MaDonVi.Equals(donvi.ParentId))?.TenDonVi;
                donvi.BHangCha = results.Any(dv => dv.ParentId != null && dv.ParentId.Equals(donvi.MaDonVi));
            }
            return results;
        }

        public TlDmDonVi FindByMaDonVi(string maDonVi)
        {
            return _repository.FindByMaDonVi(maDonVi);
        }

        public IEnumerable<TlDmDonVi> FindByCondition(Expression<Func<TlDmDonVi, bool>> predicate)
        {
            return _repository.FindAll(predicate).OrderBy(x => x.MaDonVi);
        }

        public IEnumerable<TlDmDonVi> FindByDonViCon(string maDonVi)
        {
            return _repository.FindAll().Where(x => x.ParentId == maDonVi);
        }

        public int AddRange(IEnumerable<TlDmDonVi> tlDmDonVis)
        {
            return _repository.AddRange(tlDmDonVis);
        }

        public IEnumerable<TlDmDonVi> FindAllDonVi()
        {
            return _repository.FindAllDonVi();
        }

        public IEnumerable<TlDmDonVi> FindAllDonViNq104()
        {
            return _repository.FindAllDonViNq104();
        }

        public IEnumerable<TlDmDonVi> FindDonViBaoCaoQuanSo(int nam)
        {
            return _repository.FindDonViBaoCaoQuanSo(nam);
        }

        public IEnumerable<TlDmDonVi> FindAllDonViBaoCao()
        {
            return _repository.FindAllDonViBaoCao();
        }

        public IEnumerable<TlDmDonVi> FindAllDonViBaoCaoNq104()
        {
            return _repository.FindAllDonViBaoCaoNq104();
        }

        public int UpdateRange(List<TlDmDonVi> tlDmDonVis)
        {
            return _repository.UpdateRange(tlDmDonVis);
        }

        public IEnumerable<TlDmDonVi> FindDonViTaoBangLuong(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false)
        {
            return _repository.FindDonViTaoBangLuong(nam, thang, cachTinhLuong, isThuNopBhxh, isNew);
        }

        public IEnumerable<TlDmDonVi> FindDonViTaoBangLuongBHXH(int nam, int thang, string cachTinhLuong)
        {
            return _repository.FindDonViTaoBangLuongBHXH(nam, thang, cachTinhLuong);
        }

        public IEnumerable<TlDmDonVi> FindDonViBangLuongThang(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false)
        {
            return _repository.FindDonViBangLuongThang(nam, thang, cachTinhLuong, isThuNopBhxh, isNew);
        }

        public IEnumerable<TlDmDonVi> FindDonViPhuCap(int nam, int thang, string cachTinhLuong, bool isNew = false)
        {
            return _repository.FindDonViPhuCap(nam, thang, cachTinhLuong, isNew);
        }

        public IEnumerable<TlDmDonVi> FindAllDonViQuanSo(int? thang, int nam)
        {
            return _repository.FindAllDonViQuanSo(thang, nam);
        }
        public IEnumerable<TlDmDonVi> FindAllDonViQuanSoNam(int nam)
        {
            return _repository.FindAllDonViQuanSoNam(nam);
        }

        public TlDmDonVi FirstOrDefault(Expression<Func<TlDmDonVi, bool>> predicate)
        {
            return _repository.FirstOrDefault(predicate);
        }
    }
}
