using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtNcNhuCauChiService
    {
        IEnumerable<VdtNcNhuCauChiQuery> GetNhuCauChiIndex();
        KinhPhiCucTaiChinhCapQuery GetKinhPhiCucTaiChinhCap(int iNamKeHoach, string iIdMaDonVi, int iIdNguonVon, int iQuy);
        void InsertKeHoachChiQuy(VdtNcNhuCauChi data);
        void UpdateKeHoachChiQuy(VdtNcNhuCauChi data);
        void DeleteKeHoachChiQuy(Guid iID);
        void InsertDetailData(Guid iIDParentId, List<VdtNcNhuCauChiChiTiet> datas);
        void InsertDetailDataImport(List<VdtNcNhuCauChiChiTiet> datas);
        List<VdtNcNhuCauChiChiTiet> GetDetailByParent(Guid iIdParentId);
        IEnumerable<VdtNcNhuCauChiChiTietQuery> GetNhuCauChiDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon, int iQuy, int? DonviTinh = null);

        bool IsExistSoDeNghi(VdtNcNhuCauChi dataInsert);
        bool CheckExistSoKeHoach(string sSoDeNghi, Guid? iId);
    }
}
