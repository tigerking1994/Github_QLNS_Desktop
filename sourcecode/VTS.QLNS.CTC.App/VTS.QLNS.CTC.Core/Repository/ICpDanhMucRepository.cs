using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ICpDanhMucRepository : IRepository<CpDanhMuc>
    {
        List<CpDanhMuc> FindByCondition(string maDanhMuc, string tenDanhMuc);
        List<CpDanhMuc> FindByNamLamViec(int namLamViec);
        int CountDanhMucCP(int namLamViec);
    }
}
