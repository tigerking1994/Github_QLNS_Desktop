using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtNcNhuCauChiRepository : IRepository<VdtNcNhuCauChi>
    {
        IEnumerable<VdtNcNhuCauChiQuery> GetNhuCauChiIndex();
        IEnumerable<VdtNcNhuCauChiChiTietQuery> GetNhuCauChiDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon, int iQuy, int? donviTinh);
        KinhPhiCucTaiChinhCapQuery GetKinhPhiCucTaiChinhCap(int iNamKeHoach, string iIdMaDonVi, int iIdNguonVon, int iQuy);
        bool IsExistSoDeNghi(VdtNcNhuCauChi dataInsert);
    }
}
