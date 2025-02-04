using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmTiGiaChiTietService
    {
        void Add(NhDmTiGiaChiTiet nhDmTiGia);

        void Update(NhDmTiGiaChiTiet nhDmTiGia);

        NhDmTiGiaChiTiet FindById(Guid id);

        void Delete(Guid id);

        IEnumerable<NhDmTiGiaChiTiet> FindByTiGiaId(Guid idTiGia);
        IEnumerable<NhDmTiGiaChiTiet> FindAll();
    }
}
