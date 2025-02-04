using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhThTongHopRepository : IRepository<NHTHTongHop>
    {
        void InsertNHTongHop_Tang(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid iIDQuyetDinhOld);
        void InsertNHTongHop_New(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid iIDQuyetDinhOld);
        void InsertNHTongHop_Giam(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid iIDQuyetDinhOld);
        void DeleteNHTongHop_Giam(string sLoai, Guid iIdQuyetDinh);
        void InsertNHTongHop(Guid iIDChungTu, string sLoai, List<NHTHTongHopQuery> lstData);
    }
}
