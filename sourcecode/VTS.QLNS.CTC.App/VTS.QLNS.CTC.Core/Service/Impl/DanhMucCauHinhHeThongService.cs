using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DanhMucCauHinhHeThongService : DanhMucService
    {
        private readonly IDanhMucRepository _danhMucRepository;

        public DanhMucCauHinhHeThongService(IDanhMucRepository danhMucRepository) : base(danhMucRepository)
        {
            _danhMucRepository = danhMucRepository;
        }

        public override IEnumerable<DanhMuc> FindAll(AuthenticationInfo authenticationInfo)
        {
            IEnumerable<DanhMuc> result = _danhMucRepository.FindAll(d => "DM_CauHinh".Equals(d.SType) && (d.INamLamViec == null || 
                d.INamLamViec == authenticationInfo.YearOfWork)).ToList();
            return result;
        }

        public int CountByYear(int namLamViec)
        {
            return _danhMucRepository.CountDmCauHinhHeThongByYear(namLamViec);
        }

        public override void AddOrUpdateRange(IEnumerable<DanhMuc> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            IEnumerable<string> deletedIdCodes = listEntities.Where(i => i.IsDeleted).Select(i => i.IIDMaDanhMuc).ToList();
            IEnumerable<string> modifiedIdCodes = listEntities.Where(i => !i.IsDeleted).Select(i => i.IIDMaDanhMuc).ToList();
            IEnumerable<Guid> excludeIds = listEntities.Select(i => i.Id).ToList();
            foreach (var item in listEntities)
            {
                // item.INamLamViec = authenticationInfo.YearOfWork;
                item.SType = "DM_CauHinh";
                if (item.IsModified)
                {
                    int countDuplicateIdCodes = modifiedIdCodes.Where(t => t.Equals(item.IIDMaDanhMuc)).Count();
                    if (countDuplicateIdCodes > 1)
                    {
                        throw new ArgumentException("Chỉ cho phép 1 bản ghi với mã " + item.IIDMaDanhMuc + " tồn tại");
                    }
                    // Nếu mã tồn tại và mã không thuộc danh sách bản ghi bị xóa thfi sẽ throw exception
                    if (item.INamLamViec.HasValue && CheckExistIdCode(item.IIDMaDanhMuc, item.INamLamViec.Value, item.SType, item.Id, excludeIds))
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
