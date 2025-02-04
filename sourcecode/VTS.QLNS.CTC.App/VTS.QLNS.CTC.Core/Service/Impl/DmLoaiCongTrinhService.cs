using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DmLoaiCongTrinhService : IService<VdtDmLoaiCongTrinh>, IDmLoaiCongTrinhService
    {
        private readonly IDMLoaiCongTrinhRepository _dMLoaiCongTrinhRepository;

        public DmLoaiCongTrinhService(IDMLoaiCongTrinhRepository dMLoaiCongTrinhRepository)
        {
            _dMLoaiCongTrinhRepository = dMLoaiCongTrinhRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<VdtDmLoaiCongTrinh> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                item.Parent = null;
                if (item.IsModified)
                {
                    if (Guid.Empty.Equals(item.IIdLoaiCongTrinh))
                    {
                        item.DNgayTao = time;
                        item.SIdMaNguoiDungTao = authenticationInfo.Principal;
                        item.DNgaySua = null;
                        item.SIdMaNguoiDungSua = null;
                    }
                    else
                    {
                        item.DNgaySua = time;
                        item.SIdMaNguoiDungSua = authenticationInfo.Principal;
                    }
                }
            }
            _dMLoaiCongTrinhRepository.UpdateDmLoaiCongTrinh(listEntities);
        }

        public override IEnumerable<VdtDmLoaiCongTrinh> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _dMLoaiCongTrinhRepository.FindAll(authenticationInfo).OrderBy(t => t.SMaLoaiCongTrinh);
        }

        public List<VdtDmLoaiCongTrinh> GetAll()
        {
            return _dMLoaiCongTrinhRepository.GetAll();
        }
    }
}
