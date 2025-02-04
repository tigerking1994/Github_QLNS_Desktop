using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhThTongHopService
    {
        void InsertNHTongHop_Tang(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid? iIDQuyetDinhOld = null);
        void InsertNHTongHop_New(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid? iIDQuyetDinhOld = null);
        void InsertNHTongHop_Giam(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid? iIDQuyetDinhOld = null);
        void DeleteNHTongHop_Giam(string sLoai, Guid iIdQuyetDinh);
        void InsertNHTongHop(Guid iIDChungTu, string sLoai, List<NHTHTongHopQuery> lstData);
        IEnumerable<NHTHTongHop> FindByCondition(Expression<Func<NHTHTongHop, bool>> predicate);

    }
}
