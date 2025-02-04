using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmNgayNghiService
    {
        IEnumerable<TlDmNgayNghi> FindAll();
        void Delete(Guid id);
        void Add(TlDmNgayNghi nhDmTiGia);
        TlDmNgayNghi FindById(Guid id);
        void Update(TlDmNgayNghi nhDmTiGia);
        int AddRange(IEnumerable<TlDmNgayNghi> holidays);
        IEnumerable<TlDmNgayNghi> FindByYear(int year);
    }
}
