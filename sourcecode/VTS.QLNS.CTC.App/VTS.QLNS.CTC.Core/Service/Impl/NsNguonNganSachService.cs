using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsNguonNganSachService : IService<Domain.NsNguonNganSach>, INsNguonNganSachService
    {
        private readonly INsNguonNgansachRepository _nsNguonNganSachRepository;

        public NsNguonNganSachService(INsNguonNgansachRepository NsNguonNganSachRepository)
        {
            _nsNguonNganSachRepository = NsNguonNganSachRepository;
        }
        public void Add(Domain.NsNguonNganSach entity)
        {
            throw new NotImplementedException();
        }

        public override void AddOrUpdateRange(IEnumerable<Domain.NsNguonNganSach> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                if (item.IsModified)
                {
                    if (!item.IIdMaNguonNganSach.HasValue)
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
            _nsNguonNganSachRepository.AddOrUpdateNsNguonNganSach(listEntities);
        }

        public override IEnumerable<Domain.NsNguonNganSach> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _nsNguonNganSachRepository.FindAll().OrderBy(i => i.IStt);
        }

        public IEnumerable<NsNguonNganSach> FindAll()
        {
            return _nsNguonNganSachRepository.FindAll().OrderBy(i => i.IStt);
        }

        public List<NsNguonNganSach> FindByDuAnId(string duAnId)
        {
            return _nsNguonNganSachRepository.FindByDuAnId(duAnId);
        }

        public NsNguonNganSach FindById(Guid id)
        {
            return _nsNguonNganSachRepository.Find(id);
        }

        public IEnumerable<NsNguonNganSach> FindNguonNganSach()
        {
            return _nsNguonNganSachRepository.FindNguonNganSach();
        }

        public Domain.NsNguonNganSach FindNguonNganSachById(int IdMaNguon)
        {
            return _nsNguonNganSachRepository.FindNguonNganSachById(IdMaNguon);
        }
    }
}
