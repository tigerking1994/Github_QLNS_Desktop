using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaKhlcNhaThauService
    {
        NhDaKhlcnhaThau Find(Guid iId);
        bool Insert(NhDaKhlcnhaThau obj);
        bool Update(NhDaKhlcnhaThau data);
        void Delete(Guid iId);
        void Log(Guid iID, string sUserLogIn);
        void LockOrUnlock(Guid id, bool status);
        IEnumerable<NhDaKhlcNhaThauQuery> GetAllKhlcntIndex(int iThuocMenu);
        IEnumerable<NhDaKhlcNhaThauQuery> GetAllKhlcntMuaSam();
        IEnumerable<NhDaDuAn> GetDuAnHaveQDDauTu(Guid iIdKhlcntId, string sMaDonVi);
        IEnumerable<NhDaDuAn> GetDuAnByDonVi(Guid iIdKhlcntId, Guid Id,int iloai);

        IEnumerable<NhDmHinhThucChonNhaThau> GetHinhThucLuaChonNhaThau();
        IEnumerable<NhDmPhuongThucChonNhaThau> GetPhuongThucLuaChonNhaThau();
        IEnumerable<NhDmLoaiHopDong> GetLoaiHopDong();
    }
}
