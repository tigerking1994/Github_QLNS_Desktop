using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{ 
    public interface ICpDanhMucService
    {
        IEnumerable<CpDanhMuc> FindAll();
        List<CpDanhMuc> FindByCondition(string maDanhMuc, string tenDanhMuc);
        void Update(CpDanhMuc item);
        CpDanhMuc FindById(Guid Id);
        int AddRange(IEnumerable<CpDanhMuc> listAdd);
        int UpdateRange(IEnumerable<CpDanhMuc> entities);
        IEnumerable<CpDanhMuc> FindAll(Expression<Func<CpDanhMuc, bool>> predicate);
        int Delete(Guid id);
        public List<CpDanhMuc> FindByNamLamViec(int namLamViec);
        int CountDanhMucCP(int namLamViec);
    }
}
