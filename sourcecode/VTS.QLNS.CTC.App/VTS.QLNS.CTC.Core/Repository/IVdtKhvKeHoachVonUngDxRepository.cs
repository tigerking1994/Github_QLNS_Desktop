using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvKeHoachVonUngDxRepository : IRepository<VdtKhvKeHoachVonUngDx>
    {
        IEnumerable<VdtKhvKeHoachVonUngDxQuery> GetKeHoachVonUngDxIndex();
        IEnumerable<VdtKhvKeHoachVonUngDx> GetKHVUDeXuatInKHVUDuocDuyet(string iIdMaDonVi, int iNamKeHoach, DateTime dNgayLap);

        bool CheckTrungSoDeNghi(string sSoDeNghi, Guid id);

    }
}
