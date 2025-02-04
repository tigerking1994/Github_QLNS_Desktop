using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IDmChuKyService
    {
        IEnumerable<DanhMuc> FindChuKyChucDanh();
        IEnumerable<DanhMuc> FindChuKyTen();
        IEnumerable<DanhMuc> FindChuKyTieuDe1();
        IEnumerable<DanhMuc> FindChuKyTieuDe2();
        IEnumerable<DanhMuc> FindNhomChuKy(int year);
        void Add(DmChuKy dmChuKy);
        void UpdateRange(IEnumerable<DmChuKy> dmChuKy);
        void Save(DmChuKy dmChuKy);
        DmChuKy FindById(Guid id);
        IEnumerable<DmChuKy> FindByCondition(Expression<Func<DmChuKy, bool>> predicate);
        void GetConfigSign(string sTypeChuKy, ref Dictionary<string, object> data);
        IEnumerable<DanhMuc> FindNhomChuKyTen();
        IEnumerable<DanhMuc> FindNhomChuKyChucDanh();
    }
}
