using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class CpDanhMucService : IService<CpDanhMuc>, ICpDanhMucService 
    {
        private readonly ICpDanhMucRepository _cpDanhMucRepository;

        public CpDanhMucService(ICpDanhMucRepository cpDanhMucRepository)
        {
            _cpDanhMucRepository = cpDanhMucRepository;
        }

        public IEnumerable<CpDanhMuc> FindAll()
        {
            return _cpDanhMucRepository.FindAll();
        }

        public List<CpDanhMuc> FindByCondition(string maDanhMuc, string tenDanhMuc)
        {
            return _cpDanhMucRepository.FindByCondition(maDanhMuc, tenDanhMuc);
        }

        public CpDanhMuc FindById(Guid Id)
        {
            return _cpDanhMucRepository.SingleOrDefault(n => n.Id == Id);
        }

        public void Update(CpDanhMuc item)
        {
            _cpDanhMucRepository.Update(item);
        }

        public int AddRange(IEnumerable<CpDanhMuc> listAdd)
        {
            return _cpDanhMucRepository.AddRange(listAdd);
        }

        public int UpdateRange(IEnumerable<CpDanhMuc> listUpdate)
        {
            return _cpDanhMucRepository.UpdateRange(listUpdate);
        }

        public IEnumerable<CpDanhMuc> FindAll(Expression<Func<CpDanhMuc, bool>> predicate)
        {
            return _cpDanhMucRepository.FindAll(predicate);
        }

        public int Delete(Guid id)
        {
            CpDanhMuc entity = _cpDanhMucRepository.Find(id);
            return _cpDanhMucRepository.Delete(entity);
        }

        public List<CpDanhMuc> FindByNamLamViec(int namLamViec)
        {
            return _cpDanhMucRepository.FindByNamLamViec(namLamViec);
        }

        public override IEnumerable<CpDanhMuc> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _cpDanhMucRepository.FindByNamLamViec(authenticationInfo.YearOfWork);
        }

        public void Add(CpDanhMuc entity)
        {
            throw new NotImplementedException();
        }

        public override void AddOrUpdateRange(IEnumerable<CpDanhMuc> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                item.INamLamViec = authenticationInfo.YearOfWork;
                if (item.IsModified)
                {
                    if (Guid.Empty.Equals(item.Id))
                    {
                        item.DNgayTao = time;
                        item.SNguoiTao = authenticationInfo.Principal;
                        item.DNgaySua = null;
                        item.SNguoiSua = null;
                    }
                    else
                    {
                        item.DNgaySua = time;
                        item.SNguoiSua = authenticationInfo.Principal;
                    }
                }
            }
            _cpDanhMucRepository.AddOrUpdateRange(listEntities);
        }

        public int CountDanhMucCP(int namLamViec)
        {
            return _cpDanhMucRepository.CountDanhMucCP(namLamViec);
        }
    }
}
