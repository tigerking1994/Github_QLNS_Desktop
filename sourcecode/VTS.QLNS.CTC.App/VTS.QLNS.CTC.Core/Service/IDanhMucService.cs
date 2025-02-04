using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IDanhMucService
    {
        void Add(DanhMuc danhMuc);
        void Update(DanhMuc danhMuc);
        IEnumerable<DanhMuc> FindDanhMucTheoNganh(string idChungTu, int namLamViec, string type);
        IEnumerable<DanhMuc> FindByType(string type, int namLamViec);
        IEnumerable<DanhMuc> FindByType(string type);
        IEnumerable<DanhMuc> FindByCodes(List<string> codes);
        IEnumerable<DanhMuc> FindByCondition(Expression<Func<DanhMuc, bool>> predicate);
        IEnumerable<DanhMuc> FindDmChuyenNganhByNsDonvi(IEnumerable<Guid> excludeIds, int year);
        DanhMuc FindByTypeAndCode(string type, string idCode);
        DanhMuc FindByCode(string idCode, int? namLamViec = null);
        string FindDonViQuanLy(int namLamViec);
        int countDanhMucByTypeAndNLV(string type, int namLamViec);
        int countDanhMucNganhChuyenNganh(int namLamViec);

    }
}
