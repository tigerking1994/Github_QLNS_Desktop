using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DanhMucNganhService : DanhMucService
    {
        private readonly IDanhMucRepository _danhMucRepository;
        private readonly INsDonViRepository _nsDonViRepository;

        public DanhMucNganhService(IDanhMucRepository danhMucRepository, INsDonViRepository nsDonViRepository) : base(danhMucRepository)
        {
            _danhMucRepository = danhMucRepository;
            _nsDonViRepository = nsDonViRepository;
        }

        public override IEnumerable<DanhMuc> FindAll(AuthenticationInfo authenticationInfo)
        {
            /*
            if (authenticationInfo.OptionalParam != null && authenticationInfo.OptionalParam.Length > 0 && authenticationInfo.OptionalParam[0] is DialogType dialogType)
            {
                if (DialogType.LoadDMChuyenNganhOfDonVi.Equals(dialogType))
                {
                    IEnumerable<Guid> excludeIds = authenticationInfo.OptionalParam[1] as IEnumerable<Guid>;
                    return _danhMucRepository.FindDmChuyenNganhByNsDonvi(excludeIds, authenticationInfo.YearOfWork);
                }
            }
            */
            IEnumerable<DonVi> donvis = _nsDonViRepository.FindAll(d => authenticationInfo.YearOfWork == d.NamLamViec).ToList();
            IEnumerable<DanhMuc> result = base.FindAll("NS_Nganh", authenticationInfo.YearOfWork).ToList();
            foreach (DanhMuc n in result)
            {
                if (n.SGiaTri != null)
                {
                    DonVi d = donvis.FirstOrDefault(p => p.IIDMaDonVi.Equals(n.SGiaTri));
                    n.TenDonViNoiBo = d == null ? null : d.TenDonVi;
                }
            }
            return result;
        }

        public override void AddOrUpdateRange(IEnumerable<DanhMuc> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            IEnumerable<string> deletedIdCodes = listEntities.Where(i => i.IsDeleted).Select(i => i.IIDMaDanhMuc).ToList();
            IEnumerable<string> modifiedIdCodes = listEntities.Where(i => !i.IsDeleted).Select(i => i.IIDMaDanhMuc).ToList();
            IEnumerable<Guid> excludeIds = listEntities.Select(i => i.Id).ToList();
            foreach (var item in listEntities)
            {
                item.INamLamViec = authenticationInfo.YearOfWork;
                item.SType = "NS_Nganh";
                if (item.IsModified)
                {
                    int countDuplicateIdCodes = modifiedIdCodes.Where(t => t.Equals(item.IIDMaDanhMuc)).Count();
                    if (countDuplicateIdCodes > 1)
                    {
                        throw new ArgumentException("Chỉ cho phép 1 bản ghi với mã " + item.IIDMaDanhMuc + " tồn tại");
                    }
                    // Nếu mã tồn tại và mã không thuộc danh sách bản ghi bị xóa thfi sẽ throw exception
                    if (CheckExistIdCode(item.IIDMaDanhMuc, item.INamLamViec.Value, item.SType, item.Id, excludeIds))
                    {
                        throw new ArgumentException("Bản ghi với mã " + item.IIDMaDanhMuc + " đã tồn tại");
                    }
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
            _danhMucRepository.AddOrUpdateRange(listEntities);
        }
    }
}
