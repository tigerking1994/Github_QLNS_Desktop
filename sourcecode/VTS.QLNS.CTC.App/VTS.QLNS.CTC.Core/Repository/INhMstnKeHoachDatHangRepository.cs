using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhMstnKeHoachDatHangRepository : IRepository<NhMSTNKeHoachDatHang>
    {
        IEnumerable<NhMstnKeHoachDatHangQuery> GetAllMstnKeHoachDatHangIndex();
        IEnumerable<NhMstnKeHoachDatHangQuery> FindMstnKeHoachDatHangByCondition(Guid? donviId, Guid? keHoachTongTheId, Guid? chuongTrinhId);
        void DeleteById(Guid iId);
        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
    }
}
