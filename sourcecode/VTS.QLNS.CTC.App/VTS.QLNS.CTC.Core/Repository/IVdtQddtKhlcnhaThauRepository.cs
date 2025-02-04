using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtQddtKhlcnhaThauRepository : IRepository<VdtQddtKhlcnhaThau>
    {
        IEnumerable<ChiPhiHangMucQuery> GetKHLCNTDetailByListGoiThau(List<Guid> iIdGoiThaus, Guid iIdDuToan);
        IEnumerable<ChiPhiHangMucQuery> GetKHLCNTDetailQDDauTuByListGoiThau(List<Guid> iIdGoiThaus, Guid iIdQDDauTu);
        IEnumerable<KHLCNhaThauQuery> GetDataIndex();
        IEnumerable<KHLCNhaThauQuery> GetKHLCNhaThauByIdDuAn(Guid idDuAn);
        IEnumerable<VdtDaDuAn> GetDuAnByDonViQuanLy(string iIdMaDonViQuanLy);
        IEnumerable<KHLCNhaThauDetailQuery> GetGoiThauByParentid(Guid iIdParentid);
        IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucByDuAn(Guid iIdDuAn);
        IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucByGoiThau(Guid iIdGoiThau, Guid iIdDuToan);
        IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucGoiThauByQDDauTu(Guid iIdGoiThau, Guid iIdQDDauTuId);
        bool CheckExistKHLCNhaThau(VdtQddtKhlcnhaThau data);
        void InsertGoiThauChiPhi(List<VdtDaGoiThauChiPhi> datas);
        void InsertGoiThauHangMuc(List<VdtDaGoiThauHangMuc> datas);
        void InsertGoiThauNguonVon(List<VdtDaGoiThauNguonVon> datas);
        void DeleteGoiThauDetailWhenChangeDuToan(Guid iIdKHLCNT, Guid iIdDuToan);
        //double GetTotalNguonVonInGoiThau(Guid iIdGoiThau, Guid iIdDuToan);
        double GetTotalNguonVonByGoiThau(Guid iIdGoiThau);
        double GetTotalNguonVonGoiThauDC(Guid iIdGoiThau);
        void UpdateGoiThauByLCNT(Guid lcntID);
        IEnumerable<VdtDaGoiThau> ListGoiThauByKHLCNhaThauId(Guid id);
        void DeleteGoiThauByKHLCNTId(Guid idKHLCNT);
        bool CheckDuAnkExistKHLCNT(Guid duToanId);
        IEnumerable<ChiPhiHangMucQuery> GetNguonVonByDuAnLCNTAdd(Guid id, string sLoaiChungTu);
        IEnumerable<ChiPhiHangMucQuery> GetChiPhiByDuAnLCNTAdd(Guid id, string sLoaiChungTu);
        IEnumerable<ChiPhiHangMucQuery> GetHangMucByDuAnLCNTAdd(Guid id, string sLoaiChungTu);
        IEnumerable<ChiPhiHangMucQuery> GetNguonVonByKHLCNTUpdate(Guid id);
        IEnumerable<ChiPhiHangMucQuery> GetChiPhiByKHLCNTUpdate(Guid id);
        IEnumerable<ChiPhiHangMucQuery> GetHangMucByLCNTUpdate(Guid id, bool isDuToan);
        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id);
    }
}
