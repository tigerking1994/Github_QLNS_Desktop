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
    public class BhDmMucLucBHXHMapService : IService<BhDmMucLucNganSach>, IBhDmMucLucBHXHMapService
    {
        private readonly IBhDmMucLucBHXHMapRepository _repository;
        private readonly IMucLucNganSachService _mlnsService;
        private readonly ITlDmPhuCapService _phuCapService;

        public BhDmMucLucBHXHMapService(IBhDmMucLucBHXHMapRepository repository, IMucLucNganSachService mlnsService, ITlDmPhuCapService phucapService)
        {
            _repository = repository;
            _mlnsService = mlnsService;
            _phuCapService = phucapService;
        }
        
        public IEnumerable<BhDmMucLucNganSach> FindAll()
        {
            return _repository.FindAll();
        }

        public BhDmMucLucNganSach FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public void Update(BhDmMucLucNganSach entity)
        {
            _repository.Update(entity);
        }

        public void UpdateRange(IEnumerable<BhDmMucLucNganSach> entities)
        {
            _repository.UpdateRange(entities);
        }

        public override IEnumerable<BhDmMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll().Where(x => x.INamLamViec == authenticationInfo.YearOfWork).OrderBy(s => s.SXauNoiMa).ToList();
        }

        public override void AddOrUpdateRange(IEnumerable<BhDmMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo)
        {
            _repository.AddOrUpdateRange(listEntities, authenticationInfo);
        }
        public override IEnumerable<BhDmMucLucNganSach> FindAllThu(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll().Where(x => x.INamLamViec == authenticationInfo.YearOfWork && x.SXauNoiMa.StartsWith("902")).OrderBy(s => s.SXauNoiMa).ToList();
        }
        public string GetMoTaMLNS(string xauMLNSs, int YearOfWork)
        {
            if (!string.IsNullOrEmpty(xauMLNSs))
            {
                List<string> lstXauMLNS = xauMLNSs.Split(",").ToList();
                List<string> lstMoTaMLNS = new List<string>();
                foreach (var item in lstXauMLNS)
                {
                    var mlnsData = _mlnsService.FindByMLNS(item,YearOfWork);
                    lstMoTaMLNS.Add(mlnsData.MoTa);
                }
                return string.Join("; ", lstMoTaMLNS);
            }
            return string.Empty;
        }
        public string GetTenPhuCap(string maPCs)
        {
            if (!string.IsNullOrEmpty(maPCs))
            {
                List<string> lstMaPC = maPCs.Split(",").ToList();
                List<string> lstTenPhuCap = new List<string>();
                foreach (var item in lstMaPC)
                {
                    var phuCapData = _phuCapService.FindByMaPhuCap(item);
                    lstTenPhuCap.Add(phuCapData.TenPhuCap);
                }
                return string.Join("; ", lstTenPhuCap);
            }
            return string.Empty;
        }
    }
}
