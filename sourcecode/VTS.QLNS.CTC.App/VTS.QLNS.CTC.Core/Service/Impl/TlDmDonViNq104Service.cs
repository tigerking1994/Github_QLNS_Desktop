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
    public class TlDmDonViNq104Service : IService<TlDmDonViNq104>, ITlDmDonViNq104Service
    {
        private ITlDmDonViNq104Repository _repository;

        public TlDmDonViNq104Service(ITlDmDonViNq104Repository tlDmDonViRepository)
        {
            _repository = tlDmDonViRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmDonViNq104> listEntities, AuthenticationInfo authenticationInfo)
        {
            foreach (var item in listEntities)
            {
                item.XauNoiMa = string.Format("{0} - {1}", item.ParentId ?? "", item.MaDonVi);
            }
            _repository.AddOrUpdateRange(listEntities);
        }

        public IEnumerable<TlDmDonViNq104> FindAll()
        {
            return _repository.FindAll().Where(x => (x.ITrangThai.HasValue && (bool)x.ITrangThai));
        }

        public override IEnumerable<TlDmDonViNq104> FindAll(AuthenticationInfo authenticationInfo)
        {
            IEnumerable<TlDmDonViNq104> results = _repository.FindAll().OrderBy(x => x.XauNoiMa).ToList();
            foreach (var donvi in results)
            {
                donvi.TenDonViCha = results.FirstOrDefault(dv => dv.MaDonVi != null && dv.MaDonVi.Equals(donvi.ParentId))?.TenDonVi;
                donvi.BHangCha = results.Any(dv => dv.ParentId != null && dv.ParentId.Equals(donvi.MaDonVi));
            }
            return results;
        }

        public TlDmDonViNq104 FindByMaDonVi(string maDonVi)
        {
            return _repository.FindByMaDonVi(maDonVi);
        }

        public IEnumerable<TlDmDonViNq104> FindByCondition(Expression<Func<TlDmDonViNq104, bool>> predicate)
        {
            return _repository.FindAll(predicate).OrderBy(x => x.MaDonVi);
        }

        public IEnumerable<TlDmDonViNq104> FindByDonViCon(string maDonVi)
        {
            return _repository.FindAll().Where(x => x.ParentId == maDonVi);
        }

        public int AddRange(IEnumerable<TlDmDonViNq104> tlDmDonVis)
        {
            return _repository.AddRange(tlDmDonVis);
        }

        public IEnumerable<TlDmDonViNq104> FindAllDonVi()
        {
            return _repository.FindAllDonVi();
        }

        public IEnumerable<TlDmDonViNq104> FindAllDonViNq104()
        {
            return _repository.FindAllDonViNq104();
        }

        public IEnumerable<TlDmDonViNq104> FindDonViBaoCaoQuanSo(int nam)
        {
            return _repository.FindDonViBaoCaoQuanSo(nam);
        }

        public IEnumerable<TlDmDonViNq104> FindAllDonViBaoCao()
        {
            return _repository.FindAllDonViBaoCao();
        }

        public IEnumerable<TlDmDonViNq104> FindAllDonViBaoCaoNq104()
        {
            return _repository.FindAllDonViBaoCaoNq104();
        }

        public int UpdateRange(List<TlDmDonViNq104> tlDmDonVis)
        {
            return _repository.UpdateRange(tlDmDonVis);
        }

        public IEnumerable<TlDmDonViNq104> FindDonViTaoBangLuong(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false)
        {
            return _repository.FindDonViTaoBangLuong(nam, thang, cachTinhLuong, isThuNopBhxh, isNew);
        }

        public IEnumerable<TlDmDonViNq104> FindDonViBangLuongThang(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false)
        {
            return _repository.FindDonViBangLuongThang(nam, thang, cachTinhLuong, isThuNopBhxh, isNew);
        }

        public IEnumerable<TlDmDonViNq104> FindDonViPhuCap(int nam, int thang, string cachTinhLuong, bool isNew = false)
        {
            return _repository.FindDonViPhuCap(nam, thang, cachTinhLuong, isNew);
        }

        public IEnumerable<TlDmDonViNq104> FindAllDonViQuanSo(int? thang, int nam)
        {
            return _repository.FindAllDonViQuanSo(thang, nam);
        }
        public IEnumerable<TlDmDonViNq104> FindAllDonViQuanSoNam(int nam)
        {
            return _repository.FindAllDonViQuanSoNam(nam);
        }

        public TlDmDonViNq104 FirstOrDefault(Expression<Func<TlDmDonViNq104, bool>> predicate)
        {
            return _repository.FirstOrDefault(predicate);
        }
    }
}
