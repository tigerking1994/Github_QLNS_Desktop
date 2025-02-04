using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhMstnKeHoachDatHangService
    {
        NhMSTNKeHoachDatHang Find(Guid iId);
        bool Insert(NhMSTNKeHoachDatHang obj);
        bool Update(NhMSTNKeHoachDatHang data);
        void Delete(Guid iId);
        void Log(Guid iID, string sUserLogIn);
        //void LockOrUnlock(Guid id, bool status);
        IEnumerable<NhMstnKeHoachDatHangQuery> GetAllMstnKeHoachDatHangIndex();
        IEnumerable<NhMSTNKeHoachDatHang> FindAll();
        IEnumerable<NhMstnKeHoachDatHangQuery> FindMstnKeHoachDatHangByCondition(Guid? donviId, Guid? keHoachTongTheId, Guid? chuongTrinhId);
        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
    }
}
