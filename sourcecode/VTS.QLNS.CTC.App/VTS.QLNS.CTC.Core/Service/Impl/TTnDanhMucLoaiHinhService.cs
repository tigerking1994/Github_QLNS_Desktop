using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TTnDanhMucLoaiHinhService : IService<TnDanhMucLoaiHinh>, ITTnDanhMucLoaiHinhService
    {
        private readonly ITnDanhMucLoaiHinhRepository _tnDanhMucLoaiHinhRepository;

        public TTnDanhMucLoaiHinhService(ITnDanhMucLoaiHinhRepository tnDanhMucLoaiHinhRepository)
        {
            _tnDanhMucLoaiHinhRepository = tnDanhMucLoaiHinhRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<TnDanhMucLoaiHinh> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                item.INamLamViec = authenticationInfo.YearOfWork;
                if (item.IsModified)
                {
                    if (Guid.Empty.Equals(item.Id))
                    {
                        item.DateCreated = time;
                        item.UserCreator = authenticationInfo.Principal;
                        item.DateModified = null;
                        item.UserModifier = null;
                    }
                    else
                    {
                        item.DateModified = time;
                        item.UserModifier = authenticationInfo.Principal;
                    }
                }
            }
            _tnDanhMucLoaiHinhRepository.AddOrUpdateRange(listEntities);
        }

        public void AddRange(List<TnDanhMucLoaiHinh> listMlns)
        {
            _tnDanhMucLoaiHinhRepository.AddRange(listMlns);
        }

        public override IEnumerable<TnDanhMucLoaiHinh> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tnDanhMucLoaiHinhRepository.FindAll(i => authenticationInfo.YearOfWork == i.INamLamViec).OrderBy(i => i.Lns).ToList();
        }

        public IEnumerable<TnDanhMucLoaiHinh> FindByLoaiHinh(int yearOfWork, int iTrangThai)
        {
            return _tnDanhMucLoaiHinhRepository.FindByLoaiHinh(yearOfWork, iTrangThai);
        }
    }
}
