using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmPhuCapService : IService<TlDmPhuCap>, ITlDmPhuCapService
    {
        private ITlDmPhuCapRepository _repository;

        public TlDmPhuCapService(ITlDmPhuCapRepository tlDmPhuCapRepository)
        {
            _repository = tlDmPhuCapRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmPhuCap> listEntities, AuthenticationInfo authenticationInfo)
        {
            foreach (var item in listEntities)
            {
                if (item.Parent != "" || item.Parent != null)
                {
                    item.XauNoiMa = item.Parent + "-" + item.MaPhuCap;
                }
                else
                {
                    item.XauNoiMa = item.Parent;
                }
            }
            if (listEntities != null && listEntities.Any(n => n.IsDeleted))
            {
                _repository.UpdateCanBoPhuCapWhenChangePhuCap(authenticationInfo.Month, authenticationInfo.YearOfWork, listEntities.Where(n => n.IsDeleted).Select(n => n.Id).ToList(), true);
            }
            _repository.AddOrUpdateRange(listEntities);
            if (listEntities != null && listEntities.Any(n => !n.IsDeleted))
            {
                _repository.UpdateCanBoPhuCapWhenChangePhuCap(authenticationInfo.Month, authenticationInfo.YearOfWork, listEntities.Where(n => !n.IsDeleted).Select(n => n.Id).ToList(), false);
            }   
        }

        public override IEnumerable<TlDmPhuCap> FindAll(AuthenticationInfo authenticationInfo)
        {
            if (authenticationInfo.OptionalParam != null && authenticationInfo.OptionalParam.Length > 0)
            {
                string pcLoai = authenticationInfo.OptionalParam[0].ToString();
                string mlnsBHXH = authenticationInfo.OptionalParam[1].ToString();
                string pcChosen = authenticationInfo.OptionalParam[2].ToString();
                var rs = _repository.FindByBHXHPhuCapNotIn(authenticationInfo.YearOfWork, pcLoai, mlnsBHXH, pcChosen);
                return rs.OrderBy(x => x.XauNoiMa).ToList();                
            }

            return _repository.FindAll().Where(x => x.MaPhuCap != "HETHONG" || x.Parent != "HETHONG").OrderBy(x => x.XauNoiMa).ToList();
        }

        public TlDmPhuCap FindById(string id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<TlDmPhuCap> FindAll()
        {
            return _repository.FindAll();
        }        

        public IEnumerable<TlDmPhuCap> FindByCondition()
        {
            return _repository.FindByCondition();
        }

        public IEnumerable<TlDmPhuCap> FindByCondition(Expression<Func<TlDmPhuCap, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<TlDmPhuCap> FindByHeThong()
        {
            return _repository.FindByHeThong();
        }

        public TlDmPhuCap FindByMaPhuCap(string maPhuCap)
        {
            return _repository.FindByMaPhuCap(maPhuCap);
        }

        public IEnumerable<TlDmPhuCap> GetDmPhuCapInDcTapTheCanBo()
        {
            return _repository.GetDmPhuCapInDcTapTheCanBo();
        }

        public override IEnumerable<TlDmPhuCap> FindAll(Expression<Func<TlDmPhuCap, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<TlDmPhuCap> FindHasDataBangLuong(int nam, int thang, string maCachTl)
        {
            return _repository.FindHasDataBangLuong(nam, thang, maCachTl);
        }

        public override bool CheckPhuCapExist(string maPhuCap, Guid? iId)
        {
            return _repository.CheckPhuCapExist(maPhuCap, iId ?? Guid.Empty);
        }

        public void UpdateCanBoPhuCapWhenChangePhuCap(int iThang, int iNam, List<Guid> lstIdPhuCap, bool bIsDelete)
        {
            _repository.UpdateCanBoPhuCapWhenChangePhuCap(iThang, iNam, lstIdPhuCap, bIsDelete);
        }

        public IEnumerable<TlPhuCapQuery> FindAllPhuCapVaCheDoBHXH()
        {
            return _repository.FindAllPhuCapVaCheDoBHXH();
        }
        public IEnumerable<TlDmPhuCap> FindByIdThuNopBhxh(Guid id)
        {
            return _repository.FindByIdThuNopBhxh(id);
        }
        public override IEnumerable<TlDmPhuCap> FindAllTTPhuCapLuong(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll().Where(x => !string.IsNullOrEmpty(x.Parent) && x.MaPhuCap.EndsWith("_TT")).OrderBy(s => s.MaPhuCap).ToList();
        }
        public override string FindByBHXHPhuCapIn(int namLamViec, string mlnsLoai, string mlnsBhxh)
        {
            return _repository.FindByBHXHPhuCapIn(namLamViec, mlnsLoai, mlnsBhxh);
        }
    }
}
