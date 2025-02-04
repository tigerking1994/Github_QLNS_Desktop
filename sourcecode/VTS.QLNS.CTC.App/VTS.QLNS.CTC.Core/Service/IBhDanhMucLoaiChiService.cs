using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDanhMucLoaiChiService
    {
        IEnumerable<BhDanhMucLoaiChi> FindByNamLamViec(int namLamViec);
        //DanhMuc FindMLLCTietToi(int namLamViec);
        void Add(BhDanhMucLoaiChi entity);
        void Update(BhDanhMucLoaiChi entity);
        void Delete(BhDanhMucLoaiChi entity);
        void AddRange(IEnumerable<BhDanhMucLoaiChi> entities);
        void UpdateRange(IEnumerable<BhDanhMucLoaiChi> entities);
        IEnumerable<BhDanhMucLoaiChi> FindAll();
        BhDanhMucLoaiChi FindById(Guid id);
        BhDanhMucLoaiChi FindByParentId(Guid id);
    }
}
