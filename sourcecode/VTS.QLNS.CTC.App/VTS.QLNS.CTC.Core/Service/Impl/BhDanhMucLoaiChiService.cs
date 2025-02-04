using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDanhMucLoaiChiService : IService<BhDanhMucLoaiChi>, IBhDanhMucLoaiChiService
    {
        private readonly IBhDanhMucLoaiChiRepository _repository;
        //public IDanhMucRepository _danhMucRepository;
        public IBhDmMucLucNganSachRepository _bhDmMucLucNganSachRepository;
        public BhDanhMucLoaiChiService(IBhDanhMucLoaiChiRepository repository, IBhDmMucLucNganSachRepository bhDmMucLucNganSachRepository)
        {
            _repository = repository;
            //_danhMucRepository = danhMucRepository;
            _bhDmMucLucNganSachRepository = bhDmMucLucNganSachRepository;
        }

        public void Add(BhDanhMucLoaiChi entity)
        {
            _repository.Add(entity);
        }
        public override IEnumerable<BhDanhMucLoaiChi> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll(authenticationInfo);
        }
        public void AddRange(IEnumerable<BhDanhMucLoaiChi> entities)
        {
            _repository.AddRange(entities);
        }

        public void Delete(BhDanhMucLoaiChi entity)
        {
            _repository.Delete(entity);
        }

        public IEnumerable<BhDanhMucLoaiChi> FindAll()
        {
            return _repository.FindAll();
        }

        public BhDanhMucLoaiChi FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<BhDanhMucLoaiChi> FindByNamLamViec(int namLamViec)
        {
            return _repository.FindByNamLamViec(namLamViec);
        }

        public BhDanhMucLoaiChi FindByParentId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(BhDanhMucLoaiChi entity)
        {
            _repository.Update(entity);
        }

        public void UpdateRange(IEnumerable<BhDanhMucLoaiChi> entities)
        {
            _repository.UpdateRange(entities);
        }

        public override void AddOrUpdateRange(IEnumerable<BhDanhMucLoaiChi> listEntities, AuthenticationInfo authenticationInfo)
        {
            _repository.AddOrUpdateRange(listEntities, authenticationInfo);
        }
    }
}
