using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtQddtKhlcnhaThauService
    {
        VdtQddtKhlcnhaThau Find(Guid iId);
        IEnumerable<ChiPhiHangMucQuery> GetKHLCNTDetailByListGoiThau(List<Guid> iIdGoiThaus, Guid iIdDuToan);
        IEnumerable<ChiPhiHangMucQuery> GetKHLCNTDetailQDDauTuByListGoiThau(List<Guid> iIdGoiThaus, Guid iIdQDDauTu);
        IEnumerable<KHLCNhaThauQuery> GetDataIndex();
        IEnumerable<KHLCNhaThauQuery> GetKHLCNhaThauByIdDuAn(Guid idDuAn);
        IEnumerable<VdtDaDuAn> GetDuAnByDonViQuanLy(string iIdMaDonViQuanLy);
        IEnumerable<KHLCNhaThauDetailQuery> GetGoiThauByParentid(Guid iIdParentid);
        bool Insert(VdtQddtKhlcnhaThau data, string sUserLogin);
        bool Update(VdtQddtKhlcnhaThau data, string sUserLogin);
        int UpdateLCNT(VdtQddtKhlcnhaThau entity);
        void UpdateGoiThauByLCNT(Guid lcntID);
        void Delete(KHLCNhaThauQuery data);
        IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucByDuAn(Guid iIdDuAn);
        IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucByGoiThau(Guid iIdGoiThau, Guid IIdDuToanId);
        IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucGoiThauByQDDauTu(Guid iIdGoiThau, Guid iIdQDDauTuId);
        bool CheckExistKHLCNhaThau(VdtQddtKhlcnhaThau data);
        void InsertGoiThauChiPhi( List<VdtDaGoiThauChiPhi> datas);
        void InsertGoiThauHangMuc(List<VdtDaGoiThauHangMuc> datas);
        void InsertGoiThauNguonVon(List<VdtDaGoiThauNguonVon> datas);
        double GetTotalNguonVonInGoiThau(Guid iIdGoiThau);
        double GetTotalNguonVonGoiThauDC(Guid iIdGoiThau);
        void DeleteGoiThaus(Guid idGoiThau);
        IEnumerable<VdtDaGoiThau> ListGoiThauByKHLCNhaThauId(Guid id);
        void DeleteGoiThauByKHLCNTId(Guid idKHLCNT);
        bool CheckDuAnkExistKHLCNT(Guid duToanId);
        IEnumerable<ChiPhiHangMucQuery> GetNguonVonByDuAnLCNTAdd(Guid id, string sLoaiChungTu);
        IEnumerable<ChiPhiHangMucQuery> GetChiPhiByDuAnLCNTAdd(Guid id, string sLoaiChungTu);
        IEnumerable<ChiPhiHangMucQuery> GetHangMucByDuAnLCNTAdd(Guid id, string sLoaiChungTu);
        IEnumerable<ChiPhiHangMucQuery> GetNguonVonByKHLCNTUpdate(Guid id);
        IEnumerable<ChiPhiHangMucQuery> GetChiPhiByKHLCNTUpdate(Guid id);
        IEnumerable<ChiPhiHangMucQuery> GetHangMucByLCNTUpdate(Guid id, string sLoaiChungTu);
        void LockOrUnlock(Guid id, bool isLock);
        //IEnumerable<VdtDaGoiThauQuery> ListGoiThauByKHLC(int namLamViec);
        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id);
       
    }
}
