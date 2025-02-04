using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlBangLuongThangTruyThuService
    {
        int Add(IEnumerable<TlBangLuongThangTruyThu> entity);
        IEnumerable<TlBangLuongThangTruyThu> FindAll();
        IEnumerable<TlBangLuongThangTruyThu> FindByParentId(Guid parentId);
        int DeleteByParentId(Guid parentId);
        int AddRange(IEnumerable<TlBangLuongThangTruyThu> entities);
        int AddOrUpdateRange(IEnumerable<TlBangLuongThangTruyThu> entities);
        int UpdateRange(IEnumerable<TlBangLuongThangTruyThu> entities);
        int Delete(TlBangLuongThangTruyThu entity);
        int Update(TlBangLuongThangTruyThu entity);
        IEnumerable<TlBangLuongThangTruyThu> FindByCondition(Expression<Func<TlBangLuongThangTruyThu, bool>> predicate);
        DataTable GetDataLuongThangTruyThu(Guid id);
        void CapNhatBangLuong( List<TlDsCapNhapBangLuong> tlDsCapNhapBangLuongs, List<TlBangLuongThangTruyThu> tlBangLuongThangs);


    }
}
